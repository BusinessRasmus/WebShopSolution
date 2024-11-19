using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebShop.Shared.Models
{
    public class Customer : IModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }

        [JsonIgnore]
        public ICollection<Order>? Orders { get; set; } = new List<Order>();
    }
}
