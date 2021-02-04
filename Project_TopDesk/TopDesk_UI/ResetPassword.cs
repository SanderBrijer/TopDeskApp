using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;
using TopDesk_DAL;
using TopDesk_Logic;
using TopDesk_Model;

namespace TopDesk_UI
{
    public partial class ResetPassword : Form
    {
        List<Medewerker> medewerkerList;
        Medewerker_Service medewerker_Service = new Medewerker_Service();
        string gebruiker;

        public ResetPassword()
        {
            InitializeComponent();
            pnlResetPassword.Hide();
            txtCheckGebruiker.Enabled = true;
            txtAntwoordCheck.Enabled = true;
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAntwoordCheck_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnCheckAntwoord_Click(object sender, EventArgs e)
        {
            txtCheckGebruiker.Enabled = false;
            txtAntwoordCheck.Enabled = false;
            gebruiker = txtCheckGebruiker.Text;
            string antwoord = txtAntwoordCheck.Text;
            medewerkerList =  medewerker_Service.VerkrijgAlleMedewerkers();

            List<string> gebruikers = new List<string>();
            List<string> antwoorden = new List<string>();

            foreach (Medewerker medewerker in medewerkerList)
            {
                gebruikers.Add(medewerker.Gebruikersnaam);
                antwoorden.Add(medewerker.Antwoord);
            }
            
            if (gebruikers.Contains(gebruiker))
            {
                int getal = gebruikers.IndexOf(gebruiker);
                if (antwoord != antwoorden[getal])
                {
                    MessageBox.Show("Het antwoord klopt niet bij de gebruiker");
                    Cleartext();
                }
                else
                {
                    pnlResetPassword.Show();
                    BtnCheckAntwoord.Hide();
                    Cleartext();
                }
            }
            else
            {
                MessageBox.Show("De gegeven gebruikersnaam bestaat niet");
                Cleartext();
                txtCheckGebruiker.Clear();
            }

        }
        void Cleartext()
        {
            txtAntwoordCheck.Clear();
            txtHerhaaldWW.Clear();
            txtNieuwWW.Clear();
        }

        private void btnWijzigWW_Click(object sender, EventArgs e)
        {
            string nieuwWW = txtNieuwWW.Text;
            string wwCheck = txtHerhaaldWW.Text;
            
            gebruiker = txtCheckGebruiker.Text;
           

            if (nieuwWW == wwCheck)
            {
                if(IsValidPassword(nieuwWW) == true){
                    medewerker_Service.UpdateWachtwoord(gebruiker, nieuwWW);
                    MessageBox.Show("Het wachtwoord van de gebruiker is gewijzigd.");
                    Cleartext();
                    pnlResetPassword.Hide();
                    this.Close();

                }
                else
                {
                    MessageBox.Show("Het nieuwe wachtwoord voldoet niet aan de eisen.");
                    Cleartext();
                }
            }
            else
            {
                MessageBox.Show("De wachtwoorden komen niet overeen.");
                Cleartext();
            }
        }

        public bool IsLetter(char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
        }

        public bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
        }

        public bool IsSymbol(char c)
        {
            return c > 32 && c < 127 && !IsDigit(c) && !IsLetter(c);
        }

        public bool IsValidPassword(string password)
        {
            return
               password.Any(c => IsLetter(c)) &&
               password.Any(c => IsDigit(c)) &&
               password.Any(c => IsSymbol(c));
        }
    }
}
