

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace ARCN.API.Controllers.ODATA
{
    /// <summary>
    /// Dashboard Controller
    /// </summary>
    [Route("odata")]
    public class DashboardController : ODataController
    {
       
        /// <summary>
        ///  Constructor
        /// </summary>
        /// <param name="transactionService"></param>
        public DashboardController()
        {

        }

    }
}
