document.addEventListener("DOMContentLoaded", () => {
    const btn = document.getElementById("settingsBtn");
    const menu = document.getElementById("settingsMenu");

    if (!btn || !menu) return;

    btn.addEventListener("click", (e) => {
        e.stopPropagation();
        menu.classList.toggle("show");
    });

    document.addEventListener("click", () => {
        menu.classList.remove("show");
    });
});