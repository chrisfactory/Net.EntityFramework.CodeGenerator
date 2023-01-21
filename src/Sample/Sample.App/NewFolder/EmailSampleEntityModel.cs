using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sample.App.NewFolder
{
    [Table("StudentMaster")]
    public partial class EmailSampleEntityModel
    {
        [Key]
        [Column("Identifier")]
        public int Id { get; set; }
        [Column("Mail")]
        [Required]
        [MaxLength(50)]
        [StringLength(50)]
        public string Email { get; set; }
        [NotMapped]
        public DateTime Created { get; set; }

        public static int TTTTTTTTTT { get; set; }  
    }
}
