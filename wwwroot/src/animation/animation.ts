
export function showToast(message: string) {
    const container = <HTMLDivElement>document.getElementById('toast-container');
    const toast = document.createElement('div');
    toast.classList.add('toast');
    toast.textContent = message;

    container.appendChild(toast);

    setTimeout(() => toast.classList.add('show'), 10);
    setTimeout(() => {
        toast.classList.remove('show');
        setTimeout(() => toast.remove(), 300);
        }, 3000
    );
}

export function toggleSidebar() {
    const sidebarBtn = <HTMLButtonElement>document.querySelector(".toggle-btn");

    sidebarBtn.addEventListener("click", () => {
        const sidebar = document.getElementById('sidebar')!;
        const labels = sidebar.querySelectorAll<HTMLSpanElement>('.label');
        

        if (sidebar.classList.contains('collapsed')) {
            sidebar.classList.remove('collapsed');
            setTimeout(() => labels.forEach(l => l.style.display = 'inline'), 300);
        } else {
            labels.forEach(l => l.style.display = 'none');
            sidebar.classList.add('collapsed');
        }
    });

}