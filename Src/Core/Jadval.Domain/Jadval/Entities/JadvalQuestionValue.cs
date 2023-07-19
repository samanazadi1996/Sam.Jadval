using Jadval.Domain.Common;

namespace Jadval.Domain.Jadval.Entities
{
    public class JadvalQuestionValue:BaseEntity
    {
        private JadvalQuestionValue()
        {

        }
        public JadvalQuestionValue(string question, string position)
        {
            Question = question;
            Position = position;
        }
        public string Question { get; set; }
        public string Position { get; set; }
    }
}
