using Commisions.Business.Data;
using System.Collections.Generic;
using System.Linq;

namespace Commisions.Business.Misc
{
    public static class ProcessHelper
    {
        public static void GetAllSupervisors(int userId, Users users, List<int> supervisors)
        {
            if (userId == 1)
                return;

            var user = users.List.Single(u => u.Id == userId);

            supervisors.Add(user.SupervisorId);

            GetAllSupervisors(user.SupervisorId, users, supervisors);
        }
    }
}
