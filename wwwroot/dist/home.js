import * as general from "./animation/animation.js";
// items
const updateBtn = document.getElementById("updateBtn");
const deleteBtn = document.getElementById("deleteBtn");
const submitUpdate = document.getElementById("submitUpdate");
const cancelUpdate = document.getElementById("cancelUpdate");
const submitDelete = document.getElementById("submitDelete");
const cancelDelete = document.getElementById("cancelDelete");
const updateField = document.getElementById('updateField');
const updateInputWrapper = document.getElementById('updateInputWrapper');
// add animations
general.toggleSidebar();
// home logic
// Open modal
function openModal(id) {
    const selectedModal = document.getElementById(id);
    selectedModal.style.display = 'flex';
}
// Close modal
function closeModal(id) {
    const selectedModal = document.getElementById(id);
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
    }
    else if (value === 'age') {
        inputHTML = `
            <label>New Age:</label>
            <input type="number" min="1" required>
            `;
    }
    else if (value === 'password') {
        inputHTML = `
            <label>New Password:</label>
            <input type="password" required>
            `;
    }
    else if (value === 'sex') {
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
//# sourceMappingURL=home.js.map