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
    public class Klant_DAO
    {
        ConfigDataBase configDataBase = new ConfigDataBase();

        public List<Klant> db_VerkrijgAlleKlanten() //Sander Brijer 646235
        {
            var collection = configDataBase.GetDatabase().GetCollection<Klant>("Klanten");
            var klantenlijst = collection.Find(new BsonDocument()).ToList();

            List<Klant> klanten = new List<Klant>();


            foreach (var item in klantenlijst)
            {
                Klant klant = new Klant()
                {
                    _id = item._id,
                    Voornaam = item.Voornaam,
                    Achternaam = item.Achternaam,
                    Email = item.Email
                    //Voornaam = item.GetElement("Voornaam").Value.ToString(),
                    //Achternaam = item.GetElement("Achternaam").Value.ToString(),
                    //Email = item.GetElement("Email").Value.ToString(),
                };
                klanten.Add(klant);
            }

            klanten = klanten.OrderBy(klant => klant.VolledigeNaam).ToList();
            return klanten;
        }

        public Klant db_VerkrijgKlantViaVoornaamAchternaamEmail(string voornaam, string achternaam, string email) //Sander Brijer 646235
        {
            //Select collection
            IMongoCollection<Klant> collection = configDataBase.GetDatabase().GetCollection<Klant>("Klanten");
            List<Klant> list = collection.AsQueryable().ToList<Klant>();
            //Count documents (select)
            var filter = Builders<Klant>.Filter.Eq(x => x.Voornaam, voornaam) & Builders<Klant>.Filter.Eq(x => x.Achternaam, achternaam) & Builders<Klant>.Filter.Eq(x => x.Email, email);
            Klant klant = collection.Find(filter).FirstOrDefault();
            return klant;
        }

        public void db_VoegEenKlantToe(Klant klant) //Sander Brijer 646235
        {
            var collection = configDataBase.GetDatabase().GetCollection<Klant>("Klanten");
            collection.InsertOne(klant);
        }

        public Klant KrijgKlantViaId(string klantnummer) //Sander Brijer 646235
        {
            var collection = configDataBase.GetDatabase().GetCollection<Klant>("Klanten");

            var filter_id = Builders<Klant>.Filter.Eq("_id", ObjectId.Parse(klantnummer));
            var entity = collection.Find(filter_id).FirstOrDefault();
            Klant klant = entity;
            return klant;
        }


        public Klant db_KrijgKlantViaEmail(string emailadress) //Sander Brijer 646235
        {
            var collection = configDataBase.GetDatabase().GetCollection<Klant>("Klanten");

            var filter = Builders<Klant>.Filter.Eq("Email", emailadress);
            var entity = collection.Find(filter).FirstOrDefault();
            Klant klant = entity;
            return klant;
        }
    }
}
