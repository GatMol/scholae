using Amazon.Extensions.CognitoAuthentication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scholae.Services
{
    public class Authentication
    {

        private readonly CognitoUser user;

        public Authentication(String email)
        {
            AmazonUtils.CognitoUserId = email;
            user = AmazonUtils.CognitoUser;
        }

        public async Task<string> AuthenticateUser(string password)
        {
            InitiateSrpAuthRequest authRequest = new InitiateSrpAuthRequest { Password = password };
            AuthFlowResponse authResponse = await user.StartWithSrpAuthAsync(authRequest).ConfigureAwait(false);
            var accessToken = authResponse.AuthenticationResult.AccessToken;
            return accessToken;
        }
    }
}
