namespace RequestFlowClient.Forms
{
    partial class AssignExecutorForm
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
            this.lblExecutor = new System.Windows.Forms.Label();
            this.cmbExecutor = new System.Windows.Forms.ComboBox();
            this.btnAssign = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // lblExecutor
            this.lblExecutor.AutoSize = true;
            this.lblExecutor.Location = new System.Drawing.Point(22, 27);
            this.lblExecutor.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblExecutor.Name = "lblExecutor";
            this.lblExecutor.Size = new System.Drawing.Size(77, 13);
            this.lblExecutor.TabIndex = 0;
            this.lblExecutor.Text = "Исполнитель:";
            // cmbExecutor
            this.cmbExecutor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbExecutor.Location = new System.Drawing.Point(103, 24);
            this.cmbExecutor.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbExecutor.Name = "cmbExecutor";
            this.cmbExecutor.Size = new System.Drawing.Size(151, 21);
            this.cmbExecutor.TabIndex = 0;
            // btnAssign
            this.btnAssign.Location = new System.Drawing.Point(66, 66);
            this.btnAssign.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAssign.Name = "btnAssign";
            this.btnAssign.Size = new System.Drawing.Size(90, 24);
            this.btnAssign.TabIndex = 1;
            this.btnAssign.Text = "Назначить";
            this.btnAssign.UseVisualStyleBackColor = true;
            this.btnAssign.Click += new System.EventHandler(this.btnAssign_Click);
            // btnCancel
            this.btnCancel.Location = new System.Drawing.Point(164, 66);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 24);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // AssignExecutorForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 122);
            this.Controls.Add(this.lblExecutor);
            this.Controls.Add(this.cmbExecutor);
            this.Controls.Add(this.btnAssign);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.Name = "AssignExecutorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Назначение исполнителя";
            this.Load += new System.EventHandler(this.AssignExecutorForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.Label lblExecutor;
        private System.Windows.Forms.ComboBox cmbExecutor;
        private System.Windows.Forms.Button btnAssign;
        private System.Windows.Forms.Button btnCancel;
    }
}