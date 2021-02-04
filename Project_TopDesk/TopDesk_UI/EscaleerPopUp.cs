using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TopDesk_Logic;
using TopDesk_Model;

namespace TopDesk_UI
{
    public partial class EscaleerPopUp : Form
    {
        //WOUT
        List<string> WerkCategorieën;
        Ticket_Service ticket_Service;
        Medewerker_Service medewerker_Service;
        List<Medewerker> medewerkerlijst;
        List<Medewerker> temp;
        ObjectId ticketID;
        TopDeskApp topDesk;
        bool metFilter = true;
        public EscaleerPopUp(string warning, ObjectId id, TopDeskApp topdesk) //Wout de Roy van Zuydewijn 648184
        {
            InitializeComponent();
            lblWarning.Text = "Je staat op het punt de ticket (" + warning + ") te escaleren.";
            ticket_Service = new Ticket_Service();
            medewerker_Service = new Medewerker_Service();
            ticketID = id;
            topDesk = topdesk;
            cbWerknemer.Items.Add("----");


        }
        public void Categorie()//Wout de Roy van Zuydewijn 648184
        {
            WerkCategorieën = ticket_Service.VerkrijgAlleCategorieën();
            cbCategorie.Items.Clear();
            cbWerknemer.Items.Clear();

            foreach (string werkcategorie in WerkCategorieën)
            {
                cbCategorie.Items.Add(werkcategorie);
            }
        }
        public void Werknemers()//Wout de Roy van Zuydewijn 648184
        {
            string werkcategorie = cbCategorie.GetItemText(cbCategorie.SelectedItem);
            medewerkerlijst = medewerker_Service.VerkrijgEscaleerMedewerkers(werkcategorie);
            cbWerknemer.Items.Clear();
            cbWerknemer.Items.Add("----");
            foreach (Medewerker medewerker in medewerkerlijst)
            {
                cbWerknemer.Items.Add(medewerker.Voornaam + " " + medewerker.Achternaam);
            }
        }

        private void EscaleerPopUp_Load(object sender, EventArgs e)
        {

        }

        private void btnConfirm_Click(object sender, EventArgs e)// Wout de Roy van Zuydewijn
        {
            int medewerkerindex;
            Medewerker medewerker;
            if (txbWerknemer.Text == string.Empty)
            {
                try
                {
                    medewerkerindex = cbWerknemer.SelectedIndex - 1;
                    medewerker = medewerkerlijst.ElementAt(medewerkerindex);
                }
                catch
                {
                    MessageBox.Show("Er is geen werknemer geselecteerd!");
                    return;
                }
            }
            else
            {
                try
                {
                    medewerkerindex = cbWerknemer.SelectedIndex - 1;
                    medewerker = temp.ElementAt(medewerkerindex);
                }
                catch
                {
                    MessageBox.Show("Er is geen werknemer geselecteerd!");
                    return;
                }
            }
            ObjectId medewerkerID = medewerker._id;
            if (cbWerknemer.SelectedItem.ToString() == "----")
            {
                MessageBox.Show("Er is geen geldige werknemer gekozen!");
                return;
            }
            ticket_Service.EscaleerTicket(medewerkerID, ticketID);
            this.Close();
            topDesk.OverzichtTicketsFilteren();
        }

        private void btnCancel_Click(object sender, EventArgs e)//Wout de Roy van Zuydewijn 648184
        {
            this.Close();
        }

        private void cbCategorie_SelectedIndexChanged(object sender, EventArgs e)//Wout de Roy van Zuydewijn 648184
        {
            Werknemers();
        }

        private void cbWerknemer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbWerknemer.SelectedIndex == 0)
            {
                txbWerknemer.Text = string.Empty;
            }
        }

        private void txbWerknemer_TextChanged_1(object sender, EventArgs e)
        {
            string invoer = txbWerknemer.Text.ToLower();

            if (invoer == string.Empty)
            {
                temp = medewerkerlijst.ToList();
            }
            else
            {
                temp = medewerkerlijst.ToList();
                temp.RemoveAll(x => !((string)x.VolledigeNaam.ToLower()).StartsWith(invoer));
            }

            cbWerknemer.Items.Clear();

            cbWerknemer.Items.Add("----");

            foreach (Medewerker medewerker in temp)
            {
                cbWerknemer.Items.Add(medewerker.VolledigeNaam);
            }


            if (cbWerknemer.Items.Count > 1)
            {
                cbWerknemer.SelectedIndex = 1;
                cbWerknemer.Enabled = true;
            }
            else
            {
                cbWerknemer.Enabled = false;
            }

        }
    }
}

