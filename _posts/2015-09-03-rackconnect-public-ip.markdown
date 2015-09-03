---
layout: post
title:  "RackConnect Public IP Support"
date:   2015-09-03
categories: release rackconnect
---

[Rackspace.NET v0.2][release-notes] adds partial support for [Rackspace's Hybrid Cloud][rackconnect], RackConnect.
RackConnect enables you to connect your dedicated servers to our fully managed cloud,
giving you the performance of traditional hosting plus increased security with single-tenant Cloud Networks.

With RackConnect Public IP support, you can spin up cloud servers on your private network and
also make them available on the public internet. Download [v0.2 on NuGet][nuget-pkg] and
checkout the [RackConnect Samples][rackconnect-samples] to get started today!

{% highlight csharp %}
var rackConnectService = new RackConnectService(identityService);

// Lookup your private network
var networks = await rackConnectService.ListNetworksAsync();
var myNetwork = networks.First();

// Create a cloud server on your private network
var serverService = new CloudServersProvider(null, "{region}", identityService, null);
var server = serverService.CreateServer("{server-name}", "{image-id}", "{flavor-id}",
    networks: new string[] { myNetwork.Id });
serverService.WaitForServerActive(server.Id);

// Allocate a public IP and assign it to your cloud server-name
var ip = await rackConnectService.CreatePublicIPAsync(
    new PublicIPCreateDefinition { ServerId = server.Id });
await ip.WaitUntilActiveAsync();
{% endhighlight %}

[release-notes]: https://github.com/rackspace/rackspace-net-sdk/releases/tag/v0.2.0
[rackconnect]: http://www.rackspace.com/en-us/cloud/hybrid/rackconnect
[nuget-pkg]: https://www.nuget.org/packages/Rackspace
[rackconnect-samples]: {{ site.baseurl }}/samples/rackconnect.html
