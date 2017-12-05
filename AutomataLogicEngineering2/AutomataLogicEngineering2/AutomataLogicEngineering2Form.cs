namespace AutomataLogicEngineering2
{
    using System;
    using System.Drawing;
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
            this.automata = Parser.FiniteAutomataParser.ParseAutomata("../../../test.txt");
            var file = AutomataGraphCreator.CreateNodeGraphImage(automata);
            this.automataPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            this.automataPictureBox.ImageLocation = file;
            label1.ForeColor = automata.IsDfa ? Color.Green : Color.Red;
            label2.ForeColor = automata.IsNdfa ? Color.Green : Color.Red;
            label4.Text = this.automata.Comment;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var word = this.textBox1.Text;
            var wordAccepted = this.automata.AcceptsWord(word);
            label3.ForeColor = wordAccepted ? Color.Green : Color.Red;
        }
    }
}
