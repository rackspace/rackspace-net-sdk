using System;
using net.openstack.Core.Domain;
using net.openstack.Core.Providers;
using net.openstack.Providers.Rackspace;

namespace Rackspace
{
    internal class TestIdentityProvider
    {
        internal static IIdentityProvider GetIdentityProvider()
        {
            var identity = GetIdentityFromEnvironment();
            return new CloudIdentityProvider(identity)
            {
                ApplicationUserAgent = "CI-BOT"
            };
        }

        internal static CloudIdentity GetIdentityFromEnvironment()
        {
            if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("OPENSTACKNET_USER")))
            {
                return new CloudIdentity
                {
                    Username = Environment.GetEnvironmentVariable("OPENSTACKNET_USER"),
                    APIKey = Environment.GetEnvironmentVariable("OPENSTACKNET_APIKEY")
                };
            }
            if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("BAMBOO_OPENSTACKNET_USER")))
            {
                return new CloudIdentity
                {
                    Username = Environment.GetEnvironmentVariable("BAMBOO_OPENSTACKNET_USER"),
                    APIKey = Environment.GetEnvironmentVariable("BAMBOO_OPENSTACKNET_APIKEY_PASSWORD")
                };
            }
            throw new Exception("No identity environemnt variables found. Make sure OPENSTACKNET_USER and OPENSTACKNET_APIKEY exist.");
        }
    }
}