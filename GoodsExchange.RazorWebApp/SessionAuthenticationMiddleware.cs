namespace TrialTest_SU24
{

    public class SessionAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public SessionAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.ToString().ToLower();

            // Allow access to login, register, and public pages without redirection
            if (!path.Contains("/") )
            {
                // Check if the user is authenticated
                if (!context.Session.TryGetValue("UserId", out _))
                {
                    context.Response.Redirect("/");
                    return;
                }
            }

            await _next(context);
        }
    }

}
