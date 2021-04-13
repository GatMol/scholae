using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.Extensions.CognitoAuthentication;
using System;
using System.Collections.Generic;
using System.Text;
using Scholae.Services;

namespace Scholae.Services
{
    class AmazonUtils
    {
        private static CognitoUser cognitoUser;
        public static string CognitoUserId = string.Empty;
        public static CognitoUser CognitoUser
        {
            get
            {
                if (cognitoUser == null)
                {
                    cognitoUser = new CognitoUser(CognitoUserId, Constants.APP_CLIENT_ID, UserPool, IdentityProviderClient);
                }
                return cognitoUser;
            }
        }

        private static CognitoUserPool userPool;
        public static CognitoUserPool UserPool
        {
            get
            {
                if (userPool == null)
                    userPool = new CognitoUserPool(Constants.POOL_ID, Constants.APP_CLIENT_ID, IdentityProviderClient);
                return userPool;
            }
        }


        private static AmazonCognitoIdentityProviderClient identityProviderClient;
        public static AmazonCognitoIdentityProviderClient IdentityProviderClient
        {
            get
            {
                if (identityProviderClient == null)
                {
                    identityProviderClient = new AmazonCognitoIdentityProviderClient(new Amazon.Runtime.AnonymousAWSCredentials(), RegionEndpoint.USEast2);
                }

                return identityProviderClient;
            }
        }

    }
}
