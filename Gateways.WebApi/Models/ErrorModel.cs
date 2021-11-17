namespace Gateways.WebApi.Models
{
    public class ErrorModel
    {
        public string[] Errors { get; init; }

        public static ErrorModel Create(string[] errors) => new ErrorModel { Errors = errors };
    }
}
