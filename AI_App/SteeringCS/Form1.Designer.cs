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
            this.label7 = new System.Windows.Forms.Label();
            this.pausedLabel = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.forceSpinnerGoblin = new System.Windows.Forms.NumericUpDown();
            this.massSpinnerGoblin = new System.Windows.Forms.NumericUpDown();
            this.maxSpeedSpinnerGoblin = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.maxSpeedSpinnerHobgoblin = new System.Windows.Forms.NumericUpDown();
            this.massSpinnerHobgoblin = new System.Windows.Forms.NumericUpDown();
            this.forceSpinnerHobgoblin = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.dbPanel1 = new SteeringCS.DBPanel();
            this.dbPanel2 = new SteeringCS.DBPanel();
            this.HeroPanel = new SteeringCS.DBPanel();
            this.cooldownBar = new SteeringCS.util.StatusBar();
            this.staminaBar = new SteeringCS.util.StatusBar();
            this.healthBar = new SteeringCS.util.StatusBar();
            ((System.ComponentModel.ISupportInitialize)(this.forceSpinnerGoblin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.massSpinnerGoblin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxSpeedSpinnerGoblin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxSpeedSpinnerHobgoblin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.massSpinnerHobgoblin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.forceSpinnerHobgoblin)).BeginInit();
            this.HeroPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(1357, 289);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(158, 20);
            this.label7.TabIndex = 7;
            this.label7.Text = "Space = Pause / Play";
            // 
            // pausedLabel
            // 
            this.pausedLabel.AutoSize = true;
            this.pausedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pausedLabel.Location = new System.Drawing.Point(1371, 330);
            this.pausedLabel.Name = "pausedLabel";
            this.pausedLabel.Size = new System.Drawing.Size(110, 31);
            this.pausedLabel.TabIndex = 8;
            this.pausedLabel.Text = "Playing";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(1365, 701);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(143, 20);
            this.label8.TabIndex = 11;
            this.label8.Text = "H = Add Hobgoblin";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(1365, 721);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(118, 20);
            this.label9.TabIndex = 12;
            this.label9.Text = "G = Add Goblin";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(1361, 752);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(142, 13);
            this.label10.TabIndex = 13;
            this.label10.Text = "T = Toggle triangles / circles";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(1361, 765);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(130, 13);
            this.label11.TabIndex = 14;
            this.label11.Text = "V = Toggle velocity visible";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1361, 778);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "P = Toggle pathfinding";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1360, 459);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 20);
            this.label2.TabIndex = 16;
            this.label2.Text = "Force";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1360, 488);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 20);
            this.label3.TabIndex = 17;
            this.label3.Text = "Mass";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1360, 517);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 20);
            this.label4.TabIndex = 18;
            this.label4.Text = "Max speed";
            // 
            // forceSpinnerGoblin
            // 
            this.forceSpinnerGoblin.Location = new System.Drawing.Point(1448, 462);
            this.forceSpinnerGoblin.Name = "forceSpinnerGoblin";
            this.forceSpinnerGoblin.Size = new System.Drawing.Size(67, 20);
            this.forceSpinnerGoblin.TabIndex = 19;
            this.forceSpinnerGoblin.ValueChanged += new System.EventHandler(this.forceSpinnerGoblin_ValueChanged);
            // 
            // massSpinnerGoblin
            // 
            this.massSpinnerGoblin.Location = new System.Drawing.Point(1448, 488);
            this.massSpinnerGoblin.Name = "massSpinnerGoblin";
            this.massSpinnerGoblin.Size = new System.Drawing.Size(67, 20);
            this.massSpinnerGoblin.TabIndex = 20;
            this.massSpinnerGoblin.ValueChanged += new System.EventHandler(this.massSpinnerGoblin_ValueChanged);
            // 
            // maxSpeedSpinnerGoblin
            // 
            this.maxSpeedSpinnerGoblin.Location = new System.Drawing.Point(1448, 517);
            this.maxSpeedSpinnerGoblin.Name = "maxSpeedSpinnerGoblin";
            this.maxSpeedSpinnerGoblin.Size = new System.Drawing.Size(67, 20);
            this.maxSpeedSpinnerGoblin.TabIndex = 21;
            this.maxSpeedSpinnerGoblin.ValueChanged += new System.EventHandler(this.maxSpeedSpinnerGoblin_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(1359, 430);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 29);
            this.label5.TabIndex = 22;
            this.label5.Text = "Goblins";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(1359, 563);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(137, 29);
            this.label6.TabIndex = 29;
            this.label6.Text = "Hobgoblins";
            // 
            // maxSpeedSpinnerHobgoblin
            // 
            this.maxSpeedSpinnerHobgoblin.Location = new System.Drawing.Point(1448, 650);
            this.maxSpeedSpinnerHobgoblin.Name = "maxSpeedSpinnerHobgoblin";
            this.maxSpeedSpinnerHobgoblin.Size = new System.Drawing.Size(67, 20);
            this.maxSpeedSpinnerHobgoblin.TabIndex = 28;
            this.maxSpeedSpinnerHobgoblin.ValueChanged += new System.EventHandler(this.maxSpeedSpinnerHobgoblin_ValueChanged);
            // 
            // massSpinnerHobgoblin
            // 
            this.massSpinnerHobgoblin.Location = new System.Drawing.Point(1448, 621);
            this.massSpinnerHobgoblin.Name = "massSpinnerHobgoblin";
            this.massSpinnerHobgoblin.Size = new System.Drawing.Size(67, 20);
            this.massSpinnerHobgoblin.TabIndex = 27;
            this.massSpinnerHobgoblin.ValueChanged += new System.EventHandler(this.massSpinnerHobgoblin_ValueChanged);
            // 
            // forceSpinnerHobgoblin
            // 
            this.forceSpinnerHobgoblin.Location = new System.Drawing.Point(1448, 595);
            this.forceSpinnerHobgoblin.Name = "forceSpinnerHobgoblin";
            this.forceSpinnerHobgoblin.Size = new System.Drawing.Size(67, 20);
            this.forceSpinnerHobgoblin.TabIndex = 26;
            this.forceSpinnerHobgoblin.ValueChanged += new System.EventHandler(this.forceSpinnerHobgoblin_ValueChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(1360, 650);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(86, 20);
            this.label12.TabIndex = 25;
            this.label12.Text = "Max speed";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(1360, 621);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(47, 20);
            this.label13.TabIndex = 24;
            this.label13.Text = "Mass";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(1360, 592);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(50, 20);
            this.label14.TabIndex = 23;
            this.label14.Text = "Force";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(1336, 28);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(68, 25);
            this.label15.TabIndex = 31;
            this.label15.Text = "Health";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(1337, 160);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(100, 25);
            this.label16.TabIndex = 33;
            this.label16.Text = "Cooldown";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(1337, 94);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(84, 25);
            this.label17.TabIndex = 35;
            this.label17.Text = "Stamina";
            // 
            // dbPanel1
            // 
            this.dbPanel1.BackColor = System.Drawing.Color.White;
            this.dbPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dbPanel1.Location = new System.Drawing.Point(0, 0);
            this.dbPanel1.Name = "dbPanel1";
            this.dbPanel1.Size = new System.Drawing.Size(1308, 817);
            this.dbPanel1.TabIndex = 0;
            this.dbPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.dbPanel1_Paint);
            this.dbPanel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dbPanel1_MouseClick);
            // 
            // dbPanel2
            // 
            this.dbPanel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.dbPanel2.Location = new System.Drawing.Point(1327, 405);
            this.dbPanel2.Name = "dbPanel2";
            this.dbPanel2.Size = new System.Drawing.Size(220, 412);
            this.dbPanel2.TabIndex = 33;
            // 
            // HeroPanel
            // 
            this.HeroPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.HeroPanel.Controls.Add(this.cooldownBar);
            this.HeroPanel.Controls.Add(this.staminaBar);
            this.HeroPanel.Controls.Add(this.healthBar);
            this.HeroPanel.Location = new System.Drawing.Point(1327, 19);
            this.HeroPanel.Name = "HeroPanel";
            this.HeroPanel.Size = new System.Drawing.Size(220, 229);
            this.HeroPanel.TabIndex = 0;
            // 
            // cooldownBar
            // 
            this.cooldownBar.ForeColor = System.Drawing.Color.Gold;
            this.cooldownBar.Location = new System.Drawing.Point(9, 169);
            this.cooldownBar.Name = "cooldownBar";
            this.cooldownBar.Size = new System.Drawing.Size(208, 23);
            this.cooldownBar.TabIndex = 2;
            // 
            // staminaBar
            // 
            this.staminaBar.ForeColor = System.Drawing.Color.LimeGreen;
            this.staminaBar.Location = new System.Drawing.Point(9, 103);
            this.staminaBar.Name = "staminaBar";
            this.staminaBar.Size = new System.Drawing.Size(208, 23);
            this.staminaBar.TabIndex = 1;
            // 
            // healthBar
            // 
            this.healthBar.ForeColor = System.Drawing.Color.Red;
            this.healthBar.Location = new System.Drawing.Point(9, 37);
            this.healthBar.Name = "healthBar";
            this.healthBar.Size = new System.Drawing.Size(208, 23);
            this.healthBar.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1559, 829);
            this.Controls.Add(this.dbPanel1);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.maxSpeedSpinnerHobgoblin);
            this.Controls.Add(this.massSpinnerHobgoblin);
            this.Controls.Add(this.forceSpinnerHobgoblin);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.maxSpeedSpinnerGoblin);
            this.Controls.Add(this.massSpinnerGoblin);
            this.Controls.Add(this.forceSpinnerGoblin);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.pausedLabel);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dbPanel2);
            this.Controls.Add(this.HeroPanel);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gobbo Slayer";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.forceSpinnerGoblin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.massSpinnerGoblin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxSpeedSpinnerGoblin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxSpeedSpinnerHobgoblin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.massSpinnerHobgoblin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.forceSpinnerHobgoblin)).EndInit();
            this.HeroPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DBPanel dbPanel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label pausedLabel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown forceSpinnerGoblin;
        private System.Windows.Forms.NumericUpDown massSpinnerGoblin;
        private System.Windows.Forms.NumericUpDown maxSpeedSpinnerGoblin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown maxSpeedSpinnerHobgoblin;
        private System.Windows.Forms.NumericUpDown massSpinnerHobgoblin;
        private System.Windows.Forms.NumericUpDown forceSpinnerHobgoblin;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private DBPanel dbPanel2;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private DBPanel HeroPanel;
        private util.StatusBar cooldownBar;
        private util.StatusBar staminaBar;
        private util.StatusBar healthBar;
    }
}

