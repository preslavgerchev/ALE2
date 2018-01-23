namespace AutomataLogicEngineering2
{
    partial class AutomataLogicEngineering2Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.automataPictureBox = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.dfaLbl = new System.Windows.Forms.Label();
            this.ndfaLbl = new System.Windows.Forms.Label();
            this.acceptedLbl = new System.Windows.Forms.Label();
            this.automataCommentLblTxt = new System.Windows.Forms.Label();
            this.dfaCheckBox = new System.Windows.Forms.CheckBox();
            this.ndfaCheckBox = new System.Windows.Forms.CheckBox();
            this.acceptedCheckbox = new System.Windows.Forms.CheckBox();
            this.automataLabelComment = new System.Windows.Forms.Label();
            this.parseRegExBtn = new System.Windows.Forms.Button();
            this.regexTb = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.cbFinite = new System.Windows.Forms.CheckBox();
            this.lblFinite = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.labelError = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.automataPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Generate Automata";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // automataPictureBox
            // 
            this.automataPictureBox.Location = new System.Drawing.Point(599, 22);
            this.automataPictureBox.Name = "automataPictureBox";
            this.automataPictureBox.Size = new System.Drawing.Size(682, 555);
            this.automataPictureBox.TabIndex = 1;
            this.automataPictureBox.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 86);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(249, 20);
            this.textBox1.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 57);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(115, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Accept word";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dfaLbl
            // 
            this.dfaLbl.AutoSize = true;
            this.dfaLbl.Location = new System.Drawing.Point(148, 21);
            this.dfaLbl.Name = "dfaLbl";
            this.dfaLbl.Size = new System.Drawing.Size(28, 13);
            this.dfaLbl.TabIndex = 4;
            this.dfaLbl.Text = "DFA";
            // 
            // ndfaLbl
            // 
            this.ndfaLbl.AutoSize = true;
            this.ndfaLbl.Location = new System.Drawing.Point(198, 21);
            this.ndfaLbl.Name = "ndfaLbl";
            this.ndfaLbl.Size = new System.Drawing.Size(36, 13);
            this.ndfaLbl.TabIndex = 5;
            this.ndfaLbl.Text = "NDFA";
            // 
            // acceptedLbl
            // 
            this.acceptedLbl.AutoSize = true;
            this.acceptedLbl.Location = new System.Drawing.Point(154, 62);
            this.acceptedLbl.Name = "acceptedLbl";
            this.acceptedLbl.Size = new System.Drawing.Size(53, 13);
            this.acceptedLbl.TabIndex = 6;
            this.acceptedLbl.Text = "Accepted";
            // 
            // automataCommentLblTxt
            // 
            this.automataCommentLblTxt.AutoSize = true;
            this.automataCommentLblTxt.Location = new System.Drawing.Point(702, 7);
            this.automataCommentLblTxt.Name = "automataCommentLblTxt";
            this.automataCommentLblTxt.Size = new System.Drawing.Size(0, 13);
            this.automataCommentLblTxt.TabIndex = 7;
            // 
            // dfaCheckBox
            // 
            this.dfaCheckBox.AutoSize = true;
            this.dfaCheckBox.Location = new System.Drawing.Point(129, 22);
            this.dfaCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.dfaCheckBox.Name = "dfaCheckBox";
            this.dfaCheckBox.Size = new System.Drawing.Size(15, 14);
            this.dfaCheckBox.TabIndex = 8;
            this.dfaCheckBox.UseVisualStyleBackColor = true;
            // 
            // ndfaCheckBox
            // 
            this.ndfaCheckBox.AutoSize = true;
            this.ndfaCheckBox.Location = new System.Drawing.Point(179, 22);
            this.ndfaCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.ndfaCheckBox.Name = "ndfaCheckBox";
            this.ndfaCheckBox.Size = new System.Drawing.Size(15, 14);
            this.ndfaCheckBox.TabIndex = 9;
            this.ndfaCheckBox.UseVisualStyleBackColor = true;
            // 
            // acceptedCheckbox
            // 
            this.acceptedCheckbox.AutoSize = true;
            this.acceptedCheckbox.Location = new System.Drawing.Point(135, 62);
            this.acceptedCheckbox.Margin = new System.Windows.Forms.Padding(2);
            this.acceptedCheckbox.Name = "acceptedCheckbox";
            this.acceptedCheckbox.Size = new System.Drawing.Size(15, 14);
            this.acceptedCheckbox.TabIndex = 10;
            this.acceptedCheckbox.UseVisualStyleBackColor = true;
            // 
            // automataLabelComment
            // 
            this.automataLabelComment.AutoSize = true;
            this.automataLabelComment.Location = new System.Drawing.Point(597, 5);
            this.automataLabelComment.Name = "automataLabelComment";
            this.automataLabelComment.Size = new System.Drawing.Size(106, 13);
            this.automataLabelComment.TabIndex = 11;
            this.automataLabelComment.Text = "Automata comments:";
            // 
            // parseRegExBtn
            // 
            this.parseRegExBtn.Location = new System.Drawing.Point(12, 110);
            this.parseRegExBtn.Name = "parseRegExBtn";
            this.parseRegExBtn.Size = new System.Drawing.Size(136, 23);
            this.parseRegExBtn.TabIndex = 13;
            this.parseRegExBtn.Text = "Parse regular expression";
            this.parseRegExBtn.UseVisualStyleBackColor = true;
            this.parseRegExBtn.Click += new System.EventHandler(this.parseRegExBtn_Click);
            // 
            // regexTb
            // 
            this.regexTb.Location = new System.Drawing.Point(12, 140);
            this.regexTb.Name = "regexTb";
            this.regexTb.Size = new System.Drawing.Size(249, 20);
            this.regexTb.TabIndex = 14;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // cbFinite
            // 
            this.cbFinite.AutoSize = true;
            this.cbFinite.Location = new System.Drawing.Point(246, 21);
            this.cbFinite.Margin = new System.Windows.Forms.Padding(2);
            this.cbFinite.Name = "cbFinite";
            this.cbFinite.Size = new System.Drawing.Size(15, 14);
            this.cbFinite.TabIndex = 16;
            this.cbFinite.UseVisualStyleBackColor = true;
            // 
            // lblFinite
            // 
            this.lblFinite.AutoSize = true;
            this.lblFinite.Location = new System.Drawing.Point(265, 20);
            this.lblFinite.Name = "lblFinite";
            this.lblFinite.Size = new System.Drawing.Size(32, 13);
            this.lblFinite.TabIndex = 15;
            this.lblFinite.Text = "Finite";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.Location = new System.Drawing.Point(13, 183);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(284, 394);
            this.listBox1.TabIndex = 17;
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.HorizontalScrollbar = true;
            this.listBox2.Location = new System.Drawing.Point(303, 183);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(197, 394);
            this.listBox2.TabIndex = 18;
            // 
            // labelError
            // 
            this.labelError.AutoSize = true;
            this.labelError.Location = new System.Drawing.Point(213, 63);
            this.labelError.Name = "labelError";
            this.labelError.Size = new System.Drawing.Size(0, 13);
            this.labelError.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(300, 167);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Finite words:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 167);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Test words:";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(303, 15);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(115, 23);
            this.button3.TabIndex = 22;
            this.button3.Text = "To Dfa";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // AutomataLogicEngineering2Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1397, 638);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelError);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.cbFinite);
            this.Controls.Add(this.lblFinite);
            this.Controls.Add(this.regexTb);
            this.Controls.Add(this.parseRegExBtn);
            this.Controls.Add(this.automataLabelComment);
            this.Controls.Add(this.acceptedCheckbox);
            this.Controls.Add(this.ndfaCheckBox);
            this.Controls.Add(this.dfaCheckBox);
            this.Controls.Add(this.automataCommentLblTxt);
            this.Controls.Add(this.acceptedLbl);
            this.Controls.Add(this.ndfaLbl);
            this.Controls.Add(this.dfaLbl);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.automataPictureBox);
            this.Controls.Add(this.button1);
            this.Name = "AutomataLogicEngineering2Form";
            this.Text = "ALE2";
            ((System.ComponentModel.ISupportInitialize)(this.automataPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox automataPictureBox;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label dfaLbl;
        private System.Windows.Forms.Label ndfaLbl;
        private System.Windows.Forms.Label acceptedLbl;
        private System.Windows.Forms.Label automataCommentLblTxt;
        private System.Windows.Forms.CheckBox dfaCheckBox;
        private System.Windows.Forms.CheckBox ndfaCheckBox;
        private System.Windows.Forms.CheckBox acceptedCheckbox;
        private System.Windows.Forms.Label automataLabelComment;
        private System.Windows.Forms.Button parseRegExBtn;
        private System.Windows.Forms.TextBox regexTb;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.CheckBox cbFinite;
        private System.Windows.Forms.Label lblFinite;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Label labelError;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button3;
    }
}

