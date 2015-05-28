using AspNet.Identity.MongoDB;
using Defendeu.Entities;
using Defendeu.Repositories;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {

        static IClientRepository repo = new ClientRepository();

        static void Main(string[] args)
        {
            Client c = new Client
            {
                ClientId = "ngAuthApp",
                Secret = "test",
                Name = "AngularJS front-end Application",
                ApplicationType = Defendeu.Entities.Authentication.ApplicationTypes.JavaScript,
                Active = true,
                RefreshTokenLifeTime = 7200,
                AllowedOrigin = "http://localhost:32150"
            };

            repo.Add(c);
        }

     
    }
}
