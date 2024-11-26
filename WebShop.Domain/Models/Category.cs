using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Interfaces;

namespace WebShop.Domain.Models
{
    public class Category : IModel
    {
        public int Id { get; set; }

        public string Title { get; set; }


    }
}
