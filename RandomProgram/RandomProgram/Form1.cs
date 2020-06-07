using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace RandomProgram
{
    public partial class Form1 : Form
    {
        bool _work = false;
        SynchronizationContext _syncContext = null;
        Microsoft.Office.Interop.Word._Application _app = null;
        Microsoft.Office.Interop.Word._Document _doc = null;
        List<WordData> _datas = new List<WordData>();
        Dictionary<string, string> _group = new Dictionary<string, string>();
        Graphics _graph = null;
        System.Drawing.Font _font = null;
        public Form1()
        {
            InitializeComponent();
            _syncContext = SynchronizationContext.Current;
            Init();
        }

        private void Init()
        {
            string folderName = System.Windows.Forms.Application.StartupPath + @"\size\";
            try
            {
                foreach (string fname in System.IO.Directory.GetFiles(folderName))
                {
                    string filename = fname.Replace(folderName, "").Replace(".doc", "");
                    cb_all_size.Items.Add(filename);
                }
            }
            catch
            {
                MessageBox.Show("尺寸讀取錯誤", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            listView1.View = System.Windows.Forms.View.Details;
            listView1.GridLines = true;
            listView1.LabelEdit = false;
            listView1.FullRowSelect = true;
            listView1.Columns.Add("項目", 280);
            listView1.Columns.Add("題數", 65);

            listView1.Groups.Add(new ListViewGroup("全部", "全部"));
            _group.Add("全部", "全部");
        }

        private bool IsWork()
        {
            if (_work)
            {
                MessageBox.Show("請等待完成後，再繼續其他動作。");
            }
            return _work;
        }

        private void PostProcess(object obj)
        {
            int value = (int)obj;
            progress_bar.Value = value;
        }

        private void PostLabelText(object obj)
        {
            TextPostData postData = (TextPostData)obj;
            ((Label)postData._lab).Text = postData._text;
        }

        private void PostTextBoxText(object obj)
        {
            TextPostData postData = (TextPostData)obj;
            ((TextBox)postData._lab).Text = postData._text;
        }

        private async void btn_read_word_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Word|*.doc;*.docx";
            openFileDialog.Title = "Load Setting";


            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string file = Path.GetFileNameWithoutExtension(openFileDialog.FileName);
                if (_group.ContainsKey(file))
                {
                    return;
                }
                listView1.Groups.Add(new ListViewGroup(_datas.Count.ToString(), file));
                _group.Add(file, _datas.Count.ToString());
                cb_file_words.Items.Add(file);
                await System.Threading.Tasks.Task.Run(() => ReadWord(openFileDialog.FileName, _datas.Count.ToString()));
            }
        }


        private async void btn_write_word_Click(object sender, EventArgs e)
        {
            if (_datas.Count == 0)
            {
                MessageBox.Show("請先載入Word文件");
                return;
            }

            string filepath = System.Windows.Forms.Application.StartupPath + "/size/" + cb_all_size.Text + ".doc";
            if (!System.IO.File.Exists(filepath))
            {
                MessageBox.Show("尺寸" + cb_all_size.Text + "不存在", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int spaceCount = 0;
            if (!int.TryParse(text_write_space_count.Text, out spaceCount))
            {
                MessageBox.Show("填寫處空格數格式錯誤", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Word|*.doc";
            saveFileDialog.Title = "Save Setting";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                List<string> listDatas = new List<string>();
                foreach (ListViewItem listViewItem in listView1.Items)
                {
                    listDatas.Add(listViewItem.Group.Name);
                    listDatas.Add(listViewItem.SubItems[0].Text);
                    listDatas.Add(listViewItem.SubItems[1].Text);
                }

                _graph = lab_graph.CreateGraphics();
                _font = lab_graph.Font;

                await System.Threading.Tasks.Task.Run(() => WriteWord(filepath, saveFileDialog.FileName, listDatas, spaceCount, ch_format.Checked));

                _graph.Dispose();
            }
        }

        private void ReadWord(string path, string groupID)
        {
            if (IsWork())
            {
                return;
            }
            _work = true;
            _syncContext.Post(PostLabelText, new TextPostData(lab_status, "載入中"));
            _syncContext.Post(PostProcess, 5);

            _app = new Microsoft.Office.Interop.Word.Application();
            _app.Visible = false;
            _syncContext.Post(PostProcess, 25);

            object unknow = Type.Missing;
            object file = path;
            _doc = _app.Documents.Open(ref file,
                ref unknow, ref unknow, ref unknow, ref unknow,
                ref unknow, ref unknow, ref unknow, ref unknow,
                ref unknow, ref unknow, ref unknow, ref unknow,
                ref unknow, ref unknow, ref unknow);
            _syncContext.Post(PostProcess, 50);

            string content = _doc.Content.Text;
            CloseDoc();

            _datas.Add(new WordData(content, groupID));

            _syncContext.Post(PostProcess, 100);
            _syncContext.Post(PostLabelText, new TextPostData(lab_status, "已完成"));
            _work = false;
        }




        private void WriteWord(string filepath, string path, string content)
        {
            _app = new Microsoft.Office.Interop.Word.Application();
            _doc = _app.Documents.Open(filepath);

            _doc.Paragraphs.Last.Range.Font.Name = "新明細體";
            _doc.Paragraphs.Last.Range.ParagraphFormat.AddSpaceBetweenFarEastAndDigit = 0;
            _doc.Paragraphs.Last.Range.Text = content;


            Object Nothing = System.Reflection.Missing.Value;
            object objWordName = path;
            _doc.SaveAs(ref objWordName, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing);
            CloseDoc();
        }

        private void WriteWord(string filepath, string path, List<string> listDatas, int spaceCount, bool format)
        {
            if (IsWork())
            {
                return;
            }
            _work = true;

            _syncContext.Post(PostLabelText, new TextPostData(lab_status, "寫入中"));
            _syncContext.Post(PostProcess, 5);

            string[] contents = null;

            if (format)
            {
                contents = GetFormatContent(listDatas, spaceCount);
            }
            else
            {
                contents = GetGeneralContent(listDatas, spaceCount);
            }


            WriteWord(filepath, path.Replace(".doc", "_題目.doc"), contents[0]);
            WriteWord(filepath, path.Replace(".doc", "_答案.doc"), contents[1]);

            _syncContext.Post(PostProcess, 100);
            _syncContext.Post(PostLabelText, new TextPostData(lab_status, "已完成"));
            _work = false;
        }

        private string[] GetGeneralContent(List<string> listDatas, int spaceCount)
        {
            string ansContent = "";
            string content = "";
            float processSpace = 85.0f / (float)(listDatas.Count / 3); ;
            float value = 5.0f;
            Random random = new Random(Guid.NewGuid().GetHashCode());
            int programNum = 1;
            int total = 0;
            for (int itemIndex = 0; itemIndex < listDatas.Count; itemIndex += 3)
            {
                total += int.Parse(listDatas[itemIndex + 2]);
            }

            for (int itemIndex = 0; itemIndex < listDatas.Count; itemIndex += 3)
            {
                WordData wordData = null;
                string[] datas = null;
                int start = 0;
                int end = 0;

                if (listDatas[itemIndex] == "全部")
                {
                    wordData = _datas[random.Next(0, _datas.Count)];
                }
                else
                {
                    wordData = _datas[int.Parse(listDatas[itemIndex])];

                }

                datas = wordData.GetDatas();

                if (listDatas[itemIndex + 1] == "全部")
                {
                    start = 0;
                    end = datas.Length - 1;
                }
                else
                {
                    Theme theme = wordData.GetTheme(listDatas[itemIndex + 1]);
                    start = theme.Start;
                    end = theme.End;
                }

                string programs = "";
                int programCount = int.Parse(listDatas[itemIndex + 2]);
                programCount = programCount > end - start ? end - start : programCount;
                for (int index = 0; index < programCount; index++)
                {
                    int programIndex = random.Next(start, end);
                    while (programs.IndexOf("x" + programIndex.ToString() + "x") != -1)
                    {
                        programIndex = random.Next(start, end);
                    }
                    programs += "x" + programIndex.ToString() + "x";
                    string fixProgram = FixWriteProgram(datas[programIndex], programNum, total) + "。 \r";
                    int fixIndexLeft = fixProgram.IndexOf("(");
                    ansContent += fixProgram;
                    content += fixProgram.Substring(0, fixIndexLeft + 1) + new String(' ', spaceCount) + fixProgram.Substring(fixIndexLeft + 2, fixProgram.Length - fixIndexLeft - 2);
                    programNum++;
                }

                value += processSpace;
                _syncContext.Post(PostProcess, (int)value);
            }
            return new string[] { content, ansContent };
        }

        private string FixWriteProgram(string program, int programNum, int programCount)
        {
            int strProgramCount = programCount.ToString().Length + 2;
            string strProgramNum = programNum.ToString() + ". ";
            strProgramNum += new String(' ', strProgramCount - strProgramNum.Length);
            program = strProgramNum + program.Replace("\r", "\r" + new String(' ', strProgramCount + 6));
            return program;
        }

        private string[] GetFormatContent(List<string> listDatas, int spaceCount)
        {
            string ansContent = "";
            string content = "";
            float processSpace = 85.0f / (float)(listDatas.Count / 3); ;
            float value = 5.0f;
            Random random = new Random(Guid.NewGuid().GetHashCode());
            int programNum = 1;
            int total = 0;
            for (int itemIndex = 0; itemIndex < listDatas.Count; itemIndex += 3)
            {
                total += int.Parse(listDatas[itemIndex + 2]);
            }

            for (int itemIndex = 0; itemIndex < listDatas.Count; itemIndex += 3)
            {
                WordData wordData = null;
                string[] datas = null;
                int start = 0;
                int end = 0;

                if (listDatas[itemIndex] == "全部")
                {
                    wordData = _datas[random.Next(0, _datas.Count)];
                }
                else
                {
                    wordData = _datas[int.Parse(listDatas[itemIndex])];

                }

                datas = wordData.GetDatas();

                if (listDatas[itemIndex + 1] == "全部")
                {
                    start = 0;
                    end = datas.Length - 1;
                }
                else
                {
                    Theme theme = wordData.GetTheme(listDatas[itemIndex + 1]);
                    start = theme.Start;
                    end = theme.End;
                }

                string programs = "";
                int programCount = int.Parse(listDatas[itemIndex + 2]);
                programCount = programCount > end - start ? end - start : programCount;
                for (int index = 0; index < programCount; index++)
                {
                    int programIndex = random.Next(start, end);
                    while (programs.IndexOf("x" + programIndex.ToString() + "x") != -1)
                    {
                        programIndex = random.Next(start, end);
                    }
                    programs += "x" + programIndex.ToString() + "x";
                    string fixProgram = FormatProgram(datas[programIndex], programNum, total) + " \r";
                    int fixIndexLeft = fixProgram.IndexOf("(");
                    ansContent += fixProgram;
                    content += fixProgram.Substring(0, fixIndexLeft + 1) + new String(' ', spaceCount) + fixProgram.Substring(fixIndexLeft + 2, fixProgram.Length - fixIndexLeft - 2);
                    programNum++;
                }

                value += processSpace;
                _syncContext.Post(PostProcess, (int)value);
            }
            return new string[] { content, ansContent };
        }

        private string FormatProgram(string program, int programNum, int programCount)
        {
            int strProgramCount = programCount.ToString().Length + 2;
            string strProgramNum = programNum.ToString() + ". ";
            strProgramNum += new String(' ', strProgramCount - strProgramNum.Length);
            program = strProgramNum + program.Replace(" ", "").Replace("\r", "") + "。";

            program = FormatProgram(program, 846, new String(' ', strProgramCount));

            return program;
        }

        private string FormatProgram(string program, int width, string space)
        {
            int txtWidth = (int)_graph.MeasureString(program, lab_graph.Font).Width;
            if (txtWidth <= width)
            {
                return program;
            }

            string newProgram = "";
            do
            {
                int index = program.Length > 50 ? 50 : program.Length - 1;
                string nextProgram = program.Substring(0, index + 1);
                int nextProgramWidth = (int)_graph.MeasureString(nextProgram, _font).Width;
                while ((index + 1) < program.Length
                    && nextProgramWidth <= width)
                {
                    nextProgram += program[++index];
                    nextProgramWidth = (int)_graph.MeasureString(nextProgram, _font).Width;
                }
                if (nextProgramWidth > width)
                {
                    nextProgram = nextProgram.Remove(nextProgram.Length - 1);
                    newProgram += nextProgram + "\r" + space;
                    program = space + program.Substring(index, program.Length - index);
                    index--;
                }
                else if(index == nextProgram.Length - 1)
                {
                    newProgram += nextProgram;
                    program = "";
                }
                else
                {
                    newProgram += nextProgram + "\r" + space;
                    program = space + program.Substring(index, program.Length - index);
                }
                
            } while (program.Length > 0);

            return newProgram;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseDoc();
            System.Windows.Forms.Application.Exit();
        }

        private void CloseDoc()
        {
            if (_doc != null)
            {
                _doc.Close();
                _doc = null;
            }

            if (_app != null)
            {
                _app.Quit();
                _app = null;
            }
        }

        class TextPostData
        {
            public Object _lab;
            public string _text;
            public TextPostData(Object lab, string text)
            {
                _lab = lab;
                _text = text;
            }
        }

        private void ShowWord(string content)
        {
            ShowWordContent showWordContent = new ShowWordContent(content);
            showWordContent.Show();
        }

        private void cb_file_words_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsWork())
            {
                return;
            }

            cb_file_word_item.Items.Clear();
            cb_file_word_item.Items.Add("全部");
            cb_file_word_item.Text = "全部";
            if (!_group.ContainsKey(cb_file_words.Text) || cb_file_words.Text == "全部")
            {
                return;
            }

            List<Theme> themes = _datas[int.Parse(_group[cb_file_words.Text])].GetThemes();
            for (int index = 0; index < themes.Count; index++)
            {
                cb_file_word_item.Items.Add(themes[index].ThemeName);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!_group.ContainsKey(cb_file_words.Text))
            {
                return;
            }

            foreach (ListViewItem listViewItem in listView1.Items)
            {
                if (listViewItem.Group.Name == _group[cb_file_words.Text]
                    && listViewItem.SubItems[0].Text == cb_file_word_item.Text)
                {
                    return;
                }

            }
            ListViewItem item = new ListViewItem(cb_file_word_item.Text);
            item.SubItems.Add(text_insert_count.Text);
            listView1.Groups[_group[cb_file_words.Text]].Items.Add(item);
            listView1.Items.Add(item);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 1)
            {
                return;
            }

            lab_file_word_item.Text = listView1.SelectedItems[0].SubItems[0].Text;
            lab_file_word.Text = listView1.SelectedItems[0].Group.Header;
            text_edit_count.Text = listView1.SelectedItems[0].SubItems[1].Text;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 1)
            {
                return;
            }

            listView1.SelectedItems[0].SubItems[1].Text = text_edit_count.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 1)
            {
                return;
            }

            listView1.SelectedItems[0].Remove();
        }
    }
}
