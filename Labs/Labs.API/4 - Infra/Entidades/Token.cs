namespace Labs.API._4___Infra.Entidades
{
    public class Token
    {
        public bool Authenticated { get; set; }
        public DateTime Expiration { get; set; }
        public string ValorToken { get; set; }
        public string Message { get; set; }
    }
}
