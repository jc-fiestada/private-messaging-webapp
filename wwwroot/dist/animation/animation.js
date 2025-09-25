export function showToast(message) {
    const container = document.getElementById('toast-container');
    const toast = document.createElement('div');
    toast.classList.add('toast');
    toast.textContent = message;
    container.appendChild(toast);
    setTimeout(() => toast.classList.add('show'), 10);
    setTimeout(() => {
        toast.classList.remove('show');
        setTimeout(() => toast.remove(), 300);
    }, 3000);
    console.log("trigger animation");
}
export function toggleSidebar() {
    const sidebarBtn = document.querySelector(".toggle-btn");
    sidebarBtn.addEventListener("click", () => {
        const sidebar = document.getElementById('sidebar');
        const labels = sidebar.querySelectorAll('.label');
        if (sidebar.classList.contains('collapsed')) {
            sidebar.classList.remove('collapsed');
            setTimeout(() => labels.forEach(l => l.style.display = 'inline'), 300);
        }
        else {
            labels.forEach(l => l.style.display = 'none');
            sidebar.classList.add('collapsed');
        }
    });
}
//# sourceMappingURL=animation.js.map