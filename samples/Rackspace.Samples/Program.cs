using System;
using net.openstack.Core.Domain;
using net.openstack.Providers.Rackspace;
using Rackspace.CloudNetworks.v2;
using Rackspace.Synchronous;

namespace Rackspace.Samples
{
    class Program
    {
        static void Main()
        {
            Console.Write("User: ");
            string username = Console.ReadLine();

            Console.Write("API Key: ");
            string apikey = Console.ReadLine();

            Console.Write("Region: ");
            string region = Console.ReadLine();

            // This is a example of how to authenticate and view your token, service catalog, etc.
            // It is not necessary to authenticate explicitly.
            Console.WriteLine("Explicitly Authenticating...");
            var identity = new CloudIdentity
            {
                Username = username,
                APIKey = apikey
            };
            var identityService = new CloudIdentityProvider(identity);
            UserAccess authResult = identityService.Authenticate();
            Console.WriteLine($"Welcome, {authResult.User.Name}!");

            Console.WriteLine("Listing Networks...");
            var networkService = new CloudNetworkService(identityService, region);
            foreach (Network network in networkService.ListNetworks())
            {
                Console.WriteLine($"{network.Name}\t\t\t{network.IsShared}");
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }
}
