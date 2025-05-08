using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.ReportGenerators
{
    public class ReportGenerator : IReportGenerator
    {
        public DataTable GetReport(IEnumerable<Voter> voters)
        {
            DataTable dataTable = new DataTable();

            var groups = voters.GroupBy(x => x.Party,
                (partyName, votersOfParty) => new
                {
                    PartyName = partyName,
                    VotersCount = votersOfParty.Count(),
                    AverageAge = votersOfParty.Average(x => x.Age),
                    FrequentProfessionName = GetMostFrequentProfession(votersOfParty)
                });
            dataTable.TableName = "Voters Statistics";

            dataTable.Columns.Add("Number", typeof(int));
            dataTable.Columns.Add("Party", typeof(string));
            dataTable.Columns.Add("Amount of Voters", typeof(int));
            dataTable.Columns.Add("Voters Average Age", typeof(int));
            dataTable.Columns.Add("Most frequent Voter's Profession", typeof(string));

            int i = 1;
            foreach (var group in groups)
            {
                dataTable.Rows.Add(i, group.PartyName, group.VotersCount, 
                    group.AverageAge, group.FrequentProfessionName);
                i++;
            }

            return dataTable;
        }

        private static string GetMostFrequentProfession(IEnumerable<Voter> voters)
        {
            Dictionary<string,int> pairs = new Dictionary<string,int>();

            var grouped = voters.GroupBy(x => x.ProfessionName);

            foreach (IGrouping<string, Voter> group in grouped)
            {
                pairs.Add(group.Key, group.Count());
            }

            int maxValue = pairs.Max(x => x.Value);

            return pairs.Where(x => x.Value == maxValue).Select(x => x.Key).FirstOrDefault();
        }
    }
}
