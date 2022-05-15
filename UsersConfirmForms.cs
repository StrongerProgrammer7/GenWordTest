using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Генератор_вариантов
{
    public partial class UsersConfirmForms : Form
    {
        private DialogResult _dialogResult;
        private bool _applyToAll = false;

        public bool ApplyToAll
        {
            get { return _applyToAll; }
        }

        public UsersConfirmForms(decimal versionNum, FileContent fileContent)
        {
            InitializeComponent();
            string messageText = $"Файл с именем \"Вариант {versionNum}";
            if (fileContent == FileContent.Answers)
                messageText += " ответы";
            messageText += ".docx\" уже существует";
            lbMessage.Text = messageText;
        }

        //Кнопка "Переписать файл"
        private void btnRewriteFile_Click(object sender, EventArgs e)
        {
            _dialogResult = DialogResult.Yes;
            this.Close();
        }

        //Кнопка "Сохранить оба"
        private void btnSaveBoth_Click(object sender, EventArgs e)
        {
            _dialogResult = DialogResult.No;
            this.Close();
        }

        //Кнопка "Отмена"
        private void btnCancel_Click(object sender, EventArgs e)
        {
            _dialogResult = DialogResult.Cancel;
            this.Close();
        }

        new public DialogResult ShowDialog()
        {
            return _dialogResult;
        }

        //Изменение значения чекбокса "Применить ко всем"
        private void chbApplyToAll_CheckedChanged(object sender, EventArgs e)
        {
            _applyToAll = (sender as CheckBox).Checked;
        }

        private void UsersConfirmForms_FormClosing(object sender, FormClosingEventArgs e)
        {
            _dialogResult = DialogResult.Cancel;
        }
    }
}
