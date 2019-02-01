namespace SteeringCS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pausedLabel = new System.Windows.Forms.Label();
            this.goblinButton = new System.Windows.Forms.Button();
            this.hobgoblinButton = new System.Windows.Forms.Button();
            this.dbPanel1 = new SteeringCS.DBPanel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1002, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Z = Seekbehaviour";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1002, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "X = Fleebehaviour";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1002, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "C = ArrivalBehaviour";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1002, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "V = PursuitBehaviour";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1002, 171);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(152, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "B = PursuitAndArriveBehaviour";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1002, 208);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "N = WanderBehaviour";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1002, 241);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(111, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Space = Pause / Play";
            // 
            // pausedLabel
            // 
            this.pausedLabel.AutoSize = true;
            this.pausedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pausedLabel.Location = new System.Drawing.Point(1019, 401);
            this.pausedLabel.Name = "pausedLabel";
            this.pausedLabel.Size = new System.Drawing.Size(132, 31);
            this.pausedLabel.TabIndex = 8;
            this.pausedLabel.Text = "PAUSED";
            this.pausedLabel.Visible = false;
            // 
            // goblinButton
            // 
            this.goblinButton.Location = new System.Drawing.Point(1009, 529);
            this.goblinButton.Name = "goblinButton";
            this.goblinButton.Size = new System.Drawing.Size(153, 42);
            this.goblinButton.TabIndex = 9;
            this.goblinButton.Text = "Add Goblin";
            this.goblinButton.UseVisualStyleBackColor = true;
            this.goblinButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.goblinButton_MouseClick);
            // 
            // hobgoblinButton
            // 
            this.hobgoblinButton.Location = new System.Drawing.Point(1009, 481);
            this.hobgoblinButton.Name = "hobgoblinButton";
            this.hobgoblinButton.Size = new System.Drawing.Size(153, 42);
            this.hobgoblinButton.TabIndex = 10;
            this.hobgoblinButton.Text = "Add Hobgoblin";
            this.hobgoblinButton.UseVisualStyleBackColor = true;
            this.hobgoblinButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.hobgoblinButton_MouseClick);
            // 
            // dbPanel1
            // 
            this.dbPanel1.BackColor = System.Drawing.Color.White;
            this.dbPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dbPanel1.Location = new System.Drawing.Point(0, 0);
            this.dbPanel1.Name = "dbPanel1";
            this.dbPanel1.Size = new System.Drawing.Size(996, 571);
            this.dbPanel1.TabIndex = 0;
            this.dbPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.dbPanel1_Paint);
            this.dbPanel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dbPanel1_MouseClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1174, 591);
            this.Controls.Add(this.hobgoblinButton);
            this.Controls.Add(this.goblinButton);
            this.Controls.Add(this.pausedLabel);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dbPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gobbo Slayer";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DBPanel dbPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label pausedLabel;
        private System.Windows.Forms.Button goblinButton;
        private System.Windows.Forms.Button hobgoblinButton;
    }
}

