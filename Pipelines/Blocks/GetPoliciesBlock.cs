using Plugin.Plumber.Models;
using Sitecore.Commerce.Core;
using Sitecore.Framework.Configuration;
using Sitecore.Framework.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Plugin.Plumber.Pipelines.Blocks
{
    public class GetPoliciesBlock : PipelineBlock<string, IEnumerable<PolicySnapshotModel>>
    {
        public override async Task<IEnumerable<PolicySnapshotModel>> Run(string arg, IPipelineExecutionContext context)
        {
            return await Task.Run(() =>
            {
                IEnumerable<Type> policyTypes = GetClassesInheritingFromPolicy();

                var policyValues = new List<PolicySnapshotModel>();

                foreach (var policyType in policyTypes)
                {
                    try
                    {
                        Policy policy = InvokePolicy(context, policyType);

                        var policySnapshotModel = new PolicySnapshotModel(policyType.FullName, policy);
                        policyValues.Add(policySnapshotModel);
                    }
                    catch (Exception ex)
                    {
                        context.Logger.LogInformation($"'{policyType.FullName}' could not be activated.");
                    }
                }

                return policyValues;
            });
        }

        private Policy InvokePolicy(IPipelineExecutionContext context, Type policyType)
        {
            var getPolicyMethod = context.GetType().GetMethod("GetPolicy");
            var genericMethod = getPolicyMethod.MakeGenericMethod(new Type[] { policyType });
            var policy = genericMethod.Invoke(context, null) as Policy;
            return policy;
        }

        private IEnumerable<Type> GetClassesInheritingFromPolicy()
        {
            var policyClassType = typeof(Policy);
            var policyTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => p.IsClass && p.IsSubclassOf(policyClassType));
            return policyTypes;
        }
    }
}
