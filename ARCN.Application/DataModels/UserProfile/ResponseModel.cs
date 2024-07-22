namespace ARCN.Application.DataModels.UserProfile
{
    public class ResponseModel<T> where T : class
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }

        public List<string> Errors { get; set; }

    }
}
