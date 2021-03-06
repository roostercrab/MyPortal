using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPortal.Models
{
    [Table("Personnel_TrainingCertificates")]
    public class TrainingCertificate
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Course")]
        public int CourseId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Staff")]
        public int StaffId { get; set; }

        [Display(Name = "Status")] public int StatusId { get; set; }

        public virtual Staff Staff { get; set; }

        public virtual TrainingCourse TrainingCourse { get; set; }

        public virtual TrainingStatus TrainingStatus { get; set; }
    }
}