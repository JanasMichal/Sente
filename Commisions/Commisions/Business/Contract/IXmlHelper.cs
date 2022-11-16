using System.Collections.Generic;
using System.Xml;

namespace Commisions.Business.Contract
{
    public interface IXmlHelper
    {
        string Name { get; }
        void ReadXml(XmlNodeList xmlNodes, int parentId = 0);
        IEnumerable<object> ReturnList();
    }
}
