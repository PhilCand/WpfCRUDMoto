using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCRUDMoto
{
    class DAL
    {

        public static List<Personne> GetPersons()
        {
            List<Personne> Personnes = new List<Personne>();

            string sqlQuery = String.Format("SELECT * FROM personnes");

            SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\PHIL\WpfCRUDMoto\WpfCRUDMoto\DBMoto.mdf; Integrated Security = True");
            connection.Open();


            SqlCommand command = new SqlCommand(sqlQuery, connection);

            SqlDataReader dataReader = command.ExecuteReader();

            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    Personnes.Add(new Personne(dataReader["nom"].ToString(), Convert.ToInt32(dataReader["id"])));
                }
            }

            return Personnes;

        }

        public static void DeletePerson(int PersonID)
        {
            //bool result = false;

            //Create the SQL Query for deleting an article
            string sqlQuery = String.Format($"delete from personnes where id = {PersonID}");

            //Create and open a connection to SQL Server 
            SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\PHIL\WpfCRUDMoto\WpfCRUDMoto\DBMoto.mdf; Integrated Security = True");
            connection.Open();

            //Create a Command object
            SqlCommand command = new SqlCommand(sqlQuery, connection);

            // Execute the command
            int rowsDeletedCount = command.ExecuteNonQuery();
            //if (rowsDeletedCount != 0)
            //    result = true;
            // Close and dispose

        }

        public static int CreatePerson(Personne newMotard)
        {

            //Create the SQL Query for inserting an article
            string sqlQuery = ($"Insert into personnes (nom) Values('{newMotard.Nom}');" + "Select @@Identity");


            //Create and open a connection to SQL Server 
            SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\PHIL\WpfCRUDMoto\WpfCRUDMoto\DBMoto.mdf; Integrated Security = True");
            connection.Open();

            //Create a Command object
            SqlCommand command = new SqlCommand(sqlQuery, connection);

            //Execute the command to SQL Server and return the newly created ID
            int newMotardID = Convert.ToInt32((decimal)command.ExecuteScalar());

            // Set return value
            return newMotardID;
        }

        public static List<Personne> GetPersonMoto()
        {
            List<Personne> Personnes = new List<Personne>();

            string sqlQuery = String.Format("SELECT p.id AS personneID, p.nom, m.id AS motoID, m.marque, m.cylindree FROM personnes p left join motos m ON p.id = m.personnes_id;");

            SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\PHIL\WpfCRUDMoto\WpfCRUDMoto\DBMoto.mdf; Integrated Security = True");
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

            return Personnes;

        }

        public static void CreateMoto(Moto newMoto)
        {

            //Create the SQL Query for inserting an article
            string sqlQuery = ($"Insert into motos (marque, cylindree, personnes_id) Values('{newMoto.Marque}', '{newMoto.Cylindree}', '{newMoto.IdProprietaire}');" + "Select @@Identity");


            //Create and open a connection to SQL Server 
            SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\PHIL\WpfCRUDMoto\WpfCRUDMoto\DBMoto.mdf; Integrated Security = True");
            connection.Open();

            //Create a Command object
            SqlCommand command = new SqlCommand(sqlQuery, connection);

            command.ExecuteNonQuery();

            ////Execute the command to SQL Server and return the newly created ID
            //int newMotoID = Convert.ToInt32((decimal)command.ExecuteScalar());

            //// Set return value
            //return newMotoID;
        }

        public static void DeleteMoto(int MotoID)
        {
            //bool result = false;

            //Create the SQL Query for deleting an article
            string sqlQuery = String.Format($"delete from motos where id = {MotoID}");

            //Create and open a connection to SQL Server 
            SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\PHIL\WpfCRUDMoto\WpfCRUDMoto\DBMoto.mdf; Integrated Security = True");
            connection.Open();

            //Create a Command object
            SqlCommand command = new SqlCommand(sqlQuery, connection);

            // Execute the command
            int rowsDeletedCount = command.ExecuteNonQuery();
            //if (rowsDeletedCount != 0)
            //    result = true;
            // Close and dispose

        }

        public static void UpdatePerson(Personne selectedPersonne)
        {

            //Create the SQL Query for updating an article
            string updateQuery = $"Update personnes SET nom = '{selectedPersonne.Nom}' Where id = '{selectedPersonne.Id}';";


            //Create and open a connection to SQL Server 
            SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\PHIL\WpfCRUDMoto\WpfCRUDMoto\DBMoto.mdf; Integrated Security = True");
            connection.Open();

            //Create a Command object
            SqlCommand command = new SqlCommand(updateQuery, connection);

            command.ExecuteNonQuery();
        }

        public static void UpdateMoto(Moto selectedMoto)
        {

            //Create the SQL Query for updating an article
            string updateQuery = $"Update motos SET marque = '{selectedMoto.Marque}', cylindree = '{selectedMoto.Cylindree}' Where id = '{selectedMoto.Id}';";


            //Create and open a connection to SQL Server 
            SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\PHIL\WpfCRUDMoto\WpfCRUDMoto\DBMoto.mdf; Integrated Security = True");
            connection.Open();

            //Create a Command object
            SqlCommand command = new SqlCommand(updateQuery, connection);

            command.ExecuteNonQuery();
        }
    }
}
