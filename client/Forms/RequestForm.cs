using System;
using System.Windows.Forms;
using RequestFlowClient.Models;
using RequestFlowClient.Services;

namespace RequestFlowClient.Forms
{
    public partial class RequestForm : Form
    {
        private readonly ApiService _apiService;
        private readonly RequestService _requestService;
        private readonly string _userRole;
        private Request _request;
        public RequestForm(ApiService apiService, Request request, string userRole)
        {
            InitializeComponent();
            _apiService = apiService;
            _requestService = new RequestService(apiService);
            _request = request;
            _userRole = userRole;
            btnAssignExecutor.Visible = (userRole == "MANAGER");
            LoadRequestData();
            LoadHistory();
        }
        private void LoadRequestData()
        {
            lblId.Text = _request.Id.ToString();
            lblTitle.Text = _request.Title;
            lblDescription.Text = _request.Description ?? "(нет описания)";
            lblStatus.Text = _request.Status;
            lblPriority.Text = _request.Priority;
            lblAuthor.Text = _request.CreatedBy?.Username ?? "—";
            lblAssignee.Text = _request.AssignedTo?.Username ?? "не назначен";
            lblCreated.Text = _request.CreatedAt.ToString("dd.MM.yyyy HH:mm");
            lblUpdated.Text = _request.UpdatedAt.ToString("dd.MM.yyyy HH:mm");
            cmbStatus.Items.Clear();
            cmbStatus.Items.AddRange(new object[] {
                "NEW", "ASSIGNED", "IN_PROGRESS", "COMPLETED"
            });
            cmbStatus.SelectedItem = _request.Status;

            btnChangeStatus.Enabled = _request.Status != "COMPLETED";
        }
        private async void LoadHistory()
        {
            try
            {
                var history = await _requestService.GetHistoryAsync(_request.Id);
                listViewHistory.Items.Clear();

                foreach (var item in history)
                {
                    string change = "";
                    if (item.NewStatus != null && item.OldStatus != null)
                        change = $"Статус: {item.OldStatus} → {item.NewStatus}";
                    else if (item.NewAssignee != null && item.OldAssignee != null)
                        change = $"Исполнитель: {item.OldAssignee?.Username} → {item.NewAssignee?.Username}";

                    if (string.IsNullOrEmpty(change) && !string.IsNullOrEmpty(item.Comment))
                        change = item.Comment;

                    var listItem = new ListViewItem(item.ChangedAt.ToString("dd.MM.yyyy HH:mm"));
                    listItem.SubItems.Add(item.ChangedBy?.Username ?? "—");
                    listItem.SubItems.Add(change);
                    listViewHistory.Items.Add(listItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки истории: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void btnChangeStatus_Click(object sender, EventArgs e)
        {
            string newStatus = cmbStatus.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(newStatus))
            {
                MessageBox.Show("Выберите статус", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (newStatus == _request.Status)
            {
                MessageBox.Show("Статус не изменился", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var result = MessageBox.Show($"Изменить статус на {newStatus}?",
                "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;
            btnChangeStatus.Enabled = false;
            btnChangeStatus.Text = "Сохранение...";
            try
            {
                string comment = txtComment.Text.Trim();
                if (string.IsNullOrEmpty(comment))
                    comment = $"Изменение статуса на {newStatus}";

                var updated = await _requestService.UpdateStatusAsync(_request.Id, newStatus, comment);
                _request = updated;

                LoadRequestData();
                LoadHistory();

                MessageBox.Show("Статус успешно изменён!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка изменения статуса: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnChangeStatus.Enabled = true;
                btnChangeStatus.Text = "Изменить статус";
            }
        }
        private void btnAssignExecutor_Click(object sender, EventArgs e)
        {
            var assignForm = new AssignExecutorForm(_apiService, _request);
            if (assignForm.ShowDialog() == DialogResult.OK)
            {
                LoadRequestData();
                LoadHistory();
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void RequestForm_Load_1(object sender, EventArgs e)
        {

        }
    }
}
