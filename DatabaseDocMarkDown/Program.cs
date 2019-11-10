using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Dapper;
using System.Data.SQLite;
using CommonMark;
//using HeyRed.MarkdownSharp;
using Markdig;
using Markdig.Extensions.Tables;
using System.Text.Encodings.Web;
using System.Net;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace DatabaseDocMarkDown
{
    class Program
    {
        static List<KeyValuePair<string, string>> ProcessMark(string markContent)
        {
            var pipeline = new MarkdownPipelineBuilder().UsePipeTables().Build();
            string df = Regex.Replace(markContent, @"(?<=-)[\s]+", "")
                .Replace("\t", "")
                .Replace("|", "| ");
            var matchs = Regex.Matches(df, @"(?<=|)[\u4e00-\u9fa5\w]+");
            foreach(var match in matchs)
            {
                df=df.Replace(match.ToString(), " " + match.ToString() + " ");
            }

            string std = Markdown.ToHtml(df, pipeline).Replace("\n", "");
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(std);
            HtmlNodeCollection htmlNodes = doc.DocumentNode.SelectNodes("//table/tbody/*");
            if (htmlNodes != null)
            {
                List<KeyValuePair<string, string>> keyValuePairs = new List<KeyValuePair<string, string>>();
                foreach (var item in htmlNodes)
                {
                    var keyValuePair = KeyValuePair.Create(item.FirstChild.InnerText.Replace(" ",""), item.LastChild.InnerText.Replace(" ", ""));
                    keyValuePairs.Add(keyValuePair);
                }
                return keyValuePairs;
            }
            return null;
        }
        static void Main(string[] args)
        {
            using (IDbConnection _conn = new SQLiteConnection(@"data source=C:\Users\PeterH\Desktop\DatabaseDoc\showdoc.db"))
            {
                var result = _conn.Query("select * from page where item_id=17 --and page_id=2041");
                IDictionary<string, List<KeyValuePair<string, string>>> nodes = new Dictionary<string, List<KeyValuePair<string, string>>>();
                foreach (var dataitem in result)
                {
                    var items = ProcessMark(dataitem.page_content.ToString());
                    if (nodes.ContainsKey(dataitem.page_title.ToString()))
                    {
                        continue;
                    }
                    nodes.Add(dataitem.page_title.ToString(), items);
                }

                foreach(var item in nodes)
                {
                    Console.WriteLine($"{item.Key}");
                    if (item.Value == null)
                    {
                        continue;
                    }
                    foreach (var f in item.Value)
                    {
                        
                        Console.WriteLine($"{f.Key} {f.Value}");
                        Console.WriteLine("-------------------------");
                    }
                }
                //Markdown markdown = new Markdown();
                //string str=markdown.Transform(result.page_content.ToString());

                
                //var items = result.page_content.ToString().Trim().Split('\n');
                //foreach (var item in items)
                //{
                //  foreach (var f in item.Split('|'))
                //  {
                //    Console.Write($"{f}  ");
                //  }
                //  Console.WriteLine("\n");
                //  //Console.WriteLine("--------------------------------------");
                //}
                //Console.WriteLine($"内容：{}");
            }
            /*string pathBase = AppDomain.CurrentDomain.BaseDirectory;

            string mdPath = pathBase;
            File.Delete(mdPath + "test.md");
            DataTable tables = ExecuteDataTable("select * from INFORMATION_SCHEMA.TABLES");
            foreach (DataRow tableName in tables.Rows)
            {
                string tablename = (string)tableName["TABLE_NAME"];
                //if (tablename.StartsWith("WX_"))
                //{
                List<string> Rows = new List<string>();
                Rows.Add("### " + tablename + "(*快来修改我*)");
                Rows.Add("");
                Rows.Add("| 字段                       | 数据类型           |   小数点  | PK | NULL  | DEFAULT      | 描述                                                                               |");
                Rows.Add("|----------------------------|--------------------|-------|----|-------|--------------|------------------------------------------------------------------------------------|");
                DataTable tableColumns = ExecuteDataTable("select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME=@tablename",
            new SqlParameter("tablename", tablename));
                foreach (DataRow column in tableColumns.Rows)
                {
                    string column_name = (string)column["COLUMN_NAME"];
                    string data_type = (string)column["DATA_TYPE"];
                    string decimaldot = (string)column["NUMERIC_SCALE"].ToString();
                    string character_maximum_length = column["CHARACTER_MAXIMUM_LENGTH"].ToString();
                    string column_default = column["COLUMN_DEFAULT"].ToString();
                    string is_nullable = column["IS_NULLABLE"].ToString();

                    MDTableRow mdtablerow = new MDTableRow();
                    mdtablerow.column_name = column_name;
                    mdtablerow.data_type = data_type;
                    mdtablerow.character_maximum_length = character_maximum_length;
                    mdtablerow.column_default = column_default;
                    mdtablerow.is_nullable = is_nullable;
                    mdtablerow.numeric_scale = decimaldot;
                    Rows.Add(WriteTableRow(mdtablerow));
                }
                //File.Delete(mdPath + "test.md");
                File.AppendAllLines(mdPath + string.Format("{0}.md", tableName["TABLE_CATALOG"]), Rows);
                Console.WriteLine(tablename + " 生成完成");
                //}
            }
            Console.WriteLine();
            Console.WriteLine("生成结束！");
            Console.ReadKey();*/
        }

        public static string WriteTableRow(MDTableRow mdtablerow)
        {
            string datatype = mdtablerow.character_maximum_length == "" || mdtablerow.character_maximum_length == "2147483647"
                ? "`" + mdtablerow.data_type.ToUpper() + "`" : "`" + mdtablerow.data_type.ToUpper() + "(" + mdtablerow.character_maximum_length + ")`";

            string data_tp = "`" + System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(SqlDataTypeToNetDataType(mdtablerow.data_type)) + "`";

            string str = string.Format("|{0}|{1}|{2}|{3}|{4}|{5}|{6}|"
                , Pad("", "----------------".Length, false)
                , Pad(mdtablerow.column_name, "----------------------------".Length)
                , Pad(mdtablerow.character_maximum_length == "-1" ? "`" + mdtablerow.data_type.ToUpper() + "(MAX)`" : datatype, "--------------------".Length)
                , Pad(mdtablerow.numeric_scale, "---------------------".Length)
                , Pad(mdtablerow.column_name == "ID" ? "PK" : "", "------".Length)
                , Pad(mdtablerow.is_nullable == "NO" ? "" : "NULL", "-------".Length)
                , Pad(mdtablerow.column_default, "--------------".Length)
                , Pad("", "------------------------------------------------------------------------------------".Length)
                );
            return str;
        }

        public static string Pad(string basestr, int padlength, bool isCN = false)
        {
            int baselength = (int)(basestr.Length * (isCN ? 2 : 1));
            int length = padlength - baselength;
            return basestr.PadLeft(2).PadRight(padlength - (isCN ? basestr.Length : 0));
        }


        public static DataTable ExecuteDataTable(string cmdText, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(@"server=192.168.30.7;database=HeadHunterQ1;uid=headhunter;pwd=headhunterdev;"))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = cmdText;
                    cmd.Parameters.AddRange(parameters);
                    DataTable dt = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        private static string SqlDataTypeToNetDataType(string datatype)
        {
            switch (datatype)
            {
                case "int":
                    return "int";

                case "nvarchar":
                case "varchar":
                case "nchar":
                case "char":
                    return "string";

                case "bit":
                    return "bool";

                case "datetime":
                case "datetime2":
                    return "DateTime";

                case "decimal":
                    return "decimal";

                default:
                    return "object";

            }
        }

    }
    class MDTableRow
    {
        public string column_name { get; set; }
        public string data_type { get; set; }
        public string character_maximum_length { get; set; }
        public string column_default { get; set; }
        public string numeric_scale { get; set; }
        public string is_nullable { get; set; }
    }
}
