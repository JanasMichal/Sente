using Commisions.Business.Contract;
using Commisions.Business.Data;
using System.Collections.Generic;
using System.Xml;

namespace Commisions.Business.Misc
{
    public class UsersXmlHelper : IXmlHelper
    {
        public string Name => "Users";

        public void ReadXml(XmlNodeList xmlNodes, int parentId = 0)
        {
            foreach(XmlNode node in xmlNodes)
            {
                var id = GetIdAttribute(node);

                users.Add(new User()
                {
                    Id = id,
                    SupervisorId = parentId
                });

                ReadXml(node.ChildNodes, id);
            }
        }

        public IEnumerable<object> ReturnList()
        {
            return users;
        }

        private int GetIdAttribute(XmlNode node)
        {
            return int.Parse(node.Attributes.GetNamedItem("id").Value);
        }

        private List<User> users = new();
    }
}
