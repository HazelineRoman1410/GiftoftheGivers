// Shared client-side helpers - the ASP.NET Core equivalent of the
// prototype's toast + small UI-state helpers (server now owns real data).

function showToast(msg) {
    const toast = document.getElementById('toast');
    if (!toast) return;
    toast.textContent = msg;
    toast.classList.add('show');
    setTimeout(() => toast.classList.remove('show'), 3000);
}
