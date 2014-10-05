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
            this.summoner.Location = new System.Drawing.Point(12, 12);
            this.summoner.MaxLength = 16;
            this.summoner.Name = "summoner";
            this.summoner.Size = new System.Drawing.Size(185, 20);
            this.summoner.TabIndex = 0;
            this.summoner.Text = "summoner";
            this.summoner.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.summoner_KeyPress);
            // 
            // region
            // 
            this.region.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            this.region.Location = new System.Drawing.Point(203, 12);
            this.region.Name = "region";
            this.region.Size = new System.Drawing.Size(61, 21);
            this.region.TabIndex = 1;
            // 
            // go
            // 
            this.go.Location = new System.Drawing.Point(270, 10);
            this.go.Name = "go";
            this.go.Size = new System.Drawing.Size(27, 23);
            this.go.TabIndex = 2;
            this.go.Text = "go";
            this.go.UseVisualStyleBackColor = true;
            this.go.Click += new System.EventHandler(this.go_Click);
            // 
            // status
            // 
            this.status.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.status.Location = new System.Drawing.Point(12, 39);
            this.status.Name = "status";
            this.status.ReadOnly = true;
            this.status.Size = new System.Drawing.Size(238, 13);
            this.status.TabIndex = 0;
            this.status.TabStop = false;
            // 
            // overallWin
            // 
            this.overallWin.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.overallWin.Location = new System.Drawing.Point(203, 61);
            this.overallWin.Name = "overallWin";
            this.overallWin.ReadOnly = true;
            this.overallWin.Size = new System.Drawing.Size(167, 13);
            this.overallWin.TabIndex = 0;
            this.overallWin.TabStop = false;
            this.overallWin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // champions
            // 
            this.champions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.champions.FormattingEnabled = true;
            this.champions.Location = new System.Drawing.Point(12, 58);
            this.champions.Name = "champions";
            this.champions.Size = new System.Drawing.Size(108, 21);
            this.champions.Sorted = true;
            this.champions.TabIndex = 5;
            this.champions.SelectedIndexChanged += new System.EventHandler(this.champions_SelectedIndexChanged);
            // 
            // data
            // 
            this.data.AllowUserToAddRows = false;
            this.data.AllowUserToDeleteRows = false;
            this.data.AllowUserToResizeColumns = false;
            this.data.AllowUserToResizeRows = false;
            this.data.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.data.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.data.Location = new System.Drawing.Point(11, 85);
            this.data.MaximumSize = new System.Drawing.Size(358, 9999);
            this.data.MinimumSize = new System.Drawing.Size(358, 242);
            this.data.Name = "data";
            this.data.ReadOnly = true;
            this.data.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.data.Size = new System.Drawing.Size(358, 242);
            this.data.TabIndex = 0;
            this.data.TabStop = false;
            // 
            // Gui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 336);
            this.Controls.Add(this.data);
            this.Controls.Add(this.champions);
            this.Controls.Add(this.overallWin);
            this.Controls.Add(this.status);
            this.Controls.Add(this.go);
            this.Controls.Add(this.region);
            this.Controls.Add(this.summoner);
            this.MaximumSize = new System.Drawing.Size(396, 9999);
            this.MinimumSize = new System.Drawing.Size(396, 375);
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

