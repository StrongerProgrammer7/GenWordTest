namespace Генератор_вариантов
{
    partial class UsersConfirmForms
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbMessage = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRewriteFile = new System.Windows.Forms.Button();
            this.btnSaveBoth = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chbApplyToAll = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lbMessage
            // 
            this.lbMessage.AutoSize = true;
            this.lbMessage.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbMessage.Location = new System.Drawing.Point(12, 24);
            this.lbMessage.Name = "lbMessage";
            this.lbMessage.Size = new System.Drawing.Size(344, 19);
            this.lbMessage.TabIndex = 0;
            this.lbMessage.Text = "Файл с именем \"Вариант 1.docx\" уже существует. ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(380, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "Хотите переписать существующий или сохранить оба?";
            // 
            // btnRewriteFile
            // 
            this.btnRewriteFile.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnRewriteFile.Location = new System.Drawing.Point(12, 126);
            this.btnRewriteFile.Name = "btnRewriteFile";
            this.btnRewriteFile.Size = new System.Drawing.Size(126, 27);
            this.btnRewriteFile.TabIndex = 2;
            this.btnRewriteFile.Text = "Переписать файл";
            this.btnRewriteFile.UseVisualStyleBackColor = true;
            this.btnRewriteFile.Click += new System.EventHandler(this.btnRewriteFile_Click);
            // 
            // btnSaveBoth
            // 
            this.btnSaveBoth.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSaveBoth.Location = new System.Drawing.Point(156, 126);
            this.btnSaveBoth.Name = "btnSaveBoth";
            this.btnSaveBoth.Size = new System.Drawing.Size(126, 27);
            this.btnSaveBoth.TabIndex = 3;
            this.btnSaveBoth.Text = "Сохранить оба";
            this.btnSaveBoth.UseVisualStyleBackColor = true;
            this.btnSaveBoth.Click += new System.EventHandler(this.btnSaveBoth_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnCancel.Location = new System.Drawing.Point(297, 126);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(126, 27);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chbApplyToAll
            // 
            this.chbApplyToAll.AutoSize = true;
            this.chbApplyToAll.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.chbApplyToAll.Location = new System.Drawing.Point(16, 90);
            this.chbApplyToAll.Name = "chbApplyToAll";
            this.chbApplyToAll.Size = new System.Drawing.Size(134, 19);
            this.chbApplyToAll.TabIndex = 5;
            this.chbApplyToAll.Text = "Применить ко всем";
            this.chbApplyToAll.UseVisualStyleBackColor = true;
            this.chbApplyToAll.CheckedChanged += new System.EventHandler(this.chbApplyToAll_CheckedChanged);
            // 
            // UsersConfirmForms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 165);
            this.Controls.Add(this.chbApplyToAll);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSaveBoth);
            this.Controls.Add(this.btnRewriteFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbMessage);
            this.Name = "UsersConfirmForms";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UsersConfirmForms_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbMessage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRewriteFile;
        private System.Windows.Forms.Button btnSaveBoth;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chbApplyToAll;
    }
}