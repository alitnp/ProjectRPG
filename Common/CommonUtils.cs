using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjectRPG.Common
{
    public static class CommonUtils
    {
        public static int GetUserIdFromHttpContext(IHttpContextAccessor httpContextAccessor) =>
            int.Parse(
                httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value
            );
    }
}
