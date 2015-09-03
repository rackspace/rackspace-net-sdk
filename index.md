---
layout: default
title: Rackspace .NET SDK
---

The Rackspace .NET SDK is here to help you automate any Rackspace service. Our goal?

<p style="text-align: center; color: #2e7bcf">
  <i><em>If the control panel can do it, we can automate it!</em></i>
</p>

## Features ##
Here is what you can do with the Rackspace .NET SDK:

* Provision multiple application servers with Cloud Servers
* Secure your network with Cloud Networks
* Meet high availability requirements with Cloud Load Balancers
* Associate a domain name to your server using Cloud DNS
* Scale your server's drive space with Cloud Block Storage
* Deploy your website's static files with Cloud Files
* Setup a MySQL database with Cloud Databases
* Use durable message queueing with Cloud Queues
* Enable Akamai with Rackspace CDN

## Getting Started ##
Take a look at [Getting Started with .NET SDK](https://developer.rackspace.com/sdks/dot-net/)
for information on supported services and walkthroughs. Also our [Samples](samples/) page
has quick examples to get you jump started without all that pesky reading.

Use NuGet to install the Rackspace package:

<div class="nuget-badge">
  <p>
    <code>PM&gt; Install-Package Rackspace</code>
  </p>
</div>

The first step is always authentication. You can
[find your username and api key in the Rackspace Cloud Control Panel](http://www.rackspace.com/knowledge_center/article/view-and-reset-your-api-key).
Once you have your identity configured, pass it to the service constructor and go to town!

{% highlight csharp %}
using net.openstack.Core.Domain;
using net.openstack.Providers.Rackspace;
using Rackspace.CloudNetworks.v2;

// Configure identity used for authentication
var identity = new CloudIdentity
{
    Username = username,
    APIKey = apiKey
};
var identityService = new CloudIdentityProvider(identity);

// Construct any service and pass in the identityService
var networkService = new CloudNetworkService(identityService);
// Do cool things here...
{% endhighlight %}
