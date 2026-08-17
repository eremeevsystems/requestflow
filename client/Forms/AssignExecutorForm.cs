using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using RequestFlowClient.Models;
using RequestFlowClient.Services;

namespace RequestFlowClient.Forms
{
    public partial class AssignExecutorForm : Form
    {
        private readonly ApiService _apiService;
        private readonly Request _request;
        private List<User> _executors;

        public AssignExecutorForm(ApiService apiService, Request request)
        {
            InitializeComponent();
            _apiService = apiService;
            _request = request;

            LoadExecutors();
        }

        private void LoadExecutors()
        {
            try
            {
                var allUsers = new List<User>
                {
                    new User { Id = 1, Username = "executor1", Role = "EXECUTOR" },
                    new User { Id = 2, Username = "executor2", Role = "EXECUTOR" }
                };

                _executors = allUsers.Where(u => u.Role == "EXECUTOR").ToList();

                cmbExecutor.DisplayMember = "Username";
                cmbExecutor.ValueMember = "Id";
                cmbExecutor.DataSource = _executors;

                if (_request.AssignedTo != null)
                {
                    cmbExecutor.SelectedValue = _request.AssignedTo.Id;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки исполнителей: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnAssign_Click(object sender, EventArgs e)
        {
            if (cmbExecutor.SelectedItem == null)
            {
                MessageBox.Show("Выберите исполнителя", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var executor = (User)cmbExecutor.SelectedItem;

            btnAssign.Enabled = false;
            btnAssign.Text = "Назначение...";

            try
            {
                var data = new { assigneeId = executor.Id };
                var updated = await _apiService.PatchAsync<Request>($"/requests/{_request.Id}/assignee", data);

                MessageBox.Show($"Исполнитель {executor.Username} назначен!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка назначения: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnAssign.Enabled = true;
                btnAssign.Text = "Назначить";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void AssignExecutorForm_Load(object sender, EventArgs e)
        {

        }
    }
}
