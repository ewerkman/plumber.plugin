// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigureOpsServiceApiBlock.cs" company="Sitecore Corporation">
//   Copyright (c) Sitecore Corporation 1999-2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Plugin.Plumber
{
    using Microsoft.AspNetCore.OData.Builder;
    using Plugin.Plumber.Models;
    using Sitecore.Commerce.Core;
    using Sitecore.Commerce.Core.Commands;
    using Sitecore.Framework.Conditions;
    using Sitecore.Framework.Pipelines;

    using System.Threading.Tasks;

    /// <summary>
    /// Defines a block which configures the OData model for the plugin
    /// </summary>
    /// <seealso>
    ///     <cref>
    ///         Sitecore.Framework.Pipelines.PipelineBlock{Microsoft.AspNetCore.OData.Builder.ODataConventionModelBuilder,
    ///         Microsoft.AspNetCore.OData.Builder.ODataConventionModelBuilder,
    ///         Sitecore.Commerce.Core.CommercePipelineExecutionContext}
    ///     </cref>
    /// </seealso>
    [PipelineDisplayName("Plugin.Plumber.ConfigureOpsServiceApiBlock")]
    public class ConfigureOpsServiceApiBlock : PipelineBlock<ODataConventionModelBuilder, ODataConventionModelBuilder, CommercePipelineExecutionContext>
    {
        /// <summary>
        /// The execute.
        /// </summary>
        /// <param name="modelBuilder">The argument.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        /// The <see cref="ODataConventionModelBuilder" />.
        /// </returns>
        public override Task<ODataConventionModelBuilder> Run(ODataConventionModelBuilder modelBuilder, CommercePipelineExecutionContext context)
        {
            Condition.Requires(modelBuilder).IsNotNull($"{this.Name}: The argument can not be null");

            modelBuilder.ComplexType<PolicySnapshotModel>();

            var getPolicyFunction = modelBuilder.Function("GetPolicy");
            getPolicyFunction.Parameter<string>("name");
            getPolicyFunction.Returns<PolicySnapshotModel>();

            return Task.FromResult(modelBuilder);
        }
    }
}
