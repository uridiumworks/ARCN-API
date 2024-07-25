
using ARCN.Domain.Commons.Authorization;

namespace ARCN.API.Atrributes
{
    public class MustHavePermissionAttribute:AuthorizeAttribute
    {
        public MustHavePermissionAttribute(string feature, string action)
        {
            Policy = AppPermission.NameFor(feature, action);
        }
    }
}
