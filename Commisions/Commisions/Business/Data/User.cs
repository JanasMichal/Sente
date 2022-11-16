using System.Collections.Generic;

namespace Commisions.Business.Data
{
    public class Users
    {
        public List<User> List { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public int Level { get; set; }
        public int SubordinatesCount { get; set; } // who dont have subordinates
        public int SupervisorId { get; set; }
        public int Commision { get; set; }
        public bool HasSuboridantes { get; set; } = true;

        public void AddAmount(int amount)
        {
            Commision += amount;
        }

        override
        public string ToString()
        {
            return $"{Id} {Level} {SubordinatesCount} {Commision}";
        }
    }
}
