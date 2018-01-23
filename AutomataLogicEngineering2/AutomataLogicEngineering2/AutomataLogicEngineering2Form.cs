namespace AutomataLogicEngineering2
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;
    using Automata;

    public partial class AutomataLogicEngineering2Form : Form
    {
        private FiniteAutomata automata;

        public AutomataLogicEngineering2Form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.PerformAction(() =>
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    var lines = File.ReadLines(openFileDialog1.FileName);
                    this.automata = Parser.FiniteAutomataParser.CreateAutomata(lines.ToList());
                    this.DrawAutomata();
                }
            });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.PerformAction(() =>
            {
                var word = this.textBox1.Text;
                var wordAccepted = this.automata.AcceptsWord(new Word(word));
                this.acceptedLbl.ForeColor = wordAccepted ? Color.Green : Color.Red;
                this.acceptedCheckbox.Checked = wordAccepted;
            });
        }

        private void parseRegExBtn_Click(object sender, EventArgs e)
        {
            this.PerformAction(() =>
            {
                var regex = this.regexTb.Text;
                if (string.IsNullOrEmpty(regex) || string.IsNullOrWhiteSpace(regex))
                {
                    throw new Exception("Expresson cannot be empty");
                }
                this.automata = RegExParser.RegExParser.GenerateAutomataForRegex(regex);
                this.DrawAutomata();
                AutomataFileWriter.WriteToFile(this.automata, "RegExpGenerated");
            });
        }

        private void DrawAutomata()
        {
            this.PerformAction(() =>
            {
                if (this.automata == null) return;
                var file = AutomataGraphCreator.CreateNodeGraphImage(this.automata);
                this.automataPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                this.automataPictureBox.ImageLocation = file;
                this.dfaLbl.ForeColor = this.automata.IsDfa ? Color.Green : Color.Red;
                this.dfaCheckBox.Checked = this.automata.IsDfa;
                this.ndfaLbl.ForeColor = this.automata.IsNdfa ? Color.Green : Color.Red;
                this.ndfaCheckBox.Checked = this.automata.IsNdfa;
                this.automataCommentLblTxt.Text = this.automata.Comment;
                this.cbFinite.Checked = this.automata.IsFinite;
                this.listBox1.Items.Clear();
                this.listBox2.Items.Clear();
                foreach (var testWord in this.automata.TestWords)
                {
                    this.listBox1.Items.Add(
                        $"{testWord}, should be accepted: {(testWord.ShouldBeAccepted ? "yes" : "no")}, accepted: {(testWord.IsAccepted ? "yes" : "no")}");
                }
                foreach (var finiteWord in this.automata.FiniteWords)
                {
                    listBox2.Items.Add(finiteWord.ToString());
                }
            });
        }

        private void PerformAction(Action action)
        {
            labelError.Text = string.Empty;
            try
            {
                action();
            }
            catch (Exception ex)
            {
                labelError.Text = ex.Message;
                labelError.ForeColor = Color.Red;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.PerformAction(() =>
            {
                this.automata = this.automata.ToDfa();
                this.DrawAutomata();
                AutomataFileWriter.WriteToFile(this.automata, "DFAConverted");
            });
        }
    }
}
