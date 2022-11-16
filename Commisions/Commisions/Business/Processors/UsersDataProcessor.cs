using Commisions.Business.Contract;
using Commisions.Business.Data;
using Commisions.Business.Misc;
using System.Collections.Generic;
using System.Linq;

namespace Commisions.Business.Processors
{
    public class UsersDataProcessor : IProcessor
    {
        public string Name => "Users";

        public object Process(object data, object conditionalData = null)
        {
            users = (Users)data;

            foreach (var user in users.List)
                DoProcessUser(user);

            return users;
        }

        private void DoProcessUser(User user)
        {
            ProcessUserLevel(user);
            ProcessUserSubordinates(user);
        }

        private void ProcessUserSubordinates(User user)
        {
            IsUserSupervisor(user.Id);
            CountSubordinat(user);
        }

        private void CountSubordinat(User user)
        {
            int count = 0;
            GetAllSubordinates(user.Id, ref count);
            user.SubordinatesCount = count;
            
        }
        private void GetAllSubordinates(int userId, ref int count)
        {
            var subordinates = users.List.Where(x => x.SupervisorId == userId).Select(x => x).ToList();

            if(subordinates.Any(x => x.HasSuboridantes == false))
            {
                count += subordinates.Where(x => x.HasSuboridantes == false).Select(x => x).ToList().Count;
            }

            foreach (var user in subordinates)
                GetAllSubordinates(user.Id, ref count);
        }

        private void IsUserSupervisor(int userId)
        {
            var subordinatedUsers = users.List.Where(x => x.SupervisorId == userId).Select(x => x).ToList();

            if (subordinatedUsers.Count == 0)
            {
                var user = users.List.Single(x => x.Id == userId);
                user.HasSuboridantes = false;

                return;
            }

            foreach (var subordinate in subordinatedUsers)
                IsUserSupervisor(subordinate.Id);
        }

        private void ProcessUserLevel(User user)
        {
            supervisors = new List<int>();
            ProcessHelper.GetAllSupervisors(user.Id, users, supervisors);
            user.Level = supervisors.Count;
        }

        private List<int> supervisors;
        private Users users;
    }
}
