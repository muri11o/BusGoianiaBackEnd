namespace BusGoiania.MainAPI.Configuration
{
    public class AppSettings
    {
        public string UrlMiddleware { get; set; }
        public string Secret { get; set; }
        public int ExpiracaoHoras { get; set; }
        public string Emissor { get; set; }
        public string ValidoEm { get; set; }
    }
}
