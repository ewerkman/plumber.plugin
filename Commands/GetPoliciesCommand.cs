using Plugin.Plumber.Models;
using Plugin.Plumber.Pipelines;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.Core.Commands;
using Sitecore.Framework.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Plumber.Commands
{
    public class GetPoliciesCommand : CommerceCommand
    {
        private readonly IGetPoliciesPipeline getPoliciesPipeline;

        public GetPoliciesCommand(IGetPoliciesPipeline getPoliciesPipeline, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            this.getPoliciesPipeline = getPoliciesPipeline;
        }

        public async Task<IEnumerable<PolicySnapshotModel>> Process(CommerceContext commerceContext)
        {
            IEnumerable<PolicySnapshotModel> policies;
            using (CommandActivity.Start(commerceContext, this))
            {
                CommercePipelineExecutionContextOptions pipelineContextOptions = commerceContext.GetPipelineContextOptions();
                policies = await this.getPoliciesPipeline.Run(string.Empty, (IPipelineExecutionContextOptions)pipelineContextOptions);
            }
            return policies;
        }
    }
}
