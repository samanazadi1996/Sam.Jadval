using Jadval.Domain.Common;
using System.Collections.Generic;
using System.Linq;

namespace Jadval.Domain.Jadval.Entities
{
    public class JadvalQuestion : BaseEntity
    {
        public List<JadvalQuestionValue> Value { get; set; }
    }
}
