using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Plugin.Plumber.Models;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.Core.Commands;
using Sitecore.Framework.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.OData;
using Newtonsoft.Json;
using Plugin.Plumber.Commands;

namespace Plugin.Plumber.Controllers
{
    [EnableQuery]
    [Route("plumber/[controller]")]
    public class PolicyController : CommerceController
    {
        public PolicyController(IServiceProvider serviceProvider, CommerceEnvironment globalEnvironment) : base(serviceProvider, globalEnvironment)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetPolicy()
        {
            if (!this.ModelState.IsValid)
                return (IActionResult)this.NotFound();

            var getPoliciesCommand = this.Command<GetPoliciesCommand>();
            var policySnapshots = await getPoliciesCommand.Process(this.CurrentContext);

            return new ObjectResult(policySnapshots);
        }
    }
}
