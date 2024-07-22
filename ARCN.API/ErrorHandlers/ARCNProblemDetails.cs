
using Microsoft.AspNetCore.Mvc;

namespace ARCN.API.ErrorHandlers
{
    public class ARCNProblemDetails: ProblemDetails
    {
        public Dictionary<string, List<string>>? Errors { get; set; }
        public string? TraceIdentifier { get; set; }
        public string? Path { get; set; }
    }
}
