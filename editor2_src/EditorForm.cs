using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Design
{
    public partial class EditorForm : Form
    {
        private string _filename = null;

        public EditorForm()
        {
            InitializeComponent();
            editor.Tick += new Editor.TickDelegate(editor_Tick);
        }

        private void editor_Tick()
        {
            undoToolStripMenuItem.Enabled = editor.CanUndo();
            redoToolStripMenuItem.Enabled = editor.CanRedo();
            cutToolStripMenuItem.Enabled = editor.CanCut();
            copyToolStripMenuItem.Enabled = editor.CanCopy();
            pasteToolStripMenuItem.Enabled = editor.CanPaste();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _filename = null;
            Text = null;
            editor.BodyHtml = string.Empty;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_filename == null)
            {
                if (!SaveFileDialog())
                    return;
            }
            SaveFile(_filename);
        }

        private bool SaveFileDialog()
        {
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.AddExtension = true;
                dlg.DefaultExt = "htm";
                dlg.Filter = "HTML files (*.html;*.htm)|*.html;*.htm";
                DialogResult res = dlg.ShowDialog(this);
                if (res == DialogResult.OK)
                {
                    _filename = dlg.FileName;
                    return true;
                }
                else
                    return false;
            }
        }

        private void SaveFile(string filename)
        {
            // 这里把html文档和图片保存在同一个文件夹中
            using (StreamWriter writer = File.CreateText(filename))
            {
                string body = editor.DocumentText;
                //获取所有涉及到的图片
                string[] images = editor.Images;
                if (images.Length != 0)
                {
                    for (int i = 0, count = images.Length; i < count; ++i)
                    {
                        string image = images[i];

                        if (image.Trim() == "")

                            if (!image.StartsWith("file"))
                            {
                                continue;
                            }

                        string image_path = Path.GetFullPath(image.Replace("%20", " ").Replace("file:///", ""));
                        //为图片设置一个单独的名称
                        string cid = string.Format("image_{0:00}", i);
                        string image_ext = Path.GetExtension(image_path);
                        string new_image_name = cid + image_ext;
                        //防止图片和html文件的文件夹
                        string file_path = Path.GetDirectoryName(filename);
                        try
                        {
                            File.Copy(image_path, file_path + "\\" + new_image_name);
                        }
                        catch
                        {
                            MessageBox.Show("复制文件中引用的图片是出现异常", "信息提示", MessageBoxButtons.OK);
                        }

                        body = body.Replace(image_path, "image_path:\\" + new_image_name);
                    }
                }

                writer.Write(body);
                writer.Close();
            }
        }

        private void LoadFile(string filename)
        {
            using (StreamReader reader = File.OpenText(filename))
            {
                string html = reader.ReadToEnd();
                reader.Close();
                string file_path = Path.GetDirectoryName(filename);
                html = html.Replace("image_path:", "file://" + file_path);
                editor.DocumentText = html;

                Text = editor.DocumentTitle;
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "HTML files (*.html;*.htm)|*.html;*.htm";
                DialogResult res = dlg.ShowDialog(this);
                if (res == DialogResult.OK)
                {
                    _filename = dlg.FileName;
                }
                else
                    return;
            }
            this.editor.Clear();
            LoadFile(_filename);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SaveFileDialog())
                SaveFile(_filename);
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SearchDialog dlg = new SearchDialog(editor))
            {
                dlg.ShowDialog(this);
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editor.SelectAll();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editor.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editor.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editor.Paste();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editor.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editor.Redo();
        }

        private void textToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string text = editor.BodyText;
            MessageBox.Show(this, text);
        }

        private void htmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, editor.BodyHtml);
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editor.Print();
        }

        private void breakToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editor.InsertBreak();
        }

        private void textToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (TextInsertForm form = new TextInsertForm(editor.BodyHtml))
            {
                form.ShowDialog(this);
                if (form.Accepted)
                {
                    editor.BodyHtml = form.HTML;
                }
            }
        }

        private void paragraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editor.InsertParagraph();
        }

    }
}