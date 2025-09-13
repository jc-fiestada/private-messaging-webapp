import * as general from "./animation/animation.js";

// items

const updateBtn = <HTMLButtonElement>document.getElementById("updateBtn");
const deleteBtn = <HTMLButtonElement>document.getElementById("deleteBtn");

const submitUpdate = <HTMLButtonElement>document.getElementById("submitUpdate");
const cancelUpdate = <HTMLButtonElement>document.getElementById("cancelUpdate");

const submitDelete = <HTMLButtonElement>document.getElementById("submitDelete");
const cancelDelete = <HTMLButtonElement>document.getElementById("cancelDelete");

const updateField = <HTMLSelectElement>document.getElementById('updateField');
const updateInputWrapper = <HTMLDivElement>document.getElementById('updateInputWrapper');



// add animations

general.toggleSidebar();

// home logic



    // Open modal
function openModal(id: string) {
    const selectedModal = <HTMLDivElement>document.getElementById(id);
    selectedModal.style.display = 'flex';
}

// Close modal
function closeModal(id: string) {
    const selectedModal = <HTMLDivElement>document.getElementById(id);
    selectedModal.style.display = 'none';
}

// Handle dynamic input for update


updateField.addEventListener('change', () => {
    const value = updateField.value;
    let inputHTML = '';

    if (value === 'username') {
        inputHTML = `
            <label>New Username:</label>
            <input type="text" required>
        `;
    } else if (value === 'age') {
        inputHTML = `
            <label>New Age:</label>
            <input type="number" min="1" required>
            `;
    } else if (value === 'password') {
        inputHTML = `
            <label>New Password:</label>
            <input type="password" required>
            `;
    } else if (value === 'sex') {
        inputHTML = `
            <label>Sex:</label>
            <select required>
            <option value="">-- Choose --</option>
            <option value="Male">Male</option>
            <option value="Female">Female</option>
            </select>
        `;
    }
    updateInputWrapper.innerHTML = inputHTML;
});

// Attach modal to buttons

    
updateBtn.addEventListener('click', () => {
    openModal('updateModal');
});

deleteBtn.addEventListener('click', () => {
    openModal('deleteModal');
});

cancelUpdate.addEventListener("click", () => {
    closeModal("updateModal");
});

cancelDelete.addEventListener("click", () => {
    closeModal("deleteModal");
});