using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankUpp.Core.Models
{
    [Table("quiz_attempts")]
    public class QuizAttempt
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        public int QuizOptionId { get; set; }

        [ForeignKey(nameof(QuizOptionId))]
        public virtual QuizOption QuizOption { get; set; }

        [ForeignKey(nameof(Quiz))]
        public int QuizId { get; set; }

        public virtual Quiz Quiz { get; set; }

    }
}
