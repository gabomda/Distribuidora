using Distri.Frontend.Repositories;
using Distri.Shared.Entities;
using Microsoft.AspNetCore.Components;

namespace Distri.Frontend.Pages.Countries
{
    public partial class CountriesIndex
    {
        [Inject] 
        private IRepository Repository { get; set; }=null!;
        public List<Country>? Countries { get; set; }

        protected async override Task OnInitializedAsync()
        {
            var responseHttp = await Repository.GetAsync<List<Country>>("api/countries");
            Countries = responseHttp.Response;
        }


    }
}
