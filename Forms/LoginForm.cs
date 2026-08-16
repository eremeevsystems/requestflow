using System;
using System.Windows.Forms;
using RequestFlowClient.Services;

namespace RequestFlowClient.Forms
{
    public partial class LoginForm : Form
    {
        private readonly AuthService _authService;
        private readonly ApiService _apiService;

        public LoginForm()
        {
            InitializeComponent();

            _apiService = new ApiService("http://localhost:8080"); //Подсказка: это тестовый хост, заменить на другой!
            _authService = new AuthService(_apiService);
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnLogin.Enabled = false;
            btnLogin.Text = "Вход...";

            try
            {
                var response = await _authService.LoginAsync(username, password);

                _authService.SetToken(response.Token);

                var mainForm = new MainForm(_apiService, response.Role);
                mainForm.Show();

                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка авторизации: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnLogin.Enabled = true;
                btnLogin.Text = "Войти";
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
