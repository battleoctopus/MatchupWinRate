using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatchHistoryNameSpace;
using MatchInfoNameSpace;
using System.Data;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Collections.Concurrent;
using System.Threading;

namespace MatchupWinRate
{
    // Organizes data into dictionary with champion IDs as keys and dictionaries
    // with champion IDs as keys and tallies as values as values. Creates a
    // personal history dictionary with match IDs as keys and personal match
    // statistics as values. Creates a global history dictionary with match IDs
    // as keys and a list of champion IDs as values. Creates a champion stats
    // dictionary with champion IDs as keys and dictionaries with champion IDs
    // as keys and dictionaries with outcomes as keys and tallies as values as
    // values as values. Creates a champion names dictionary with champion IDs
    // as keys and champion names as values.
    class Model
    {
        public Reader reader;
        private Dictionary<int, PersonalParticipant> personalHistory = new Dictionary<int, PersonalParticipant>();
        private ConcurrentDictionary<int, List<int>> globalHistory = new ConcurrentDictionary<int, List<int>>();
        public Dictionary<int, Dictionary<int, Dictionary<Stats, int>>> championStats = new Dictionary<int, Dictionary<int, Dictionary<Stats, int>>>();
        public Dictionary<int, String> championNames = new Dictionary<int, String>();
        public Dictionary<String, int> championIds = new Dictionary<String, int>();
        public DataTable winRates = new DataTable();
        private String region;
        private const int MATCH_SEARCH_LIMIT = 15; // Riot restricts the amount of match history that can be searched

        public const String WIN_RATE = "Win %";
        public const String GAMES = "Games";

        public Model(String region)
        {
            reader = new Reader();
            this.region = region;

            // populate championNames and championIds
            String champions = reader.Request(Coder.GetChampionNamesUrl());
            Regex regexId = new Regex("\"id\":(\\d+)");
            MatchCollection idMatches = regexId.Matches(champions);
            Regex regexName = new Regex("\"name\":\"([^\"]+)\"");
            MatchCollection nameMatches = regexName.Matches(champions);

            for (int i = 0; i < idMatches.Count; i++)
            {
                int id = Convert.ToInt32(idMatches[i].Groups[1].Value);
                String name = nameMatches[i].Groups[1].Value;
                championNames[id] = name;
                championIds[name] = id;
            }
        }

        // Stores personal history in a dictionary.
        public void StorePersonalHistory(String summonerName, TextBox status)
        {
            String summonerIdUrl = Coder.GetSummonerIdUrl(region, summonerName);
            String summonerIdJson = reader.Request(summonerIdUrl);
            String summonerId = Parser.GetSummonerId(summonerIdJson);

            int matchNumber = 0;

            // loops until there is no more match history
            while (true)
            {
                String matchHistoryUrl = Coder.GetMatchHistoryUrl(region, summonerId, matchNumber, matchNumber + MATCH_SEARCH_LIMIT);
                String matchHistoryJson = reader.Request(matchHistoryUrl);

                // there is no more match history
                if (matchHistoryJson.Equals("{}") | matchHistoryJson.Equals(String.Empty))
                {
                    break;
                }

                MatchHistory matchHistory = Parser.ParseMatchHistory(matchHistoryJson);

                status.Text = (matchNumber + matchHistory.matches.Count) + " games found";
                status.Refresh();

                foreach (MatchHistoryNameSpace.Match match in matchHistory.matches)
                {
                    MatchHistoryNameSpace.Participant participant = match.participants[0];
                    personalHistory[match.matchId] = new PersonalParticipant(match.participants[0].teamId, participant.stats.winner, participant.championId);
                }

                matchNumber += MATCH_SEARCH_LIMIT;
            }
        }

        // Stores global history in a dictionary.
        public void StoreGlobalHistory(TextBox status)
        {
            int matchCount = 1;

            Parallel.ForEach(personalHistory.Keys, matchId =>
            {
                if (!status.InvokeRequired)
                {
                    status.Text = "Found all games. Loading game data " + matchCount + "/" + personalHistory.Keys.Count + ".";
                    status.Refresh();
                }

                String matchInfoUrl = Coder.GetMatchInfoUrl(region, matchId);
                String matchInfoJson = reader.Request(matchInfoUrl);
                MatchInfo matchInfo = Parser.ParseMatchInfo(matchInfoJson);
                globalHistory[matchId] = new List<int>();

                foreach (MatchInfoNameSpace.Participant participant in matchInfo.participants)
                {
                    if (participant.teamId != personalHistory[matchId].teamId)
                    {
                        globalHistory[matchId].Add(participant.championId);
                    }
                }

                Interlocked.Increment(ref matchCount);
            });
        }

        // Tallies the wins and games played in a dictionary.
        private void AddTally(int allyChampionId, int enemyChampionId, Stats stat)
        {
            // add ally champion to dictionary if not already contained
            if (!championStats.Keys.Contains(allyChampionId))
            {
                championStats[allyChampionId] = new Dictionary<int, Dictionary<Stats, int>>();
            }

            // add enemy champion to dictionary if not already contained
            if (!championStats[allyChampionId].Keys.Contains(enemyChampionId))
            {
                championStats[allyChampionId][enemyChampionId] = new Dictionary<Stats, int>();

                // initialize all wins and games statistics played to zero
                foreach(Stats statEnum in (Stats[])Enum.GetValues(typeof(Stats)))
                {
                    championStats[allyChampionId][enemyChampionId][statEnum] = 0;
                }
            }

            championStats[allyChampionId][enemyChampionId][stat] += 1;
        }

        // Calculates champion statistics from personal and global history
        // dictionaries.
        public void CalcChampionStats()
        {
            foreach (int matchId in personalHistory.Keys)
            {
                PersonalParticipant personalParticipant = personalHistory[matchId];

                foreach (int enemyChampionId in globalHistory[matchId])
                {
                    AddTally(personalParticipant.championId, enemyChampionId, Stats.Games);

                    if (personalParticipant.isWin)
                    {
                        AddTally(personalParticipant.championId, enemyChampionId, Stats.Wins);
                    }
                }
            }
        }

        // Calculates win rates for each champion from champion statistics
        // dictionary.
        public void CalcWinRates(int allyChampionId)
        {
            winRates = new DataTable();
            winRates.Columns.Add("Champion", typeof(String));
            winRates.Columns.Add(WIN_RATE, typeof(double));
            winRates.Columns.Add(GAMES, typeof(int));

            foreach (DataColumn dataColumn in winRates.Columns)
            {
                dataColumn.ReadOnly = true;
            }

            foreach (int enemyChampionId in championStats[allyChampionId].Keys)
            {
                Dictionary<Stats, int> stats = championStats[allyChampionId][enemyChampionId];
                double winRate = 100d * stats[Stats.Wins] / stats[Stats.Games];
                double games = stats[Stats.Games];

                winRates.Rows.Add(championNames[enemyChampionId], winRate, games);
            }
        }

        // Calculates personal win rate from personal history dictionary.
        public double CalcPersonalWinRate()
        {
            int wins = 0;
            int games = 0;

            foreach (int matchId in personalHistory.Keys)
            {
                games += 1;
                PersonalParticipant personalParticipant = personalHistory[matchId];

                if (personalParticipant.isWin)
                {
                    wins += 1;
                }
            }

            return 100d * wins / games;
        }

        public int CountMatches()
        {
            return personalHistory.Keys.Count;
        }

        // Calculates personal win rate for a champion.
        public double CalcPersonalChampionWinRate(int championId)
        {
            int games = 0;
            int wins = 0;

            foreach(int matchId in personalHistory.Keys)
            {
                if (personalHistory[matchId].championId == championId)
                {
                    games += 1;

                    if (personalHistory[matchId].isWin)
                    {
                        wins += 1;
                    }
                }
            }

            return 100d * wins / games;
        }

        // Counts number of matches for a champion.
        public int CountMatchesForChampion(int championId)
        {
            int games = 0;

            foreach(int matchId in personalHistory.Keys)
            {
                if (personalHistory[matchId].championId == championId)
                {
                    games += 1;
                }
            }

            return games;
        }
    }

    public struct PersonalParticipant
    {
        public int teamId;
        public bool isWin;
        public int championId;

        public PersonalParticipant(int teamId, bool isWin, int championId)
        {
            this.teamId = teamId;
            this.isWin = isWin;
            this.championId = championId;
        }
    }

    // statistics for wins and games played
    enum Stats
    {
        Wins, Games
    }
}
