using System;
using System.Linq;
using System.Threading.Tasks;
using net.openstack.Core.Domain;
using net.openstack.Providers.Rackspace;
using Rackspace.CloudNetworks;
using Rackspace.CloudNetworks.v2;

public class AssignPublicIPSamples : ISample
{
    public async Task Run(string username, string apiKey, string region)
    {
        // Configure authentication
        var identity = new CloudIdentity
        {
            Username = username,
            APIKey = apiKey
        };
        var rackConnectService = new RackConnectService(identity);
        var serverService = new CloudServersProvider(identity);

        Console.WriteLine("Looking up a RackConnect network...");

        Console.WriteLine("Creating sample cloud server... ");
        
        Console.WriteLine("Assigning a public ip address...");
        rackConnectService.AssignPublicIPAsync(server.Id);
        Console.WriteLine("Deleting sample cloud server...");
        await networkService.DeletePortAsync(samplePort.Id);
        await networkService.DeleteNetworkAsync(sampleNetwork.Id);
    }

    public void PrintTasks()
    {
        Console.WriteLine("This sample will perform the following tasks:");
        Console.WriteLine("\t* Create a network");
        Console.WriteLine("\t* Add a subnet to the network");
        Console.WriteLine("\t* Attach a port to the network");
        Console.WriteLine("\t* Delete the network");
    }

}