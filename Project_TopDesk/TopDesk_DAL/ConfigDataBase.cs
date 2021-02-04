using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using System.Configuration;
using TopDesk_Model;
using MongoDB.Bson;
using System.Security;
using System.Net;

namespace TopDesk_DAL
{
    public class ConfigDataBase //Sander Brijer 646235
    {


        private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["MongoDB"].ToString();
        private MongoClient dbClient = new MongoClient(_connectionString);
        public IMongoDatabase GetDatabase() { return dbClient.GetDatabase("TheGardenGroup_Database"); }


    }
}
