namespace HotMusicReviews.GraphQL.Common
{
    public class UserError
    {
        public UserError(string message, string code)
        {
            Message = message;
            Code = code;
        }

        public string Message { get; }

        public string Code { get; }
    }

    public class NoAccessError : UserError
    {
        public NoAccessError() : base("The current user is not authorized to access this resource.", "400")
        {
        }
    }
}