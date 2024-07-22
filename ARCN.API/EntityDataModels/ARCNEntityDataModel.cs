

namespace ARCN.API.EntityDataModels
{

    public class ARCNEntityDataModel
    {
        static string Namespace = "ARCN";

        public IEdmModel GetEntityDataModel() 
        {
            var builder = new ODataConventionModelBuilder
            {
                Namespace = Namespace,
                ContainerName = "ARCNContainer"
            };

            var userEntity = builder.EntitySet<ApplicationUser>(nameof(ApplicationUser));
          



            return builder.GetEdmModel();
        }
    }
}
