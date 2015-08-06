using System;
using net.openstack.Core.Domain;
using net.openstack.Providers.Rackspace;

namespace Rackspace
{
    public class TestIdentityProvider
    {
        private static readonly string EnvironmentVariablesNotFoundErrorMessage =
            "No identity environment variables found. Make sure the following environment variables exist: " + Environment.NewLine +
            "RACKSPACENET_USER" + Environment.NewLine +
            "RACKSPACENET_APIKEY";

        public static CloudIdentityProvider GetIdentityProvider()
        {
            var identity = GetIdentityFromEnvironment();
            return new CloudIdentityProvider(identity)
            {
                ApplicationUserAgent = "CI-BOT"
            };
        }
        
        public static CloudIdentity GetIdentityFromEnvironment()
        {
            var user = Environment.GetEnvironmentVariable("RACKSPACENET_USER");
            if (!string.IsNullOrEmpty(user))
            {
                var apiKey = Environment.GetEnvironmentVariable("RACKSPACENET_APIKEY");

                if (!string.IsNullOrEmpty(apiKey))
                {
                    return new CloudIdentityWithProject
                    {
                        Username = user,
                        APIKey = apiKey
                    };
                }
            }

            user = Environment.GetEnvironmentVariable("BAMBOO_RACKSPACENET_USER");
            if (!string.IsNullOrEmpty(user))
            {
                var apiKey = Environment.GetEnvironmentVariable("BAMBOO_RACKSPACENET_PASSWORD");

                if (!string.IsNullOrEmpty(apiKey))
                {
                    return new CloudIdentity
                    {
                        Username = user,
                        APIKey = apiKey
                    };
                }
            }

            throw new Exception(EnvironmentVariablesNotFoundErrorMessage);
        }
    }
}