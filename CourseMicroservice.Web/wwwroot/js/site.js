// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// DOM yüklendiğinde çalış
document.addEventListener("DOMContentLoaded", function () {
    const toastMessage = window.AppConfig?.toastMessage;
    const toastType = window.AppConfig?.toastType;

    if (toastMessage) {
        const toastElement = document.getElementById('globalToast');
        const toastBody = document.getElementById('globalToastBody');

        if (!toastElement || !toastBody) {
            console.error("Global toast HTML elementleri bulunamadı.");
            return;
        }

        const toastTypes = {
            "success": { "class": "text-bg-success" },
            "error": { "class": "text-bg-danger" },
            "warning": { "class": "text-bg-warning" },
            "info": { "class": "text-bg-info" }
        };

        const settings = toastTypes[toastType] || toastTypes["info"];

        Object.values(toastTypes).forEach(t => toastElement.classList.remove(t.class));

        toastElement.classList.add(settings.class);
        toastBody.innerText = toastMessage;

        const bsToast = new bootstrap.Toast(toastElement);
        bsToast.show();
    }
});