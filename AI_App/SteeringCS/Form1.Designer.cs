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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.dbPanel3 = new SteeringCS.DBPanel();
            this.DebugEntityInfo = new System.Windows.Forms.Label();
            this.DebugEntityName = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.HeroPanel = new SteeringCS.DBPanel();
            this.cooldownBar = new SteeringCS.util.StatusBar();
            this.staminaBar = new SteeringCS.util.StatusBar();
            this.healthBar = new SteeringCS.util.StatusBar();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.dbPanel1 = new SteeringCS.DBPanel();
            this.dbPanel2 = new SteeringCS.DBPanel();
            this.hobgoblinCount = new System.Windows.Forms.Label();
            this.goblinCount = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.goblinAmountText = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.hobgoblinAmountText = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.forceSpinnerHobgoblin = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.massSpinnerHobgoblin = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.maxSpeedSpinnerHobgoblin = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.forceSpinnerGoblin = new System.Windows.Forms.NumericUpDown();
            this.massSpinnerGoblin = new System.Windows.Forms.NumericUpDown();
            this.maxSpeedSpinnerGoblin = new System.Windows.Forms.NumericUpDown();
            this.menu = new System.Windows.Forms.MainMenu(this.components);
            this.menu_debugList = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menu_resetButton = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.dbPanel3.SuspendLayout();
            this.HeroPanel.SuspendLayout();
            this.dbPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.forceSpinnerHobgoblin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.massSpinnerHobgoblin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxSpeedSpinnerHobgoblin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.forceSpinnerGoblin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.massSpinnerGoblin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxSpeedSpinnerGoblin)).BeginInit();
            this.SuspendLayout();
            // 
            // dbPanel3
            // 
            this.dbPanel3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.dbPanel3.Controls.Add(this.DebugEntityInfo);
            this.dbPanel3.Controls.Add(this.DebugEntityName);
            this.dbPanel3.Controls.Add(this.label7);
            this.dbPanel3.Location = new System.Drawing.Point(1324, 216);
            this.dbPanel3.Name = "dbPanel3";
            this.dbPanel3.Size = new System.Drawing.Size(223, 183);
            this.dbPanel3.TabIndex = 36;
            this.dbPanel3.Visible = false;
            // 
            // DebugEntityInfo
            // 
            this.DebugEntityInfo.BackColor = System.Drawing.SystemColors.Control;
            this.DebugEntityInfo.Location = new System.Drawing.Point(14, 31);
            this.DebugEntityInfo.Name = "DebugEntityInfo";
            this.DebugEntityInfo.Size = new System.Drawing.Size(196, 142);
            this.DebugEntityInfo.TabIndex = 2;
            // 
            // DebugEntityName
            // 
            this.DebugEntityName.Location = new System.Drawing.Point(110, 14);
            this.DebugEntityName.Name = "DebugEntityName";
            this.DebugEntityName.Size = new System.Drawing.Size(100, 13);
            this.DebugEntityName.TabIndex = 1;
            this.DebugEntityName.Text = "None";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Currently selected:";
            // 
            // HeroPanel
            // 
            this.HeroPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.HeroPanel.Controls.Add(this.cooldownBar);
            this.HeroPanel.Controls.Add(this.staminaBar);
            this.HeroPanel.Controls.Add(this.healthBar);
            this.HeroPanel.Controls.Add(this.label16);
            this.HeroPanel.Controls.Add(this.label15);
            this.HeroPanel.Controls.Add(this.label17);
            this.HeroPanel.Location = new System.Drawing.Point(1324, 0);
            this.HeroPanel.Name = "HeroPanel";
            this.HeroPanel.Size = new System.Drawing.Size(223, 210);
            this.HeroPanel.TabIndex = 0;
            // 
            // cooldownBar
            // 
            this.cooldownBar.ForeColor = System.Drawing.Color.Gold;
            this.cooldownBar.Location = new System.Drawing.Point(17, 169);
            this.cooldownBar.Name = "cooldownBar";
            this.cooldownBar.Size = new System.Drawing.Size(186, 23);
            this.cooldownBar.TabIndex = 2;
            // 
            // staminaBar
            // 
            this.staminaBar.ForeColor = System.Drawing.Color.LimeGreen;
            this.staminaBar.Location = new System.Drawing.Point(17, 103);
            this.staminaBar.Name = "staminaBar";
            this.staminaBar.Size = new System.Drawing.Size(186, 23);
            this.staminaBar.TabIndex = 1;
            // 
            // healthBar
            // 
            this.healthBar.ForeColor = System.Drawing.Color.Red;
            this.healthBar.Location = new System.Drawing.Point(17, 37);
            this.healthBar.Name = "healthBar";
            this.healthBar.Size = new System.Drawing.Size(186, 23);
            this.healthBar.TabIndex = 0;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(12, 141);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(100, 25);
            this.label16.TabIndex = 33;
            this.label16.Text = "Cooldown";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(12, 9);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(68, 25);
            this.label15.TabIndex = 31;
            this.label15.Text = "Health";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(12, 75);
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
            this.dbPanel2.Controls.Add(this.hobgoblinCount);
            this.dbPanel2.Controls.Add(this.goblinCount);
            this.dbPanel2.Controls.Add(this.label14);
            this.dbPanel2.Controls.Add(this.goblinAmountText);
            this.dbPanel2.Controls.Add(this.label13);
            this.dbPanel2.Controls.Add(this.hobgoblinAmountText);
            this.dbPanel2.Controls.Add(this.label12);
            this.dbPanel2.Controls.Add(this.label5);
            this.dbPanel2.Controls.Add(this.forceSpinnerHobgoblin);
            this.dbPanel2.Controls.Add(this.label2);
            this.dbPanel2.Controls.Add(this.massSpinnerHobgoblin);
            this.dbPanel2.Controls.Add(this.label3);
            this.dbPanel2.Controls.Add(this.maxSpeedSpinnerHobgoblin);
            this.dbPanel2.Controls.Add(this.label4);
            this.dbPanel2.Controls.Add(this.label6);
            this.dbPanel2.Controls.Add(this.label1);
            this.dbPanel2.Controls.Add(this.label11);
            this.dbPanel2.Controls.Add(this.label10);
            this.dbPanel2.Controls.Add(this.forceSpinnerGoblin);
            this.dbPanel2.Controls.Add(this.massSpinnerGoblin);
            this.dbPanel2.Controls.Add(this.maxSpeedSpinnerGoblin);
            this.dbPanel2.Location = new System.Drawing.Point(1324, 405);
            this.dbPanel2.Name = "dbPanel2";
            this.dbPanel2.Size = new System.Drawing.Size(223, 400);
            this.dbPanel2.TabIndex = 33;
            // 
            // hobgoblinCount
            // 
            this.hobgoblinCount.AutoSize = true;
            this.hobgoblinCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hobgoblinCount.Location = new System.Drawing.Point(180, 201);
            this.hobgoblinCount.Name = "hobgoblinCount";
            this.hobgoblinCount.Size = new System.Drawing.Size(18, 20);
            this.hobgoblinCount.TabIndex = 33;
            this.hobgoblinCount.Text = "0";
            // 
            // goblinCount
            // 
            this.goblinCount.AutoSize = true;
            this.goblinCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.goblinCount.Location = new System.Drawing.Point(152, 49);
            this.goblinCount.Name = "goblinCount";
            this.goblinCount.Size = new System.Drawing.Size(18, 20);
            this.goblinCount.TabIndex = 32;
            this.goblinCount.Text = "0";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(13, 229);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(50, 20);
            this.label14.TabIndex = 23;
            this.label14.Text = "Force";
            // 
            // goblinAmountText
            // 
            this.goblinAmountText.AutoSize = true;
            this.goblinAmountText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.goblinAmountText.Location = new System.Drawing.Point(13, 49);
            this.goblinAmountText.Name = "goblinAmountText";
            this.goblinAmountText.Size = new System.Drawing.Size(145, 20);
            this.goblinAmountText.TabIndex = 30;
            this.goblinAmountText.Text = "Amount of goblins: ";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(14, 255);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(47, 20);
            this.label13.TabIndex = 24;
            this.label13.Text = "Mass";
            // 
            // hobgoblinAmountText
            // 
            this.hobgoblinAmountText.AutoSize = true;
            this.hobgoblinAmountText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hobgoblinAmountText.Location = new System.Drawing.Point(14, 201);
            this.hobgoblinAmountText.Name = "hobgoblinAmountText";
            this.hobgoblinAmountText.Size = new System.Drawing.Size(172, 20);
            this.hobgoblinAmountText.TabIndex = 31;
            this.hobgoblinAmountText.Text = "Amount of hobgoblins: ";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(13, 284);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(86, 20);
            this.label12.TabIndex = 25;
            this.label12.Text = "Max speed";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(134, 29);
            this.label5.TabIndex = 22;
            this.label5.Text = "Goblins [G]";
            // 
            // forceSpinnerHobgoblin
            // 
            this.forceSpinnerHobgoblin.Location = new System.Drawing.Point(101, 229);
            this.forceSpinnerHobgoblin.Name = "forceSpinnerHobgoblin";
            this.forceSpinnerHobgoblin.Size = new System.Drawing.Size(67, 20);
            this.forceSpinnerHobgoblin.TabIndex = 26;
            this.forceSpinnerHobgoblin.TabStop = false;
            this.forceSpinnerHobgoblin.ValueChanged += new System.EventHandler(this.forceSpinnerHobgoblin_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 20);
            this.label2.TabIndex = 16;
            this.label2.Text = "Force";
            // 
            // massSpinnerHobgoblin
            // 
            this.massSpinnerHobgoblin.Location = new System.Drawing.Point(101, 255);
            this.massSpinnerHobgoblin.Name = "massSpinnerHobgoblin";
            this.massSpinnerHobgoblin.Size = new System.Drawing.Size(67, 20);
            this.massSpinnerHobgoblin.TabIndex = 27;
            this.massSpinnerHobgoblin.TabStop = false;
            this.massSpinnerHobgoblin.ValueChanged += new System.EventHandler(this.massSpinnerHobgoblin_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(13, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 20);
            this.label3.TabIndex = 17;
            this.label3.Text = "Mass";
            // 
            // maxSpeedSpinnerHobgoblin
            // 
            this.maxSpeedSpinnerHobgoblin.Location = new System.Drawing.Point(101, 284);
            this.maxSpeedSpinnerHobgoblin.Name = "maxSpeedSpinnerHobgoblin";
            this.maxSpeedSpinnerHobgoblin.Size = new System.Drawing.Size(67, 20);
            this.maxSpeedSpinnerHobgoblin.TabIndex = 28;
            this.maxSpeedSpinnerHobgoblin.TabStop = false;
            this.maxSpeedSpinnerHobgoblin.ValueChanged += new System.EventHandler(this.maxSpeedSpinnerHobgoblin_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(13, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 20);
            this.label4.TabIndex = 18;
            this.label4.Text = "Max speed";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(12, 167);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(174, 29);
            this.label6.TabIndex = 29;
            this.label6.Text = "Hobgoblins [H]";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Location = new System.Drawing.Point(44, 370);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "P = Toggle pathfinding";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label11.Location = new System.Drawing.Point(44, 357);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(130, 13);
            this.label11.TabIndex = 14;
            this.label11.Text = "V = Toggle velocity visible";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(44, 344);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(142, 13);
            this.label10.TabIndex = 13;
            this.label10.Text = "T = Toggle triangles / circles";
            // 
            // forceSpinnerGoblin
            // 
            this.forceSpinnerGoblin.Location = new System.Drawing.Point(101, 74);
            this.forceSpinnerGoblin.Name = "forceSpinnerGoblin";
            this.forceSpinnerGoblin.Size = new System.Drawing.Size(67, 20);
            this.forceSpinnerGoblin.TabIndex = 19;
            this.forceSpinnerGoblin.TabStop = false;
            this.forceSpinnerGoblin.ValueChanged += new System.EventHandler(this.forceSpinnerGoblin_ValueChanged);
            // 
            // massSpinnerGoblin
            // 
            this.massSpinnerGoblin.Location = new System.Drawing.Point(101, 100);
            this.massSpinnerGoblin.Name = "massSpinnerGoblin";
            this.massSpinnerGoblin.Size = new System.Drawing.Size(67, 20);
            this.massSpinnerGoblin.TabIndex = 20;
            this.massSpinnerGoblin.TabStop = false;
            this.massSpinnerGoblin.ValueChanged += new System.EventHandler(this.massSpinnerGoblin_ValueChanged);
            // 
            // maxSpeedSpinnerGoblin
            // 
            this.maxSpeedSpinnerGoblin.Location = new System.Drawing.Point(101, 126);
            this.maxSpeedSpinnerGoblin.Name = "maxSpeedSpinnerGoblin";
            this.maxSpeedSpinnerGoblin.Size = new System.Drawing.Size(67, 20);
            this.maxSpeedSpinnerGoblin.TabIndex = 21;
            this.maxSpeedSpinnerGoblin.TabStop = false;
            this.maxSpeedSpinnerGoblin.ValueChanged += new System.EventHandler(this.maxSpeedSpinnerGoblin_ValueChanged);
            // 
            // menu
            // 
            this.menu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menu_resetButton,
            this.menu_debugList});
            // 
            // menu_debugList
            // 
            this.menu_debugList.Index = 1;
            this.menu_debugList.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem3,
            this.menuItem4,
            this.menuItem5,
            this.menuItem6});
            this.menu_debugList.Text = "Debug";
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 0;
            this.menuItem3.RadioCheck = true;
            this.menuItem3.Text = "Velocity Visible";
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 1;
            this.menuItem4.Text = "Wall sensors visible";
            // 
            // menu_resetButton
            // 
            this.menu_resetButton.Index = 0;
            this.menu_resetButton.Text = "Reset";
            this.menu_resetButton.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 2;
            this.menuItem5.Text = "Line of sight visible";
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 3;
            this.menuItem6.Text = "Hero attack circle";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1559, 817);
            this.Controls.Add(this.dbPanel3);
            this.Controls.Add(this.HeroPanel);
            this.Controls.Add(this.dbPanel1);
            this.Controls.Add(this.dbPanel2);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Menu = this.menu;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "None";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.dbPanel3.ResumeLayout(false);
            this.dbPanel3.PerformLayout();
            this.HeroPanel.ResumeLayout(false);
            this.HeroPanel.PerformLayout();
            this.dbPanel2.ResumeLayout(false);
            this.dbPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.forceSpinnerHobgoblin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.massSpinnerHobgoblin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxSpeedSpinnerHobgoblin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.forceSpinnerGoblin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.massSpinnerGoblin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxSpeedSpinnerGoblin)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DBPanel dbPanel1;
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
        private DBPanel dbPanel3;
        private System.Windows.Forms.Label DebugEntityInfo;
        private System.Windows.Forms.Label DebugEntityName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label goblinAmountText;
        private System.Windows.Forms.Label hobgoblinAmountText;
        private System.Windows.Forms.Label hobgoblinCount;
        private System.Windows.Forms.Label goblinCount;
        private System.Windows.Forms.MainMenu menu;
        private System.Windows.Forms.MenuItem menu_resetButton;
        private System.Windows.Forms.MenuItem menu_debugList;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem menuItem6;
    }
}

