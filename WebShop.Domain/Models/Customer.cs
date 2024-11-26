using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WebShop.Domain.Interfaces;

namespace WebShop.Domain.Models
{
    public class Customer : IModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Length(2, 30)]
        public required string FirstName { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [JsonIgnore]
        public ICollection<Order>? Orders { get; set; } = new List<Order>();
    }
}
