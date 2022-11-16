using Commisions.Business.Contract;
using Commisions.Business.Data;
using Commisions.Business.Misc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Commisions.Business.Processors
{
    public class TransferDataProcessor : IProcessor
    {
        public string Name => "Transfers";

        public object Process(object data, object conditionalData = null)
        {
            transfers = (Transfers)data;
            users = (Users)conditionalData;

            foreach(var transfer in transfers.List)
            {
                DoProcessTransfer(transfer, users);
            }

            return users;
        }

        private void DoProcessTransfer(Transfer transfer, Users users)
        {
            supervisors = new List<int>();
            ProcessHelper.GetAllSupervisors(transfer.SourceId, users, supervisors);
            supervisors = supervisors.OrderBy(x => x).ToList();

            DivideMoney(transfer.Amount);
        }

        private void DivideMoney(int amount)
        {
            foreach(var supervisor in supervisors)
            {
                if(supervisor == supervisors.Last())
                {
                    users.List
                        .Where(u => u.Id == supervisor)
                        .Single()
                        .AddAmount(amount);
                    break;
                }

                var tmpAmount = (int)Math.Floor((decimal)amount / 2);

                users.List
                    .Where(u => u.Id == supervisor)
                    .Single()
                    .AddAmount(tmpAmount);

                amount -= tmpAmount;
            }
        }

        private List<int> supervisors;
        private Transfers transfers;
        private Users users;
    }
}
