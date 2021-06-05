using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenbrugerApp
{
    public class Repository
    {
        public SqlConnection connection;
        public Repository()
        {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringDelta"].ConnectionString);
        }

        public void Add(string Mængde, int Måleenhed, int Kategori, string Beskrivelse, string Ansvarlig, string CVR)
        {
            try
            {
                string cmd = string.Format("INSERT INTO Skrald (Mængde, Måleenhed, Kategori, Beskrivelse, Ansvarlig, CVR, Tid) " +
                    "VALUES('{0}', '{1}' ,'{2}', '{3}', '{4}', '{5}', format(getdate(), 'yyyy-MM-dd hh:mm'))",
                    Mængde.Trim(), Måleenhed, Kategori, Beskrivelse.Trim(),
                    Ansvarlig.Trim(), CVR.Trim());

                SqlCommand command = new SqlCommand(cmd, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open) connection.Close();
            }
        }

        //public void Edit(string Mængde, int Måleenhed, int Kategori, string Beskrivelse, string Ansvarlig, string CVR, string Tid, string SkraldeID)
        //{
        //    try
        //    {
        //        string cmd = string.Format("UPDATE Skrald SET Mængde = '{0}', Måleenhed = '{1}', Kategori = '{2}', Beskrivelse = '{3}', Ansvarlig = '{4}', " +
        //            "CVR = '{5}', Tid = '{6}' WHERE SkraldeID = '{7}'",
        //            Mængde.Trim(), Måleenhed, Kategori, Beskrivelse.Trim(), Ansvarlig.Trim(),
        //            CVR.Trim(), Tid.Trim(), SkraldeID);
        //        SqlCommand command = new SqlCommand(cmd, connection);
        //        connection.Open();
        //        SqlDataReader reader = command.ExecuteReader();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    finally
        //    {
        //        if (connection != null && connection.State == ConnectionState.Open) connection.Close();
        //    }
        //}
    }
}
