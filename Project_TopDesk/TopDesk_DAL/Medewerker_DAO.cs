using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using TopDesk_Model;

namespace TopDesk_DAL
{
    public class Medewerker_DAO
    {
        ConfigDataBase configDataBase = new ConfigDataBase();

        public Medewerker CheckUser(string username, SecureString password) //Sander Brijer 646235
        {
            //Select collection
            IMongoCollection<Medewerker> collection = configDataBase.GetDatabase().GetCollection<Medewerker>("Medewerkers");
            List<Medewerker> list = collection.AsQueryable().ToList<Medewerker>();
            //Count documents (select)
            var filter = Builders<Medewerker>.Filter.Eq(x => x.Gebruikersnaam, username) & Builders<Medewerker>.Filter.Eq(x => x.Wachtwoord, new NetworkCredential("", password).Password);
            var select = Builders<Medewerker>.Projection.Include(x => x.Wachtwoord);
            Medewerker user = collection.Find(filter).FirstOrDefault();
            return user;
        }


        public List<Medewerker> VerkrijgAlleMedewerkers() //Sander Brijer 646235
        {
            var collection = configDataBase.GetDatabase().GetCollection<Medewerker>("Medewerkers");
            var medewerkerlijst = collection.Find(new BsonDocument()).ToList();

            List<Medewerker> medewerkers = new List<Medewerker>();


            foreach (var item in medewerkerlijst)
            {
                Medewerker medewerker = new Medewerker()
                {
                    _id = item._id,
                    Gebruikersnaam = item.Gebruikersnaam,
                    Voornaam = item.Voornaam,
                    Achternaam = item.Achternaam,
                    Antwoord = item.Antwoord,
                    Wachtwoord = item.Wachtwoord,
                    WerkCategorie = item.WerkCategorie,
                    isAdmin = item.isAdmin,
                    Email = item.Email
                    //Voornaam = item.GetElement("Voornaam").Value.ToString(),
                    //Achternaam = item.GetElement("Achternaam").Value.ToString(),
                    //Email = item.GetElement("Email").Value.ToString(),
                };
                medewerkers.Add(medewerker);
            }
            return medewerkers;
        }

        public Medewerker KrijgMedewerkerViaId(string medewerkerNummer) //Sander Brijer 646235
        {
            var collection = configDataBase.GetDatabase().GetCollection<Medewerker>("Medewerkers");

            var filter_id = Builders<Medewerker>.Filter.Eq("_id", ObjectId.Parse(medewerkerNummer));
            var entity = collection.Find(filter_id).FirstOrDefault();
            Medewerker medewerker = entity;
            return medewerker;
        }


        //WOUT
        //Escaleer popup

        public List<Medewerker> VerkrijgEscaleerMedewerkers(string werkcategorie)// Wout de Roy van Zuydewijn
        {
            IMongoCollection<Medewerker> collection = configDataBase.GetDatabase().GetCollection<Medewerker>("Medewerkers");
            var filter = Builders<Medewerker>.Filter.Eq(x => x.WerkCategorie, werkcategorie);
            var medewerkerlijst = collection.Find(filter).ToList();

            List<Medewerker> medewerkers = new List<Medewerker>();

            foreach (var item in medewerkerlijst)
            {
                Medewerker medewerker = new Medewerker()
                {
                    _id = item._id,
                    Gebruikersnaam = item.Gebruikersnaam,
                    Voornaam = item.Voornaam,
                    Achternaam = item.Achternaam,
                    Antwoord = item.Antwoord,
                    isAdmin = item.isAdmin,
                    Email = item.Email

                };
                medewerkers.Add(medewerker);
            }
            return medewerkers;

        }

        //Nicky
        public void VoegEenMedewerkerToe(Medewerker medewerker) //Nicky Garcia Jorge 619307
        {
            var collection = configDataBase.GetDatabase().GetCollection<Medewerker>("Medewerkers");
            collection.InsertOne(medewerker);
        }
        public void UpdateWachtWoord(string gebruiker, string wachtwoord)
        {
            var collection = configDataBase.GetDatabase().GetCollection<BsonDocument>("Medewerkers");
            var filter = Builders<BsonDocument>.Filter.Eq("Gebruikersnaam", gebruiker);
            var update = Builders<BsonDocument>.Update.Set("Wachtwoord", wachtwoord);
            collection.UpdateOne(filter, update);
        }
        public void VerwijderMedewerker(ObjectId id)
        {
            var collection = configDataBase.GetDatabase().GetCollection<Medewerker>("Medewerkers");
            var deleteFilter = Builders<Medewerker>.Filter.Eq("_id", id);
            collection.DeleteOne(deleteFilter);
        }
    }
}
