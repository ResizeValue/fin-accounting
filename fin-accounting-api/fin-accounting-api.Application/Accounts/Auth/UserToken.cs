namespace FinAccountingApi.Application.Accounts.Auth
{
    public class UserToken
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string AccessToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}
