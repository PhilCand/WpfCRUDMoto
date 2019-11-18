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
        private static SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionTestString"].ConnectionString);

        static DAL()
        {
            connection.Open();
        }

        public static List<Personne> GetPersonMoto()
        {
            List<Personne> Personnes = new List<Personne>();
            string sqlQuery = String.Format("SELECT p.id AS personneID, p.nom, m.id AS motoID, m.marque, m.cylindree FROM personnes p left join motos m ON p.id = m.personnes_id;");
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
            dataReader.Close();
            command.Dispose();
            return Personnes;
        }

        #region MOTARD
        public static int CreatePerson(Personne newMotard)
        {
            string sqlQuery = ($"Insert into personnes (nom) Values('{newMotard.Nom}');" + "Select @@Identity");
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            int newMotardID = Convert.ToInt32((decimal)command.ExecuteScalar());
            command.Dispose();
            return newMotardID;
        }

        public static void UpdatePerson(Personne selectedPersonne)
        {
            string updateQuery = $"Update personnes SET nom = '{selectedPersonne.Nom}' Where id = '{selectedPersonne.Id}';";

            SqlCommand command = new SqlCommand(updateQuery, connection);
            command.ExecuteNonQuery();

            command.Dispose();
        }

        public static void DeletePerson(int PersonID)
        {
            string sqlQuery = String.Format($"delete from personnes where id = {PersonID}");

            SqlCommand command = new SqlCommand(sqlQuery, connection);
            int rowsDeletedCount = command.ExecuteNonQuery();

            command.Dispose();
        }
        #endregion

        #region MOTO
        public static void CreateMoto(Moto newMoto)
        {
            string sqlQuery = ($"Insert into motos (marque, cylindree, personnes_id) Values('{newMoto.Marque}', '{newMoto.Cylindree}', '{newMoto.IdProprietaire}');" + "Select @@Identity");

            SqlCommand command = new SqlCommand(sqlQuery, connection);
            command.ExecuteNonQuery();

            command.Dispose();
        }

        public static void UpdateMoto(Moto selectedMoto)
        {
            string updateQuery = $"Update motos SET marque = '{selectedMoto.Marque}', cylindree = '{selectedMoto.Cylindree}' Where id = '{selectedMoto.Id}';";

            SqlCommand command = new SqlCommand(updateQuery, connection);
            command.ExecuteNonQuery();

            command.Dispose();
        }

        public static void DeleteMoto(int MotoID)
        {
            string sqlQuery = String.Format($"delete from motos where id = {MotoID}");

            SqlCommand command = new SqlCommand(sqlQuery, connection);
            int rowsDeletedCount = command.ExecuteNonQuery();

            command.Dispose();
        }
        #endregion

        #region COTISATIONS

        public static List<Cotisation> GetCotisation(Personne motard)
        {
            List<Cotisation> Cotisations = new List<Cotisation>();
            string sqlQuery = ($"SELECT * FROM cotisations WHERE personne_id = {motard.Id} ORDER BY annee;");

            SqlCommand command = new SqlCommand(sqlQuery, connection);
            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    Cotisation newCotiz = new Cotisation();
                    newCotiz.Annee = Convert.ToInt32(dataReader["annee"]);
                    newCotiz.Montant = Convert.ToDouble(dataReader["montant"]);
                    newCotiz.MotardId = Convert.ToInt32(dataReader["personne_id"]);
                    newCotiz.Id = Convert.ToInt32(dataReader["id"]);
                    Cotisations.Add(newCotiz);
                }
            }
            dataReader.Close();
            command.Dispose();
            return Cotisations;
        }

        public static void CreateCotisation(Cotisation cotiz)
        {
            string sqlQuery = ($"Insert into cotisations (annee, montant, personne_id) Values('{cotiz.Annee}', '{cotiz.Montant}', '{cotiz.MotardId}');");

            SqlCommand command = new SqlCommand(sqlQuery, connection);
            command.ExecuteNonQuery();

            command.Dispose();
        }

        public static void DeleteCotisation(int CotisationID)
        {
            string sqlQuery = String.Format($"delete from cotisations where id = {CotisationID}");

            SqlCommand command = new SqlCommand(sqlQuery, connection);
            int rowsDeletedCount = command.ExecuteNonQuery();

            command.Dispose();
        }


        #endregion
    }
}
