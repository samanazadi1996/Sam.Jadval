using System.Collections.Generic;
using Jadval.Domain.Common;

namespace Jadval.Domain.Crosswords.Entities
{
    public class CrosswordQuestion : BaseEntity
    {
        public int QuestionId { get; set; }
        public List<CrosswordQuestionValue> Value { get; set; }

        public Crossword Crossword { get; set; }
        public long CrosswordId { get; set; }
    }
}
