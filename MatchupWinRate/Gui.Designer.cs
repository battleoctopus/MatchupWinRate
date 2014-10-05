namespace MatchupWinRate
{
    partial class Gui
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
            this.summoner = new System.Windows.Forms.TextBox();
            this.region = new System.Windows.Forms.ComboBox();
            this.go = new System.Windows.Forms.Button();
            this.status = new System.Windows.Forms.TextBox();
            this.overallWin = new System.Windows.Forms.TextBox();
            this.champions = new System.Windows.Forms.ComboBox();
            this.data = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.data)).BeginInit();
            this.SuspendLayout();
            // 
            // summoner
            // 
            this.summoner.Location = new System.Drawing.Point(13, 13);
            this.summoner.Name = "summoner";
            this.summoner.Size = new System.Drawing.Size(100, 20);
            this.summoner.TabIndex = 0;
            this.summoner.Text = "summoner";
            // 
            // region
            // 
            this.region.FormattingEnabled = true;
            this.region.Items.AddRange(new object[] {
            "BR",
            "EUNE",
            "EUW",
            "KR",
            "LAN",
            "LAS",
            "NA",
            "OCE",
            "RU",
            "TR"});
            this.region.Location = new System.Drawing.Point(119, 13);
            this.region.Name = "region";
            this.region.Size = new System.Drawing.Size(121, 21);
            this.region.TabIndex = 1;
            this.region.Text = "region";
            // 
            // go
            // 
            this.go.Location = new System.Drawing.Point(246, 11);
            this.go.Name = "go";
            this.go.Size = new System.Drawing.Size(75, 23);
            this.go.TabIndex = 2;
            this.go.Text = "go";
            this.go.UseVisualStyleBackColor = true;
            this.go.Click += new System.EventHandler(this.go_Click);
            // 
            // status
            // 
            this.status.Location = new System.Drawing.Point(327, 13);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(362, 20);
            this.status.TabIndex = 3;
            this.status.Text = "status";
            // 
            // overallWin
            // 
            this.overallWin.Location = new System.Drawing.Point(13, 40);
            this.overallWin.Name = "overallWin";
            this.overallWin.Size = new System.Drawing.Size(227, 20);
            this.overallWin.TabIndex = 4;
            this.overallWin.Text = "overallWin";
            // 
            // champions
            // 
            this.champions.FormattingEnabled = true;
            this.champions.Location = new System.Drawing.Point(246, 40);
            this.champions.Name = "champions";
            this.champions.Size = new System.Drawing.Size(121, 21);
            this.champions.Sorted = true;
            this.champions.TabIndex = 5;
            this.champions.Text = "champions";
            this.champions.TextChanged += new System.EventHandler(this.champions_TextChanged);
            // 
            // data
            // 
            this.data.AllowUserToAddRows = false;
            this.data.AllowUserToDeleteRows = false;
            this.data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.data.Location = new System.Drawing.Point(13, 67);
            this.data.Name = "data";
            this.data.ReadOnly = true;
            this.data.Size = new System.Drawing.Size(676, 434);
            this.data.TabIndex = 6;
            // 
            // Gui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 513);
            this.Controls.Add(this.data);
            this.Controls.Add(this.champions);
            this.Controls.Add(this.overallWin);
            this.Controls.Add(this.status);
            this.Controls.Add(this.go);
            this.Controls.Add(this.region);
            this.Controls.Add(this.summoner);
            this.Name = "Gui";
            this.ShowIcon = false;
            this.Text = "MatchupWinRate";
            ((System.ComponentModel.ISupportInitialize)(this.data)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox summoner;
        private System.Windows.Forms.ComboBox region;
        private System.Windows.Forms.Button go;
        private System.Windows.Forms.TextBox status;
        private System.Windows.Forms.TextBox overallWin;
        private System.Windows.Forms.ComboBox champions;
        private System.Windows.Forms.DataGridView data;
    }
}

