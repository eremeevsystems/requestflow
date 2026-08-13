using System;
using System.Collections.Generic;
using System.Windows.Forms;
using RequestFlowClient.Models;
using RequestFlowClient.Services;

namespace RequestFlowClient.Forms
{
    public partial class MainForm : Form
    {
        private readonly ApiService _apiService;
        private readonly RequestService _requestService;
        private readonly string _userRole;
        private List<Request> _requests;
        public MainForm(ApiService apiService, string userRole)
        {
            InitializeComponent();
            _apiService = apiService;
            _requestService = new RequestService(apiService);
            _userRole = userRole;

            btnAssignExecutor.Visible = (userRole == "MANAGER");

            LoadRequests();
        }
        private async void LoadRequests()
        {
            try
            {
                _requests = await _requestService.GetRequestsAsync();
                RefreshGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки заявок: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void RefreshGrid()
        {
            dataGridViewRequests.Rows.Clear();

            foreach (var req in _requests)
            {
                dataGridViewRequests.Rows.Add(
                    req.Id,
                    req.Title,
                    req.Status,
                    req.Priority,
                    req.CreatedBy?.Username ?? "—",
                    req.AssignedTo?.Username ?? "—",
                    req.CreatedAt.ToString("dd.MM.yyyy HH:mm")
                );
            }
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadRequests();
        }
        private void btnOpenRequest_Click(object sender, EventArgs e)
        {
            if (dataGridViewRequests.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите заявку", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            long id = Convert.ToInt64(dataGridViewRequests.SelectedRows[0].Cells[0].Value);
            var request = _requests.Find(r => r.Id == id);

            if (request != null)
            {
                var requestForm = new RequestForm(_apiService, request, _userRole);
                requestForm.ShowDialog();
                LoadRequests();
            }
        }
        private void dataGridViewRequests_DoubleClick(object sender, EventArgs e)
        {
            btnOpenRequest_Click(sender, e);
        }
        private void btnCreateRequest_Click(object sender, EventArgs e)
        {
            var createForm = new CreateRequestForm(_apiService);
            if (createForm.ShowDialog() == DialogResult.OK)
            {
                LoadRequests();
            }
        }
        private void btnAssignExecutor_Click(object sender, EventArgs e)
        {
            if (dataGridViewRequests.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите заявку", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            long id = Convert.ToInt64(dataGridViewRequests.SelectedRows[0].Cells[0].Value);
            var request = _requests.Find(r => r.Id == id);

            if (request != null)
            {
                var assignForm = new AssignExecutorForm(_apiService, request);
                if (assignForm.ShowDialog() == DialogResult.OK)
                {
                    LoadRequests();
                }
            }
        }
        private void btnLogout_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Вы уверены, что хотите выйти?", "Выход",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
                var loginForm = new LoginForm();
                loginForm.Show();
            }
        }
        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
