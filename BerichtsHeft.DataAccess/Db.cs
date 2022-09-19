﻿using System.Data.SqlClient;

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

        public static void DeleteActivityTable() => ExecuteNonQuery("""
            DELETE FROM [dbo].[Activity]
                  WHERE <Search Conditions,,>          
            """);

        public static void UpdateActivityTable() => ExecuteNonQuery("""
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
            """);

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
    }
}