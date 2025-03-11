using CurrieTechnologies.Razor.SweetAlert2;
using Distri.Frontend.Pages.Countries;
using Distri.Frontend.Repositories;
using Distri.Frontend.Shared;
using Distri.Shared.Entities;
using Microsoft.AspNetCore.Components;

namespace Distri.Frontend.Pages.Categories
{
    public partial class CategoryEdit
    {
        private Category? category;
        private FormWithName<Category>? categoryForm;

        [Inject] private NavigationManager navigationManager { get; set; } = null!;
        [Inject] private IRepository repository { get; set; } = null!;
        [Inject] private SweetAlertService sweetAlertService { get; set; } = null!;
        [EditorRequired, Parameter] public int Id { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            var responseHttp = await repository.GetAsync<Category>($"api/categories/{Id}");

            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    navigationManager.NavigateTo("categories");
                }
                else
                {
                    var messageError = await responseHttp.GetErrorMessageAsync();
                    await sweetAlertService.FireAsync("Error", messageError, SweetAlertIcon.Error);
                }
            }
            else
            {
                category = responseHttp.Response;
            }
        }

        private async Task EditAsync()
        {
            var responseHttp = await repository.PutAsync("api/categories", category);

            if (responseHttp.Error)
            {
                var mensajeError = await responseHttp.GetErrorMessageAsync();
                await sweetAlertService.FireAsync("Error", mensajeError, SweetAlertIcon.Error);
                return;
            }

            Return();
            var toast = sweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Cambios guardados con éxito.");
        }

        private void Return()
        {
            categoryForm!.FormPostedSuccessfully = true;
            navigationManager.NavigateTo("categories");
        }

    }
}
