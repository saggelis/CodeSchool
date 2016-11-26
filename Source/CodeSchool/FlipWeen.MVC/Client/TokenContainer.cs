using FlipWeen.MVC.Api;

namespace FlipWeen.MVC.Client
{
    using System;
    using System.Web;

    public class TokenContainer : ITokenContainer
    {
        private const string ApiTokenKey = "ApiToken";
        private const string ApiUserName = "ApiUserName";
        private const string ApiUserId = "ApiUserId";

        public object ApiToken
        {
            get { return Current.Session != null ? Current.Session[ApiTokenKey] : null; }
            set { if (Current.Session != null) Current.Session[ApiTokenKey] = value; }
        }

        public string FullName
        {
            get { return Current.Session != null ? Current.Session[ApiUserName].ToString() : null; }
            set { if (Current.Session != null) Current.Session[ApiUserName] = value; }
        }

        public int? UserId
        {
            get { return Current.Session != null ? Convert.ToInt32(Current.Session[ApiUserId]) : default(int); }
            set { if (Current.Session != null) Current.Session[ApiUserId] = value; }
        }

        private static HttpContextBase Current
        {
            get { return new HttpContextWrapper(HttpContext.Current); }
        }
    }
}