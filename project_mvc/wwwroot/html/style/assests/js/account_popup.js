document.addEventListener('DOMContentLoaded', function () {
    // Elements
    const accountIcon = document.getElementById('account-icon');
    const accountText = document.getElementById('account-text');
    const welcomeText = document.getElementById('welcome-text');
    const loginPopup = document.getElementById('login-popup');
    const registerPopup = document.getElementById('register-popup');
    const successMessage = document.getElementById('success-message');
    const showRegister = document.getElementById('show-register');
    const showLogin = document.getElementById('show-login');
    const goToLogin = document.getElementById('go-to-login');
    const loginForm = document.getElementById('login-form');
    const registerForm = document.getElementById('register-form');

    // Toggle popups
    accountIcon.addEventListener('click', function (e) {
        e.preventDefault();

        // If user is logged in, do nothing or show profile options
        if (welcomeText.style.display === 'block') return;

        // Show login popup by default
        if (!loginPopup.style.display || loginPopup.style.display === 'none') {
            hideAllPopups();
            loginPopup.style.display = 'block';
        } else {
            hideAllPopups();
        }
    });

    // Show register popup
    showRegister.addEventListener('click', function (e) {
        e.preventDefault();
        hideAllPopups();
        registerPopup.style.display = 'block';
    });

    // Show login popup from register
    showLogin.addEventListener('click', function (e) {
        e.preventDefault();
        hideAllPopups();
        loginPopup.style.display = 'block';
    });

    // Go to login from success message
    goToLogin.addEventListener('click', function (e) {
        e.preventDefault();
        hideAllPopups();
        loginPopup.style.display = 'block';
    });

    // Login form submission
    loginForm.addEventListener('submit', function (e) {
        e.preventDefault();

        // Get form values
        const email = document.getElementById('login-email').value;
        const password = document.getElementById('login-password').value;

        // Here you would typically make an AJAX call to your backend
        // For demo purposes, we'll simulate a successful login
        simulateLogin(email);
    });

    // Register form submission
    registerForm.addEventListener('submit', function (e) {
        e.preventDefault();

        // Get form values
        const name = document.getElementById('register-name').value;
        const phone = document.getElementById('register-phone').value;
        const email = document.getElementById('register-email').value;
        const password = document.getElementById('register-password').value;

        // Here you would typically make an AJAX call to your backend
        // For demo purposes, we'll simulate a successful registration
        hideAllPopups();
        successMessage.style.display = 'block';
    });

    // Hide all popups
    function hideAllPopups() {
        loginPopup.style.display = 'none';
        registerPopup.style.display = 'none';
        successMessage.style.display = 'none';
    }

    // Simulate successful login
    function simulateLogin(email) {
        // Hide account text and show welcome text
        accountText.style.display = 'none';
        welcomeText.style.display = 'block';
        welcomeText.textContent = 'Xin chào, ' + email.split('@')[0];

        // Hide the popup
        hideAllPopups();

        // In a real app, you would store the login state
        localStorage.setItem('isLoggedIn', 'true');
        localStorage.setItem('userEmail', email);
    }

    // Check if user is already logged in (for page reloads)
    function checkLoginStatus() {
        if (localStorage.getItem('isLoggedIn') === 'true') {
            const email = localStorage.getItem('userEmail');
            accountText.style.display = 'none';
            welcomeText.style.display = 'block';
            welcomeText.textContent = 'Xin chào, ' + email.split('@')[0];
        }
    }

    // Initialize login status check
    checkLoginStatus();
});
