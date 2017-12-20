namespace HuszarBejaras
{
    partial class Form1
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
            this.gameTableBox = new System.Windows.Forms.GroupBox();
            this.smallNewGameButton = new System.Windows.Forms.Button();
            this.mediumNewGameButton = new System.Windows.Forms.Button();
            this.bigNewGameButton = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.scoreTextLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.scoreValueLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gameTableBox
            // 
            this.gameTableBox.Location = new System.Drawing.Point(33, 67);
            this.gameTableBox.Name = "gameTableBox";
            this.gameTableBox.Size = new System.Drawing.Size(200, 100);
            this.gameTableBox.TabIndex = 0;
            this.gameTableBox.TabStop = false;
            this.gameTableBox.Text = "Jatek tabla";
            // 
            // smallNewGameButton
            // 
            this.smallNewGameButton.Location = new System.Drawing.Point(12, 12);
            this.smallNewGameButton.Name = "smallNewGameButton";
            this.smallNewGameButton.Size = new System.Drawing.Size(75, 23);
            this.smallNewGameButton.TabIndex = 1;
            this.smallNewGameButton.Text = "3x3";
            this.smallNewGameButton.UseVisualStyleBackColor = true;
            this.smallNewGameButton.Click += new System.EventHandler(EasyNewGameButton_Click);
            // 
            // mediumNewGameButton
            // 
            this.mediumNewGameButton.Location = new System.Drawing.Point(104, 12);
            this.mediumNewGameButton.Name = "mediumNewGameButton";
            this.mediumNewGameButton.Size = new System.Drawing.Size(75, 23);
            this.mediumNewGameButton.TabIndex = 2;
            this.mediumNewGameButton.Text = "4x4";
            this.mediumNewGameButton.UseVisualStyleBackColor = true;
            this.mediumNewGameButton.Click += new System.EventHandler(MediumNewGameButton_Click);
            // 
            // bigNewGameButton
            // 
            this.bigNewGameButton.Location = new System.Drawing.Point(197, 12);
            this.bigNewGameButton.Name = "bigNewGameButton";
            this.bigNewGameButton.Size = new System.Drawing.Size(75, 23);
            this.bigNewGameButton.TabIndex = 3;
            this.bigNewGameButton.Text = "6x6";
            this.bigNewGameButton.UseVisualStyleBackColor = true;
            this.bigNewGameButton.Click += new System.EventHandler(BigNewGameButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scoreTextLabel,
            this.scoreValueLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 239);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(284, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // scoreTextLabel
            // 
            this.scoreTextLabel.Name = "scoreTextLabel";
            this.scoreTextLabel.Size = new System.Drawing.Size(39, 17);
            this.scoreTextLabel.Text = "Score:";
            // 
            // scoreValueLabel
            // 
            this.scoreValueLabel.Name = "scoreValueLabel";
            this.scoreValueLabel.Size = new System.Drawing.Size(13, 17);
            this.scoreValueLabel.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.bigNewGameButton);
            this.Controls.Add(this.mediumNewGameButton);
            this.Controls.Add(this.smallNewGameButton);
            this.Controls.Add(this.gameTableBox);
            this.Name = "Form1";
            this.Text = "HuszarBejaras";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gameTableBox;
        private System.Windows.Forms.Button smallNewGameButton;
        private System.Windows.Forms.Button mediumNewGameButton;
        private System.Windows.Forms.Button bigNewGameButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel scoreTextLabel;
        private System.Windows.Forms.ToolStripStatusLabel scoreValueLabel;
    }
}

