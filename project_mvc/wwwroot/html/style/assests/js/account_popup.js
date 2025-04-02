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
    const accountDropdown = document.getElementById('account-dropdown');
    const logoutBtn = document.getElementById('logout-btn');
    const dropdownUsername = document.getElementById('dropdown-username');
    const dropdownEmail = document.getElementById('dropdown-email');

    // Toggle popups
    accountIcon.addEventListener('click', function (e) {
        e.preventDefault();

        // Nếu đã đăng nhập, hiển thị dropdown thông tin tài khoản
        if (isLoggedIn()) {
            if (accountDropdown.style.display === 'block') {
                accountDropdown.style.display = 'none';
            } else {
                accountDropdown.style.display = 'block';
                hideAllPopups();
            }
            return;
        }

        // Nếu chưa đăng nhập, hiển thị popup đăng nhập
        if (!loginPopup.style.display || loginPopup.style.display === 'none') {
            hideAllPopups();
            loginPopup.style.display = 'block';
        } else {
            hideAllPopups();
        }
    });

    // Đăng xuất
    logoutBtn.addEventListener('click', function (e) {
        e.preventDefault();
        logout();
    });

    // Click ra ngoài để đóng dropdown
    document.addEventListener('click', function (e) {
        if (!e.target.closest('.account') && !e.target.closest('.account-popup')) {
            accountDropdown.style.display = 'none';
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
        const email = document.getElementById('login-email').value;
        const password = document.getElementById('login-password').value;
        simulateLogin(email);
    });

    // Register form submission
    registerForm.addEventListener('submit', function (e) {
        e.preventDefault();
        const name = document.getElementById('register-name').value;
        const phone = document.getElementById('register-phone').value;
        const email = document.getElementById('register-email').value;
        const password = document.getElementById('register-password').value;
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
        accountText.style.display = 'none';
        welcomeText.style.display = 'block';
        welcomeText.textContent = 'Xin chào, ' + email.split('@')[0];

        dropdownUsername.textContent = email.split('@')[0];
        dropdownEmail.textContent = email;

        hideAllPopups();
        localStorage.setItem('isLoggedIn', 'true');
        localStorage.setItem('userEmail', email);
    }

    // Logout function
    function logout() {
        localStorage.removeItem('isLoggedIn');
        localStorage.removeItem('userEmail');
        accountDropdown.style.display = 'none';
        welcomeText.style.display = 'none';
        accountText.style.display = 'block';
        hideAllPopups();
    }

    // Check login status
    function isLoggedIn() {
        return localStorage.getItem('isLoggedIn') === 'true';
    }

    // Check if user is already logged in (for page reloads)
    function checkLoginStatus() {
        if (isLoggedIn()) {
            const email = localStorage.getItem('userEmail');
            accountText.style.display = 'none';
            welcomeText.style.display = 'block';
            welcomeText.textContent = 'Xin chào, ' + email.split('@')[0];
            dropdownUsername.textContent = email.split('@')[0];
            dropdownEmail.textContent = email;
        }
    }

    // Initialize login status check
    checkLoginStatus();
});