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
            var lines = File.ReadLines("../../../test.txt");
            this.automata = Parser.FiniteAutomataParser.CreateAutomata(lines.ToList());
            var file = AutomataGraphCreator.CreateNodeGraphImage(automata);
            this.automataPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            this.automataPictureBox.ImageLocation = file;
            this.dfaLbl.ForeColor = automata.IsDfa ? Color.Green : Color.Red;
            this.dfaCheckBox.Checked = automata.IsDfa;
            this.ndfaLbl.ForeColor = automata.IsNdfa ? Color.Green : Color.Red;
            this.ndfaCheckBox.Checked = automata.IsNdfa;
            this.automataCommentLblTxt.Text = this.automata.Comment;
            this.testWordsListBox.Items.Clear();
            foreach (var testWord in automata.TestWords)
            {
                this.testWordsListBox.Items.Add(
                    $"{testWord}, should be accepted: {(testWord.ShouldBeAccepted ? "yes" : "no")}, accepted: {(testWord.IsAccepted ? "yes" : "no")}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var word = this.textBox1.Text;
            var wordAccepted = this.automata.AcceptsWord(new Word(word));
            this.acceptedLbl.ForeColor = wordAccepted ? Color.Green : Color.Red;
            this.acceptedCheckbox.Checked = wordAccepted;
        }
    }
}
