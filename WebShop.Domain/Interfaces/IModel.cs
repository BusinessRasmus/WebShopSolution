using System.ComponentModel.DataAnnotations;

namespace WebShop.Domain.Interfaces
{
    public interface IModel
    {
        [Key]
        public int Id { get; set; }
    }
}
