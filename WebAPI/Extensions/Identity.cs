using System.Security.Claims;
using System.Security.Principal;

namespace WebAPI.Extensions
{
    public static class Identity
    {
        #region Id()
        public static long Id(this IPrincipal principal)
        {
            return ToLong((principal as ClaimsPrincipal).FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }

        public static long Id(this IIdentity identity)
        {
            return ToLong((identity as ClaimsIdentity).FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }
        #endregion

        public static long ToLong(object value)
        {
            if (value == null)
            {
                return 0;
            }

            return ToLong(value.ToString());
        }
    }
}
