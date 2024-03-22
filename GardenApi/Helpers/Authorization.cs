using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GardenApi.Helpers
{
    public class Authorization
    {
        public enum Roles
        {
            Administrator,
            Manager,
            Employee,
            Person
        }

        public const Roles rol_default = Roles.Person;
    }
}