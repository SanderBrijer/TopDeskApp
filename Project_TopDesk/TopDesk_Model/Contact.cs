using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDesk_Model
{
    public class Contact
    {
        public int TicketId { get; set; }
        public String Contactbericht { get; set; }
        public Persoon Zender { get; set; }
    }
}
