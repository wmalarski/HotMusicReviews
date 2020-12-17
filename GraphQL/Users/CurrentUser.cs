using System.Security.Claims;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.AspNetCore;

namespace HotMusicReviews.GraphQL.Users
{
    public class CurrentUser
    {
        public string UserId { get; }

        public CurrentUser(string userId)
        {
            UserId = userId;
        }
    }

    public class CurrentUserGlobalState : GlobalStateAttribute
    {
        public CurrentUserGlobalState() : base("currentUser")
        {
        }

        public static HttpRequestInterceptorDelegate AuthenticationInterceptor()
        {
            return (context, executor, builder, ct) =>
            {
                var user = context?.User;
                var userId = user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var isAuthenticated = user?.Identity?.IsAuthenticated ?? false;

                if (userId != null && isAuthenticated)
                {
                    builder.SetProperty(
                        "currentUser",
                        new CurrentUser(userId)
                    );
                }
                return ValueTask.CompletedTask;
            };
        }
    }

    // public class CurrentUserRequestInterceptor : DefaultHttpRequestInterceptor
    // {
    //     public override ValueTask OnCreateAsync(HttpContext context, IRequestExecutor requestExecutor, IQueryRequestBuilder requestBuilder, CancellationToken cancellationToken)
    //     {
    //         var user = context?.User;
    //         var userId = user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    //         var isAuthenticated = user?.Identity?.IsAuthenticated ?? false;

    //         Console.WriteLine($"OnCreateAsync{userId}|{isAuthenticated}");

    //         if (userId != null && isAuthenticated)
    //         {
    //             requestBuilder.SetProperty(
    //                 "currentUser",
    //                 new CurrentUser(userId)
    //             );
    //         }
    //         return ValueTask.CompletedTask;
    //     }
    // }
}