// ── File download ──────────────────────────────────────────────────
window.udpDownload = function (filename, content, mimeType) {
    const encoded = 'data:' + mimeType + ';charset=utf-8,' + encodeURIComponent(content);
    const a = document.createElement('a');
    a.href = encoded;
    a.download = filename;
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
};

// ── DotNet references — one per section key ────────────────────────
// _dotNetRefs maps menuId -> DotNetObjectReference for that section.
// udpSetDotNetRef(ref, menuId) registers the ref for a specific menu.
// Falls back to legacy single-ref behaviour if menuId is omitted.
const _dotNetRefs = {};
let _dotNetRef = null;   // legacy single ref (Section02 / Section09 etc.)

window.udpSetDotNetRef = function (ref, menuId) {
    if (menuId) {
        _dotNetRefs[menuId] = ref;
    } else {
        _dotNetRef = ref;
    }
};

function _refForMenu(menuId) {
    return _dotNetRefs[menuId] || _dotNetRef;
}

// ── File picker (load JSON) ────────────────────────────────────────
window.udpOpenFilePicker = function () {
    const input = document.createElement('input');
    input.type = 'file';
    input.accept = '.json';
    input.style.display = 'none';
    document.body.appendChild(input);
    input.addEventListener('change', function () {
        const file = input.files[0];
        if (!file) { document.body.removeChild(input); return; }
        const reader = new FileReader();
        reader.onload = function (e) {
            document.body.removeChild(input);
            // Try the active menu's ref first, fall back to global
            const ref = Object.values(_dotNetRefs).slice(-1)[0] || _dotNetRef;
            if (ref) ref.invokeMethodAsync('ReceiveFileContent', e.target.result);
        };
        reader.readAsText(file);
    });
    input.click();
};

// ── Context menus ─────────────────────────────────────────────────
window.udpInitContextMenus = function (menuId) {
    document.querySelectorAll('textarea[data-ctx]').forEach(function (ta) {
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
    if (el) {
        el.style.display = 'flex';
        window.udpInitContextMenus('s6-ctx-menu');
    }
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
