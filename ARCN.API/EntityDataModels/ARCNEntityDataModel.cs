

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

            builder.EntitySet<ApplicationUser>(nameof(ApplicationUser));
            builder.EntitySet<ApplicationUser>(nameof(ApplicationUser));
            builder.EntitySet<CordinationReport>(nameof(CordinationReport));
            builder.EntitySet<Blog>(nameof(Blog));
            builder.EntitySet<Entrepreneurship>(nameof(Entrepreneurship));
            builder.EntitySet<Event>(nameof(Event));
            builder.EntitySet<Extension>(nameof(Extension));
            builder.EntitySet<FCA>(nameof(FCA));
            builder.EntitySet<Journals>(nameof(Journals));
            builder.EntitySet<Naris>(nameof(Naris));
            builder.EntitySet<NewsLetter>(nameof(NewsLetter));
            builder.EntitySet<Project>(nameof(Project));
            builder.EntitySet<Reports>(nameof(Reports));
            builder.EntitySet<SupervisionReport>(nameof(SupervisionReport));
            builder.EntitySet<State>(nameof(State));
            builder.EntitySet<LocalGovernmentArea>(nameof(LocalGovernmentArea));
            builder.EntitySet<ARCNProgram>(nameof(ARCNProgram));




            return builder.GetEdmModel();
        }
    }
}
