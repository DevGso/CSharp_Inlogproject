using BCrypt.Net;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

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
        public bool checkLogin(string email, string plainWachtwoord)
        {
            try
            {
                string loginQuery = "SELECT email, wachtwoord FROM Gebruiker WHERE email = @email";

                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open(); 

                    using (var cmd = new MySqlCommand(loginQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@email", email);

                        using (var reader = cmd.ExecuteReader())
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
        public string Get_Gebruiker_Rol(string email, string wachtwoord)
        {
            string query_get_gebruiker_rol = "SELECT Gebruiker.email, Gebruiker.rolld, Rol.ID, Rol.naam FROM Gebruiker JOIN Rol on Gebruiker.rolld = Rol.ID AND Gebruiker.email = @email";

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (var cmd = new MySqlCommand(query_get_gebruiker_rol, connection))
                {
                    cmd.Parameters.AddWithValue("@email", email);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string rol = reader.GetString("naam");
                            return rol;
                        }
                        else
                        {
                            errorProviderGetGebruikerRol.SetError(lbShowRolGebruiker, "Gebruiker rol ophalen niet mogelijk");
                            return "";
                        }
                    }
                }
            }
        }

        private void show_gebruikers()
        {
            rTBGebruikers.Text = "";
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
                            if (is_Email_Null == false)
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
                            rTBGebruikers.Text += $"Email: {email}, Voornaam: {voornaam}, Achternaam: {achternaam} Klas: {klas}, Rol: {rolNaam} ";
                        }
                    }
                }
            }
        }
        private void btnInloggen_Click(object sender, EventArgs e)
        {
            bool verificatie = checkLogin(txtEmailInvoer.Text, txtWachtwoordInvoer.Text);
            string gebruiker_RoL = Get_Gebruiker_Rol(txtEmailInvoer.Text, txtWachtwoordInvoer.Text);

            if (verificatie == true && gebruiker_RoL != "")
            {
                MessageBox.Show("ingelogd");
                show_gebruikers();

                lblVoorGebruikerRolLabel.Text = "Gebruiker rol:";

                lbShowRolGebruiker.Text = gebruiker_RoL;

                if (gebruiker_RoL == "Admin")
                {
                    this.BackColor = Color.LightGreen;
                } else if (gebruiker_RoL == "Gebruiker")
                {
                    this.BackColor = Color.LightBlue;
                } else
                {
                    this.BackColor = Color.Red;
                }

                txtEmailInvoer.Text = "";
                txtWachtwoordInvoer.Text= "";
            } else if (verificatie == false)
            {
                errorProviderDataBase.SetError(btnInloggen, "foute Inloggegevens");
            }
        }

        private void btnRegistreren_Click(object sender, EventArgs e)
        {
            FormRegistreren formR = new FormRegistreren();
            this.Hide();
            formR.ShowDialog();
            this.Show();
        }
    }
}
