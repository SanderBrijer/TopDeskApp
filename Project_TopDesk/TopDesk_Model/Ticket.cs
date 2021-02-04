using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace TopDesk_Model
{
    public class Ticket // Sander Brijer 646235
    {
        public ObjectId _id { get; set; }
        public ObjectId KlantId { get; set; }
        public ObjectId MedewerkerId { get; set; }
        public string Titel { get; set; }
        public string Beschrijving { get; set; }

        public string Categorie { get; set; }
        public string Status { get; set; }
        public string Aanmaakdatumtijd { get; set; }
        public ObjectId AangemaaktDoorId { get; set; }
    }
}
