using System.Linq;
using RestSharp;

namespace Amazon.SellingPartnerAPIAA
{
    public class LWAAuthorizationSigner
    {
        public const string AccessTokenHeaderName = "x-amz-access-token";

        public LWAClient LWAClient { get; set; }

        /// <summary>
        /// Constructor for LWAAuthorizationSigner
        /// </summary>
        /// <param name="lwaAuthorizationCredentials">LWA Authorization Credentials for token exchange</param>
        public LWAAuthorizationSigner(LWAAuthorizationCredentials lwaAuthorizationCredentials)
        {
            LWAClient = new LWAClient(lwaAuthorizationCredentials);
        }

        /// <summary>
        /// Signs a request with LWA Access Token
        /// </summary>
        /// <param name="restRequest">Request to sign</param>
        /// <returns>restRequest with LWA signature</returns>
        public IRestRequest Sign(IRestRequest restRequest)
        {
            var tokenHeader = restRequest.Parameters.FirstOrDefault(p => p.Type == ParameterType.HttpHeader && p.Name == AccessTokenHeaderName && p.Value != null);
            if (tokenHeader == null)
            {
                string accessToken = LWAClient.GetAccessToken();

                restRequest.AddHeader(AccessTokenHeaderName, accessToken);
            }
            
            return restRequest;
        }
    }
}
