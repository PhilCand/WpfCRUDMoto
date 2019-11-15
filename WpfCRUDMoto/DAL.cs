using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCRUDMoto
{
    class DAL
    {
        private static SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);

        public static List<Personne> GetPersonMoto()
        {
            List<Personne> Personnes = new List<Personne>();
            string sqlQuery = String.Format("SELECT p.id AS personneID, p.nom, m.id AS motoID, m.marque, m.cylindree FROM personnes p left join motos m ON p.id = m.personnes_id;");
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader dataReader = command.ExecuteReader();
            Personne newMotard = new Personne();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    if (newMotard.Id != Convert.ToInt32(dataReader["personneID"]))
                    {
                        newMotard = new Personne();
                        newMotard.Nom = dataReader["nom"].ToString();
                        newMotard.Id = Convert.ToInt32(dataReader["personneId"]);
                        Personnes.Add(newMotard);
                    }

                    if (!(dataReader["motoID"] is System.DBNull))
                    {
                        Moto NewMoto = new Moto();
                        NewMoto.Id = Convert.ToInt32(dataReader["motoID"]);
                        NewMoto.Marque = dataReader["marque"].ToString();
                        NewMoto.Cylindree = Convert.ToInt32(dataReader["cylindree"]);
                        NewMoto.IdProprietaire = Convert.ToInt32(dataReader["personneID"]);
                        newMotard.Garage.Add(NewMoto);
                    }
                }
            }
            connection.Close();
            return Personnes;
        }

        #region MOTARD
        public static int CreatePerson(Personne newMotard)
        {
            string sqlQuery = ($"Insert into personnes (nom) Values('{newMotard.Nom}');" + "Select @@Identity");
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            int newMotardID = Convert.ToInt32((decimal)command.ExecuteScalar());
            connection.Close();
            return newMotardID;
        }

        public static void UpdatePerson(Personne selectedPersonne)
        {
            string updateQuery = $"Update personnes SET nom = '{selectedPersonne.Nom}' Where id = '{selectedPersonne.Id}';";
            connection.Open();
            SqlCommand command = new SqlCommand(updateQuery, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static void DeletePerson(int PersonID)
        {
            string sqlQuery = String.Format($"delete from personnes where id = {PersonID}");
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            int rowsDeletedCount = command.ExecuteNonQuery();
            connection.Close();
        }
        #endregion

        #region MOTO
        public static void CreateMoto(Moto newMoto)
        {
            string sqlQuery = ($"Insert into motos (marque, cylindree, personnes_id) Values('{newMoto.Marque}', '{newMoto.Cylindree}', '{newMoto.IdProprietaire}');" + "Select @@Identity");
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static void UpdateMoto(Moto selectedMoto)
        {
            string updateQuery = $"Update motos SET marque = '{selectedMoto.Marque}', cylindree = '{selectedMoto.Cylindree}' Where id = '{selectedMoto.Id}';";
            connection.Open();
            SqlCommand command = new SqlCommand(updateQuery, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static void DeleteMoto(int MotoID)
        {
            string sqlQuery = String.Format($"delete from motos where id = {MotoID}");
            connection.Open();
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            int rowsDeletedCount = command.ExecuteNonQuery();
            connection.Close();
        }
        #endregion
    }
}
