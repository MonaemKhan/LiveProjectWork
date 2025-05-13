namespace CentralModels.Administration
{
    public class TokenModel
    {
        public string userId { get; set; }
        public string token { get; set; }
        public DateTime tokenExptime { get; set; }
    }
}
