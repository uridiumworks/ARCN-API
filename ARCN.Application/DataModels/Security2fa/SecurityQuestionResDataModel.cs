using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARCN.Application.DataModels.Security2fa
{
    public class SecurityQuestionResDataModel
    {
        public int? SecurityQuestionId { get; set; }
        public string Question { get; set; }
        public int Category { get; set; }
    }
}
