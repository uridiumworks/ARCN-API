using ARCN.Application.DataModels.ContentManagement;
using ARCN.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace ARCN.API.Controllers.ODATA
{
    [Route("odata")]
    public class DashboardController : ODataController
    {

        private readonly INarisService _narService;
        private readonly INewsLetterService _newsLetterService;
        private readonly IProgramService _programService;
        private readonly IFCAService _fcaService;
        private readonly IJournalsService _journalsService;
        private readonly IEventService _eventService;

        public DashboardController(INarisService narService, INewsLetterService newsLetterService,
            IProgramService programService, IFCAService fcaService,
            IJournalsService journalsService, IEventService eventService)
        {
            _narService = narService;
            _newsLetterService = newsLetterService;
            _programService = programService;
            _fcaService = fcaService;
            _journalsService = journalsService;
            _eventService = eventService;
        }

        [HttpGet("GetDashboardData")]
        public async Task<ActionResult<DashboardData>> GetDashboardData()
        {
            double CalculateGrowth(double current, double previous)
            {
                return previous == 0 ? 0 : ((current - previous) / previous) * 100;
            }

            var currentNARs = _narService.GetAllNarisTotal();
            var previousNARs = _narService.GetAllNarisPreviousTotal();
            var currentAROCs = _newsLetterService.GetAllNewsLettersTotal();
            var previousAROCs = _newsLetterService.GetAllNewsLettersPreviousTotal();
            var currentPrograms = _programService.GetAllProgramTotal();
            var previousPrograms = _programService.GetAllProgramPreviousTotal();
            var currentFCAs = _fcaService.GetAllfcaTotal();
            var previousFCAs = _fcaService.GetAllfcaPreviousTotal();
            var currentPublications = _journalsService.GetAllJournalTotal();
            var previousPublications = _journalsService.GetAllJournalPreviousTotal();
            var currentEvents = _eventService.GetAllEventTotal();
            var previousEvents = _eventService.GetAllEventPreviousTotal();

            var data = new DashboardData
            {
                TotalNARs = currentNARs,
                NARGrowth = CalculateGrowth(currentNARs, previousNARs),
                TotalNewsLetter = currentAROCs,
                NewsLetterGrowth = CalculateGrowth(currentAROCs, previousAROCs),
                TotalPrograms = currentPrograms,
                ProgramGrowth = CalculateGrowth(currentPrograms, previousPrograms),
                TotalFCAs = currentFCAs,
                FCAGrowth = CalculateGrowth(currentFCAs, previousFCAs),
                TotalJournal = currentPublications,
                JournalGrowth = CalculateGrowth(currentPublications, previousPublications),
                TotalEvents = currentEvents,
                EventGrowth = CalculateGrowth(currentEvents, previousEvents)
            };

            return Ok(data);
        }
    }
}


