using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserRol
    {
        public int Id {get; set;}
        public int IdUser { get; set; }

        public User Users { get; set;}

        public int IdRol {get; set;}

        public Rol Rols {get; set;}
        
    }
}