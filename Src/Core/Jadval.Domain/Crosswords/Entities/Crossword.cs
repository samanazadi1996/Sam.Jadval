using System.Collections.Generic;
using Jadval.Domain.Common;

namespace Jadval.Domain.Crosswords.Entities
{
    public class Crossword : AuditableBaseEntity
    {
        private Crossword()
        {

        }
        public Crossword(string content)
        {
            SetContent(content);
        }

        public void SetContent(string content)
        {
            Content = content;
        }

        public string Content { get; set; }
    }
}
