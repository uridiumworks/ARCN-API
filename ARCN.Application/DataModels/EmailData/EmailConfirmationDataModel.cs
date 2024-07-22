using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARCN.Application.DataModels.EmailData
{
    public class EmailConfirmationDataModel
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }

    }
}
