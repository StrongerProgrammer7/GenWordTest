using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using WaitWnd;
using Point = System.Drawing.Point;

namespace Генератор_вариантов
{
    using Word = Microsoft.Office.Interop.Word;

    public enum FileContent { Tasks, Answers }; //Варианты содержимого файла

    public partial class Form1 : Form
    {
        private Form2 source_version_;
        private delegate void _workWithWordDelegate(string path, string text, decimal versionNum, FileContent fileContnet, Word.Document doc);
        private Microsoft.Office.Interop.Word.Application _appWord;
        private WaitWndFun _waitWindow;
        private bool _applyToAll;
        private DialogResult _usersConfirmFormResult = DialogResult.None; 

        public Form1()
        {
            InitializeComponent();
            countVarLB.Hide();
            folderVarLB.Hide();
            folderAnsLB.Hide();
            foldeAnsTB.Hide();
            folderTestTB.Hide();
            button1.Hide();
            source_version_ = new Form2(this);

            //Открываем ворд на фоне
            _appWord = new Microsoft.Office.Interop.Word.Application();
            _appWord.Visible = false;

            _waitWindow = new WaitWndFun();

            //Настраиваем расположение окна
            Screen screen = Screen.FromControl(this);
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(screen.WorkingArea.Width/2 - this.Width/2, screen.WorkingArea.Height/2 - this.Height);
        }

//------------------------------------------------------Текстбоксы--------------------------------------------------------------
        private void folderTest_and_AnsTB_Click(object sender, EventArgs e)
        {
            TextBox text_box = (TextBox)sender;
            if (text_box.Text.Contains("Введите путь"))
                text_box.Text = "";
        }

        private void folderTest_and_AnsTB_MouseLeave(object sender, EventArgs e)
        {
            TextBox text_box = (TextBox)sender;
            if (text_box.Text.Length == 0)
            {
                text_box.ForeColor = Color.Gray;
                text_box.Text = "Введите путь";
            }
        }

        private void folderTest_and_AnsTB_TextChanged(object sender, EventArgs e)
        {
            TextBox text_box = (TextBox)sender;
            if (text_box.Text == "Введите путь") return;
            text_box.ForeColor = Color.Black;
        }

        private void FolderTestTB_MouseClick(object sender, MouseEventArgs e)
        {
            FolderBrowserDialog folder_browser_dialog = new FolderBrowserDialog();

            if (folder_browser_dialog.ShowDialog() == DialogResult.OK)
            {
                folderTestTB.Text = folder_browser_dialog.SelectedPath;
            }
        }

        private void FoldeAnsTB_MouseClick(object sender, MouseEventArgs e)
        {
            FolderBrowserDialog folder_browser_dialog = new FolderBrowserDialog();

            if (folder_browser_dialog.ShowDialog() == DialogResult.OK)
            {
                foldeAnsTB.Text = folder_browser_dialog.SelectedPath;
            }
        }
        //------------------------------------------------------Кнопки------------------------------------------------------------------
   
        //Большущая кнопка "Сгенерировать"
        private void b_gen_Click(object sender, EventArgs e)
        {
            if (!PathsAreCorrect()) return; //Проверка введенных путей

            //Генерируем тексты вариантов и ответы к ним
            List<TestVersion> testVersions = GenerateTestVersions(countVar.Value);

            //Сохраняем тексты вариантов
            _waitWindow.Show();

            /*foreach (TestVersion version in testVersions)
            {
                
                if (_usersConfirmFormResult == DialogResult.Cancel && _applyToAll == true) return; //Если пользователь отменил
                                                                                                   //сохранение всех файлов
                                                                                                   //выходим
                _workWithWordDelegate d = new _workWithWordDelegate(SaveToWordFile);
                IAsyncResult result = d.BeginInvoke(folderTestTB.Text, version.VersionText, version.VersionNum,
                    FileContent.Tasks, null, null,doc);
                d.EndInvoke(result);

            }
            doc.Close();
            //Сохраняем ответы
            foreach (TestVersion version in testVersions)
            {
                if (_usersConfirmFormResult == DialogResult.Cancel && _applyToAll == true) return; //Если пользователь отменил
                                                                                                   //сохранение всех файлов
                                                                                                   //выходим
                _workWithWordDelegate d = new _workWithWordDelegate(SaveToWordFile);
                IAsyncResult result = d.BeginInvoke(foldeAnsTB.Text, version.AnswersText, version.VersionNum,
                    FileContent.Answers, null, null);
                d.EndInvoke(result);
            }*/
            saveText(testVersions);
            saveTextAnswer(testVersions);
            _waitWindow.Close();

            _usersConfirmFormResult = DialogResult.None;
            _applyToAll = false;
        }

        private void b_show_Click(object sender, EventArgs e)
        {
            this.Hide();
            source_version_.Show();
        }

        private void b_generate_Click(object sender, EventArgs e)
        {
            this.Height = 337;
            countVarLB.Show();
            folderVarLB.Show();
            folderAnsLB.Show();
            foldeAnsTB.Show();
            folderTestTB.Show();
            button1.Show();
 
            countVar.Value = 1;
            countVar.Maximum = 200;
            countVar.Minimum = 1;

            //Делаем кнопку недоступной
            Button but = (Button)sender;
            but.Enabled = false;
        }

        private List<TestVersion> GenerateTestVersions(decimal numOfVersions)
        {
            List<TestVersion> resultList = new List<TestVersion>();
            for (decimal i = 1; i <= numOfVersions; ++i)
            {
                TestVersion version = GenerateTestVersion(i);
                resultList.Add(version);
            }
            return resultList;
        }

        private TestVersion GenerateTestVersion(decimal num_of_version)
        {
            //Создаем экземпляр класа TestVersion, который будет хранить текст сгенерированных заданий и решения к ним
            TestVersion testVersion = new TestVersion(num_of_version);
            testVersion.generateTasks();

            return testVersion;
        }

        private void saveText(List<TestVersion> testVersion)
        {
            string title = folderTestTB.Text + @"\Варианты.docx";

            Word.Document doc = _appWord.Documents.Add();
            foreach (TestVersion version in testVersion)
            {

                if (_usersConfirmFormResult == DialogResult.Cancel && _applyToAll == true) return; //Если пользователь отменил
                                                                                                   //сохранение всех файлов
                                                                                                   //выходим
                doc.Paragraphs.Last.Range.Text = version.VersionText;

                for (int i = 1; i <= doc.Paragraphs.Count; ++i)
                {
                    doc.Paragraphs[i].Range.Font.Name = "Times New Roman";
                    doc.Paragraphs[i].Range.Font.Size = 14;
                }
                /*_workWithWordDelegate d = new _workWithWordDelegate(SaveToWordFile);
                IAsyncResult result = d.BeginInvoke(folderTestTB.Text, version.VersionText, version.VersionNum,
                    FileContent.Tasks, null, null, doc);*/
                //d.EndInvoke(result);

            }
          
            //Генерируем название документа в зависимости от его содержимого (ответы или варианты)

            if (File.Exists(title) && _applyToAll == false) //Если файл с таким именем уже существует
            {
                UsersConfirmForms usersConfirm = new UsersConfirmForms(1, FileContent.Tasks); //Открываем окно, в котором 
                                                                                                   //спрашиваем пользователя,
                                                                                                   //что делать
                _applyToAll = usersConfirm.ApplyToAll;
                _usersConfirmFormResult = usersConfirm.ShowDialog();

                if (_usersConfirmFormResult == DialogResult.Cancel)
                {
                    doc.Close();
                    return;
                }
            }

            if (_usersConfirmFormResult == DialogResult.No) //Если пользователь решил сохранить оба документа
            {
                string finalTitle = setTitle(title); //Настраиваем название файла в зависимости от того, существуют ли файлы с таким 
                                                     //же названием
                finalTitle = finalTitle.Remove(finalTitle.Length - 5, 5); //Убираем расширение .docx из названия файла
                doc.SaveAs2(finalTitle);
            }
            else
                doc.SaveAs2(title);

            doc.Close();

        }
        private void saveTextAnswer(List<TestVersion> testVersion)
        {
            string title = foldeAnsTB.Text + @"\Варианты ответов.docx";

            Word.Document doc = _appWord.Documents.Add();
            foreach (TestVersion version in testVersion)
            {

                if (_usersConfirmFormResult == DialogResult.Cancel && _applyToAll == true) return; //Если пользователь отменил
                                                                                                   //сохранение всех файлов
                                                                                                   //выходим
                doc.Paragraphs.Last.Range.Text = version.AnswersText;

                for (int i = 1; i <= doc.Paragraphs.Count; ++i)
                {
                    doc.Paragraphs[i].Range.Font.Name = "Times New Roman";
                    doc.Paragraphs[i].Range.Font.Size = 14;
                }
                /*_workWithWordDelegate d = new _workWithWordDelegate(SaveToWordFile);
                IAsyncResult result = d.BeginInvoke(folderTestTB.Text, version.VersionText, version.VersionNum,
                    FileContent.Tasks, null, null, doc);*/
                //d.EndInvoke(result);

            }

            //Генерируем название документа в зависимости от его содержимого (ответы или варианты)

            if (File.Exists(title) && _applyToAll == false) //Если файл с таким именем уже существует
            {
                UsersConfirmForms usersConfirm = new UsersConfirmForms(1, FileContent.Tasks); //Открываем окно, в котором 
                                                                                              //спрашиваем пользователя,
                                                                                              //что делать
                _applyToAll = usersConfirm.ApplyToAll;
                _usersConfirmFormResult = usersConfirm.ShowDialog();

                if (_usersConfirmFormResult == DialogResult.Cancel)
                {
                    doc.Close();
                    return;
                }
            }

            if (_usersConfirmFormResult == DialogResult.No) //Если пользователь решил сохранить оба документа
            {
                string finalTitle = setTitle(title); //Настраиваем название файла в зависимости от того, существуют ли файлы с таким 
                                                     //же названием
                finalTitle = finalTitle.Remove(finalTitle.Length - 5, 5); //Убираем расширение .docx из названия файла
                doc.SaveAs2(finalTitle);
            }
            else
                doc.SaveAs2(title);

            doc.Close();
        }
        private void SaveToWordFile(string path, string text, decimal numOfVersion, FileContent fileContent)
        {
            //Создаем новый вордовский документ
            Word.Document doc = _appWord.Documents.Add();
            doc.Paragraphs.Last.Range.Text = text;

            for (int i = 1; i <= doc.Paragraphs.Count; ++i)
            {
                doc.Paragraphs[i].Range.Font.Name = "Times New Roman";
                doc.Paragraphs[i].Range.Font.Size = 14;
            }

            //Генерируем название документа в зависимости от его содержимого (ответы или варианты)
            string title;
            if (fileContent == FileContent.Answers)
            {
                title = path + @"\Вариант " + numOfVersion + " ответы.docx";
            }
            else
            {
                title = path + @"\Вариант " + numOfVersion + ".docx";
            }

            if (File.Exists(title) && _applyToAll == false) //Если файл с таким именем уже существует
            {
                UsersConfirmForms usersConfirm = new UsersConfirmForms(numOfVersion, fileContent); //Открываем окно, в котором 
                                                                                                   //спрашиваем пользователя,
                                                                                                   //что делать
                _applyToAll = usersConfirm.ApplyToAll;
                _usersConfirmFormResult = usersConfirm.ShowDialog();

                if (_usersConfirmFormResult == DialogResult.Cancel)
                {
                    doc.Close();
                    return;
                }
            }

            if (_usersConfirmFormResult == DialogResult.No) //Если пользователь решил сохранить оба документа
            {
                string finalTitle = setTitle(title); //Настраиваем название файла в зависимости от того, существуют ли файлы с таким 
                                                     //же названием
                finalTitle = finalTitle.Remove(finalTitle.Length - 5, 5); //Убираем расширение .docx из названия файла
                doc.SaveAs2(finalTitle);
            }
            else
                doc.SaveAs2(title);

            doc.Close();
        }

        //Проверка, существуют ли введенные пути
        private bool PathsAreCorrect()
        {
            if (!Directory.Exists(folderTestTB.Text))
            {
                MessageBox.Show("Путь для вариантов не найден.");
                return false;
            }

            if (!Directory.Exists(foldeAnsTB.Text))
            {
                MessageBox.Show("Путь для ответов не найден.");
                return false;
            }
            return true;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Закрываем ворд
            _appWord.Quit();
        }

        //Настройка названия в зависимости от того, существуют ли файлы с таким же названием
        private string setTitle(string primaryTitle)
        {
            //Возвращаем первоначальное название, если файла с таким именем не существует
            if (!File.Exists(primaryTitle)) return primaryTitle;

            string title = primaryTitle;
            int counter = 0; //Номер файла
            while (File.Exists(title)) //Если файл с таким названием уже существует
            {
                counter++; //Увеличиваем номер файлa
                if (!title.Contains('('))
                {
                    title = title.Remove(title.Length - 5, 5);
                    title += $" ({counter}).docx";
                }
                else
                {
                    string[] titleFragmentally = title.Split(new char[] { '(' }); //Отделяем название файла от его номера
                    title = $"{titleFragmentally[0]} ({counter}).docx";
                }
            }

            return title;
        }

        
    }
}
