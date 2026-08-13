namespace RequestFlowClient.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.dataGridViewRequests = new System.Windows.Forms.DataGridView();
            this.ColumnId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPriority = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnAuthor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnAssignee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCreated = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnOpenRequest = new System.Windows.Forms.Button();
            this.btnCreateRequest = new System.Windows.Forms.Button();
            this.btnAssignExecutor = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRequests)).BeginInit();
            this.SuspendLayout();
            // dataGridViewRequests
            this.dataGridViewRequests.AllowUserToAddRows = false;
            this.dataGridViewRequests.AllowUserToDeleteRows = false;
            this.dataGridViewRequests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRequests.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnId,
            this.ColumnTitle,
            this.ColumnStatus,
            this.ColumnPriority,
            this.ColumnAuthor,
            this.ColumnAssignee,
            this.ColumnCreated});
            this.dataGridViewRequests.Location = new System.Drawing.Point(9, 41);
            this.dataGridViewRequests.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewRequests.Name = "dataGridViewRequests";
            this.dataGridViewRequests.ReadOnly = true;
            this.dataGridViewRequests.RowHeadersWidth = 51;
            this.dataGridViewRequests.RowTemplate.Height = 24;
            this.dataGridViewRequests.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewRequests.Size = new System.Drawing.Size(833, 325);
            this.dataGridViewRequests.TabIndex = 0;
            this.dataGridViewRequests.DoubleClick += new System.EventHandler(this.dataGridViewRequests_DoubleClick);
            // ColumnId
            this.ColumnId.HeaderText = "ID";
            this.ColumnId.MinimumWidth = 6;
            this.ColumnId.Name = "ColumnId";
            this.ColumnId.ReadOnly = true;
            this.ColumnId.Width = 50;
            // ColumnTitle
            this.ColumnTitle.HeaderText = "Заголовок";
            this.ColumnTitle.MinimumWidth = 6;
            this.ColumnTitle.Name = "ColumnTitle";
            this.ColumnTitle.ReadOnly = true;
            this.ColumnTitle.Width = 200;
            // ColumnStatus
            this.ColumnStatus.HeaderText = "Статус";
            this.ColumnStatus.MinimumWidth = 6;
            this.ColumnStatus.Name = "ColumnStatus";
            this.ColumnStatus.ReadOnly = true;
            this.ColumnStatus.Width = 110;
            // ColumnPriority
            this.ColumnPriority.HeaderText = "Приоритет";
            this.ColumnPriority.MinimumWidth = 6;
            this.ColumnPriority.Name = "ColumnPriority";
            this.ColumnPriority.ReadOnly = true;
            this.ColumnPriority.Width = 90;
            // ColumnAuthor
            this.ColumnAuthor.HeaderText = "Автор";
            this.ColumnAuthor.MinimumWidth = 6;
            this.ColumnAuthor.Name = "ColumnAuthor";
            this.ColumnAuthor.ReadOnly = true;
            this.ColumnAuthor.Width = 90;
            // ColumnAssignee
            this.ColumnAssignee.HeaderText = "Исполнитель";
            this.ColumnAssignee.MinimumWidth = 6;
            this.ColumnAssignee.Name = "ColumnAssignee";
            this.ColumnAssignee.ReadOnly = true;
            this.ColumnAssignee.Width = 110;
            // ColumnCreated
            this.ColumnCreated.HeaderText = "Создана";
            this.ColumnCreated.MinimumWidth = 6;
            this.ColumnCreated.Name = "ColumnCreated";
            this.ColumnCreated.ReadOnly = true;
            this.ColumnCreated.Width = 130;
            // btnRefresh
            this.btnRefresh.Location = new System.Drawing.Point(9, 10);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(90, 24);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Обновить";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // btnOpenRequest
            this.btnOpenRequest.Location = new System.Drawing.Point(104, 10);
            this.btnOpenRequest.Margin = new System.Windows.Forms.Padding(2);
            this.btnOpenRequest.Name = "btnOpenRequest";
            this.btnOpenRequest.Size = new System.Drawing.Size(112, 24);
            this.btnOpenRequest.TabIndex = 2;
            this.btnOpenRequest.Text = "Открыть заявку";
            this.btnOpenRequest.UseVisualStyleBackColor = true;
            this.btnOpenRequest.Click += new System.EventHandler(this.btnOpenRequest_Click);
            // btnCreateRequest
            this.btnCreateRequest.Location = new System.Drawing.Point(220, 10);
            this.btnCreateRequest.Margin = new System.Windows.Forms.Padding(2);
            this.btnCreateRequest.Name = "btnCreateRequest";
            this.btnCreateRequest.Size = new System.Drawing.Size(112, 24);
            this.btnCreateRequest.TabIndex = 3;
            this.btnCreateRequest.Text = "Создать заявку";
            this.btnCreateRequest.UseVisualStyleBackColor = true;
            this.btnCreateRequest.Click += new System.EventHandler(this.btnCreateRequest_Click);
            // btnAssignExecutor
            this.btnAssignExecutor.Location = new System.Drawing.Point(338, 10);
            this.btnAssignExecutor.Margin = new System.Windows.Forms.Padding(2);
            this.btnAssignExecutor.Name = "btnAssignExecutor";
            this.btnAssignExecutor.Size = new System.Drawing.Size(135, 24);
            this.btnAssignExecutor.TabIndex = 4;
            this.btnAssignExecutor.Text = "Назначить исполнителя";
            this.btnAssignExecutor.UseVisualStyleBackColor = true;
            this.btnAssignExecutor.Click += new System.EventHandler(this.btnAssignExecutor_Click);
            // btnLogout
            this.btnLogout.Location = new System.Drawing.Point(739, 10);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(100, 24);
            this.btnLogout.TabIndex = 5;
            this.btnLogout.Text = "Выход";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(851, 375);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.dataGridViewRequests);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnOpenRequest);
            this.Controls.Add(this.btnCreateRequest);
            this.Controls.Add(this.btnAssignExecutor);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Список заявок - RequestFlow";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRequests)).EndInit();
            this.ResumeLayout(false);
        }
        private System.Windows.Forms.DataGridView dataGridViewRequests;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnOpenRequest;
        private System.Windows.Forms.Button btnCreateRequest;
        private System.Windows.Forms.Button btnAssignExecutor;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPriority;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnAuthor;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnAssignee;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCreated;
    }
}