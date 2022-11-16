using Commisions.Business.Contract;
using Commisions.Business.Data;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Commisions.Business
{
    public class Managment : IManagment
    {
        public Managment(
            IEnumerable<IXmlHelper> xmlHelpers,
            IEnumerable<IProcessor> processors)
        {
            this.xmlHelpers = xmlHelpers;
            this.processors = processors;
        }

        public void Run()
        {
            PrepareData();
            ProcessData();
            PrintResult();
        }

        private void ProcessData()
        {
            ProcessFor("Transfers");
            ProcessFor("Users");
        }

        private void ProcessFor(string type)
        {
            var service = processors.Where(x => x.Name == type).Single();

            switch(type)
            {
                case "Users":
                    users = (Users)service.Process(users);
                    break;
                case "Transfers":
                    users = (Users)service.Process(transfers, users);
                    break;
            }
        }

        private void PrepareData()
        {
            PrepareFor("Users");
            PrepareFor("Transfers");
        }

        private void PrepareFor(string type)
        {
            XmlDocument xml = new XmlDocument();

            xml.Load($"{type}.xml");

            var service = xmlHelpers.Where(x => x.Name == type).Single();

            service.ReadXml(xml.DocumentElement.ChildNodes);

            switch(type)
            {
                case "Users":
                    users = new Users
                    {
                        List = (List<User>)service.ReturnList()
                    };
                    break;
                case "Transfers":
                    transfers = new Transfers
                    {
                        List = (List<Transfer>)service.ReturnList()
                    };
                    break;
            }
        }

        private void PrintResult()
        {
            foreach(var user in users.List)
                System.Console.WriteLine(user.ToString());
        }


        private Users users;
        private Transfers transfers;
        private readonly IEnumerable<IProcessor> processors;
        private readonly IEnumerable<IXmlHelper> xmlHelpers;
    }
}
