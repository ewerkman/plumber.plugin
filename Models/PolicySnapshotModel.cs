using Sitecore.Commerce.Core;

namespace Plugin.Plumber.Models
{
    public class PolicySnapshotModel 
    {
        public PolicySnapshotModel(string policyType, Policy policy)
        {
            this.PolicyType = policyType;
            this.Policy = policy;
        }

        public string PolicyType { get; set; }
        public Policy Policy { get; set; }
    }
}
