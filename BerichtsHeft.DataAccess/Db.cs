using System;
using System.Data;
using System.Data.SqlClient;

namespace BerichtsHeft.DataAccess
{
    public class Db
    {
        public static string SayHello() => ExecuteScalar<string>("select 'hello world'");

        private static T Execute<T>(Func<SqlCommand, T> func, string sql)
        {
            SqlConnection db = new(ConnectionString.Value);
            db.Open();
            SqlCommand cmd = db.CreateCommand();
            cmd.CommandText = sql;
            var result = func(cmd);
            db.Close();
            return result;
        }

        public static int ExecuteNonQuery(string sql) => Execute(cmd => cmd.ExecuteNonQuery(), sql);

        public static T ExecuteScalar<T>(string sql) => Execute(cmd => (T)cmd.ExecuteScalar(), sql);
        public static void CreateActivityTable() => ExecuteNonQuery("""
            CREATE TABLE [Activity]
            (
                [ID] [varchar](40) NOT NULL,
                [HauptText] [nvarchar](255) NULL,
                [WochenTag] [nvarchar](10) NULL,
                [Name] [nvarchar](255) NULL,
                [Fach] [nvarchar](255) NULL,
                [AbgabeType] [nvarchar](255) NULL,
                [DateBlock] [int] NULL,
                [DauertMin] [int] NULL,
                [DateOfReport] [datetime] NULL
                
                CONSTRAINT [PK_Activity] PRIMARY KEY CLUSTERED 
                (
                	[ID] ASC
                )
            )
            """);

        public static void DeleteActivityTable(string id) => ExecuteNonQuery($"DELETE FROM [dbo].[Activity] WHERE ID='{id}'");

        public static void UpdateActivityTable(string id) => ExecuteNonQuery("""
            UPDATE [dbo].[Activity]
            SET [ID] = 1,
                [HauptText] = 2,
                [WochenTag] = 3,
                [Name] = 4,
                [Fach] = 5,
                [AbgabeType] = 6,
                [DateBlock] = 7,
                [Dauertmin] = 8,
                [DateOfReport] = 9,
            WHERE <Search Conditions,>
            """
            /*NavigationManager.NavigateTo($"activityform/{activityChange.ID}");*/);

        public static void InsertActivityTable(string id, string hauptText, string wochenTag, string name,
            string fach, string abgabeType, int dateBlock, int dauertMin, DateTime dateOfReport)
            => ExecuteNonQuery($"""

            INSERT INTO [dbo].[Activity]
            (
              [ID], [HauptText], [WochenTag], [Name],
              [Fach], [AbgabeType], [DateBlock], [Dauertmin], [DateOfReport]
            )
            VALUES
            (
              '{id}', '{hauptText}', '{wochenTag}', '{name}',
              '{fach}', '{abgabeType}', {dateBlock}, {dauertMin}, '{dateOfReport: yyyyMMdd}'
              -- google for Sql Parameters
            )
            """);

        public static void SelectActivityTable() => ExecuteNonQuery("""
            SELECT
              [ID], [HauptText], [WochenTag], [Name], [Fach],
              [AbgabeType], [DateBlock], [Dauertmin], [DateOfReport]
            FROM
              [dbo].[Activity]
            """);

        private const string SelectSql = @"SELECT
        [ID], [HauptText], [WochenTag], [Name], [Fach],
        [AbgabeType], [DateBlock], [Dauertmin], [DateOfReport]
        FROM
        [dbo].[Activity]";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fach"></param>
        /// <param name="hauptTextPattern">Sucht nach Activities, die diese Zeichenkette enthalten</param>
        /// <returns></returns>
        public static DataTable GetActivities(string fach = null, string HauptTextPattern = null)
        {
            //return Execute<DataTable>(GetActivitiesInternal, SelectSql);

            return Execute<DataTable>(new Func<SqlCommand, DataTable>((cmd) =>
            {
                return GetActivitiesInternal(cmd, fach, HauptTextPattern);
            }), SelectSql);
        }

        private static DataTable GetActivitiesInternal(SqlCommand sqlCmd, string fach = null, string hauptTextPattern = null)

        {
            sqlCmd.CommandText = SelectSql;

            //Fach
            fach = TrimText(fach);
            string fachSql = null;
            if (string.IsNullOrEmpty(fach) == false)
            {
                fachSql = "Fach = @Fach";
                sqlCmd.Parameters.Add("Fach", SqlDbType.VarChar, 255).Value = fach;
            }
            
            //Haupttext
            hauptTextPattern = TrimText(hauptTextPattern);
            string hauptTextSql = null;
            if (string.IsNullOrEmpty(hauptTextPattern) == false)
            {
                hauptTextPattern = $"%{hauptTextPattern}%";
                hauptTextSql = "HauptText LIKE @HauptTextPattern";
                sqlCmd.Parameters.Add("HauptTextPattern", SqlDbType.VarChar, 255).Value = hauptTextPattern;
            }

            string logic = "AND";
            string whereSql = "";
            if (fachSql != null)
            {
                whereSql = fachSql;
            }

            if (hauptTextSql != null)
            {
                if (whereSql == "")
                {
                    whereSql = hauptTextSql;
                }
                else
                {
                    whereSql = $"{whereSql} {logic} {hauptTextSql}";
                }
            }

            if (whereSql != "")
            {
                whereSql = $" WHERE {whereSql}";
            }

            sqlCmd.CommandText = SelectSql + whereSql;
            
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            DataTable t = new DataTable();
            adp.Fill(t);
            return t;
        }
        private static string TrimText(string textToTrim)
        {
            if (textToTrim != null)
            {
                return textToTrim.Trim();
            }
            return null;
        }
        public static void HauptTextC(string hauptTextPattern)
        {
            hauptTextPattern = "%" + hauptTextPattern + "%";
        }
        public static void SCommand(SqlCommand sqlCmd)
        {
            sqlCmd.CommandText = sqlCmd.CommandText;
        }
        public static void AddFach(SqlCommand sqlCmd, string fach)
        {
            sqlCmd.Parameters.Add("Fach", SqlDbType.VarChar, 255).Value = fach;
        }
        public static void AddHauptText(SqlCommand sqlCmd, string hauptTextPattern)
        {
            sqlCmd.Parameters.Add("HauptTextPattern", SqlDbType.VarChar, 255).Value = hauptTextPattern;
        }
        
        public static int GetActivitiyCount()
        {
            return Execute<int>(GetActivityCountInternal, "SELECT COUNT(*) FROM Activity");
        }

        private static int GetActivityCountInternal(SqlCommand sqlCmd)
        {
            return (int)sqlCmd.ExecuteScalar();
        }
    }
}