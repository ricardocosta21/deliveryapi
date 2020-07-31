using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Supermarket.API.Domain.Models
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        

        public int CategoryId { get; set; }
        //public Category Category { get; set; }

    }
}