namespace PwProject.WebApiService.Authentication
{
    public abstract class Authentication
    {
        protected Authentication(string type, string value)
        {
            Type = type;
            Value = value;
        }

        public string Type { get; set; }

        public string Value { get; set; }
    }
}
