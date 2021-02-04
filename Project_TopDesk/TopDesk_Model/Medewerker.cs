using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace TopDesk_Model
{
    public class Medewerker : Persoon //Sander Brijer 646235
    {
        public string Gebruikersnaam { get; set; }
        public string Wachtwoord { get; set; }
        public string Antwoord { get; set; }
        public string WerkCategorie { get; set; }
        public bool isAdmin { get; set; }
    }
}
