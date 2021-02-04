using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using TopDesk_DAL;
using TopDesk_Model;

namespace TopDesk_Logic
{
    public class Medewerker_Service
    {

        ConfigDataBase configDataBase = new ConfigDataBase();
        Medewerker_DAO medewerker_DAO = new Medewerker_DAO();

        public Medewerker KrijgMedewekerViaId(string medewerkerId) //Sander Brijer 646235
        {
            //Select collection
            var collection = configDataBase.GetDatabase().GetCollection<Medewerker>("Medewerkers");

            var filter_id = Builders<Medewerker>.Filter.Eq("_id", ObjectId.Parse(medewerkerId));
            var entity = collection.Find(filter_id).FirstOrDefault();
            Medewerker medewerker = entity;

            return medewerker;
        }

        public Medewerker CheckUser(string email, SecureString password) //Sander Brijer 646235
        {
            return medewerker_DAO.CheckUser(email, password);
        }

        public List<Medewerker> VerkrijgAlleMedewerkers() //Sander Brijer 646235
        {
            List<Medewerker> medewerkers = medewerker_DAO.VerkrijgAlleMedewerkers();
            medewerkers = medewerkers.OrderBy(m => m.VolledigeNaam).ToList();
            return medewerkers;
        }

        public Medewerker KrijgMedewerkerViaId(string medewerkerNummer) //Sander Brijer 646235
        {
            return medewerker_DAO.KrijgMedewerkerViaId(medewerkerNummer);
        }

        public void VoegEenMedewerkerToe(Medewerker medewerker)
        {
            medewerker_DAO.VoegEenMedewerkerToe(medewerker);
        }

        //Wout
        //Escaleer popup
        public List<Medewerker> VerkrijgEscaleerMedewerkers(string werkcategorie)// Wout de Roy van Zuydewijn 648184
        {
            return medewerker_DAO.VerkrijgEscaleerMedewerkers(werkcategorie);
        }


        //Nicky

        public void UpdateWachtwoord(string gebruiker, string wachtwoord)
        {
            medewerker_DAO.UpdateWachtWoord(gebruiker, wachtwoord);
        }
        public void VerwijderMedewerker(ObjectId id)
        {
            medewerker_DAO.VerwijderMedewerker(id);
        }
    }
}
