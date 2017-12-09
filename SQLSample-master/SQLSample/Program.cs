using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Globalization;

namespace SQLSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

            try
            {
                byte[] data;
                string pathFile = @"./Data/MOCK_DATA.csv";
                data = File.Exists(pathFile) ? File.ReadAllBytes(pathFile) : null;

                //
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["Test"].ConnectionString;
                con.Open();

                //
                SqlCommand commandInsert = new SqlCommand();
                commandInsert.Connection = con;

                string sqlInsert = String.Format("INSERT INTO {0}({1}) VALUES(@fileData)",
                                    "FileSample", "FileData");
                commandInsert.CommandText = sqlInsert;
                commandInsert.Parameters.Add(new SqlParameter("@fileData", data));
                commandInsert.ExecuteNonQuery();


                //
                SqlCommand commandSelect= new SqlCommand();
                commandSelect.Connection = con;

                string sqlSelect = String.Format("select myfile = convert(varchar(max), {0})  from {1} where {2} = @fileId",
                    "FileData", "FileSample", "FileId");
                commandSelect.CommandText = sqlSelect;
                commandSelect.Parameters.Add(new SqlParameter("@fileId", 1));
                var result = commandSelect.ExecuteReader();

                if(result.HasRows)
                {
                    StringBuilder builder = new StringBuilder();
                    while(result.Read())
                    {
                        builder.Append(result.GetString(0));
                    }
                    string output = @"./Data/Result.csv";
                    File.WriteAllText(output, builder.ToString());
                }

                //
                con.Close();

                //
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
                Console.ReadKey();
            }
        }
    }
}
