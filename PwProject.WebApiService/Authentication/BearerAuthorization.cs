namespace PwProject.WebApiService.Authentication
{
    public class BearerAuthorization : Authentication
    {
        public BearerAuthorization(string value)
            : base("Bearer", value)
        {
        }
    }
}
