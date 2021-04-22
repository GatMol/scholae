using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;
using System;
using System.Collections.Generic;
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

        public static List<AttributeType> GetUserAttributes(string accessToken)
        {
            var getUserAttributesRequest = new GetUserRequest { AccessToken = accessToken };
            var getUser = AmazonUtils.IdentityProviderClient.GetUserAsync(getUserAttributesRequest);
            var userAttributes = getUser.Result.UserAttributes;
            return userAttributes;
        }
    }
}
