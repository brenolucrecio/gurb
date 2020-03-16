using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gurb
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnConvert_Click(object sender, EventArgs e)
        {
            CreateCommand();
        }

        private void CreateCommand()
        {
            int cmdLenght = this.tbtText.Lines.Length;
            for (int i = 0; i < cmdLenght; i++)
            {
                try
                {
                    string text = tbtText.Lines[i];
                    if (!string.IsNullOrWhiteSpace(text))
                    {
                        var datatype = this.GetType(text.Substring(0, 2));
                        var column = text.Substring(0, text.IndexOf('-') - 1);
                        var table = text.Substring(text.IndexOf('-') + 1);

                        var cmd = string.Format("ALTER TABLE {0} ADD {1} {2};\n", table.ToUpper(), column.ToUpper(), datatype);
                        this.tbCommand.AppendText(cmd);
                        this.ChangeColorCommand();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Formato de entrada inválida!\nVerifica as informações de entrada.", "Gurb", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private string GetType(string text)
        {
            var datatype = "";
            text = text.ToUpper();
            switch (text)
            {
                case "CD":
                    datatype = "INT";
                    break;
                case "DS":
                    datatype = "NVARCHAR(MAX)";
                    break;
                case "NR":
                    datatype = "DECIMAL";
                    break;
                case "VL":
                    datatype = "DECIMAL";
                    break;
                case "X_":
                    datatype = "BIT";
                    break;
                default:
                    break;
            }

            return datatype;
        }

        private void ChangeColorCommand()
        {
            this.CheckKeyword("ALTER", Color.Blue, 0);
            this.CheckKeyword("TABLE", Color.Blue, 0);
            this.CheckKeyword("ADD", Color.Blue, 0);
            this.CheckKeyword("BIT", Color.Blue, 0);
            this.CheckKeyword("NVARCHAR", Color.Blue, 0);
            this.CheckKeyword("INT", Color.Blue, 0);
            this.CheckKeyword("DECIMAL", Color.Blue, 0);
            this.CheckKeyword("MAX", Color.Purple, 0);
        }

        private void CheckKeyword(string word, Color color, int startIndex)
        {
            if (this.tbCommand.Text.Contains(word))
            {
                int index = -1;
                int selectStart = this.tbCommand.SelectionStart;

                while ((index = this.tbCommand.Text.IndexOf(word, (index + 1))) != -1)
                {
                    this.tbCommand.Select((index + startIndex), word.Length);
                    this.tbCommand.SelectionColor = color;
                    this.tbCommand.Select(selectStart, 0);
                    this.tbCommand.SelectionColor = Color.Black;
                }
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Em desenvolvimento...", "Gurb", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            this.tbCommand.Clear();
            this.tbtText.Clear();
            this.tbtText.Focus();
        }
    }
}
