using Plugin.Plumber.Models;
using System.Collections.Generic;
using Sitecore.Framework.Pipelines;
using Sitecore.Commerce.Core;

namespace Plugin.Plumber.Pipelines
{
    public interface IGetPoliciesPipeline : IPipeline<string, IEnumerable<PolicySnapshotModel>, CommercePipelineExecutionContext>
    {
    }
}
