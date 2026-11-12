// ── File download (text) ───────────────────────────────────────────
window.udpDownload = function (filename, content, mimeType) {
    const encoded = 'data:' + mimeType + ';charset=utf-8,' + encodeURIComponent(content);
    const a = document.createElement('a');
    a.href = encoded;
    a.download = filename;
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
};

// ── File download (base-64 binary, e.g. JSON from SUSHI) ──────────
window.udpDownloadBase64 = function (filename, base64, mimeType) {
    const byteChars = atob(base64);
    const bytes = new Uint8Array(byteChars.length);
    for (let i = 0; i < byteChars.length; i++) bytes[i] = byteChars.charCodeAt(i);
    const mime = mimeType || (filename.endsWith('.fsh') ? 'text/plain' : 'application/json');
    const blob = new Blob([bytes], { type: mime });
    const url  = URL.createObjectURL(blob);
    const a    = document.createElement('a');
    a.href     = url;
    a.download = filename;
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
    URL.revokeObjectURL(url);
};

// ── Zip download (base-64 encoded zip blob) ────────────────────────
window.udpDownloadZip = function (filename, base64) {
    const byteChars = atob(base64);
    const bytes = new Uint8Array(byteChars.length);
    for (let i = 0; i < byteChars.length; i++) bytes[i] = byteChars.charCodeAt(i);
    const blob = new Blob([bytes], { type: 'application/zip' });
    const url  = URL.createObjectURL(blob);
    const a    = document.createElement('a');
    a.href     = url;
    a.download = filename;
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
    URL.revokeObjectURL(url);
};

// ── Scroll element to bottom (used by SUSHI live log) ─────────────
window.udpScrollBottom = function (elementId) {
    const el = document.getElementById(elementId);
    if (el) el.scrollTop = el.scrollHeight;
};

// ── FSH file picker (import into Examples folder) ─────────────────
window.udpOpenFshPicker = function (dotNetRef) {
    const input = document.createElement('input');
    input.type = 'file';
    input.accept = '.fsh';
    input.style.display = 'none';
    document.body.appendChild(input);
    input.addEventListener('change', function () {
        const file = input.files[0];
        document.body.removeChild(input);
        if (!file) return;
        const reader = new FileReader();
        reader.onload = function (e) {
            // result is a data URL: "data:...;base64,<content>"
            const base64 = e.target.result.split(',')[1];
            dotNetRef.invokeMethodAsync('ReceiveFshFile', file.name, base64);
        };
        reader.readAsDataURL(file);
    });
    input.click();
};

// ── DotNet references — keyed by menuId with timestamp ────────────
// Stores { ref, ts } so we can always find the most recently registered
// live reference, avoiding stale disposed refs after navigation.
const _dotNetRefs = {};
let _dotNetRef = null;       // legacy fallback
let _activeMenuId = null;    // menuId of the most recently registered ref

window.udpSetDotNetRef = function (ref, menuId) {
    if (menuId) {
        _dotNetRefs[menuId] = { ref: ref, ts: Date.now() };
        _activeMenuId = menuId;
    } else {
        _dotNetRef = ref;
    }
};

function _refForMenu(menuId) {
    if (menuId && _dotNetRefs[menuId]) return _dotNetRefs[menuId].ref;
    // Fall back to most-recently-registered ref by timestamp
    let best = null, bestTs = 0;
    for (const key in _dotNetRefs) {
        if (_dotNetRefs[key].ts > bestTs) { bestTs = _dotNetRefs[key].ts; best = _dotNetRefs[key].ref; }
    }
    return best || _dotNetRef;
}

// ── Global file-input handler (called from App.razor onchange) ────────
// Reads the selected file from the persistent global file input and
// calls ReceiveFileContent on whichever section registered most recently.
window.udpHandleGlobalFileInput = function () {
    const input = document.getElementById('udp-global-file-input');
    if (!input || !input.files || !input.files[0]) return;
    const file = input.files[0];
    const reader = new FileReader();
    reader.onload = function (e) {
        input.value = '';   // reset so the same file can be re-loaded
        const ref = _refForMenu(_activeMenuId);
        if (ref) ref.invokeMethodAsync('ReceiveFileContent', e.target.result);
    };
    reader.readAsText(file);
};

// ── File picker (load JSON) ────────────────────────────────────────
// Clicks the persistent hidden <input id="udp-global-file-input"> that is
// rendered in App.razor. Because the element is already in the DOM when the
// button is pressed, browsers on Azure (Blazor Server over SignalR) still
// treat the programmatic .click() as a trusted user-gesture action.
// The onchange handler (udpHandleGlobalFileInput, defined in App.razor) reads
// the file and calls ReceiveFileContent on the active DotNet ref.
window.udpOpenFilePicker = function () {
    const input = document.getElementById('udp-global-file-input');
    if (input) {
        input.click();
    } else {
        // Fallback: dynamically-created input (works locally, may fail on Azure).
        const dyn = document.createElement('input');
        dyn.type = 'file';
        dyn.accept = '.json';
        dyn.style.display = 'none';
        document.body.appendChild(dyn);
        dyn.addEventListener('change', function () {
            const file = dyn.files[0];
            if (!file) { document.body.removeChild(dyn); return; }
            const reader = new FileReader();
            reader.onload = function (e) {
                document.body.removeChild(dyn);
                const ref = Object.values(_dotNetRefs).slice(-1)[0] || _dotNetRef;
                if (ref) ref.invokeMethodAsync('ReceiveFileContent', e.target.result);
            };
            reader.readAsText(file);
        });
        dyn.click();
    }
};

// ── File picker by pre-existing input element ID (Azure-safe) ────────
// Clicks a hidden <input type="file"> that is already in the DOM.
// Because the element exists before the click, browsers allow the gesture
// even when the click arrives via SignalR (Blazor Server on Azure).
// The input's onchange handler reads the file and calls ReceiveFileContent.
window.udpOpenFilePickerById = function (inputId) {
    const input = document.getElementById(inputId);
    if (input) input.click();
};

// Called by the hidden file input's onchange (wired in Razor markup).
window.udpReadFileInput = function (inputId, menuId) {
    const input = document.getElementById(inputId);
    if (!input || !input.files || !input.files[0]) return;
    const file = input.files[0];
    const reader = new FileReader();
    reader.onload = function (e) {
        // Reset so the same file can be reloaded
        input.value = '';
        const ref = menuId ? (_dotNetRefs[menuId] || _dotNetRef) :
                             (Object.values(_dotNetRefs).slice(-1)[0] || _dotNetRef);
        if (ref) ref.invokeMethodAsync('ReceiveFileContent', e.target.result);
    };
    reader.readAsText(file);
};

// ── Context menus ─────────────────────────────────────────────────
window.udpInitContextMenus = function (menuId) {
    // Standard editable fields
    document.querySelectorAll('textarea[data-ctx], input[data-ctx], select[data-ctx]').forEach(function (ta) {
        ta.removeEventListener('contextmenu', ta._ctxHandler);
        ta._ctxHandler = function (e) {
            e.preventDefault();
            const code = ta.getAttribute('data-ctx');
            // Walk up to find which popout or page this textarea lives in,
            // then pick the right ref via its associated menu.
            const ref = _refForMenu(menuId);
            if (ref) ref.invokeMethodAsync('OnContextMenuJS', code, e.clientX, e.clientY);
        };
        ta.addEventListener('contextmenu', ta._ctxHandler);
    });
    // Wrapper divs for disabled/read-only fields (data-ctx-wrap) and plain divs with data-ctx
    document.querySelectorAll('[data-ctx-wrap], div[data-ctx]').forEach(function (el) {
        el.removeEventListener('contextmenu', el._ctxHandler);
        el._ctxHandler = function (e) {
            e.preventDefault();
            const code = el.getAttribute('data-ctx-wrap') || el.getAttribute('data-ctx');
            const ref = _refForMenu(menuId);
            if (ref) ref.invokeMethodAsync('OnContextMenuJS', code, e.clientX, e.clientY);
        };
        el.addEventListener('contextmenu', el._ctxHandler);
    });
};

// ── Position and show context menu ────────────────────────────────
window.udpShowCtxMenu = function (menuId, x, y) {
    document.querySelectorAll('.ctx-menu').forEach(m => m.style.display = 'none');
    const menu = document.getElementById(menuId || 'ctx-menu');
    if (!menu) return;
    const margin = 8;
    const w = menu.offsetWidth  || 340;
    const h = menu.offsetHeight || 200;
    const left = Math.min(x, window.innerWidth  - w - margin);
    const top  = Math.min(y, window.innerHeight - h - margin);
    menu.style.left    = left + 'px';
    menu.style.top     = top  + 'px';
    menu.style.display = 'block';
};

window.udpHideCtxMenu = function (menuId) {
    const menu = document.getElementById(menuId || 'ctx-menu');
    if (menu) menu.style.display = 'none';
};

// Close all context menus when clicking outside
document.addEventListener('mousedown', function (e) {
    document.querySelectorAll('.ctx-menu').forEach(function (menu) {
        if (!menu.contains(e.target) && menu.style.display === 'block') {
            menu.style.display = 'none';
            // Notify whichever section owns this menu
            const ref = _refForMenu(menu.id);
            if (ref) ref.invokeMethodAsync('OnCtxMenuClosed');
        }
    });
});

// ── Section 5 inclusion popout ────────────────────────────────────
window.s5ShowInclPopout = function () {
    const el = document.getElementById('s5-incl-popout');
    if (el) {
        el.style.display = 'flex';
        window.udpInitContextMenus('s5-ctx-menu');
    }
};
window.s5HideInclPopout = function () {
    const el = document.getElementById('s5-incl-popout');
    if (el) el.style.display = 'none';
};

// ── Section 5 exclusion popout ────────────────────────────────────
window.s5ShowExclPopout = function () {
    const el = document.getElementById('s5-excl-popout');
    if (el) {
        el.style.display = 'flex';
        window.udpInitContextMenus('s5-ctx-menu');
    }
};
window.s5HideExclPopout = function () {
    const el = document.getElementById('s5-excl-popout');
    if (el) el.style.display = 'none';
};

// ── Section 6 popout ──────────────────────────────────────────────
window.s6ShowPopout = function () {
    const el = document.getElementById('s6-popout');
    if (el) el.style.display = 'flex';
    // Context menus initialised by Blazor OnAfterRenderAsync after render
};
window.s6HidePopout = function () {
    const el = document.getElementById('s6-popout');
    if (el) el.style.display = 'none';
};

// ── Section 9 popout ──────────────────────────────────────────────
window.s9ShowPopout = function () {
    const el = document.getElementById('s9-popout');
    if (el) {
        el.style.display = 'flex';
        window.udpInitContextMenus('s9-ctx-menu');
    }
};
window.s9HidePopout = function () {
    const el = document.getElementById('s9-popout');
    if (el) el.style.display = 'none';
};

function s1ShowDesignPopout() {
    var el = document.getElementById('s1-design-popout');
    if (el) el.style.display = 'flex';
}
function s1HideDesignPopout() {
    var el = document.getElementById('s1-design-popout');
    if (el) el.style.display = 'none';
}
