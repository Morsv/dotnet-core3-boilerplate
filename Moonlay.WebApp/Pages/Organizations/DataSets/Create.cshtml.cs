using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moonlay.Confluent.Kafka;
using Moonlay.Core.Models;
using Moonlay.WebApp.Clients;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Linq;

namespace Moonlay.WebApp
{
    public class CreateDataSetModel : PageModel
    {
        public class NewDataSetForm
        {
            public class AttributeArg
            {
                public string Name { get; set; }
                public string Type { get; set; }
                public string Value { get;  set; }
                public string PrimaryKey { get;  set; }
                public string AutoIncrement { get;  set; }
                public string Null { get;  set; }
            }

            [Required]
            [MaxLength(64)]
            [Display(Name="Domain")]
            public string DomainName { get; set; }

            [Required]
            [MaxLength(64)]
            public string Name { get; set; }

            [Required]
            [MaxLength(64)]
            [Display(Name = "Organization")]
            public string OrgName { get; set; }

            public List<AttributeArg> AttributeArgs { get; set; } = new List<AttributeArg>();
        }

        private readonly IKafkaProducer _producer;
        private readonly IManageDataSetClient _dataSetClient;
        private readonly ISignInService _signIn;

        [BindProperty]
        public NewDataSetForm Form { get; set; }

        public CreateDataSetModel(IManageDataSetClient dataSetClient, ISignInService signIn)
        {
            _dataSetClient = dataSetClient;
            _signIn = signIn;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var request = new MasterData.Protos.NewDatasetReq
            {
                Name = Form.Name,
                DomainName = Form.DomainName,
                OrganizationName = Form.OrgName,
            };

            Form.AttributeArgs.ForEach(o => request.Attribute.Add(new MasterData.Protos.AttributeARG
            {
                Name = o.Name,
                Type = o.Type,
                Value = o.Value,
                Primarykey = o.PrimaryKey,
                Autoincrement = o.AutoIncrement,
                Null = o.Null
            }
            ));

            var reply = await _dataSetClient.NewDatasetAsync(request);

            return RedirectToPage("./Index");
        }
    }
}