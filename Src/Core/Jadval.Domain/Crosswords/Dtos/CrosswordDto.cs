using System.Collections.Generic;

namespace Jadval.Domain.Crosswords.Dtos
{
    public partial class CrosswordDto
    {
        public List<List<string>> Data { get; set; }
        public List<QuestionDto> Questions { get; set; }
    }
}