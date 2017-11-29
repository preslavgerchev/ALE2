namespace AutomataLogicEngineering2
{
    using System;
    using System.Windows.Forms;
    using Automata;

    public partial class AutomataLogicEngineering2Form : Form
    {
        public AutomataLogicEngineering2Form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var automata = Parser.FiniteAutomataParser.ParseAutomata("../../../test.txt");
            var file = AutomataGraphCreator.CreateNodeGraphImage(automata);
            this.automataPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            this.automataPictureBox.ImageLocation = file;
        }
    }
}
