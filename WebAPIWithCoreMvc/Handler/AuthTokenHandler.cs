namespace WebAPIWithCoreMvc.Handler
{
    public class AuthTokenHandler:DelegatingHandler
    {
        public AuthTokenHandler()
        {

        }
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return base.SendAsync(request, cancellationToken);
        }
    }
}
