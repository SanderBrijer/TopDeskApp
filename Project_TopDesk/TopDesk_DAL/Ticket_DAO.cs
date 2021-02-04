using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using TopDesk_Model;

namespace TopDesk_DAL
{
    public class Ticket_DAO
    {
        ConfigDataBase configDataBase = new ConfigDataBase();
        Klant_DAO klant_DAO = new Klant_DAO();
        Medewerker_DAO medewerker_DAO = new Medewerker_DAO();

        public List<string> VerkrijgAlleCategorieën() //Sander Brijer 646235
        {
            //Select collection
            // IMongoCollection<string> collection = GetDatabase().GetCollection<string>("TicketCategorieën");
            var collection = configDataBase.GetDatabase().GetCollection<BsonDocument>("TicketCategorieën");
            var categorielist = collection.Find(new BsonDocument()).ToList();
            //List<string> categorieën = collection.Find(Builders<string>.Filter.Empty).ToList();

            List<string> categorieën = new List<string>();
            foreach (var item in categorielist)
            {
                categorieën.Add(item.GetElement("Categorie").Value.ToString());
            }

            return categorieën;
        }

        public List<string> VerkrijgAlleStatussen() //Sander Brijer 646235
        {
            //Select collection
            // IMongoCollection<string> collection = GetDatabase().GetCollection<string>("TicketCategorieën");
            var collection = configDataBase.GetDatabase().GetCollection<BsonDocument>("TicketStatussen");
            var statusList = collection.Find(new BsonDocument()).ToList();
            //List<string> categorieën = collection.Find(Builders<string>.Filter.Empty).ToList();

            List<string> statussen = new List<string>();
            foreach (var item in statusList)
            {
                statussen.Add(item.GetElement("Status").Value.ToString());
            }

            return statussen;
        }


        public List<Ticket> VerkrijgAlleTickets() //Sander Brijer 646235
        {
            ////Select collection
            //IMongoCollection<Ticket> collection = GetDatabase().GetCollection<Ticket>("Tickets");
            //List<Ticket> tickets = collection.AsQueryable().ToList<Ticket>();

            //return tickets;
            //get the incidents from the database
            var collection = configDataBase.GetDatabase().GetCollection<Ticket>("Tickets");
            var incidentList = collection.Find(new BsonDocument()).ToList();


            List<Ticket> incidents = MaakLijst(incidentList);

            //List<BsonDocument> resultList = new List<BsonDocument>();
            //foreach (var item in incidentList)
            //{
                //ObjectId id = item.GetElement("EmployeeID").Value.AsObjectId;
                //var user = GetDatabase().LoadRecordById<BsonDocument>("Users", id);

                //string name;
                //try
                //{
                //    name = user.GetElement("FirstName").Value.ToString();
                //}
                //catch
                //{
                //    name = "Unknown";
                //}

                //Ticket incident = new Ticket()
                //{
                //    _id = item._id,
                //    Titel = item.Titel,
                //    Categorie = item.Categorie,
                //    Status = item.Status,
                //    Notitie = item.Notitie,
                //    Klant = klant_Service.KrijgKlantViaId(item.KlantId.ToString()),
                //    Eigenaar = medewerker_Service.KrijgMedewekerViaId(item.MedewerkerId.ToString())
                //};

                //Ticket incident = new Ticket()
                //{
                //    Titel = item.ToString(),
                //    Categorie = item.GetElement("Categorie").Value.ToString(),
                //    Status = item.GetElement("Status").Value.ToString(),
                //    Notitie = item.GetElement("Notitie").Value.ToString(),
                //    KlantId = item.
                //Klant = klant.KrijgKlantViaId(KlantId)
                //    //Resolved = false,
                //    //Date = item.GetElement("Date").Value.AsDateTime,
                //    //Deadline = item.GetElement("Deadline").Value.AsDateTime,
                //    //EmployeeID = item.GetElement("EmployeeID").Value.AsObjectId,
                //    //TypeOfIncident = item.GetElement("TypeOfIncident").Value.ToString(),
                //    //Description = item.GetElement("Description").Value.ToString(),
                //    //Id = item.GetElement("_id").Value.AsObjectId
                //};
                //incidents.Add(incident);
            //}
            return incidents;
        }

        public List<Ticket> VerkrijgAlleTicketsGesorteerdCategorie(string categorie) //Sander Brijer 646235
        {
            var collection = configDataBase.GetDatabase().GetCollection<Ticket>("Tickets");
            var incidentList = collection.Find(x => x.Categorie == categorie).ToList();


            List<Ticket> incidents = MaakLijst(incidentList);
            return incidents;
        }

        public void VoegEenTicketToe(Ticket ticket) //Sander Brijer 646235
        {
            var collection = configDataBase.GetDatabase().GetCollection<Ticket>("Tickets");
            collection.InsertOne(ticket);
        }

        private List<Ticket> MaakLijst(List<Ticket> incidentList) //Sander Brijer 646235
        {
            List<Ticket> incidents = new List<Ticket>();
            foreach (var item in incidentList)
            {
                Ticket incident = new Ticket()
                {
                    _id = item._id,
                    Titel = item.Titel,
                    Categorie = item.Categorie,
                    Status = item.Status,
                    KlantId = item.KlantId,
                    //Klant = klant_DAO.KrijgKlantViaId(item.KlantId.ToString()),
                    MedewerkerId = item.MedewerkerId,
                    //Eigenaar = medewerker_DAO.KrijgMedewerkerViaId(item.MedewerkerId.ToString()),
                    Beschrijving = item.Beschrijving
                };
                incidents.Add(incident);
            }
            return incidents;
        }
        public List<Ticket> VerkrijgAlleTicketsFilter(string titel, string categorie, string status, Medewerker medewerker, Klant klant) //Sander Brijer 646235
        {

            ////var filter1 = Builders<Ticket>.Filter.Eq(x => x.Categorie, categorie);

            ////var filter2 = Builders<Ticket>.Filter.Eq(x => x.Categorie, status);
            //////var filter3 = Builders<Ticket>.Filter.Eq(x => x.MedewerkerId, medewerker._id);
            //////var filter4 = Builders<Ticket>.Filter.Eq(x => x.KlantId, klant._id);
            //var items = new List<object>();
            //List<string> queries = new List<string>();
            //if (categorie != null && categorie.Length != 0)
            //{
            //    items.Add(categorie);
            //    queries.Add($"'Categorie' : '{categorie}'");
            //}

            //if (status != null && status.Length != 0)
            //{
            //    items.Add(status);
            //    queries.Add($"'Status' : '{status}'");
            //}

            //if (medewerker != null)
            //{
            //    items.Add(medewerker.VolledigeNaam);
            //    queries.Add($"'Medewerker' : '{medewerker._id}'");
            //}

            //if (klant != null)
            //{
            //    items.Add(klant.VolledigeNaam);
            //    queries.Add($"'Klant' : '{klant._id}'");
            //}

            ////var combineFilters = Builders<Ticket>.Filter.And(filter1, filter2);

            //var collection = configDataBase.GetDatabase().GetCollection<Ticket>("Tickets");
            //string helequery = "";
            ////string query = "{ 'Categorie' : 'testdb', 'ok' : '1.0' }";
            //string queryBegin = "{ ";

            //string queryEind = " }";

            //helequery += queryBegin;

            //int toegevoegd = 0;
            //for (int i = 0; i < items.Count; i++)
            //{
            //    if (toegevoegd == 0)
            //        helequery += queries[i];
            //    else
            //        helequery += ", " + queries[i];

            //    toegevoegd++;
            //}
            //helequery += queryEind;

            ////var incidentList = collection.Find(new BsonDocument()).Project(helequery).ToList();
            //var docs = collection.Find(new BsonDocument()).Project(helequery).ToList();

            //var incidentList = collection.Find(new BsonDocument()).ToList();

            ////List<Ticket> incidents = new List<Ticket>();
            //////List<BsonDocument> resultList = new List<BsonDocument>();
            ////foreach (var item in docs)
            ////{
            ////    Ticket incident = new Ticket()
            ////    {
            ////        Titel = item.GetElement("Titel").Value.ToString(),
            ////        Categorie = item.GetElement("Categorie").Value.ToString(),
            ////        Status = item.GetElement("Status").Value.ToString(),
            ////    };
            ////    incidents.Add(incident);
            ////}
            //// return incidents;

            //// return null;


            //var builder = Builders<Ticket>.Filter;

            //// Not sure if this is the correct way to initialize it because it seems adding an empty filter condition returning ALL document;
            //IList<FilterDefinition<Ticket>> filters = new List<FilterDefinition<Ticket>>();

            //FilterDefinition<Ticket> filter = new BsonDocument();

            //if (categorie != null && categorie.Length != 0 && categorie.Any())
            //{
            //    filters.Add(categorie
            //                .Select(p => builder.Eq("Categorie", categorie))
            //                .Aggregate((p1, p2) => p1 | p2));
            //}
            
            //if (status != null && status.Length != 0 && status.Any())
            //{
            //    filters.Add(status
            //                .Select(p => builder.Eq("Status", status))
            //                .Aggregate((p1, p2) => p1 | p2));
            //}

            //if (medewerker != null && medewerker._id.ToString().Any())
            //{
            //    filters.Add(medewerker._id.ToString()
            //                .Select(p => builder.Eq("MedewerkerId", medewerker._id))
            //                .Aggregate((p1, p2) => p1 | p2));
            //}

            //if (klant != null && klant._id.ToString().Any())
            //{
            //    filters.Add(klant._id.ToString()
            //                .Select(p => builder.Eq("KlantId", klant._id))
            //                .Aggregate((p1, p2) => p1 | p2));
            //}

            //var filterConcat = builder.And(filters);
            //List<Ticket> ticket = collection.Find(filter).ToList();


            IList<FilterDefinition<Ticket>> filtersList = new List<FilterDefinition<Ticket>>();
            if (titel != null && titel.Length != 0)
            {
                filtersList.Add(new BsonDocument("Titel", titel));
            }
            if (categorie != null && categorie.Length != 0)
            {
                filtersList.Add(new BsonDocument("Categorie", categorie));
            }

            if (status != null && status.Length != 0)
            {
                filtersList.Add(new BsonDocument("Status", status));
            }

            if (medewerker != null)
            {
                filtersList.Add(new BsonDocument("MedewerkerId", medewerker._id));
            }

            if (klant != null)
            {
                filtersList.Add(new BsonDocument("KlantId", klant._id));
            }

            var collection = configDataBase.GetDatabase().GetCollection<Ticket>("Tickets");
            var builder = Builders<Ticket>.Filter;
            if (filtersList.Count > 0)
            {
                var gezamelijkeFilters = builder.And(filtersList);
                List<Ticket> ticketList = collection.Find(gezamelijkeFilters).ToList();
                return ticketList;
            }
            else
            {
                return null;
            }
        }

        //var filter2 = Builder.Eq(x => x.Status, "open") & Builder.Eq(x => x.UserId, userId);

        //Wout
        //Wout Ticket overzicht
        public void CloseTicket(ObjectId objectID) //Wout de Roy van Zuydewijn 648184
        {
            IMongoCollection<Ticket> collection = configDataBase.GetDatabase().GetCollection<Ticket>("Tickets");
            var filter = Builders<Ticket>.Filter.Eq(x => x._id, objectID);
            Ticket ticket = collection.Find(filter).FirstOrDefault();
            var update = Builders<Ticket>.Update.Set("Status", "Gesloten");
            collection.UpdateOne(filter, update);
        }

        public void ReopenTicket(ObjectId objectID) //Wout de Roy van Zuydewijn 648184
        {
            IMongoCollection<Ticket> collection = configDataBase.GetDatabase().GetCollection<Ticket>("Tickets");
            var filter = Builders<Ticket>.Filter.Eq(x => x._id, objectID);
            Ticket ticket = collection.Find(filter).FirstOrDefault();
            var update = Builders<Ticket>.Update.Set("Status", "Open");
            collection.UpdateOne(filter, update);
        }

        public void EscaleerTicket(ObjectId medewerkerID, ObjectId ticketID)// Wout de Roy van Zuydewijn 648184
        {
            var collection = configDataBase.GetDatabase().GetCollection<Ticket>("Tickets");
            var medewerkercollection = configDataBase.GetDatabase().GetCollection<Medewerker>("Medewerkers");

            var medewerkerfilter = Builders<Medewerker>.Filter.Eq(x => x._id, medewerkerID);
            Medewerker medewerker = medewerkercollection.Find(medewerkerfilter).FirstOrDefault();

            var filter = Builders<Ticket>.Filter.Eq(x => x._id, ticketID);
            var updateid = Builders<Ticket>.Update.Set("MedewerkerId", medewerkerID);
            var updatecategorie = Builders<Ticket>.Update.Set("Categorie", medewerker.WerkCategorie);

            collection.UpdateOne(filter, updatecategorie);
            collection.UpdateOne(filter, updateid);
        }
        //Wout Dashboard
        public List<Ticket> KrijgMedewerkerTickets(string medewerkerid) // Wout de Roy van Zuydewijn
        {
            var collection = configDataBase.GetDatabase().GetCollection<Ticket>("Tickets");

            var ticketlist = collection.Find(x => x.MedewerkerId == ObjectId.Parse(medewerkerid)).ToList();

            List<Ticket> ticketsgepersonaliseerd = MaakLijst(ticketlist);
            return ticketsgepersonaliseerd;

        }

        //Nicky
        public void VerwijderTicket(ObjectId ticketid)
        {
            var collection = configDataBase.GetDatabase().GetCollection<BsonDocument>("Tickets");
            var deleteFilter = Builders<BsonDocument>.Filter.Eq("_id", ticketid);
            collection.DeleteOne(deleteFilter);
        }

        public void Archieveer(Ticket ticket)
        {
            var collection = configDataBase.GetDatabase().GetCollection<Ticket>("Gearchieveerde tickets ");
            collection.InsertOne(ticket);
        }

        public void Update(string titel, ObjectId ticketid, ObjectId klantid)
        {
            var collection = configDataBase.GetDatabase().GetCollection<Ticket>("Tickets");
            var filter = Builders<Ticket>.Filter.Eq("_id", ticketid);
            var update = Builders<Ticket>.Update.Set("Titel", titel);
            var update2 = Builders<Ticket>.Update.Set("KlantId", klantid);
            collection.UpdateOne(filter, update);
            collection.UpdateOne(filter, update2);
        }
    }
}
