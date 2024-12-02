using System.Text;

namespace PwProject.WebApiService.Authentication
{
    public class BasicAuthentication : Authentication
    {
        public BasicAuthentication(string name, string password)
            : base("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{name}:{password}")))
        {
        }
    }
}
