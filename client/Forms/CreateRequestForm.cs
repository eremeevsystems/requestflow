using System;
using System.Windows.Forms;
using RequestFlowClient.Models;
using RequestFlowClient.Services;

namespace RequestFlowClient.Forms
{
    public partial class CreateRequestForm : Form
    {
        private readonly ApiService _apiService;
        public Request CreatedRequest { get; private set; }

        public CreateRequestForm(ApiService apiService)
        {
            InitializeComponent();  
            _apiService = apiService;
        }

        private async void btnCreate_Click(object sender, EventArgs e)
        {
            string title = txtTitle.Text.Trim();
            string description = txtDescription.Text.Trim();
            string priority = cmbPriority.SelectedItem?.ToString() ?? "MEDIUM";

            if (string.IsNullOrEmpty(title))
            {
                MessageBox.Show("Введите заголовок заявки", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnCreate.Enabled = false;
            btnCreate.Text = "Создание...";

            try
            {
                var data = new { title, description, priority };
                var request = await _apiService.PostAsync<Request>("/requests", data);

                CreatedRequest = request;

                MessageBox.Show("Заявка успешно создана!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка создания заявки: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnCreate.Enabled = true;
                btnCreate.Text = "Создать";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void CreateRequestForm_Load(object sender, EventArgs e)
        {

        }
    }
}
