namespace Генератор_вариантов
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.b_show = new System.Windows.Forms.Button();
            this.b_generate = new System.Windows.Forms.Button();
            this.countVarLB = new System.Windows.Forms.Label();
            this.folderVarLB = new System.Windows.Forms.Label();
            this.folderAnsLB = new System.Windows.Forms.Label();
            this.countVar = new System.Windows.Forms.NumericUpDown();
            this.folderTestTB = new System.Windows.Forms.TextBox();
            this.foldeAnsTB = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.countVar)).BeginInit();
            this.SuspendLayout();
            // 
            // b_show
            // 
            this.b_show.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.b_show.Location = new System.Drawing.Point(12, 10);
            this.b_show.Name = "b_show";
            this.b_show.Size = new System.Drawing.Size(159, 31);
            this.b_show.TabIndex = 0;
            this.b_show.Text = "Исходные варианты";
            this.b_show.UseVisualStyleBackColor = true;
            this.b_show.Click += new System.EventHandler(this.b_show_Click);
            // 
            // b_generate
            // 
            this.b_generate.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.b_generate.Location = new System.Drawing.Point(12, 47);
            this.b_generate.Name = "b_generate";
            this.b_generate.Size = new System.Drawing.Size(159, 47);
            this.b_generate.TabIndex = 1;
            this.b_generate.Text = "Усложнить жизнь студентам";
            this.b_generate.UseVisualStyleBackColor = true;
            this.b_generate.Click += new System.EventHandler(this.b_generate_Click);
            // 
            // countVarLB
            // 
            this.countVarLB.AutoSize = true;
            this.countVarLB.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.countVarLB.Location = new System.Drawing.Point(8, 99);
            this.countVarLB.Name = "countVarLB";
            this.countVarLB.Size = new System.Drawing.Size(167, 19);
            this.countVarLB.TabIndex = 2;
            this.countVarLB.Text = "Количество вариантов";
            // 
            // folderVarLB
            // 
            this.folderVarLB.AutoSize = true;
            this.folderVarLB.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.folderVarLB.Location = new System.Drawing.Point(26, 148);
            this.folderVarLB.Name = "folderVarLB";
            this.folderVarLB.Size = new System.Drawing.Size(131, 19);
            this.folderVarLB.TabIndex = 3;
            this.folderVarLB.Text = "Папка для тестов";
            // 
            // folderAnsLB
            // 
            this.folderAnsLB.AutoSize = true;
            this.folderAnsLB.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.folderAnsLB.Location = new System.Drawing.Point(18, 201);
            this.folderAnsLB.Name = "folderAnsLB";
            this.folderAnsLB.Size = new System.Drawing.Size(140, 19);
            this.folderAnsLB.TabIndex = 4;
            this.folderAnsLB.Text = "Папка для ответов";
            // 
            // countVar
            // 
            this.countVar.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.countVar.Location = new System.Drawing.Point(30, 121);
            this.countVar.Name = "countVar";
            this.countVar.Size = new System.Drawing.Size(120, 22);
            this.countVar.TabIndex = 5;
            // 
            // folderTestTB
            // 
            this.folderTestTB.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.folderTestTB.Location = new System.Drawing.Point(30, 171);
            this.folderTestTB.Name = "folderTestTB";
            this.folderTestTB.Size = new System.Drawing.Size(120, 25);
            this.folderTestTB.TabIndex = 6;
            this.folderTestTB.Text = "Введите путь";
            this.folderTestTB.Click += new System.EventHandler(this.folderTest_and_AnsTB_Click);
            this.folderTestTB.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FolderTestTB_MouseClick);
            this.folderTestTB.TextChanged += new System.EventHandler(this.folderTest_and_AnsTB_TextChanged);
            this.folderTestTB.MouseLeave += new System.EventHandler(this.folderTest_and_AnsTB_MouseLeave);
            // 
            // foldeAnsTB
            // 
            this.foldeAnsTB.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.foldeAnsTB.Location = new System.Drawing.Point(30, 223);
            this.foldeAnsTB.Name = "foldeAnsTB";
            this.foldeAnsTB.Size = new System.Drawing.Size(120, 25);
            this.foldeAnsTB.TabIndex = 7;
            this.foldeAnsTB.Text = "Введите путь";
            this.foldeAnsTB.Click += new System.EventHandler(this.folderTest_and_AnsTB_Click);
            this.foldeAnsTB.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FoldeAnsTB_MouseClick);
            this.foldeAnsTB.TextChanged += new System.EventHandler(this.folderTest_and_AnsTB_TextChanged);
            this.foldeAnsTB.MouseLeave += new System.EventHandler(this.folderTest_and_AnsTB_MouseLeave);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(19, 254);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(136, 31);
            this.button1.TabIndex = 8;
            this.button1.Text = "Генерация";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.b_gen_Click);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(179, 99);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.foldeAnsTB);
            this.Controls.Add(this.folderTestTB);
            this.Controls.Add(this.countVar);
            this.Controls.Add(this.folderAnsLB);
            this.Controls.Add(this.folderVarLB);
            this.Controls.Add(this.countVarLB);
            this.Controls.Add(this.b_generate);
            this.Controls.Add(this.b_show);
            this.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Упроститель жизни";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.countVar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button b_show;
        private System.Windows.Forms.Button b_generate;
        private System.Windows.Forms.Label countVarLB;
        private System.Windows.Forms.Label folderVarLB;
        private System.Windows.Forms.Label folderAnsLB;
        private System.Windows.Forms.NumericUpDown countVar;
        private System.Windows.Forms.TextBox folderTestTB;
        private System.Windows.Forms.TextBox foldeAnsTB;
        private System.Windows.Forms.Button button1;
    }
}

