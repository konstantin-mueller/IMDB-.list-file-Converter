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

        public static void WriteSqlFile(IEnumerable<string> lines, string sqlFilePath) {
            File.WriteAllLines(sqlFilePath + "\\" + DateTime.Now.ToString().Replace(":", ".") + ".sql", GetSqlFrom(lines));
        }

        private static IEnumerable<string> GetSqlFrom(IEnumerable<string> lines) {
            List<string> sql = new List<string>();
            Regex regexName = new Regex("\"[^\"]+\""),
                regexYear = new Regex("\\d+$"),
                regexEpisodeName = new Regex("{[^(]+"),
                regexSeason = new Regex("\\(#[^\\.]+"),
                regexEpisode = new Regex("\\.\\d+\\)}"),
                regexMovieName = new Regex("^[^(]+");

            int i = 0;
            foreach (string line in lines) {
                if(line.StartsWith("\"")) {
                    sql[i-1] = sql[i-1].Substring(0, sql[i-1].Length - 3) + "1);";
                    sql.Add("insert into imdb (Name, Year, EpisodeName, Season, Episode, IsSeries) values ('" +
                            regexName.Match(line).Value.Replace("\"", "") + "', " +
                            regexYear.Match(line).Value + ", '" +
                            regexEpisodeName.Match(line).Value.Replace("{", "").Trim() + "', " +
                            regexSeason.Match(line).Value.Replace("(#", "") + ", " +
                            regexEpisode.Match(line).Value.Replace(")}", "").Replace(".", "") + ", 1);");
                } else {
                    if(i < 16) continue;
                    sql.Add("insert into imdb (Name, Year, IsSeries) values ('" +
                            regexMovieName.Match(line).Value.Trim() + "', " +
                            regexYear.Match(line).Value + ", 0);");
                }
                i++;
            }
            return sql;
        }
    }
}
