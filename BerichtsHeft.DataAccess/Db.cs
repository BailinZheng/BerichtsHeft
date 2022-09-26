using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

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

        public static bool DeleteActivityTable(string id)
        {
            return Execute<bool>((cmd) =>
            {
                cmd.Parameters.Add("ID", SqlDbType.VarChar, 40).Value = id;
                int affectedResults = cmd.ExecuteNonQuery();

                return (affectedResults == 1);

            }, "DELETE FROM [Activity] WHERE ID=@ID");
        }

        public static bool UpdateActivity(BerichtsHeft.Shared.Activity a)
        {
            return Execute<bool>((cmd) =>
            {
                cmd.Parameters.Add("HauptText", SqlDbType.NVarChar, 255).Value = a.HauptText;
                cmd.Parameters.Add("WochenTag", SqlDbType.NVarChar, 10).Value = a.WochenTag;
                cmd.Parameters.Add("Name", SqlDbType.NVarChar, 255).Value = a.Name;
                cmd.Parameters.Add("Fach", SqlDbType.NVarChar, 255).Value = a.Fach;
                cmd.Parameters.Add("AbgabeType", SqlDbType.NVarChar, 255).Value = a.AbgabeType;
                cmd.Parameters.Add("DateBlock", SqlDbType.Int).Value = a.DateBlock;
                cmd.Parameters.Add("Dauertmin", SqlDbType.Int).Value = a.DauertMin;
                cmd.Parameters.Add("DateOfReport", SqlDbType.DateTime).Value = a.DateOfReport;
                cmd.Parameters.Add("ID", SqlDbType.VarChar, 40).Value = a.ID;

                int affectedResults = cmd.ExecuteNonQuery();
                return (affectedResults == 1);
            }, @"UPDATE [dbo].[Activity]  
            SET [HauptText] = @HauptText, 
            [WochenTag] = @WochenTag, 
            [Name] = @Name,
            [Fach] = @Fach,
            [AbgabeType] = @AbgabeType,
            [DateBlock] = @DateBlock,
            [Dauertmin] = @Dauertmin,
            [DateOfReport] = @DateOfReport
            WHERE [ID] = @ID
            ");
        }

        public static bool InsertActivity(BerichtsHeft.Shared.Activity a)
        {
            return Execute<bool>((cmd) =>
            {
                cmd.Parameters.Add("HauptText", SqlDbType.NVarChar, 255).Value = a.HauptText;
                cmd.Parameters.Add("WochenTag", SqlDbType.NVarChar, 10).Value = a.WochenTag;
                cmd.Parameters.Add("Name", SqlDbType.NVarChar, 255).Value = a.Name;
                cmd.Parameters.Add("Fach", SqlDbType.NVarChar, 255).Value = a.Fach;
                cmd.Parameters.Add("AbgabeType", SqlDbType.NVarChar, 255).Value = a.AbgabeType;
                cmd.Parameters.Add("DateBlock", SqlDbType.Int).Value = a.DateBlock;
                cmd.Parameters.Add("Dauertmin", SqlDbType.Int).Value = a.DauertMin;
                cmd.Parameters.Add("DateOfReport", SqlDbType.DateTime).Value = a.DateOfReport;
                cmd.Parameters.Add("ID", SqlDbType.VarChar, 40).Value = a.ID;
                //cmd.Parameters.Add("ID", SqlDbType.VarChar, 40).Value = id;
                int affectedResults = cmd.ExecuteNonQuery();
                return (affectedResults == 1);
            }, @"INSERT INTO [dbo].[Activity]
(
  [ID], [HauptText], [WochenTag], [Name],
  [Fach], [AbgabeType], [DateBlock], [Dauertmin], [DateOfReport]
)
VALUES
(
  @ID, @HauptText, @WochenTag, @Name,
  @Fach , @AbgabeType , @DateBlock, @Dauertmin, @DateOfReport
)
");
        }



        public static BerichtsHeft.Shared.Activity GetActivity(string id)
        {
            return Execute<BerichtsHeft.Shared.Activity>((cmd) =>
            {
                BerichtsHeft.Shared.Activity a = new BerichtsHeft.Shared.Activity();

                cmd.Parameters.Add("ID", SqlDbType.VarChar, 40).Value = id;

                using (IDataReader r = cmd.ExecuteReader())
                {
                    if (r.Read())
                    {
                        a.ID = r.GetString(0);
                        a.HauptText = r.GetString(1);
                        a.WochenTag = r.GetString(2);
                        a.Name = r.GetString(3);
                        a.Fach = r.GetString(4);
                        a.AbgabeType = r.GetString(5);
                        a.DateBlock = r.GetInt32(6);
                        a.DauertMin = r.GetInt32(7);
                        a.DateOfReport = r.GetDateTime(8);
                    }
                    else
                    {
                        throw new Exception($"Datensatz mit ID='{id}' existiert nicht!");
                    }
                }

                return a;
            }, @"SELECT [ID]
      ,[HauptText]
      ,[WochenTag]
      ,[Name]
      ,[Fach]
      ,[AbgabeType]
      ,[DateBlock]
      ,[DauertMin]
      ,[DateOfReport]
  FROM [dbo].[Activity]
WHERE ID=@ID");
        }

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
        public static DataTable GetActivities(string fach = null, string HauptTextPattern = null, string andOrOr = "and")
        {
            //return Execute<DataTable>(GetActivitiesInternal, SelectSql);

            return Execute<DataTable>(new Func<SqlCommand, DataTable>((cmd) =>
            {
                return GetActivitiesInternal(cmd, fach, HauptTextPattern, andOrOr);
            }), SelectSql);
        }

        private static DataTable GetActivitiesInternal(SqlCommand sqlCmd, string fach = null, string hauptTextPattern = null, string andOrOr = "and")

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
            if (andOrOr == "or")
            {
                logic = "OR";
            }
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