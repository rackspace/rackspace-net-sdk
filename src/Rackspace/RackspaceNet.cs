using System;
using System.Diagnostics;
using Flurl.Http;
using Flurl.Http.Configuration;
using Newtonsoft.Json;
using OpenStack;

namespace Rackspace
{
    /// <summary>
    /// A static container for global configuration settings affecting Rackspace.NET behavior.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    public static class RackspaceNet
    {
        /// <summary>
        /// Provides thread-safe accesss to Rackspace.NET's global configuration options.
        /// <para>
        /// Can only be called once at application start-up, before instantiating any Rackspace.NET objects.
        /// </para>
        /// </summary>
        /// <param name="configureFlurl">Addtional configuration of Flurl's global settings <seealso cref="FlurlHttp.Configure"/>.</param>
        /// <param name="configureJson">Additional configuration of Json.NET's glboal settings <seealso cref="JsonConvert.DefaultSettings"/>.</param>
        public static void Configure(Action<FlurlHttpConfigurationOptions> configureFlurl = null, Action<JsonSerializerSettings> configureJson = null)
        {
            OpenStackNet.Configure(configureFlurl, configureJson);
        }

        /// <inheritdoc cref="OpenStack.OpenStackNet.Tracing" />
        public static class Tracing
        {
            /// <inheritdoc cref="OpenStack.OpenStackNet.Tracing.Http" />
            public static readonly TraceSource Http = OpenStackNet.Tracing.Http;
        }
    }
}
