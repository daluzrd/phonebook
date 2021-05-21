using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("phonebook")]
    public class Phonebook
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [Column("nickname")]
        [MaxLength(20)]
        public string Nickname { get; set; }

        [ForeignKey("User")]
        [Column("idUser")]
        public int IdUser { get; set; }

        [Column("cellphone")]
        [MaxLength(20)]
        public string Cellphone { get; set; }
    }
}