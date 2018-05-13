using Microsoft.Extensions.Logging;
using Plugin.Plumber.Models;
using Sitecore.Commerce.Core;
using Sitecore.Framework.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Plumber.Pipelines
{
    public class GetPoliciesPipeline : CommercePipeline<string, IEnumerable<PolicySnapshotModel>>, IGetPoliciesPipeline
    {
        public GetPoliciesPipeline(IPipelineConfiguration<IGetPoliciesPipeline> configuration, ILoggerFactory loggerFactory)
            : base((IPipelineConfiguration)configuration, loggerFactory)
        {
        }
    }
}
