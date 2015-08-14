---
layout: post
title:  "Introducing Rackspace .NET SDK"
date:   2015-08-14
categories: release roadmap
---

The Rackspace .NET SDK beta is now available for you to use!
In our inaugural release we have added support for [Cloud Networks][cloud-networks-overview].
If you are using OpenStack.NET already, just [install the Rackspace package][nuget-pkg]
and start using the new Rackspace features.

<div class="nuget-badge">
  <p>
    <code>PM&gt; Install-Package Rackspace -Pre</code>
  </p>
</div>

## Roadmap ##
We are built on top of [OpenStack.NET](http://openstacknetsdk.org), as many of
Rackspace's services use OpenStack. In fact we are still in the process of migrating
Rackspace specific solutions out of OpenStack.NET into the Rackspace .NET SDK. When
completed, OpenStack.NET v2.0 will be pure OpenStack and Rackspace.NET v1.0 pure
Rackspace.

The full roadmap is outlined in our project's [beta milestones][rackspacenet-milestones].
Here's a peek at the first few releases:

* v0.2 - Add Support for RackConnect Public IPs
* v0.3 - Migrate Cloud Servers from OpenStack.NET
* v0.4 - Migrate Cloud Identity from OpenStack.NET

At that point, one of our key scenarios for Rackspace customers will be
easy as pie and demonstrate why we are going through the trouble of this migration.
As a hybrid customer, you can spin up a cloud server and connect it to the network
where your dedicated servers live.

{% highlight csharp %}
using Rackspace.Identity.v2;
using Rackspace.CloudNetworks.v2;
using Rackspace.CloudServers.v2;
using Rackspace.RackConnect.v3;

var identity = new CloudIdentity("{username}", "{api-key}");

// Lookup your private network
var networkService = new CloudNetworkService(cloudIdentity);
var networks = networkService.ListNetworks();
var myNetwork = networks.First(x => x.Label == "{private-network-name}");

// Create a cloud server on your private network
var serverService = new CloudServerService(identity);
var serverDefinition = new ServerCreateDefinition
{
  ...
  Networks = { myNetwork.Id }
};
var server = await serverService.CreateServerAsync(serverDefinition);
await server.WaitUntilActiveAsync();
server.AssignPublicIP();
{% endhighlight %}

For more details on how this will improve OpenStack.NET, checkout
[Rackspace.NET and OpenStack.NET: Peas and Carrots][rackspacenet-openstacknet].

[v0.4.0]: https://github.com/rackspace/Rackspace.NET/milestones/v0.4.0
[rackspacenet-milestones]: https://github.com/rackspace/rackspace.net/milestones
[cloud-networks-overview]: http://www.rackspace.com/cloud/networks
[nuget-pkg]: http://nuget.org/packages/Rackspace
[rackspacenet-openstacknet]: https://github.com/openstacknetsdk/openstack.net/wiki/Rackspace-and-OpenStack.NET
