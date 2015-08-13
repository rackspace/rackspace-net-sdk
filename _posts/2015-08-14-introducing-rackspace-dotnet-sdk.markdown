---
layout: post
title:  "Introducing Rackspace .NET SDK"
date:   2015-08-14
categories: release roadmap
---

Introduce Rackspace .NET SDK. State its goal and how it interacts with OpenStack.NET.

Here's an example of how easy it will be to use the SDK once we hit [v0.4.0]

{% highlight csharp %}
using Rackspace.Identity;
using Rackspace.CloudNetworks.v2;

var identity = new CloudIdentity("{username}", "{api-key}");
var networkService = new CloudNetworkService(identity);

var network = networkService.CreateNetwork(new NetworkDefinition("{network-name}}"));
{% endhighlight %}

[v0.4.0]: https://github.com/rackspace/Rackspace.NET/milestones/v0.4.0
