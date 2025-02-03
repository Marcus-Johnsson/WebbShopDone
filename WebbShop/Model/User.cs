using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebbShop.Model
{
    internal class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string? SecurityNumber { get; set; }
        public string? Mail { get; set; }
        public string? Password { get; set; }

        public string? Addres { get; set; }

        public DateTime Age { get; set; }

    }
}
