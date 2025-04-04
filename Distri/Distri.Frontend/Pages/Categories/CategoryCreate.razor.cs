﻿using CurrieTechnologies.Razor.SweetAlert2;
using Distri.Frontend.Pages.Countries;
using Distri.Frontend.Repositories;
using Distri.Frontend.Shared;
using Distri.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace Distri.Frontend.Pages.Categories
{
    [Authorize(Roles = "Admin")]
    public partial class CategoryCreate
    {

        private Category category = new();
        private FormWithName<Category>? categoryForm;

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;

        private async Task CreateAsync()
        {
            var responseHttp = await Repository.PostAsync("/api/categories", category);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message);
                return;
            }
            Return();

            var toast = SweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Registro creado con éxito.");
        }

        private void Return()
        {
            categoryForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo("/categories");
        }

    }
}
