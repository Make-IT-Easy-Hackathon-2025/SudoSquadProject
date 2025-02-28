using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankUpp.Core.Models
{
    [Table("quiz_questions")]
    public class QuizQuestion
    {
        [Key]
        public int Id { get; set; }

        public string QuestionValue { get; set; }

        [ForeignKey(nameof(Quiz))]
        public int QuizId { get; set; }

        public virtual Quiz Quiz { get; set; }

        public virtual ICollection<QuizOption> Options { get; set; }
    }
}
