namespace RequestFlowClient.Forms
{
    partial class RequestForm
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
            this.lblId = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblPriority = new System.Windows.Forms.Label();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.lblAssignee = new System.Windows.Forms.Label();
            this.lblCreated = new System.Windows.Forms.Label();
            this.lblUpdated = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.btnChangeStatus = new System.Windows.Forms.Button();
            this.btnAssignExecutor = new System.Windows.Forms.Button();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.listViewHistory = new System.Windows.Forms.ListView();
            this.columnDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnUser = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnChange = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnClose = new System.Windows.Forms.Button();
            this.labelId = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelDesc = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.labelPriority = new System.Windows.Forms.Label();
            this.labelAuthor = new System.Windows.Forms.Label();
            this.labelAssignee = new System.Windows.Forms.Label();
            this.labelCreated = new System.Windows.Forms.Label();
            this.labelUpdated = new System.Windows.Forms.Label();
            this.labelChangeStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // lblId
            this.lblId.Location = new System.Drawing.Point(105, 16);
            this.lblId.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(150, 16);
            this.lblId.TabIndex = 1;
            // lblTitle
            this.lblTitle.Location = new System.Drawing.Point(105, 41);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(375, 16);
            this.lblTitle.TabIndex = 3;
            // lblDescription
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(105, 65);
            this.lblDescription.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(0, 13);
            this.lblDescription.TabIndex = 5;
            // lblStatus
            this.lblStatus.Location = new System.Drawing.Point(105, 122);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(75, 16);
            this.lblStatus.TabIndex = 7;
            // lblPriority
            this.lblPriority.Location = new System.Drawing.Point(105, 146);
            this.lblPriority.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPriority.Name = "lblPriority";
            this.lblPriority.Size = new System.Drawing.Size(75, 16);
            this.lblPriority.TabIndex = 9;
            // lblAuthor
            this.lblAuthor.Location = new System.Drawing.Point(105, 171);
            this.lblAuthor.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(75, 16);
            this.lblAuthor.TabIndex = 11;
            // lblAssignee
            this.lblAssignee.Location = new System.Drawing.Point(105, 195);
            this.lblAssignee.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAssignee.Name = "lblAssignee";
            this.lblAssignee.Size = new System.Drawing.Size(75, 16);
            this.lblAssignee.TabIndex = 13;
            // lblCreated
            this.lblCreated.Location = new System.Drawing.Point(105, 219);
            this.lblCreated.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCreated.Name = "lblCreated";
            this.lblCreated.Size = new System.Drawing.Size(112, 16);
            this.lblCreated.TabIndex = 15;
            // lblUpdated
            this.lblUpdated.Location = new System.Drawing.Point(105, 244);
            this.lblUpdated.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUpdated.Name = "lblUpdated";
            this.lblUpdated.Size = new System.Drawing.Size(112, 16);
            this.lblUpdated.TabIndex = 17;
            // cmbStatus
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Location = new System.Drawing.Point(105, 271);
            this.cmbStatus.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(91, 21);
            this.cmbStatus.TabIndex = 19;
            // btnChangeStatus
            this.btnChangeStatus.Location = new System.Drawing.Point(360, 269);
            this.btnChangeStatus.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnChangeStatus.Name = "btnChangeStatus";
            this.btnChangeStatus.Size = new System.Drawing.Size(98, 24);
            this.btnChangeStatus.TabIndex = 21;
            this.btnChangeStatus.Text = "Изменить статус";
            this.btnChangeStatus.UseVisualStyleBackColor = true;
            this.btnChangeStatus.Click += new System.EventHandler(this.btnChangeStatus_Click);
            // btnAssignExecutor
            this.btnAssignExecutor.Location = new System.Drawing.Point(465, 269);
            this.btnAssignExecutor.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAssignExecutor.Name = "btnAssignExecutor";
            this.btnAssignExecutor.Size = new System.Drawing.Size(128, 24);
            this.btnAssignExecutor.TabIndex = 22;
            this.btnAssignExecutor.Text = "Назначить исполнителя";
            this.btnAssignExecutor.UseVisualStyleBackColor = true;
            this.btnAssignExecutor.Click += new System.EventHandler(this.btnAssignExecutor_Click);
            // txtComment
            this.txtComment.Location = new System.Drawing.Point(202, 271);
            this.txtComment.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(151, 20);
            this.txtComment.TabIndex = 20;
            // listViewHistory
            this.listViewHistory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnDate,
            this.columnUser,
            this.columnChange});
            this.listViewHistory.HideSelection = false;
            this.listViewHistory.Location = new System.Drawing.Point(15, 309);
            this.listViewHistory.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.listViewHistory.Name = "listViewHistory";
            this.listViewHistory.Size = new System.Drawing.Size(578, 123);
            this.listViewHistory.TabIndex = 23;
            this.listViewHistory.UseCompatibleStateImageBehavior = false;
            this.listViewHistory.View = System.Windows.Forms.View.Details;
            // columnDate
            this.columnDate.Text = "Дата";
            this.columnDate.Width = 130;
            // columnUser
            this.columnUser.Text = "Пользователь";
            this.columnUser.Width = 120;
            // columnChange
            this.columnChange.Text = "Изменение";
            this.columnChange.Width = 380;
            // btnClose
            this.btnClose.Location = new System.Drawing.Point(518, 453);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 24);
            this.btnClose.TabIndex = 24;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // labelId
            this.labelId.Location = new System.Drawing.Point(15, 16);
            this.labelId.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelId.Name = "labelId";
            this.labelId.Size = new System.Drawing.Size(75, 16);
            this.labelId.TabIndex = 0;
            this.labelId.Text = "ID:";
            // labelTitle
            this.labelTitle.Location = new System.Drawing.Point(15, 41);
            this.labelTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(75, 16);
            this.labelTitle.TabIndex = 2;
            this.labelTitle.Text = "Заголовок:";
            // labelDesc
            this.labelDesc.Location = new System.Drawing.Point(15, 65);
            this.labelDesc.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelDesc.Name = "labelDesc";
            this.labelDesc.Size = new System.Drawing.Size(75, 16);
            this.labelDesc.TabIndex = 4;
            this.labelDesc.Text = "Описание:";
            // labelStatus
            this.labelStatus.Location = new System.Drawing.Point(15, 122);
            this.labelStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(75, 16);
            this.labelStatus.TabIndex = 6;
            this.labelStatus.Text = "Статус:";
            // labelPriority
            this.labelPriority.Location = new System.Drawing.Point(15, 146);
            this.labelPriority.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelPriority.Name = "labelPriority";
            this.labelPriority.Size = new System.Drawing.Size(75, 16);
            this.labelPriority.TabIndex = 8;
            this.labelPriority.Text = "Приоритет:";
            // labelAuthor
            this.labelAuthor.Location = new System.Drawing.Point(15, 171);
            this.labelAuthor.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelAuthor.Name = "labelAuthor";
            this.labelAuthor.Size = new System.Drawing.Size(75, 16);
            this.labelAuthor.TabIndex = 10;
            this.labelAuthor.Text = "Автор:";
            // labelAssignee
            this.labelAssignee.Location = new System.Drawing.Point(15, 195);
            this.labelAssignee.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelAssignee.Name = "labelAssignee";
            this.labelAssignee.Size = new System.Drawing.Size(75, 16);
            this.labelAssignee.TabIndex = 12;
            this.labelAssignee.Text = "Исполнитель:";
            // labelCreated
            this.labelCreated.Location = new System.Drawing.Point(15, 219);
            this.labelCreated.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelCreated.Name = "labelCreated";
            this.labelCreated.Size = new System.Drawing.Size(75, 16);
            this.labelCreated.TabIndex = 14;
            this.labelCreated.Text = "Создана:";
            // labelUpdated
            this.labelUpdated.Location = new System.Drawing.Point(15, 244);
            this.labelUpdated.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelUpdated.Name = "labelUpdated";
            this.labelUpdated.Size = new System.Drawing.Size(75, 16);
            this.labelUpdated.TabIndex = 16;
            this.labelUpdated.Text = "Обновлена:";
            // labelChangeStatus
            this.labelChangeStatus.Location = new System.Drawing.Point(15, 272);
            this.labelChangeStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelChangeStatus.Name = "labelChangeStatus";
            this.labelChangeStatus.Size = new System.Drawing.Size(75, 16);
            this.labelChangeStatus.TabIndex = 18;
            this.labelChangeStatus.Text = "Изменить статус:";
            // RequestForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 488);
            this.Controls.Add(this.labelId);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.labelDesc);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.labelPriority);
            this.Controls.Add(this.lblPriority);
            this.Controls.Add(this.labelAuthor);
            this.Controls.Add(this.lblAuthor);
            this.Controls.Add(this.labelAssignee);
            this.Controls.Add(this.lblAssignee);
            this.Controls.Add(this.labelCreated);
            this.Controls.Add(this.lblCreated);
            this.Controls.Add(this.labelUpdated);
            this.Controls.Add(this.lblUpdated);
            this.Controls.Add(this.labelChangeStatus);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.txtComment);
            this.Controls.Add(this.btnChangeStatus);
            this.Controls.Add(this.btnAssignExecutor);
            this.Controls.Add(this.listViewHistory);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.Name = "RequestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Карточка заявки - RequestFlow";
            this.Load += new System.EventHandler(this.RequestForm_Load_1);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblPriority;
        private System.Windows.Forms.Label lblAuthor;
        private System.Windows.Forms.Label lblAssignee;
        private System.Windows.Forms.Label lblCreated;
        private System.Windows.Forms.Label lblUpdated;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Button btnChangeStatus;
        private System.Windows.Forms.Button btnAssignExecutor;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.ListView listViewHistory;
        private System.Windows.Forms.ColumnHeader columnDate;
        private System.Windows.Forms.ColumnHeader columnUser;
        private System.Windows.Forms.ColumnHeader columnChange;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label labelId;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelDesc;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Label labelPriority;
        private System.Windows.Forms.Label labelAuthor;
        private System.Windows.Forms.Label labelAssignee;
        private System.Windows.Forms.Label labelCreated;
        private System.Windows.Forms.Label labelUpdated;
        private System.Windows.Forms.Label labelChangeStatus;
    }
}