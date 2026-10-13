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

    // ── Тема: „light“ / „dark“; без зачуван избор се следи системот (види index.html)
    isDark() {
        return document.documentElement.dataset.theme === 'dark';
    },
    setTheme(mode) {
        document.documentElement.dataset.theme = mode;
        this.setItem('theme', mode);
        const meta = document.querySelector('meta[name="theme-color"]');
        if (meta) meta.content = mode === 'dark' ? '#0a1122' : '#0b1f4d';
    },

    // Скролај до секција; ако страната уште се црта (пр. по враќање од /download), почекај малку.
    scrollToSection(id, attempt = 0) {
        if (id === 'top') { window.scrollTo({ top: 0, behavior: 'smooth' }); return; }
        const el = document.getElementById(id);
        if (!el) {
            if (attempt < 40) setTimeout(() => this.scrollToSection(id, attempt + 1), 50);
            return;
        }
        const offset = (document.querySelector('.nav')?.offsetHeight ?? 0) + 8;
        window.scrollTo({ top: el.getBoundingClientRect().top + window.scrollY - offset, behavior: 'smooth' });
    },
    open(url) {
        window.location.href = url;
    },
    openWindow(url) {
        const w = window.open(url, '_blank', 'noopener,noreferrer,width=640,height=640');
        if (!w) window.location.href = url;
    },

    // ── Споделување
    canNativeShare() {
        return !!navigator.share;
    },
    async nativeShare(title, text, url) {
        if (!navigator.share) return false;
        try { await navigator.share({ title, text, url }); return true; }
        catch { return false; } // корисникот откажал
    },
    async copyText(text) {
        try { await navigator.clipboard.writeText(text); return true; }
        catch {
            const ta = document.createElement('textarea');
            ta.value = text; ta.style.position = 'fixed'; ta.style.opacity = '0';
            document.body.appendChild(ta); ta.select();
            let ok = false; try { ok = document.execCommand('copy'); } catch { }
            ta.remove(); return ok;
        }
    }
};

// Ако корисникот не избрал тема, следи ја промената на системот во живо.
try {
    window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', e => {
        if (!window.portfolio.getItem('theme')) {
            document.documentElement.dataset.theme = e.matches ? 'dark' : 'light';
        }
    });
} catch { /* стар прелистувач */ }

// Сенка на навигацијата при скролање
window.addEventListener('scroll', () => {
    document.body.classList.toggle('scrolled', window.scrollY > 8);
}, { passive: true });
