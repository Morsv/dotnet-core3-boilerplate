using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moonlay.Core.Models;
using Moonlay.WebApp.Clients;

namespace Moonlay.WebApp
{
    public class IndexDataSetModel : PageModel
    {
        public class GetAllDataset
        {
            public string Name { get; set; }
            public string  DomainName { get; set; }
            public string OrganizationName { get; set; }
        }

        private readonly IManageDataSetClient _dataSetClient;
        private readonly ISignInService _signIn;

        public GetAllDataset getView { get; set; }

        public IndexDataSetModel(IManageDataSetClient dataSetClient, ISignInService signIn)
        {
            _dataSetClient = dataSetClient;
            _signIn = signIn;
        }

        public async Task<IActionResult> OnGet()
        {
            var request = new MasterData.Protos.GetAllDatasetReq
            {
                Name = getView.Name,
                DomainName = getView.DomainName,
                OrganizationName = getView.OrganizationName,
            };

            var reply =  await _dataSetClient.GetAllDatasetAsync(request);

            return RedirectToPage("/Index");
        }
    }
}