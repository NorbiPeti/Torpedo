namespace Torpedo
{
    partial class GameForm
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
            this.ownPanel = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.egyjátékosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.többjátékosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enemyPanel = new System.Windows.Forms.Panel();
            this.shipSizeLabel = new System.Windows.Forms.Label();
            this.moveUpButton = new System.Windows.Forms.Button();
            this.moveRightButton = new System.Windows.Forms.Button();
            this.moveDownButton = new System.Windows.Forms.Button();
            this.moveLeftButton = new System.Windows.Forms.Button();
            this.rotateButton = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ownPanel
            // 
            this.ownPanel.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.ownPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ownPanel.Location = new System.Drawing.Point(12, 27);
            this.ownPanel.Name = "ownPanel";
            this.ownPanel.Size = new System.Drawing.Size(300, 300);
            this.ownPanel.TabIndex = 0;
            this.ownPanel.Click += new System.EventHandler(this.ownPanel_Click);
            this.ownPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.ownPanel_Paint);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.egyjátékosToolStripMenuItem,
            this.többjátékosToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(723, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // egyjátékosToolStripMenuItem
            // 
            this.egyjátékosToolStripMenuItem.Enabled = false;
            this.egyjátékosToolStripMenuItem.Name = "egyjátékosToolStripMenuItem";
            this.egyjátékosToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.egyjátékosToolStripMenuItem.Text = "Egyjátékos";
            this.egyjátékosToolStripMenuItem.Click += new System.EventHandler(this.GameTypeMenuClick);
            // 
            // többjátékosToolStripMenuItem
            // 
            this.többjátékosToolStripMenuItem.Name = "többjátékosToolStripMenuItem";
            this.többjátékosToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            this.többjátékosToolStripMenuItem.Text = "Többjátékos";
            this.többjátékosToolStripMenuItem.Click += new System.EventHandler(this.GameTypeMenuClick);
            // 
            // enemyPanel
            // 
            this.enemyPanel.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.enemyPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.enemyPanel.Location = new System.Drawing.Point(405, 27);
            this.enemyPanel.Name = "enemyPanel";
            this.enemyPanel.Size = new System.Drawing.Size(300, 300);
            this.enemyPanel.TabIndex = 1;
            this.enemyPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.enemyPanel_Paint);
            // 
            // shipSizeLabel
            // 
            this.shipSizeLabel.AutoSize = true;
            this.shipSizeLabel.Location = new System.Drawing.Point(12, 339);
            this.shipSizeLabel.Name = "shipSizeLabel";
            this.shipSizeLabel.Size = new System.Drawing.Size(81, 13);
            this.shipSizeLabel.TabIndex = 2;
            this.shipSizeLabel.Text = "Következő hajó";
            // 
            // moveUpButton
            // 
            this.moveUpButton.Location = new System.Drawing.Point(167, 333);
            this.moveUpButton.Name = "moveUpButton";
            this.moveUpButton.Size = new System.Drawing.Size(75, 75);
            this.moveUpButton.TabIndex = 3;
            this.moveUpButton.Text = "Fel";
            this.moveUpButton.UseVisualStyleBackColor = true;
            this.moveUpButton.Click += new System.EventHandler(this.MoveShip);
            // 
            // moveRightButton
            // 
            this.moveRightButton.Location = new System.Drawing.Point(248, 411);
            this.moveRightButton.Name = "moveRightButton";
            this.moveRightButton.Size = new System.Drawing.Size(75, 75);
            this.moveRightButton.TabIndex = 4;
            this.moveRightButton.Text = "Jobbra";
            this.moveRightButton.UseVisualStyleBackColor = true;
            this.moveRightButton.Click += new System.EventHandler(this.MoveShip);
            // 
            // moveDownButton
            // 
            this.moveDownButton.Location = new System.Drawing.Point(167, 411);
            this.moveDownButton.Name = "moveDownButton";
            this.moveDownButton.Size = new System.Drawing.Size(75, 75);
            this.moveDownButton.TabIndex = 5;
            this.moveDownButton.Text = "Le";
            this.moveDownButton.UseVisualStyleBackColor = true;
            this.moveDownButton.Click += new System.EventHandler(this.MoveShip);
            // 
            // moveLeftButton
            // 
            this.moveLeftButton.Location = new System.Drawing.Point(86, 411);
            this.moveLeftButton.Name = "moveLeftButton";
            this.moveLeftButton.Size = new System.Drawing.Size(75, 75);
            this.moveLeftButton.TabIndex = 6;
            this.moveLeftButton.Text = "Le";
            this.moveLeftButton.UseVisualStyleBackColor = true;
            this.moveLeftButton.Click += new System.EventHandler(this.MoveShip);
            // 
            // rotateButton
            // 
            this.rotateButton.Location = new System.Drawing.Point(405, 381);
            this.rotateButton.Name = "rotateButton";
            this.rotateButton.Size = new System.Drawing.Size(75, 75);
            this.rotateButton.TabIndex = 7;
            this.rotateButton.Text = "Forgatás";
            this.rotateButton.UseVisualStyleBackColor = true;
            this.rotateButton.Click += new System.EventHandler(this.rotateButton_Click);
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 539);
            this.Controls.Add(this.rotateButton);
            this.Controls.Add(this.moveLeftButton);
            this.Controls.Add(this.moveDownButton);
            this.Controls.Add(this.moveRightButton);
            this.Controls.Add(this.moveUpButton);
            this.Controls.Add(this.shipSizeLabel);
            this.Controls.Add(this.enemyPanel);
            this.Controls.Add(this.ownPanel);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Torpedó";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel ownPanel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem egyjátékosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem többjátékosToolStripMenuItem;
        private System.Windows.Forms.Panel enemyPanel;
        private System.Windows.Forms.Label shipSizeLabel;
        private System.Windows.Forms.Button moveUpButton;
        private System.Windows.Forms.Button moveRightButton;
        private System.Windows.Forms.Button moveDownButton;
        private System.Windows.Forms.Button moveLeftButton;
        private System.Windows.Forms.Button rotateButton;
    }
}

