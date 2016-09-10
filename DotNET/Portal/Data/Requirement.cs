using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data
{
    [Table("Requirement")]
    public class Requirement
    {
        [Column("RequirementId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required, Column("RequirementCode")]
        public string Code { get; set; }

        [Column("RequirementDescription")]
        public string Description { get; set; }

        [Required, Column("RequirementContent")]
        public string Context { get; set; }
    }
}