using Microsoft.AspNetCore.Http;

namespace Contracts.Security.Claims
{
    public interface IClaimChecker
    {
        string GetCurrentUser(HttpContext currentContext);
    }
}
