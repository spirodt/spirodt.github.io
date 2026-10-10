// Мали помошни функции што Blazor ги повикува преку JS interop.
window.portfolio = {
    getItem(key) {
        try { return localStorage.getItem(key); } catch { return null; }
    },
    setItem(key, value) {
        try { localStorage.setItem(key, value); } catch { /* приватен прозорец */ }
    },
    setDocumentLanguage(code, title) {
        document.documentElement.lang = code;
        if (title) document.title = title;
    },
    scrollToSection(id) {
        if (id === 'top') { window.scrollTo({ top: 0, behavior: 'smooth' }); return; }
        const el = document.getElementById(id);
        if (!el) return;
        const offset = (document.querySelector('.nav')?.offsetHeight ?? 0) + 8;
        window.scrollTo({ top: el.getBoundingClientRect().top + window.scrollY - offset, behavior: 'smooth' });
    },
    open(url) {
        window.location.href = url;
    }
};

// Сенка на навигацијата при скролање
window.addEventListener('scroll', () => {
    document.body.classList.toggle('scrolled', window.scrollY > 8);
}, { passive: true });
