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
        private async void btnInloggen_Click(object sender, EventArgs e)
        {
            bool verificatie = await checkLogin(txtEmailInvoer.Text, txtWachtwoordInvoer.Text);

            if (verificatie == true)
            {
                MessageBox.Show("ingelogd");
            } else if (verificatie == false)
            {
                errorProviderDataBase.SetError(btnInloggen, "foute Inloggegevens");
            }
        }
    }
}
