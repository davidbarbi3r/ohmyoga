namespace Ohmyoga.Api;

public static class ApiEndpoints
{
    private const string ApiBase = "api";

    public static class Courses
    {
        // without first and last "/" 
        private const string Base = $"{ApiBase}/courses";

        public const string Create = Base;

        public const string Get = $"{Base}/{{id:guid}}";
        public const string GetAll = Base;
        
        public const string Update = $"{Base}/{{id:guid}}";
    }
}