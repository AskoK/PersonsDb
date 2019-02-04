using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using PersonDb.Models;
using Newtonsoft.Json;

namespace PersonDb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _config;

        public IndexModel(IConfiguration config)
        {
            _config = config;
        }

        [BindProperty] public List<Persons> Persons { get; set; }
        [BindProperty] public List<Fields> Fields { get; set; }


        public async Task OnGet()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{_config["BaseUrl"]}/api/persons");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    Persons = JsonConvert.DeserializeObject<List<Persons>>(data);
                }
            }

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{_config["BaseUrl"]}/api/field");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    Fields = JsonConvert.DeserializeObject<List<Fields>>(data);
                }
            }
        }
    }
}
