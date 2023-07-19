using Jadval.Domain.Common;

namespace Jadval.Domain.Crosswords.Entities
{
    public class CrosswordQuestionValue : BaseEntity
    {
        private CrosswordQuestionValue()
        {

        }
        public CrosswordQuestionValue(string question, string position)
        {
            Question = question;
            Position = position;
        }
        public string Question { get; set; }
        public string Position { get; set; }

        public CrosswordQuestion CrosswordQuestion { get; set; }
        public long CrosswordQuestionId { get; set; }

    }
}
