

using System.ComponentModel.DataAnnotations.Schema;
namespace TableToEntity.Entity
{
    public class User
    {
        [Column("No")]
        public long Id { get; set; }
        [Column("Name")]
        public string Name { get; set; }
    }
}