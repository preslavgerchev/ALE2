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
            this.testWordsListBox = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.automataPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(16, 20);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(153, 28);
            this.button1.TabIndex = 0;
            this.button1.Text = "Generate Automata";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // automataPictureBox
            // 
            this.automataPictureBox.Location = new System.Drawing.Point(364, 41);
            this.automataPictureBox.Margin = new System.Windows.Forms.Padding(4);
            this.automataPictureBox.Name = "automataPictureBox";
            this.automataPictureBox.Size = new System.Drawing.Size(910, 683);
            this.automataPictureBox.TabIndex = 1;
            this.automataPictureBox.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(16, 106);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(331, 22);
            this.textBox1.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(16, 70);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(153, 28);
            this.button2.TabIndex = 3;
            this.button2.Text = "Accept word";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dfaLbl
            // 
            this.dfaLbl.AutoSize = true;
            this.dfaLbl.Location = new System.Drawing.Point(197, 26);
            this.dfaLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.dfaLbl.Name = "dfaLbl";
            this.dfaLbl.Size = new System.Drawing.Size(35, 17);
            this.dfaLbl.TabIndex = 4;
            this.dfaLbl.Text = "DFA";
            // 
            // ndfaLbl
            // 
            this.ndfaLbl.AutoSize = true;
            this.ndfaLbl.Location = new System.Drawing.Point(264, 26);
            this.ndfaLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ndfaLbl.Name = "ndfaLbl";
            this.ndfaLbl.Size = new System.Drawing.Size(45, 17);
            this.ndfaLbl.TabIndex = 5;
            this.ndfaLbl.Text = "NDFA";
            // 
            // acceptedLbl
            // 
            this.acceptedLbl.AutoSize = true;
            this.acceptedLbl.Location = new System.Drawing.Point(205, 76);
            this.acceptedLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.acceptedLbl.Name = "acceptedLbl";
            this.acceptedLbl.Size = new System.Drawing.Size(67, 17);
            this.acceptedLbl.TabIndex = 6;
            this.acceptedLbl.Text = "Accepted";
            // 
            // automataCommentLblTxt
            // 
            this.automataCommentLblTxt.AutoSize = true;
            this.automataCommentLblTxt.Location = new System.Drawing.Point(659, 20);
            this.automataCommentLblTxt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.automataCommentLblTxt.Name = "automataCommentLblTxt";
            this.automataCommentLblTxt.Size = new System.Drawing.Size(0, 17);
            this.automataCommentLblTxt.TabIndex = 7;
            // 
            // dfaCheckBox
            // 
            this.dfaCheckBox.AutoSize = true;
            this.dfaCheckBox.Location = new System.Drawing.Point(172, 27);
            this.dfaCheckBox.Name = "dfaCheckBox";
            this.dfaCheckBox.Size = new System.Drawing.Size(18, 17);
            this.dfaCheckBox.TabIndex = 8;
            this.dfaCheckBox.UseVisualStyleBackColor = true;
            // 
            // ndfaCheckBox
            // 
            this.ndfaCheckBox.AutoSize = true;
            this.ndfaCheckBox.Location = new System.Drawing.Point(239, 27);
            this.ndfaCheckBox.Name = "ndfaCheckBox";
            this.ndfaCheckBox.Size = new System.Drawing.Size(18, 17);
            this.ndfaCheckBox.TabIndex = 9;
            this.ndfaCheckBox.UseVisualStyleBackColor = true;
            // 
            // acceptedCheckbox
            // 
            this.acceptedCheckbox.AutoSize = true;
            this.acceptedCheckbox.Location = new System.Drawing.Point(180, 76);
            this.acceptedCheckbox.Name = "acceptedCheckbox";
            this.acceptedCheckbox.Size = new System.Drawing.Size(18, 17);
            this.acceptedCheckbox.TabIndex = 10;
            this.acceptedCheckbox.UseVisualStyleBackColor = true;
            // 
            // automataLabelComment
            // 
            this.automataLabelComment.AutoSize = true;
            this.automataLabelComment.Location = new System.Drawing.Point(511, 20);
            this.automataLabelComment.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.automataLabelComment.Name = "automataLabelComment";
            this.automataLabelComment.Size = new System.Drawing.Size(140, 17);
            this.automataLabelComment.TabIndex = 11;
            this.automataLabelComment.Text = "Automata comments:";
            // 
            // testWordsListBox
            // 
            this.testWordsListBox.FormattingEnabled = true;
            this.testWordsListBox.ItemHeight = 16;
            this.testWordsListBox.Location = new System.Drawing.Point(16, 135);
            this.testWordsListBox.Name = "testWordsListBox";
            this.testWordsListBox.Size = new System.Drawing.Size(331, 468);
            this.testWordsListBox.TabIndex = 12;
            // 
            // AutomataLogicEngineering2Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1863, 785);
            this.Controls.Add(this.testWordsListBox);
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
            this.Margin = new System.Windows.Forms.Padding(4);
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
        private System.Windows.Forms.ListBox testWordsListBox;
    }
}

