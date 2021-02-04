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
using Telerik.OpenAccess.Metadata.Validation;
using TopDesk_DAL;
using TopDesk_Logic;
using TopDesk_Model;

namespace TopDesk_UI
{
    public partial class TopDeskApp : Form
    {
        bool overzichtIngeladen;
        Panel huidigePanel;
        List<Ticket> ticketsGefilterd;
        ConfigDataBase configDataBase = new ConfigDataBase();
        List<Ticket> ticketList;
        List<Medewerker> medewerkerList;
        List<Medewerker> medewerkerLijstTemp;
        List<Klant> klantenLijstTemp;
        List<Klant> klantenList;
        Medewerker_Service medewerker_Service;
        Klant_Service klant_Service;
        Medewerker ingelogdeMedewerker;
        string geenfilter;
        ObjectId updateId;


        public string ticketTitel = "Tickettitel";
        public string voornaam = "Voornaam";
        public string achternaam = "Achternaam";
        public string email = "Email";

        public Ticket_Service ticket_Service;
        public List<string> categorieën;
        public List<string> statussen;

        private string loginName;
        private SecureString loginPassword;

        public TopDeskApp() //Sander Brijer 646235
        {
            this.WindowState = FormWindowState.Maximized;


            InitializeComponent();
            //Breedte - hoogte
            this.Size = new Size(1460, 800);
            ticket_Service = new Ticket_Service();
            ticketList = new List<Ticket>();
            medewerkerLijstTemp = new List<Medewerker>();
            klantenLijstTemp = new List<Klant>();
            categorieën = ticket_Service.VerkrijgAlleCategorieën();

            medewerker_Service = new Medewerker_Service();
            medewerkerList = medewerker_Service.VerkrijgAlleMedewerkers();

            klant_Service = new Klant_Service();
            klantenList = klant_Service.VerkrijgAlleKlanten();

            statussen = ticket_Service.VerkrijgAlleStatussen();

            geenfilter = "❌Geen filter instellen";
            Start();
        }


        //SANDER BRIJER 646235
        //Start
        private void Start() //Sander Brijer 646235
        {
            AllePanelsOnzichtbaar();
            pnlMenu.Hide();
            pnlLogin.Show();
        }

        public void AllePanelsOnzichtbaar() //Sander Brijer 646235
        {
            foreach (Control c in this.Controls)

                if (c is Panel)

                    if (c != pnlMenu)
                        c.Visible = false;
        }

        private void btnLoginKnop_Click(object sender, EventArgs e) //Sander Brijer 646235
        {
            string gebruikersnaam = txtLoginGebruikersnaam.Text;
            string wachtwoord = txtLoginWachtwoord.Text;
            SecureString beveiligdWachtwoord = new NetworkCredential("", wachtwoord).SecurePassword;

            if (cbLoginOnthoudMij.Checked == true)
            {
                loginName = gebruikersnaam;
                loginPassword = beveiligdWachtwoord;
            }
            else
            {
                loginName = null;
                loginPassword = null;
            }

            ingelogdeMedewerker = medewerker_Service.CheckUser(gebruikersnaam, beveiligdWachtwoord);
            if (ingelogdeMedewerker != null)
            {
                txtLoginGebruikersnaam.Text = string.Empty;
                txtLoginWachtwoord.Text = string.Empty;
                AllePanelsOnzichtbaar();
                pnlMenu.Show();
                if (ingelogdeMedewerker.isAdmin) StartNaInlogAdmin();
                else StartNaInlog();
            }
            else
            {
                MessageBox.Show("Email en wachtwoord combinatie is ongeldig.");
            }
        }

        private void StartNaInlog() //Sander Brijer 646235
        {
            btnWerknemer.Visible = false;
            ShowDashboard();
        }

        //MENU
        private void btnMenuOverzicht_Click(object sender, EventArgs e) //Sander Brijer 646235
        {
            OpenOverzicht();
        }

        private void btnMenuDashboard_Click(object sender, EventArgs e) //Sander Brijer 646235
        {
            ShowDashboard();
        }
        private void btnMenuToevoegenTicket_Click(object sender, EventArgs e) //Sander Brijer 646235
        {
            AllePanelsOnzichtbaar();
            TicketToevoegenPanel();
        }
        private void btnMenuUitloggen_Click(object sender, EventArgs e) //Sander Brijer 646235
        {
            ingelogdeMedewerker = null;
            AllePanelsOnzichtbaar();
            pnlMenu.Visible = false;
            pnlLogin.Visible = true;

            if (loginPassword != null && loginName != null)
            {
                txtLoginGebruikersnaam.Text = loginName;
                txtLoginWachtwoord.Text = new NetworkCredential("", loginPassword).Password;
            }
        }


        //DASHBOARD
        private void btnDashboardRefresh_Click(object sender, EventArgs e)
        {
            medewerkerList = medewerker_Service.VerkrijgAlleMedewerkers();
            ticketList = ticket_Service.VerkrijgAlleTickets();
            categorieën = ticket_Service.VerkrijgAlleCategorieën();
            klantenList = klant_Service.VerkrijgAlleKlanten();
            statussen = ticket_Service.VerkrijgAlleStatussen();


            if (huidigePanel == pnlWerknemer)
            {
                WerknemerToevoegenScherm();
            }
            else if (huidigePanel == pnlToevoegenTicket)
            {
                TicketToevoegenPanel();
            }
            else if (huidigePanel == pnlOverzicht)
            {
                OpenOverzicht();
            }
            else
            {
                ShowDashboard();
            }
        }

        //OVERZICHT

        private void btnOverzichtTicketToevoegen_Click(object sender, EventArgs e) //Sander Brijer 646235
        {
            TicketToevoegenPanel();
            AllePanelsOnzichtbaar();
            pnlToevoegenTicket.Visible = true;
        }


        private void UpdateButtons(bool laatzien) //Sander Brijer 646235
        {
            btnArchieveren.Visible = laatzien;
            btnOverzichtTicketToevoegen.Visible = laatzien;
            btnOverzichtTicketVerwijderen.Visible = laatzien;
            btnUpdateStart.Visible = laatzien;
            txtOverzichtFilterKlant.Visible = laatzien;
            btnOverzichtFilterCategorieSorteren.Visible = laatzien;
            btnOverzichtFilterTitel.Visible = laatzien;
            btnSluit.Visible = laatzien;
            btnHeropen.Visible = laatzien;
            btnEscaleer.Visible = laatzien;
            txtTitel.Visible = !laatzien;
            btnUpdateConfirm.Visible = !laatzien;
            comboUpdateKlant.Visible = !laatzien;
            btnUpdateConfirm.Visible = !laatzien;

            cBOverzichtFilterCategorie.Visible = laatzien;
            cBOverzichtFilterMedewerker.Visible = laatzien;
            cBOverzichtFilterStatus.Visible = laatzien;
            txtOverzichtFilterMedewerker.Visible = laatzien;
            cBOverzichtFilterKlant.Visible = laatzien;
        }

        private void OpenOverzicht() //Sander Brijer 646235
        {
            huidigePanel = pnlOverzicht;
            UpdateButtons(true);


            txtTitel.Visible = false;
            comboUpdateKlant.Visible = false;
            txtOverzichtFilterMedewerker.Visible = ingelogdeMedewerker.isAdmin;
            lblOverzichtFilterMedewerker.Visible = ingelogdeMedewerker.isAdmin;
            cBOverzichtFilterMedewerker.Visible = ingelogdeMedewerker.isAdmin;


            overzichtIngeladen = false;


            txtOverzichtFilterTitel.Text = string.Empty;

            if (ingelogdeMedewerker.isAdmin)
            {
                btnOverzichtFilterCategorieSorteren.Visible = true;
            }
            else
            {
                btnOverzichtFilterCategorieSorteren.Visible = false;
            }

            VulListViewTicketInfo(null);
            cBOverzichtFilterCategorie.Items.Clear();


            cBOverzichtFilterCategorie.Items.Add(geenfilter);
            foreach (string categorie in categorieën)
            {
                cBOverzichtFilterCategorie.Items.Add(categorie);
            }

            if (!ingelogdeMedewerker.isAdmin)
            {
                for (int i = 0; i < categorieën.Count; i++)
                {
                    if (categorieën[i] == ingelogdeMedewerker.WerkCategorie)
                    {
                        cBOverzichtFilterCategorie.Enabled = false;
                        cBOverzichtFilterCategorie.SelectedIndex = i + 1;
                    }
                }
            }
            else if (cBOverzichtFilterCategorie.Items.Count > 0)
            {
                cBOverzichtFilterCategorie.SelectedIndex = 0;
                cBOverzichtFilterCategorie.Enabled = true;
            }
            else
            {
                cBOverzichtFilterCategorie.Enabled = false;
            }

            cBOverzichtFilterStatus.Items.Clear();
            cBOverzichtFilterStatus.Items.Add(geenfilter);
            foreach (string status in statussen)
            {
                cBOverzichtFilterStatus.Items.Add(status);
            }
            if (cBOverzichtFilterStatus.Items.Count > 0)
            {
                cBOverzichtFilterStatus.SelectedIndex = 0;
                cBOverzichtFilterStatus.Enabled = true;
            }
            else
            {
                cBOverzichtFilterStatus.Enabled = false;
            }

            cBOverzichtFilterMedewerker.Items.Clear();
            cBOverzichtFilterMedewerker.Items.Add(geenfilter);
            foreach (Medewerker medewerker in medewerkerList)
            {
                cBOverzichtFilterMedewerker.Items.Add(medewerker.VolledigeNaam);
            }
            if (cBOverzichtFilterMedewerker.Items.Count > 0)
            {
                if (ingelogdeMedewerker.isAdmin)
                {
                    cBOverzichtFilterMedewerker.SelectedIndex = 0;
                    if (lVTicketInfo.Columns.Count < 4)
                    {
                        lVTicketInfo.Columns.Insert(3, "Eigenaar");
                        lVTicketInfo.Columns.Insert(4, "Categorie");
                    }
                    lVTicketInfo.Columns[3].Width = 200;
                    lVTicketInfo.Columns[4].Width = 100;
                }
                else
                {
                    try
                    {
                        lVTicketInfo.Columns.RemoveAt(3);
                        lVTicketInfo.Columns.RemoveAt(3);
                    }
                    catch
                    {

                    }
                    for (int i = 0; i < medewerkerList.Count; i++)
                    {
                        if (medewerkerList[i]._id == ingelogdeMedewerker._id)
                        {
                            overzichtIngeladen = true;
                            cBOverzichtFilterMedewerker.SelectedIndex = i + 1;
                            overzichtIngeladen = false;
                        }
                    }
                }
                cBOverzichtFilterMedewerker.Enabled = true;
                lVTicketInfo.Columns[0].Width = 300;
                lVTicketInfo.Columns[1].Width = 200;
                lVTicketInfo.Columns[2].Width = 100;
            }
            else
            {
                cBOverzichtFilterMedewerker.Enabled = false;
            }


            cBOverzichtFilterKlant.Items.Clear();
            cBOverzichtFilterKlant.Items.Add(geenfilter);
            foreach (Klant klant in klantenList)
            {
                cBOverzichtFilterKlant.Items.Add(klant.VolledigeNaam);
            }
            if (cBOverzichtFilterKlant.Items.Count > 0)
            {
                cBOverzichtFilterKlant.SelectedIndex = 0;
                cBOverzichtFilterKlant.Enabled = true;
            }
            else
            {
                cBOverzichtFilterKlant.Enabled = false;
            }


            AllePanelsOnzichtbaar();
            pnlOverzicht.Visible = true;

            overzichtIngeladen = true;
        }

        private void btnOverzichtFilterTitel_Click(object sender, EventArgs e) //Sander Brijer 646235
        {
            OverzichtTicketsFilteren();
        }

        private void btnOverzichtFilterCategorieSorteren_Click(object sender, EventArgs e) //Sander Brijer 646235
        {
            if (ticketsGefilterd == null)
            {
                ticketsGefilterd = ticketList;
            }

            ticketsGefilterd.Sort((p, q) => p.Categorie.CompareTo(q.Categorie));
            VulListViewTicketInfoLijst(ticketsGefilterd);
        }

        private void VulListViewTicketInfo(string categorie) //Sander Brijer 646235
        {
            lVTicketInfo.Items.Clear();
            if (categorie != null)
            {
                if (categorie.Length != 0)
                    ticketsGefilterd = ticket_Service.LoadAlleTicketsFromSort(categorie);
                else
                    ticketsGefilterd = ticket_Service.VerkrijgAlleTickets();

                VulListViewTicketInfoLijst(ticketsGefilterd);
            }
            else
            {
                ticketList = ticket_Service.VerkrijgAlleTickets();
                VulListViewTicketInfoLijst(ticketList);
            }
        }

        private void VulListViewTicketInfoLijst(List<Ticket> tickets) //Sander Brijer 646235
        {
            lVTicketInfo.Items.Clear();
            foreach (Ticket ticket in tickets)
            {
                ListViewItem item = new ListViewItem(ticket.Titel);
                try
                {
                    Klant klant = klantenList.Find(k => k._id == ticket.KlantId);
                    //Klant klant = klant_Service.KrijgKlantViaId(ticket.KlantId.ToString());
                    item.SubItems.Add(klant.VolledigeNaam);
                }
                catch
                {
                    item.SubItems.Add("-");
                }
                item.SubItems.Add(ticket.Status);
                try
                {
                    if (ingelogdeMedewerker.isAdmin)
                    {
                        Medewerker medewerker = medewerkerList.Find(m => m._id == ticket.MedewerkerId);
                        //Medewerker medewerker = medewerker_Service.KrijgMedewekerViaId(ticket.MedewerkerId.ToString());
                        item.SubItems.Add(medewerker.VolledigeNaam);
                    }
                }
                catch
                {
                    item.SubItems.Add("-");
                }

                if (ingelogdeMedewerker.isAdmin)
                {
                    item.SubItems.Add(ticket.Categorie);
                }

                lVTicketInfo.Items.Add(item);
            }

            lblOverzichtFilterResultaten.Text = $"Resultaten: {tickets.Count}";
        }


        public void VulCategorieënListview() //Sander Brijer 646235
        {
            ticketList = ticket_Service.VerkrijgAlleTickets();

            IDictionary<string, int> categorieëncount = new Dictionary<string, int>();

            foreach (string categorie in categorieën)
            {
                categorieëncount.Add(categorie, 0);
            }

            //int number2 = categorieëncount.ElementAt(0).Value;

            foreach (Ticket ticket in ticketList)
            {
                if (categorieëncount.ContainsKey(ticket.Categorie))

                    categorieëncount[ticket.Categorie] += 1;
            }

            for (int i = 0; i < categorieën.Count; i++)
            {
                int aantal = categorieëncount.ElementAt(i).Value;

                ListViewItem itemOfListview = new ListViewItem(categorieën[i]);
                itemOfListview.SubItems.Add(aantal.ToString());
            }
        }

        public void OverzichtTicketsFilteren() //Sander Brijer 646235
        {
            if (overzichtIngeladen == false)
            {
                return;
            }

            string titel = string.Empty;
            string categorie = string.Empty;
            string status = string.Empty;
            Medewerker medewerker = null;
            Klant klant = null;
            if (btnOverzichtFilterTitel.Text != string.Empty)
                titel = txtOverzichtFilterTitel.Text;
            if (cBOverzichtFilterCategorie.SelectedIndex > 0)
                categorie = cBOverzichtFilterCategorie.SelectedItem.ToString();
            if (cBOverzichtFilterStatus.SelectedIndex > 0)
                status = cBOverzichtFilterStatus.SelectedItem.ToString();
            if (cBOverzichtFilterMedewerker.SelectedIndex > 0)
            //medewerker = medewerkerList.Equals();
            {
                try
                {

                    if (medewerkerLijstTemp != null && medewerkerLijstTemp.Count > 0)
                    {
                        medewerker = medewerkerLijstTemp[cBOverzichtFilterMedewerker.SelectedIndex - 1];
                    }
                    else
                    {
                        medewerker = medewerkerList[cBOverzichtFilterMedewerker.SelectedIndex - 1];
                    }
                }
                catch(Exception ex)
                {
                    medewerker = medewerkerList[cBOverzichtFilterMedewerker.SelectedIndex - 1];
                }
            }
            if (cBOverzichtFilterKlant.SelectedIndex > 0)
            {
                if (klantenLijstTemp != null && klantenLijstTemp.Count > 0)
                {
                    klant = klantenLijstTemp[cBOverzichtFilterKlant.SelectedIndex - 1];
                }
                else
                {
                    klant = klantenList[cBOverzichtFilterKlant.SelectedIndex - 1];
                }
            }


            ticketsGefilterd = ticket_Service.VerkrijgAlleTicketsFilter(titel, categorie, status, medewerker, klant);
            if (ticketsGefilterd != null)
            {
                VulListViewTicketInfoLijst(ticketsGefilterd);
            }
            else
            {
                VulListViewTicketInfoLijst(ticket_Service.VerkrijgAlleTickets());
                ticketsGefilterd = ticketList;
            }
        }

        private void cBOverzichtFilterStatus_SelectedIndexChanged(object sender, EventArgs e) //Sander Brijer 646235
        {
            OverzichtTicketsFilteren();
        }

        private void cBOverzichtFilterCategorie_SelectedIndexChanged(object sender, EventArgs e) //Sander Brijer 646235
        {
            OverzichtTicketsFilteren();
        }

        private void cBOverzichtFilterMedewerker_SelectedIndexChanged(object sender, EventArgs e) //Sander Brijer 646235
        {
            if (cBOverzichtFilterMedewerker.SelectedIndex == 0)
            {
                txtOverzichtFilterMedewerker.Text = string.Empty;
            }
            OverzichtTicketsFilteren();
        }

        private void cBOverzichtFilterKlant_SelectedIndexChanged(object sender, EventArgs e) //Sander Brijer 646235
        {
            if (cBOverzichtFilterKlant.SelectedIndex == 0)
            {
                txtOverzichtFilterKlant.Text = string.Empty;

            }
            OverzichtTicketsFilteren();
        }

        private void txtOverzichtFilterTitel_TextChanged(object sender, EventArgs e) //Sander Brijer 646235
        {
            if (txtOverzichtFilterTitel.Text == string.Empty)
            {
                OverzichtTicketsFilteren();
            }
        }

        private void txtOverzichtFilterKlant_TextChanged(object sender, EventArgs e) //Sander Brijer 646235
        {
            FilterKlantCB(cBOverzichtFilterKlant, txtOverzichtFilterKlant, true);
        }

        private void FilterKlantCB(ComboBox combobox, TextBox textbox, bool metFilter) //Sander Brijer 646235
        {
            string invoer = textbox.Text.ToLower();

            if (invoer == string.Empty)
            {
                klantenLijstTemp = klantenList.ToList();
            }
            else
            {
                klantenLijstTemp = klantenList.ToList();
                klantenLijstTemp.RemoveAll(x => !((string)x.VolledigeNaam.ToLower()).StartsWith(invoer));
            }

            combobox.Items.Clear();

            if (metFilter)
            {
                combobox.Items.Add(geenfilter);
            }


            foreach (Klant klant in klantenLijstTemp)
            {
                combobox.Items.Add(klant.VolledigeNaam);
            }

            if (metFilter)
            {
                if (combobox.Items.Count > 1)
                {
                    combobox.SelectedIndex = 1;
                    combobox.Enabled = true;
                }
                else
                {
                    combobox.Enabled = false;
                }
            }
            else
            {
                if (combobox.Items.Count > 0)
                {
                    combobox.SelectedIndex = 0;
                    combobox.Enabled = true;
                }
                else
                {
                    combobox.Enabled = false;
                }
            }
        }


        //Ticket toevoegen
        private void TicketToevoegenPanel() //Sander Brijer 646235
        {
            huidigePanel = pnlToevoegenTicket;
            TicketToevoegenClear();
            AllePanelsOnzichtbaar();
            pnlToevoegenTicket.Visible = true;

            klantenList = klant_Service.VerkrijgAlleKlanten();

            cBToevoegenTicketKlantSelecteren.Items.Clear();
            foreach (Klant klant in klantenList)
            {
                cBToevoegenTicketKlantSelecteren.Items.Add(klant.VolledigeNaam);
            }

            medewerkerList = medewerker_Service.VerkrijgAlleMedewerkers();

            cBToevoegenTicketMedewerker.Items.Clear();
            foreach (Medewerker medewerker in medewerkerList)
            {
                cBToevoegenTicketMedewerker.Items.Add(medewerker.VolledigeNaam);
            }
            if (medewerkerList.Count > 0)
            {
                cBToevoegenTicketMedewerker.SelectedIndex = 0;
            }


            cBToevoegenTicketCategorie.Items.Clear();
            foreach (string categorie in categorieën)
            {
                cBToevoegenTicketCategorie.Items.Add(categorie);
            }
            if (categorieën.Count > 0)
            {
                cBToevoegenTicketCategorie.SelectedIndex = 0;
            }


            cBToevoegenTicketStatus.Items.Clear();
            statussen = ticket_Service.VerkrijgAlleStatussen();
            foreach (string status in statussen)
            {
                cBToevoegenTicketStatus.Items.Add(status);
            }
            if (statussen.Count > 0)
            {
                cBToevoegenTicketStatus.SelectedIndex = 0;
            }

            if (!ingelogdeMedewerker.isAdmin)
            {
                cBToevoegenTicketCategorie.Text = ingelogdeMedewerker.WerkCategorie.ToString();
                cBToevoegenTicketCategorie.Enabled = false;
                cBToevoegenTicketMedewerker.Text = ingelogdeMedewerker.VolledigeNaam.ToString();
                cBToevoegenTicketMedewerker.Enabled = false;
                txtToevoegenTicketMedewerker.Enabled = false;
            }
            else
            {
                cBToevoegenTicketCategorie.Enabled = true;
                cBToevoegenTicketMedewerker.Enabled = true;
                txtToevoegenTicketMedewerker.Enabled = true;
            }

            resetTextVeldenTicketToevoegenKlant();
        }

        private void MedewerkerComboBoxVullen() //Sander Brijer 646235
        {
            cBToevoegenTicketMedewerker.Items.Clear();
            {
                string categorie = cBToevoegenTicketCategorie.Text;
                medewerkerLijstTemp = medewerkerList.Where(medewerker => medewerker.WerkCategorie == categorie).ToList();

                foreach (Medewerker medewerker in medewerkerLijstTemp)
                {
                    cBToevoegenTicketMedewerker.Items.Add(medewerker.VolledigeNaam);
                }

                if (cBToevoegenTicketMedewerker.Items.Count != 0)
                {
                    cBToevoegenTicketMedewerker.SelectedIndex = 0;
                    cBToevoegenTicketMedewerker.Enabled = true;
                }
                else
                {
                    cBToevoegenTicketMedewerker.Enabled = false;
                }
            }

        }

        private void cBToevoegenTicketCategorie_SelectedIndexChanged(object sender, EventArgs e) //Sander Brijer 646235
        {
            MedewerkerComboBoxVullen();
        }

        private void TicketToevoegenClear() //Sander Brijer 646235
        {
            txtToevoegenTicketMedewerker.Text = string.Empty;
            resetTextVeldenTicketToevoegenKlant();

            rBNieuweKlant.Checked = true;
            txtToevoegenTicketTitel.Text = string.Empty;
            txtToevoegenTicketBeschrijving.Text = string.Empty;
            cBToevoegenTicketCategorie.SelectedIndex = -1;

            if (cBToevoegenTicketKlantSelecteren.Items.Count > 0)
                cBToevoegenTicketKlantSelecteren.SelectedIndex = 0;
            else
                cBToevoegenTicketKlantSelecteren.SelectedIndex = -1;

            ToevoegenTicketToevoegenOfBestaandeKlantVisible(false);
        }

        private void rBBestaandeKlant_CheckedChanged(object sender, EventArgs e) //Sander Brijer 646235
        {
            if (rBBestaandeKlant.Checked)
            {
                //bestaande klant zichtbaar
                ToevoegenTicketToevoegenOfBestaandeKlantVisible(true);

                int index = cBToevoegenTicketKlantSelecteren.SelectedIndex;

                if (index < 0)
                {
                    cBToevoegenTicketKlantSelecteren.SelectedIndex = 0;
                }
                else
                {
                    panelTicketToevoegenVulKlantTxt(klantenList[index]);
                }
            }
        }

        public void panelTicketToevoegenVulKlantTxt(Klant klant) //Sander Brijer 646235
        {
            txtToevoegenTicketKlantVoornaam.ForeColor = Color.Gray;
            txtToevoegenTicketKlantAchternaam.ForeColor = Color.Gray;
            txtToevoegenTicketKlantEmail.ForeColor = Color.Gray;

            txtToevoegenTicketKlantVoornaam.Text = klant.Voornaam;
            txtToevoegenTicketKlantAchternaam.Text = klant.Achternaam;
            txtToevoegenTicketKlantEmail.Text = klant.Email;
        }

        public void resetTextVeldenTicketToevoegenKlant()
        {
            txtToevoegenTicketKlantVoornaam.ForeColor = Color.Gray;
            txtToevoegenTicketKlantAchternaam.ForeColor = Color.Gray;
            txtToevoegenTicketKlantEmail.ForeColor = Color.Gray;

            txtToevoegenTicketKlantVoornaam.Text = voornaam;
            txtToevoegenTicketKlantAchternaam.Text = achternaam;
            txtToevoegenTicketKlantEmail.Text = email;
        }

        private void rBNieuweKlant_CheckedChanged(object sender, EventArgs e) //Sander Brijer 646235
        {
            if (rBNieuweKlant.Checked)
            {
                //nieuwe klant zichtbaar
                ToevoegenTicketToevoegenOfBestaandeKlantVisible(false);


                if (cBToevoegenTicketKlantSelecteren.Items.Count > 0)
                {
                    cBToevoegenTicketKlantSelecteren.SelectedIndex = 0;
                }

                resetTextVeldenTicketToevoegenKlant();
            }
        }

        private void ToevoegenTicketToevoegenOfBestaandeKlantVisible(bool visible) //Sander Brijer 646235
        {
            lblToevoegenTicketKlantSelecteer.Visible = visible;
            cBToevoegenTicketKlantSelecteren.Visible = visible;
            txtToevoegenTicketKlantSelecteren.Visible = visible;


            lblToevoegenTicketKlantVoergegevensin.Visible = !visible;

            txtToevoegenTicketKlantVoornaam.Enabled = !visible;
            txtToevoegenTicketKlantAchternaam.Enabled = !visible;
            txtToevoegenTicketKlantEmail.Enabled = !visible;
        }

        private void btnToevoegenTicketAnnuleren_Click(object sender, EventArgs e) //Sander Brijer 646235
        {
            AllePanelsOnzichtbaar();
        }

        private void btnToevoegenTicketAanmaken_Click(object sender, EventArgs e) //Sander Brijer 646235
        {
            string titel = txtToevoegenTicketTitel.Text;
            string categorie = cBToevoegenTicketCategorie.Text;
            string status = cBToevoegenTicketStatus.Text;
            string beschrijving = txtToevoegenTicketBeschrijving.Text;

            if (titel.Length <= 0)
            {
                MessageBox.Show("Er is geen titel ingevuld bij het ticket.");
            }
            if (categorie.Length <= 0)
            {
                MessageBox.Show("Er is geen categorie ingevuld bij het ticket.");
            }
            if (status.Length <= 0)
            {
                MessageBox.Show("Er is geen status ingevuld bij het ticket.");
            }
            if (beschrijving.Length <= 0)
            {
                MessageBox.Show("Er is geen beschrijving ingevuld bij het ticket.");
            }

            Klant klant = null;
            Medewerker medewerkerAanTicket = null;


            try
            {

                //indien bestaande klant
                if (rBBestaandeKlant.Checked == true)
                {
                    int indexKlant = cBToevoegenTicketKlantSelecteren.SelectedIndex;

                    klant = klantenLijstTemp[indexKlant];
                }
                else
                {

                    string voornaam = txtToevoegenTicketKlantVoornaam.Text;
                    string achternaam = txtToevoegenTicketKlantAchternaam.Text;
                    string email = txtToevoegenTicketKlantEmail.Text;


                    if (voornaam.Length == 0 || achternaam.Length == 0)
                    {
                        MessageBox.Show("Er is geen naam ingevuld bij de klant.");
                        return;
                    }
                    else if (!IsValidEmail(email))
                    {
                        MessageBox.Show("Er is geen geldige email ingevoerd.");
                        throw new Exception();
                    }
                    else
                    {
                        klant = new Klant()
                        {
                            Voornaam = voornaam,
                            Achternaam = achternaam,
                            Email = email
                        };
                    }
                    Klant checkKlant = klant_Service.KrijgKlantViaEmail(klant.Email);
                    if (checkKlant == null)
                    {
                        klant_Service.VoegEenKlantToe(klant);
                        klant = klant_Service.KrijgKlantViaVoorAchterNaamEmail(klant.Voornaam, klant.Achternaam, klant.Email);
                    }
                    else
                    {
                        MessageBox.Show("Dit emailadres is al toegevoegd aan de database.");
                        klant = checkKlant;
                    }
                }

                //Medewerker bepalen
                int indexMedewerker = cBToevoegenTicketMedewerker.SelectedIndex;

                try
                {

                    if (medewerkerLijstTemp != null)
                    {
                        medewerkerAanTicket = medewerkerLijstTemp[indexMedewerker];
                    }
                    else
                    {
                        medewerkerAanTicket = medewerkerList[indexMedewerker];
                    }
                }
                catch
                {
                    medewerkerAanTicket = ingelogdeMedewerker;
                }

                Ticket ticket = new Ticket()
                {
                    Titel = titel,
                    Categorie = categorie,
                    Status = status,
                    Aanmaakdatumtijd = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"),
                    AangemaaktDoorId = ingelogdeMedewerker._id,
                    KlantId = klant._id,
                    MedewerkerId = medewerkerAanTicket._id,
                    Beschrijving = beschrijving
                };

                ticket_Service.VoegEenTicketToe(ticket);
                TicketToevoegenPanel();
                MessageBox.Show("Toevoegen ticket was succesvol.");
                ticketsGefilterd = ticket_Service.VerkrijgAlleTickets();
            }
            catch
            {
                MessageBox.Show("Niet mogelijk toe te voegen van ticket.");
            }
        }


        private bool IsValidEmail(string email) //Sander Brijer 646235
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void cBToevoegenTicketKlantSelecteren_SelectedIndexChanged(object sender, EventArgs e) //Sander Brijer 646235
        {
            int index = cBToevoegenTicketKlantSelecteren.SelectedIndex;

            if (klantenLijstTemp == null || (klantenLijstTemp.Count == 0 && txtToevoegenTicketKlantSelecteren.Text == string.Empty))
            {
                klantenLijstTemp = klantenList.ToList();
            }

            panelTicketToevoegenVulKlantTxt(klantenLijstTemp[index]);
        }

        private void txtOverzichtFilterMedewerker_TextChanged(object sender, EventArgs e) //Sander Brijer 646235
        {
            FilterMedewerkerCB(cBOverzichtFilterMedewerker, txtOverzichtFilterMedewerker, true);
        }

        private void FilterMedewerkerCB(ComboBox combobox, TextBox textbox, bool metFilter) //Sander Brijer 646235
        {
            string invoer = textbox.Text.ToLower();

            if (invoer == string.Empty)
            {
                medewerkerLijstTemp = medewerkerList.ToList();
            }
            else
            {
                medewerkerLijstTemp = medewerkerList.ToList();
                medewerkerLijstTemp.RemoveAll(x => !((string)x.VolledigeNaam.ToLower()).StartsWith(invoer));
            }

            combobox.Items.Clear();

            if (metFilter)
            {
                combobox.Items.Add(geenfilter);
            }


            foreach (Medewerker medewerker in medewerkerLijstTemp)
            {
                combobox.Items.Add(medewerker.VolledigeNaam);
            }

            if (metFilter)
            {
                if (combobox.Items.Count > 1)
                {
                    combobox.SelectedIndex = 1;
                    combobox.Enabled = true;
                }
                else
                {
                    combobox.Enabled = false;
                }
            }
            else
            {
                if (combobox.Items.Count > 0)
                {
                    combobox.SelectedIndex = 0;
                    combobox.Enabled = true;
                }
                else
                {
                    combobox.Enabled = false;
                }
            }
        }

        private void txtToevoegenTicketMedewerker_TextChanged(object sender, EventArgs e) //Sander Brijer 646235
        {
            FilterMedewerkerCB(cBToevoegenTicketMedewerker, txtToevoegenTicketMedewerker, false);
            //MedewerkerComboBoxVullen();
        }

        private void txtToevoegenTicketTitel_Enter(object sender, EventArgs e) //Sander Brijer 646235
        {
            if (txtToevoegenTicketTitel.Text == ticketTitel)
            {
                txtToevoegenTicketTitel.ForeColor = Color.Black;
                txtToevoegenTicketTitel.Text = "";
            }
        }

        private void txtToevoegenTicketTitel_Leave(object sender, EventArgs e) //Sander Brijer 646235
        {
            if (txtToevoegenTicketTitel.Text == "")
            {
                txtToevoegenTicketTitel.Text = ticketTitel;
                txtToevoegenTicketTitel.ForeColor = Color.Gray;
            }
        }

        private void txtToevoegenTicketKlantVoornaam_Enter(object sender, EventArgs e) //Sander Brijer 646235
        {
            if (txtToevoegenTicketKlantVoornaam.Text == voornaam)
            {
                txtToevoegenTicketKlantVoornaam.ForeColor = Color.Black;
                txtToevoegenTicketKlantVoornaam.Text = "";
            }
        }

        private void txtToevoegenTicketKlantVoornaam_Leave(object sender, EventArgs e) //Sander Brijer 646235
        {
            if (txtToevoegenTicketKlantVoornaam.Text == "")
            {
                txtToevoegenTicketKlantVoornaam.Text = voornaam;
                txtToevoegenTicketKlantVoornaam.ForeColor = Color.Gray;
            }
        }

        private void txtToevoegenTicketKlantAchternaam_Enter(object sender, EventArgs e)
        {
            if (txtToevoegenTicketKlantAchternaam.Text == achternaam)
            {
                txtToevoegenTicketKlantAchternaam.ForeColor = Color.Black;
                txtToevoegenTicketKlantAchternaam.Text = "";
            }
        }

        private void txtToevoegenTicketKlantAchternaam_Leave(object sender, EventArgs e) //Sander Brijer 646235
        {
            if (txtToevoegenTicketKlantAchternaam.Text == "")
            {
                txtToevoegenTicketKlantAchternaam.Text = achternaam;
                txtToevoegenTicketKlantAchternaam.ForeColor = Color.Gray;
            }
        }

        private void txtToevoegenTicketKlantEmail_Enter(object sender, EventArgs e) //Sander Brijer 646235
        {
            if (txtToevoegenTicketKlantEmail.Text == email)
            {
                txtToevoegenTicketKlantEmail.ForeColor = Color.Black;
                txtToevoegenTicketKlantEmail.Text = "";
            }
        }

        private void txtToevoegenTicketKlantEmail_Leave(object sender, EventArgs e) //Sander Brijer 646235
        {
            if (txtToevoegenTicketKlantEmail.Text == "")
            {
                txtToevoegenTicketKlantEmail.Text = email;
                txtToevoegenTicketKlantEmail.ForeColor = Color.Gray;
            }
        }

        private void txtToevoegenTicketKlantSelecteren_TextChanged(object sender, EventArgs e) //Sander Brijer 646235
        {
            FilterKlantCB(cBToevoegenTicketKlantSelecteren, txtToevoegenTicketKlantSelecteren, false);

            if (cBToevoegenTicketKlantSelecteren.Enabled == false)
            {
                txtToevoegenTicketKlantVoornaam.Text = string.Empty;
                txtToevoegenTicketKlantAchternaam.Text = string.Empty;
                txtToevoegenTicketKlantEmail.Text = string.Empty;
            }
        }



        //NICKY

        public void VulWerknemerlijst() //Nicky Garcia Jorge 619307
        {
            try
            {
                lvWerknemers.Items.Clear();
                List<Medewerker> medewerkersLijst = medewerker_Service.VerkrijgAlleMedewerkers();

                foreach (Medewerker medewerker in medewerkersLijst)
                {
                    ListViewItem item = new ListViewItem(medewerker.Gebruikersnaam);
                    item.SubItems.Add(medewerker.VolledigeNaam);
                    item.SubItems.Add(medewerker.Email);
                    lvWerknemers.Items.AddRange(new ListViewItem[] { item });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ophalen van medewerkers vanuit de database is niet mogelijk.");
            }
        }

        private void ClearText() //Nicky Garcia Jorge 619307
        {
            txtToevoegAchternaam.Clear();
            txtToevoegEmail.Clear();
            txtToevoegGebruikersnaam.Clear();
            txtToevoegVoornaam.Clear();
            txtToevoegWachtwoord.Clear();
        }

        private void btnWWvergeten_Click(object sender, EventArgs e)
        {
            //Resetpassword resetPassword = new Resetpassword();
            //resetPassword.ShowDialog();
        }

        private void btnWerknemer_Click_1(object sender, EventArgs e)
        {
            WerknemerToevoegenScherm();
        }

        private void WerknemerToevoegenScherm()
        {
            comboCategorie.SelectedIndex = 0;
            huidigePanel = pnlWerknemer;
            AllePanelsOnzichtbaar();
            pnlWerknemer.Visible = true;
            VulWerknemerlijst();
        }

        //WOUT
        //PANEL OVERZICHT TICKETS!!
        private void btnSluit_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Je staat op het punt deze ticket te sluiten.", "", MessageBoxButtons.OKCancel);
            Ticket ticket;
            ObjectId ticketid;
            int ticketindex = 0;
            if (dialogResult == DialogResult.OK)
            {
                try
                {
                    ticketindex = lVTicketInfo.Items.IndexOf(lVTicketInfo.SelectedItems[0]);
                }
                catch
                {
                    MessageBox.Show("Er is geen ticket geselecteerd!");
                    return;
                }
                if (ticketsGefilterd != null)
                {
                    ticket = ticketsGefilterd.ElementAt(ticketindex);
                    ticketid = ticket._id;
                }
                else
                {
                    ticket = ticketList.ElementAt(ticketindex);
                    ticketid = ticket._id;
                }


                ticket_Service.CloseTicket(ticketid);
                OverzichtTicketsFilteren();
            }
            //RefillTicke
        }
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Je staat op het punt deze ticket te heropenen.", "", MessageBoxButtons.OKCancel);
            Ticket ticket;
            ObjectId ticketid;
            int ticketindex = 0;
            if (dialogResult == DialogResult.OK)
            {
                try
                {
                    ticketindex = lVTicketInfo.Items.IndexOf(lVTicketInfo.SelectedItems[0]);
                }
                catch
                {
                    MessageBox.Show("Er is geen ticket geselecteerd!");
                    return;
                }
                if (ticketsGefilterd != null)
                {
                    ticket = ticketsGefilterd.ElementAt(ticketindex);
                    ticketid = ticket._id;
                }
                else
                {
                    ticket = ticketList.ElementAt(ticketindex);
                    ticketid = ticket._id;
                }

                ticket_Service.ReopenTicket(ticketid);
                OverzichtTicketsFilteren();
            }
            //RefillTickets();
        }

        private void btnEscaleer_Click(object sender, EventArgs e)
        {
            int ticketindex = 0;
            string warning = "";
            try
            {
                warning = lVTicketInfo.SelectedItems[0].Text;
                ticketindex = lVTicketInfo.Items.IndexOf(lVTicketInfo.SelectedItems[0]);
            }
            catch
            {
                MessageBox.Show("Er is geen ticket geselecteerd!");
                return;
            }
            Ticket ticket;
            ObjectId ticketID;
            if (ticketsGefilterd != null)
            {
                ticket = ticketsGefilterd.ElementAt(ticketindex);
                ticketID = ticket._id;
            }
            else
            {
                ticket = ticketList.ElementAt(ticketindex);
                ticketID = ticket._id;
            }
            TopDeskApp topdesk = this;
            EscaleerPopUp popup = new EscaleerPopUp(warning, ticketID, topdesk);

            popup.Show();
            popup.Categorie();
        }
        //Wout
        //Wout Dashboard
        private void FillPieTicket()//Wout de Roy van Zuydewijn 648184
        {
            int countGesloten = 0;
            int countOpen = 0;
            chTickets.Titles.Clear();
            chTickets.Series["Tickets"].Points.Clear();
            ticketList = new List<Ticket>();
            ticketList = ticket_Service.VerkrijgAlleTickets();

            chTickets.Titles.Add("Verhouding alle tickets");
            foreach (Ticket ticket in ticketList)
            {
                if (ticket.Status == "Open") countOpen++;
                else if (ticket.Status == "Gesloten") countGesloten++;

            }

            chTickets.Series["Tickets"].Points.AddXY($"Open ({countOpen})", countOpen);
            chTickets.Series["Tickets"].Points.AddXY($"Gesloten ({countGesloten})", countGesloten);
            chTickets.Series["Tickets"].Points[0].Color = Color.Green;
            chTickets.Series["Tickets"].Points[1].Color = Color.Red;
            chTickets.Series["Tickets"]["PieLabelStyle"] = "Disabled";
        }

        private void FillPiePersonalizedtickets()//Wout de Roy van Zuydewijn 648184
        {
            try
            {


                string medewerkerid = ingelogdeMedewerker._id.ToString();
                List<Ticket> lijst = ticket_Service.KrijgMedewerkerTickets(medewerkerid);

                int countGesloten = 0;
                int countOpen = 0;
                chPieGepersonaliseerd.Titles.Clear();
                foreach (var series in chPieGepersonaliseerd.Series) series.Points.Clear();
                chPieGepersonaliseerd.Titles.Add($"Verhouding tickets van {ingelogdeMedewerker.VolledigeNaam}");
                foreach (Ticket ticket in lijst)
                {
                    if (ticket.Status == "Open") countOpen++;
                    else if (ticket.Status == "Gesloten") countGesloten++;

                }

                chPieGepersonaliseerd.Series["Tickets"].Points.AddXY($"Open ({countOpen})", countOpen);
                chPieGepersonaliseerd.Series["Tickets"].Points.AddXY($"Gesloten ({countGesloten})", countGesloten);
                chPieGepersonaliseerd.Series["Tickets"].Points[0].Color = Color.Green;
                chPieGepersonaliseerd.Series["Tickets"].Points[1].Color = Color.Red;
                chPieGepersonaliseerd.Series["Tickets"]["PieLabelStyle"] = "Disabled";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gegevens ophalen mislukt");
            }
        }
        private void FillPieUrgentiePie()//Wout de Roy van Zuydewijn 648184
        {
            try
            {

                int incident = 0;
                int laag = 0;
                int midden = 0;
                int kritiek = 0;
                int probleem = 0;
                chUrgentie.Titles.Clear();
                ticketList = new List<Ticket>();
                ticketList = ticket_Service.VerkrijgAlleTickets();

                chUrgentie.Series["Tickets"].Points.Clear();
                chUrgentie.Titles.Add("Urgentie alle tickets");
                foreach (Ticket ticket in ticketList)
                {
                    if (ticket.Categorie == "1 Incident") incident++;
                    else if (ticket.Categorie == "2 Laag") laag++;
                    else if (ticket.Categorie == "3 Midden") midden++;
                    else if (ticket.Categorie == "4 Kritiek") kritiek++;
                    else if (ticket.Categorie == "5 Probleem") probleem++;

                }

                chUrgentie.Series["Tickets"].Points.AddXY($"Incident ({incident})", incident);
                chUrgentie.Series["Tickets"].Points.AddXY($"Laag ({laag})", laag);
                chUrgentie.Series["Tickets"].Points.AddXY($"Midden ({midden})", midden);
                chUrgentie.Series["Tickets"].Points.AddXY($"Kritiek ({kritiek})", kritiek);
                chUrgentie.Series["Tickets"].Points.AddXY($"Probleem ({probleem})", probleem);
                chUrgentie.Series["Tickets"].Points[0].Color = Color.Red;
                chUrgentie.Series["Tickets"].Points[1].Color = Color.Orange;
                chUrgentie.Series["Tickets"].Points[2].Color = Color.Yellow;
                chUrgentie.Series["Tickets"].Points[3].Color = Color.Green;
                chUrgentie.Series["Tickets"].Points[4].Color = Color.Blue;
                chUrgentie.Series["Tickets"]["PieLabelStyle"] = "Disabled";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gegevens ophalen mislukt");
            }

        }
        private void ShowProfile()//Wout de Roy van Zuydewijn 648184
        {
            lblvnaam.Text = ingelogdeMedewerker.Voornaam;
            lblanaam.Text = ingelogdeMedewerker.Achternaam;
            lblmail.Text = ingelogdeMedewerker.Email;
            lblwcat.Text = ingelogdeMedewerker.WerkCategorie;
            if (ingelogdeMedewerker.isAdmin) lblfunc.Text = "Admin";
            else lblfunc.Text = lblfunc.Text = "Werknemer";
        }
        private void ShowDashboard()// Wout de Roy van Zuydewijn 648184
        {
            huidigePanel = pnlDashboard;
            AllePanelsOnzichtbaar();
            ShowProfile();
            FillPieTicket();
            FillPiePersonalizedtickets();
            FillPieUrgentiePie();
            pnlDashboard.Show();
        }
        void StartNaInlogAdmin() //Wout de Roy van Zuydewijn 648184
        {
            btnWerknemer.Visible = true;
            ShowDashboard();
        }

        private void btnVoegWerknemer_Click(object sender, EventArgs e)
        {
            Medewerker werknemer = new Medewerker();

            if (txtToevoegWachtwoord.Text != "" && txtToevoegVoornaam.Text != "" && txtToevoegAchternaam.Text != "" && txtToevoegEmail.Text != "" && txtToevoegGebruikersnaam.Text != "" && txtAntwoord.Text != "" && comboCategorie.SelectedIndex != -1)
            {
                string password = txtToevoegWachtwoord.Text;
                if (IsValidPassword(password) == true)
                {
                    werknemer.Wachtwoord = password;
                    werknemer.Voornaam = txtToevoegVoornaam.Text;
                    werknemer.Achternaam = txtToevoegAchternaam.Text;
                    werknemer.Email = txtToevoegEmail.Text;
                    werknemer.Gebruikersnaam = txtToevoegGebruikersnaam.Text;
                    werknemer.isAdmin = false;
                    werknemer.Antwoord = txtAntwoord.Text;
                    werknemer.WerkCategorie = comboCategorie.Text;
                    try
                    {
                        medewerker_Service.VoegEenMedewerkerToe(werknemer);
                        VulWerknemerlijst();
                        ClearText();
                    }
                    catch
                    {
                        MessageBox.Show("Het toevoegen is niet gelukt.");
                        ClearText();
                    }
                }
                else
                {
                    MessageBox.Show("Het wachtwoord voldoet niet aan de minimumbenodigdheden");
                    ClearText();
                }
            }
            else
            {
                MessageBox.Show("Alle velden moeten gevuld zijn.");
                ClearText();
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

        private void btnWWvergeten_Click_2(object sender, EventArgs e)
        {
            ResetPassword resetPassword = new ResetPassword();
            resetPassword.ShowDialog();
        }

        private void btnOverzichtTicketVerwijderen_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Je staat op het punt een ticket te verwijderen.", "", MessageBoxButtons.OKCancel);
                Ticket ticket;
                ObjectId ticketid;
                if (dialogResult == DialogResult.OK)
                {
                    int ticketindex = lVTicketInfo.Items.IndexOf(lVTicketInfo.SelectedItems[0]);
                    if (ticketsGefilterd != null)
                    {
                        ticket = ticketsGefilterd.ElementAt(ticketindex);
                        ticketid = ticket._id;
                    }
                    else
                    {
                        ticket = ticketList.ElementAt(ticketindex);
                        ticketid = ticket._id;
                    }

                    ticket_Service.VerwijderTicket(ticketid);
                    OverzichtTicketsFilteren();
                }
            }
            catch
            {
                MessageBox.Show("Selecteer een ticket om te verwijderen");
            }
        }

        private void btnArchieveren_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Je staat op het punt een ticket te archieveren.", "", MessageBoxButtons.OKCancel);
                Ticket ticket;
                ObjectId ticketid;
                if (dialogResult == DialogResult.OK)
                {
                    int ticketindex = lVTicketInfo.Items.IndexOf(lVTicketInfo.SelectedItems[0]);
                    if (ticketsGefilterd != null)
                    {
                        ticket = ticketsGefilterd.ElementAt(ticketindex);
                        ticketid = ticket._id;
                    }
                    else
                    {
                        ticket = ticketList.ElementAt(ticketindex);
                        ticketid = ticket._id;
                    }

                    if (ticket.Status == "Gesloten")
                    {
                        ticket_Service.Archieveer(ticket);
                        ticket_Service.VerwijderTicket(ticketid);
                        OverzichtTicketsFilteren();
                    }
                    else
                    {
                        MessageBox.Show("U kunt alleen gesloten tickets archieveren");
                    }
                }
            }
            catch
            {
                MessageBox.Show("Selecteer een ticket om te archiveren");
            }
        }

        private void btnUpdateStart_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateButtons(false);


                Ticket ticket;
                int ticketindex = lVTicketInfo.Items.IndexOf(lVTicketInfo.SelectedItems[0]);

                if (ticketsGefilterd != null)
                {
                    ticket = ticketsGefilterd.ElementAt(ticketindex);
                    updateId = ticket._id;
                }
                else
                {
                    ticket = ticketList.ElementAt(ticketindex);
                    updateId = ticket._id;
                }

                klantenList = klant_Service.VerkrijgAlleKlanten();
                foreach (Klant klant in klantenList)
                {
                    comboUpdateKlant.Items.Add(klant.VolledigeNaam);
                }

                cBOverzichtFilterCategorie.Hide();
                cBOverzichtFilterMedewerker.Hide();
                cBOverzichtFilterStatus.Hide();
                txtOverzichtFilterMedewerker.Hide();
                cBOverzichtFilterKlant.Hide();
                comboUpdateKlant.Text = lVTicketInfo.SelectedItems[0].SubItems[1].Text;
                txtTitel.Text = ticket.Titel;
            }
            catch
            {
                MessageBox.Show("Selecteer een ticket");
                VulListViewTicketInfo(null);
                UpdateButtons(true);
            }
        }

        private void btnUpdateConfirm_Click(object sender, EventArgs e)
        {
            klantenList = klant_Service.VerkrijgAlleKlanten();
            List<string> klantenNamen = new List<string>();

            foreach (Klant klant in klantenList)
            {
                klantenNamen.Add(klant.VolledigeNaam);
            }

            string naam = comboUpdateKlant.SelectedItem.ToString();
            string titel = txtTitel.Text;
            ObjectId klantid;

            if (!klantenNamen.Contains(comboUpdateKlant.Text))
            {
                MessageBox.Show("De gegeven naam hoort niet bij een bestaande klant");
            }

            foreach (Klant klant in klantenList)
            {
                if (naam == klant.VolledigeNaam)
                {
                    klantid = klant._id;
                    try
                    {
                        MessageBox.Show("U staat op het punt de geselecteerde ticket te updaten");
                        ticket_Service.Update(titel, updateId, klantid);
                    }
                    catch
                    {
                        MessageBox.Show("De ticket kan niet worden geupdate");
                    }
                    break;
                }
            }

            VulListViewTicketInfo(null);
            UpdateButtons(true);
        }

        private void btnWerknemerDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Je staat op het punt een medewerker te verwijderen.", "", MessageBoxButtons.OKCancel);
                Medewerker medewerker;
                ObjectId medewerkerid;
                if (dialogResult == DialogResult.OK)
                {
                    int index = lvWerknemers.Items.IndexOf(lvWerknemers.SelectedItems[0]);
                    medewerker = medewerkerList.ElementAt(index);
                    medewerkerid = medewerker._id;

                    medewerker_Service.VerwijderMedewerker(medewerkerid);
                    VulWerknemerlijst();
                    ClearText();
                }
            }
            catch
            {
                MessageBox.Show("Selecteer een medewerker om te verwijderen");
            }
        }
    }
}
