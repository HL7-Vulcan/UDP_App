// ── File download ─────────────────────────────────────────────────
window.udpDownload = function (filename, content, mimeType) {
    const encoded = 'data:' + mimeType + ';charset=utf-8,' + encodeURIComponent(content);
    const a = document.createElement('a');
    a.href = encoded;
    a.download = filename;
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
};

// ── File picker (load JSON) ────────────────────────────────────────
let _dotNetRef = null;

window.udpSetDotNetRef = function (ref) {
    _dotNetRef = ref;
};

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
            if (_dotNetRef) {
                _dotNetRef.invokeMethodAsync('ReceiveFileContent', e.target.result);
            }
        };
        reader.readAsText(file);
    });
    input.click();
};

// ── Context menu — JS-driven (bypasses Blazor event system) ──────
window.udpInitContextMenus = function () {
    // Attach native contextmenu listeners to all textareas with data-ctx attribute
    document.querySelectorAll('textarea[data-ctx]').forEach(function (ta) {
        // Remove any previous listener to avoid duplicates
        ta.removeEventListener('contextmenu', ta._ctxHandler);
        ta._ctxHandler = function (e) {
            e.preventDefault();
            const code = ta.getAttribute('data-ctx');
            if (_dotNetRef) {
                _dotNetRef.invokeMethodAsync('OnContextMenuJS', code, e.clientX, e.clientY);
            }
        };
        ta.addEventListener('contextmenu', ta._ctxHandler);
    });
};

// ── Position and show context menu ────────────────────────────────
window.udpShowCtxMenu = function (x, y) {
    const menu = document.getElementById('ctx-menu');
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

window.udpHideCtxMenu = function () {
    const menu = document.getElementById('ctx-menu');
    if (menu) menu.style.display = 'none';
};

// Close when clicking outside
document.addEventListener('mousedown', function (e) {
    const menu = document.getElementById('ctx-menu');
    if (menu && !menu.contains(e.target) && menu.style.display === 'block') {
        menu.style.display = 'none';
        if (_dotNetRef) _dotNetRef.invokeMethodAsync('OnCtxMenuClosed');
    }
});
