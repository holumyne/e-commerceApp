using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class ProductType : BaseEntity
    {
        public string Name { get; set; }
    }
}