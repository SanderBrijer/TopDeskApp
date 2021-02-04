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
    public class Klant_Service
    {

        ConfigDataBase configDataBase = new ConfigDataBase();
        Klant_DAO klant_DAO = new Klant_DAO();

        public Klant KrijgKlantViaId(string klantNummer) //Sander Brijer 646235
        {
            return klant_DAO.KrijgKlantViaId(klantNummer);
        }

        public Klant KrijgKlantViaEmail(string emailadress) //Sander Brijer 646235
        {
            return klant_DAO.db_KrijgKlantViaEmail(emailadress);
        }

        public Klant KrijgKlantViaVoorAchterNaamEmail(string voornaam, string achternaam, string email) //Sander Brijer 646235
        {
            return klant_DAO.db_VerkrijgKlantViaVoornaamAchternaamEmail(voornaam, achternaam, email);
        }

        public List<Klant> VerkrijgAlleKlanten() //Sander Brijer 646235
        {
            return klant_DAO.db_VerkrijgAlleKlanten();
        }

        public void VoegEenKlantToe(Klant klant) //Sander Brijer 646235
        {
            klant_DAO.db_VoegEenKlantToe(klant);
        }
    }
}
