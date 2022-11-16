using System.Collections.Generic;

namespace Commisions.Business.Data
{
    public class Transfers
    {
        public Transfers()
        {
            List = new List<Transfer>();
        }

        public List<Transfer> List { get; set; }
    }
    public class Transfer
    {
        public int SourceId { get; set; }
        public int Amount { get; set; }
    }
}
