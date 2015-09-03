---
layout: default
title: RackConnect Sample
---

[Back to Samples]({{site.baseurl}}/samples/)

# RackConnect #

## Available Commands ##
* RackConnectService.ListNetworks()
* RackConnectService.CreatePublicIP(definition)
* PublicIP.Assign(serverId)
* PublicIP.Unassign()
* PublicIP.Delete()
* PublicIP.WaitUntilActive()
* PublicIP.WaitUntilDeleted()

## Sample ##
The sample below creates a cloud server on a private, hybrid network and assigns it a public IP address. Download the [sample project][sample-project] to get started with a working console
application which demonstrates how to use each Rackspace service.

<script src="http://gist-it.appspot.com/https://github.com/rackspace/Rackspace.NET/blob/develop/samples/Rackspace.Samples/AssignPublicIPSamples.cs?slice=20:48"></script>

[sample-project]: https://github.com/rackspace/Rackspace.NET/tree/master/samples/Rackspace.Samples
