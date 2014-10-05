using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatchupWinRate
{
    public partial class Gui : Form
    {
        private Model model;
        private const int ENTER = 13; // char of "enter" key

        public Gui()
        {
            InitializeComponent();
            this.region.Text = "NA";
            this.champions.Visible = false;
            this.status.Visible = false;
            this.overallWin.Visible = false;
        }

        // Event: go is clicked. Gets data for summoner and region and populates
        //        champions. The alphabetically first champion is chosen and
        //        champions_TextChanged() is called.
        private void go_Click(object sender, EventArgs e)
        {
            overallWin.Text = String.Empty;
            overallWin.Refresh();

            status.Text = String.Empty;
            status.Visible = true;
            status.Refresh();

            champions.Items.Clear();
            champions.Text = String.Empty;
            champions.Refresh();

            data.DataSource = null;
            data.Refresh();

            String regionLower = region.Text.ToLower();
            model = new Model(regionLower);
            String summonerIdUrl = Coder.GetSummonerIdUrl(regionLower, summoner.Text);

            // invalid summoner name and or region
            if (model.reader.Request(summonerIdUrl).Equals(String.Empty))
            {
                System.Windows.Forms.MessageBox.Show("invalid summoner name and or region");
                return;
            }

            String summonerIdJson = model.reader.Request(summonerIdUrl);

            model.StorePersonalHistory(summoner.Text, status);
            model.StoreGlobalHistory(status);
            model.CalcChampionStats();

            foreach(int championId in model.championStats.Keys)
            {
                champions.Items.Add(model.championNames[championId]);
            }

            champions.SelectedIndex = 0;
            champions_SelectedIndexChanged(String.Empty, new EventArgs());
            champions.Visible = true;

            status.Text = "done with " + model.CountMatches() + " games";
            status.Refresh();
        }

        // Event: The selected index in champions is changed. Calculates the win
        //        rates for champions and displays it in data.
        private void champions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (champions.Text != String.Empty)
            {
                int championId = model.championIds[champions.Text];
                model.CalcWinRates(championId);
                DataView dataView = new DataView(model.winRates);
                data.DataSource = dataView;
                data.Columns[Model.WIN_RATE].DefaultCellStyle.Format = "0.#";
                data.Refresh();

                double personalChampionWinRate = model.CalcPersonalChampionWinRate(championId);
                int games = model.CountMatchesForChampion(championId);
                String gamePlurality = " games";

                if (games == 1)
                {
                    gamePlurality = " game";
                }

                overallWin.Text = "win rate: " + personalChampionWinRate.ToString("0.#") + "% in " + games + gamePlurality;
                overallWin.Visible = true;
                overallWin.Refresh();
            }
        }

        // Event: A key press in summoner. If "enter" was pressed, calls
        //        go_Click().
        private void summoner_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) ENTER)
            {
                go_Click(String.Empty, new EventArgs());
            }
        }
    }
}
