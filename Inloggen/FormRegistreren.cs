using BCrypt.Net;
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

namespace Inloggen
{
    public partial class FormRegistreren : Form
    {
        public FormRegistreren()
        {
            InitializeComponent();

            dataBase_Connection();
        }

        string connectionString = "server=localhost;database=InlogProject_Giel;uid=InlogProject;pwd=Gilde123!;port=3306;SslMode=None;";

        private void dataBase_Connection()
        {

            //string connectionString = "server=localhost;database=InlogProject_Giel;uid=InlogProject;pwd=Gilde123!;port=3306;SslMode=None;";

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                }
            }
            catch (Exception ex)
            {
                errorProviderRegistreren.SetError(btAccountRegistreren, ex.Message);
            }
        }

        private void stuur_Gebruiker_Naar_Inlog_Form()
        {
            Form1 formL = new Form1();
            formL.Show();

            this.Hide();
        }

        public bool check_alle_data_ingevuld()
        {
            if (tbRegistrerenEmail.Text == "" || 
                tbRegistrerenVoornaam.Text == "" || 
                tbRegistrerenAchternaam.Text == "" || 
                tbRegistrerenKlas.Text == "" || 
                tbRegistrerenWachtwoord.Text == "" || 
                tbRegistrerenWachtwoordHerhalen.Text == "")
            {
                return false; 
            } 
            else { 
            return true;
            }
        }

        public string wachtwoord_hashen(string plainText_Wachtwoord)
        {
            string hashedWachtwoord = BCrypt.Net.BCrypt.HashPassword(plainText_Wachtwoord);

            return hashedWachtwoord;
        }

        public int get_gebruiker_rolid()
        {
            string gebrukers_Rolid_query = "SELECT ID FROM Rol WHERE naam = 'Gebruiker'";

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (var cmd = new MySqlCommand(gebrukers_Rolid_query, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int rol = reader.GetInt32("ID");
                            return rol;
                        }
                        else
                        {
                            errorProviderRegistreren.SetError(btAccountRegistreren, "Gebruiker rol ophalen niet mogelijk. Probeer het later opniew");
                            return -1;
                        }
                    }

                }
            }
        }

        private void gebruiker_Registreren_In_Db()
        {
            string inster_query = "INSERT INTO Gebruiker (email, wachtwoord, voornaam, achternaam, klas, rolld) VALUES (@email, @hashed_wachtwoord, @voornaam, @achternaam, @klas, @gebruiker_rolid)";

            int rolid_van_gebruiker = get_gebruiker_rolid();

            if (rolid_van_gebruiker != -1) {

                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (var cmd = new MySqlCommand(inster_query, connection))
                    {
                        cmd.Parameters.AddWithValue("@email", tbRegistrerenEmail.Text);
                        cmd.Parameters.AddWithValue("@hashed_wachtwoord", wachtwoord_hashen(tbRegistrerenWachtwoord.Text));
                        cmd.Parameters.AddWithValue("@voornaam", tbRegistrerenVoornaam.Text);
                        cmd.Parameters.AddWithValue("@achternaam", tbRegistrerenAchternaam.Text);
                        cmd.Parameters.AddWithValue("@klas", tbRegistrerenKlas.Text);
                        cmd.Parameters.AddWithValue("@gebruiker_rolid", rolid_van_gebruiker);

                        cmd.ExecuteReader();

                        MessageBox.Show("Gebruiker geregistreerd. Sluit de popup om naar de inlog pagina te gaan.");
                        stuur_Gebruiker_Naar_Inlog_Form();
                    }
                }
            }
        }
        

        private void btAccountRegistreren_Click(object sender, EventArgs e)
        {
           if (tbRegistrerenWachtwoord.Text == tbRegistrerenWachtwoordHerhalen.Text)
            {
                if (check_alle_data_ingevuld())
                {
                    gebruiker_Registreren_In_Db();
                } else
                {
                    errorProviderRegistreren.SetError(btAccountRegistreren, "Zorg dat alle velden zijn ingevuld.");
                }
            } else
            {
                errorProviderRegistreren.SetError(tbRegistrerenWachtwoordHerhalen, "De wachtwoorden komen niet overeen");
            }
        }
        
        
        
        
        
        
        private void btNaarInloggen_Click(object sender, EventArgs e)
        {
            stuur_Gebruiker_Naar_Inlog_Form();
        }
    }
}
