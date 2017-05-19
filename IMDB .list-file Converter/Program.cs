using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace IMDB.list_file_Converter
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static void WriteSqlFile(string databaseName, IEnumerable<string> lines, string sqlFilePath) {
            File.WriteAllLines(sqlFilePath + "\\" + DateTime.Now.ToString().Replace(":", ".") + ".sql", GetSqlFrom(lines, databaseName));
        }

        private static IEnumerable<string> GetSqlFrom(IEnumerable<string> lines, string databaseName) {
            List<string> sql = new List<string>();
            Regex regexName = new Regex("\"[^\"]+\""),
                regexYear = new Regex("\\d+$"),
                regexEpisodeName = new Regex("{[^(]+"),
                regexSeason = new Regex("\\(#[^\\.]+"),
                regexEpisode = new Regex("\\.\\d+\\)}"),
                regexMovieName = new Regex("^[^(]+");

            int i = 0;
            foreach (string line in lines) {
                if(line.StartsWith('"'.ToString()) && regexSeason.Match(line).Value != string.Empty) {
                    string year = regexYear.Match(line).Value;
                    sql.Add("insert into " + databaseName + " (Name, " + (year == string.Empty ? "" : "Year, ") + "EpisodeName, Season, Episode, IsSeries) values ('" +
                            regexName.Match(line).Value.Replace("\"", "").Replace("'", "´") + "', " +
                            (year == string.Empty ? "'" : year + ", '") +
                            regexEpisodeName.Match(line).Value.Replace("{", "").Trim().Replace("'", "´") + "', " +
                            regexSeason.Match(line).Value.Replace("(#", "") + ", " +
                            regexEpisode.Match(line).Value.Replace(")}", "").Replace(".", "") + ", 1);");
                } else {
                    if(i++ < 15) continue;
                    string year = regexYear.Match(line).Value;
                    sql.Add("insert into " + databaseName + " (Name, " + (year == string.Empty ? "" : "Year ,") + "IsSeries) values ('" +
                            regexMovieName.Match(line).Value.Trim().Replace("'", "´") +
                            (year == string.Empty ? "'" : "', " + year) + ", " +
                            (line.StartsWith('"'.ToString()) ? 1 : 0) + ");");
                }
            }
            return sql;
        }
    }
}
