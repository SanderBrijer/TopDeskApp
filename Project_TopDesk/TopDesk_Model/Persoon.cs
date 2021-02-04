using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace TopDesk_Model
{
    public class Persoon //Sander Brijer 646235
    {
        public ObjectId _id { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public string VolledigeNaam { get { return Voornaam + " " + Achternaam; } }

        public string Email { get; set; }
    }
}
