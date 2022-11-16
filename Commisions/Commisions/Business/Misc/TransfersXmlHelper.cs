using Commisions.Business.Contract;
using System.Collections.Generic;
using Commisions.Business.Data;
using System.Xml;

namespace Commisions.Business.Misc
{
    internal class TransfersXmlHelper : IXmlHelper
    {
        public string Name => "Transfers";

        public void ReadXml(XmlNodeList xmlNodes, int parentId)
        {
            foreach(XmlNode node in xmlNodes)
            {
                var result = GetAttributes(node);

                transfers.Add(new Transfer()
                {
                    SourceId = result.id,
                    Amount = result.amount
                });
            }
        }

        public IEnumerable<object> ReturnList()
        {
            return transfers;
        }

        private (int id, int amount) GetAttributes(XmlNode node)
        {
            int id = int.Parse(node.Attributes.GetNamedItem("od").Value);
            int amount = int.Parse(node.Attributes.GetNamedItem("kwota").Value);

            return (id, amount);
        }

        private List<Transfer> transfers = new();
    }
}
