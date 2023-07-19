using System.Collections.Generic;

namespace Jadval.Domain.Crosswords.Dtos
{
    public class QuestionDto
    {
        public int QuestionId { get; set; }
        public List<ValueDto> Value { get; set; }
    }
}