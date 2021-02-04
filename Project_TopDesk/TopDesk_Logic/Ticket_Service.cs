using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using TopDesk_DAL;
using TopDesk_Model;

namespace TopDesk_Logic
{
    public class Ticket_Service
    {
        ConfigDataBase configDataBase = new ConfigDataBase();
        Klant_Service klant_Service = new Klant_Service();
        Ticket_DAO ticket_DAO = new Ticket_DAO();
        Medewerker_Service medewerker_Service = new Medewerker_Service();

        public List<Ticket> LoadAlleTicketsFromSort(string sort) //Sander Brijer 646235
        {
            List<Ticket> tickets = new List<Ticket>();
            tickets = VerkrijgAlleTicketsGesorteerdCategorie(sort);

            return tickets;
        }

        public List<string> VerkrijgAlleCategorieën() //Sander Brijer 646235
        {
            List<string> categorieën;
            categorieën = ticket_DAO.VerkrijgAlleCategorieën();
            categorieën.Sort();
            return categorieën;
        }

        public List<string> VerkrijgAlleStatussen() //Sander Brijer 646235
        {
            List<string> statussen;
            statussen = ticket_DAO.VerkrijgAlleStatussen();
            statussen.Sort();
            return statussen;
        }

        public List<Ticket> VerkrijgAlleTickets() //Sander Brijer 646235
        {
            return ticket_DAO.VerkrijgAlleTickets();
        }

        public List<Ticket> VerkrijgAlleTicketsGesorteerdCategorie(string categorie) //Sander Brijer 646235
        {
            var collection = configDataBase.GetDatabase().GetCollection<Ticket>("Tickets");
            var incidentList = collection.Find(x => x.Categorie == categorie).ToList();



            List<Ticket> incidents = new List<Ticket>();
            foreach (var item in incidentList)
            {
                Ticket incident = new Ticket()
                {
                    _id = item._id,
                    Titel = item.Titel,
                    Categorie = item.Categorie,
                    Status = item.Status,
                    Beschrijving = item.Beschrijving,
                    KlantId = item.KlantId,
                    MedewerkerId = item.MedewerkerId
                };
                incidents.Add(incident);
            }
            return incidents;
        }

        public void VoegEenTicketToe(Ticket ticket) //Sander Brijer 646235
        {
            ticket_DAO.VoegEenTicketToe(ticket);
        }

        public List<Ticket> VerkrijgAlleTicketsFilter(string titel, string categorie, string status, Medewerker medewerker, Klant klant) //Sander Brijer 646235
        {
            return ticket_DAO.VerkrijgAlleTicketsFilter(titel, categorie, status, medewerker, klant);
        }

        //Wout
        //Wout overzicht tickets

        public void EscaleerTicket(ObjectId medewerkerID, ObjectId ticketID)//Wout de Roy van Zuydewijn 648184
        {
            ticket_DAO.EscaleerTicket(medewerkerID, ticketID);
        }

        public void CloseTicket(ObjectId ObjectID) //Wout de Roy van Zuydewijn 648184
        {
            ticket_DAO.CloseTicket(ObjectID);
        }
        public void ReopenTicket(ObjectId ObjectID) //Wout de Roy van Zuydewijn 648184
        {
            ticket_DAO.ReopenTicket(ObjectID);
        }
        //Wout Dashboard
        public List<Ticket> KrijgMedewerkerTickets(string medewerkerid)//Wout de Roy van Zuydewijn 648184
        {
            return ticket_DAO.KrijgMedewerkerTickets(medewerkerid);
        }

        //Nicky
        public void VerwijderTicket(ObjectId ticketid)
        {
            ticket_DAO.VerwijderTicket(ticketid);
        }
        public void Archieveer(Ticket ticket)
        {
            ticket_DAO.Archieveer(ticket);
        }
        public void Update(string titel, ObjectId ticketid, ObjectId klantid)
        {
            ticket_DAO.Update(titel, ticketid, klantid);
        }
    }
}
