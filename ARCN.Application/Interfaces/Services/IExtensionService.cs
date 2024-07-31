using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARCN.Application.DataModels.UserProfile;

namespace ARCN.Application.Interfaces.Services
{
    public interface IExtensionService
    {
        ValueTask<ResponseModel<Extension>> AddExtensionAsync(Extension model, CancellationToken cancellationToken);
        ValueTask<ResponseModel<Extension>> GetAllExtension();
        ValueTask<ResponseModel<Extension>> GetExtensionById(int Extensionid);
        ValueTask<ResponseModel<Extension>> UpdateExtensionAsync(int Extensionid, ExtensionDataModel model);
        ValueTask<ResponseModel<string>> DeleteExtensionAsync(int Extensionid);
    }
}
