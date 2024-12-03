document.addEventListener('DOMContentLoaded', () => {
    console.log('Web sitesi yüklendi');

    // Mobil menü toggle işlevi
    const mobileMenuButton = document.getElementById('mobile-menu-button');
    const mobileMenu = document.getElementById('mobile-menu');

    if (mobileMenuButton && mobileMenu) {
        mobileMenuButton.addEventListener('click', () => {
            mobileMenu.classList.toggle('hidden');
        });
    }

    // Oyun kartları için hover efekti
    const gameCards = document.querySelectorAll('.game-card');
    gameCards.forEach(card => {
        card.addEventListener('mouseenter', () => {
            card.classList.add('shadow-lg', 'scale-105');
        });
        card.addEventListener('mouseleave', () => {
            card.classList.remove('shadow-lg', 'scale-105');
        });
    });

    // İletişim formu gönderimi
    const contactForm = document.getElementById('contact-form');
    if (contactForm) {
        contactForm.addEventListener('submit', (e) => {
            e.preventDefault();
            // Form verilerini al
            const formData = new FormData(contactForm);
            const name = formData.get('name');
            const email = formData.get('email');
            const message = formData.get('message');

            // Burada form verilerini işleyebilir veya bir API'ye gönderebilirsiniz
            console.log('Form gönderildi:', { name, email, message });

            // Kullanıcıya geri bildirim ver
            alert('Mesajınız başarıyla gönderildi! Teşekkür ederiz.');

            // Formu sıfırla
            contactForm.reset();
        });
    }

    // Sayfa geçişleri için smooth scroll
    document.querySelectorAll('a[href^="#"]').forEach(anchor => {
        anchor.addEventListener('click', function (e) {
            e.preventDefault();
            const target = document.querySelector(this.getAttribute('href'));
            if (target) {
                target.scrollIntoView({
                    behavior: 'smooth',
                    block: 'start',
                });
            }
        });
    });

    // Animasyonlu elementler için gözlemci
    const observerOptions = { threshold: 0.1 };

    const observer = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add('animate-fadeIn');
                observer.unobserve(entry.target); // Animasyon bir kez gerçekleşsin
            }
        });
    }, observerOptions);

    // Animasyonlu öğeleri gözlemle
    document.querySelectorAll('.animate-on-scroll').forEach((el) => observer.observe(el));
});
