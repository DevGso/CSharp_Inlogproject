using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BCrypt.Net;
using System.Runtime.Remoting.Messaging;

namespace Inloggen
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            dataBase_Connection();
        }

        string connectionString = "server=localhost;database=InlogProject_Giel;uid=InlogProject;pwd=Gilde123!;port=3306;SslMode=None;";

        public class Gebruikers_data
        {
            public string email { get; set; } = "";
            public string voornaam { get; set; } = "";
            public string achternaam { get; set; } = "";
            public string klas { get; set; } = "";
            public string rolNaam { get; set; } = "";
        }
        private void dataBase_Connection()
        {

            string connectionString = "server=localhost;database=InlogProject_Giel;uid=InlogProject;pwd=Gilde123!;port=3306;SslMode=None;";

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                }
            }
            catch (Exception ex)
            {
                errorProviderDataBase.SetError(btnInloggen, ex.Message);
            }
        }

        //private void hashWachtwoord(string plainWachtwoord) -- Kan nuttig zijn voor registreren later.
        //{
        //    string hashedWachtwoord = BCrypt.Net.BCrypt.HashPassword(plainWachtwoord);

        //    Console.WriteLine(plainWachtwoord, hashedWachtwoord);
        //}
        private async Task<bool> checkLogin(string email, string plainWachtwoord)
        {

            try
            {

                string loginQuery = "SELECT email, wachtwoord FROM Gebruiker WHERE email = @email";

                using (var connection = new MySqlConnection(connectionString))
                {
                    await connection.OpenAsync(); 

                    using (var cmd = new MySqlCommand(loginQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@email", email);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {


                            if (!reader.Read())
                            {
                                return false;
                            }

                            string hashVanDb = reader.GetString("wachtwoord");

                            return BCrypt.Net.BCrypt.Verify(plainWachtwoord, hashVanDb);
                        } 
                    } 
                }
            }
            catch (Exception ex)
            {
                errorProviderDataBase.SetError(btnInloggen, ex.Message);
                return false;
            }
        }

        private void get_gebruikers()
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string selectQuery = "SELECT Gebruiker.email, Gebruiker.voornaam, Gebruiker.achternaam, Gebruiker.klas, Gebruiker.rolld, Rol.ID, Rol.naam FROM Gebruiker JOIN Rol on Gebruiker.rolld = Rol.ID;";
                using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                {
                    
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bool is_Klas_Null = reader.IsDBNull(reader.GetOrdinal("klas"));

                            bool is_Email_Null = reader.IsDBNull(reader.GetOrdinal("email"));


                            string email;
                            if (is_Klas_Null == false)
                            {
                                email = reader.GetString("email");
                            }
                            else
                            {
                                email = "NULL";
                            }

                            string voornaam = reader.GetString("voornaam");
                            string achternaam = reader.GetString("achternaam");
                            string klas;
                            if (is_Klas_Null == false)
                            {
                                klas = reader.GetString("klas");
                            }
                            else
                            {
                                klas = "NULL";
                            }
                            string rolNaam = reader.GetString("naam");

                            rTBGebruikers.Text += $"Email: {email}, Voornaam: {voornaam}, Achternaam: {achternaam} Klas: {klas}, Rol: {rolNaam}";
                        }
                    }
                }
            }
        }
        private async void btnInloggen_Click(object sender, EventArgs e)
        {
            bool verificatie = await checkLogin(txtEmailInvoer.Text, txtWachtwoordInvoer.Text);

            if (verificatie == true)
            {
                MessageBox.Show("ingelogd");
                get_gebruikers();
            } else if (verificatie == false)
            {
                errorProviderDataBase.SetError(btnInloggen, "foute Inloggegevens");
            }
        }
    }
}
