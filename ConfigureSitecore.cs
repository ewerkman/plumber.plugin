// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigureSitecore.cs" company="Sitecore Corporation">
//   Copyright (c) Sitecore Corporation 1999-2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Plugin.Plumber
{
    using System.Reflection;

    using Microsoft.Extensions.DependencyInjection;
    using Plugin.Plumber.Pipelines;
    using Sitecore.Commerce.Core;
    using Sitecore.Framework.Configuration;
    using Sitecore.Framework.Pipelines.Definitions.Extensions;

    /// <summary>
    /// The Habitat configure class.
    /// </summary>
    /// <seealso cref="Sitecore.Framework.Configuration.IConfigureSitecore" />
    public class ConfigureSitecore : IConfigureSitecore
    {
        /// <summary>
        /// The configure services.
        /// </summary>
        /// <param name="services">
        /// The services.
        /// </param>
        public void ConfigureServices(IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.RegisterAllPipelineBlocks(assembly);
            services.RegisterAllCommands(assembly);

            services.Sitecore().Pipelines(config => config
               .ConfigurePipeline<IConfigureOpsServiceApiPipeline>(c => { c.Add<Plugin.Plumber.ConfigureOpsServiceApiBlock>(); })
               .AddPipeline<IGetPoliciesPipeline, GetPoliciesPipeline>( c => { c.Add<Plugin.Plumber.Pipelines.Blocks.GetPoliciesBlock>(); })
            );
        }        
    }
}