namespace ARCN.Application.DataModels.Security2fa
{
    public class SecurityAnsweDataModel
    {
        public int QuestionId { get; set; }
        public string Answer { get; set; }
    }

    public class SecurityQuestionAnswerList
    {
        public List<SecurityAnsweDataModel> SecurityQuestionAnswerDataModels { get; set; }
    }
}
