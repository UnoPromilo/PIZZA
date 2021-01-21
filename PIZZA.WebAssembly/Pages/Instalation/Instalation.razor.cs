using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PIZZA.Models.Instalation;
using PIZZA.WebAssembly.Api.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIZZA.WebAssembly.Pages.Instalation
{
    public sealed partial class Instalation
    {
        private bool _showSavingAnimation = false;
        private SQLServerConfiguration _sqlServerConfiguration = new();
        private AdministratorUserCreationModel _administratorUserCreationModel = new();
        private FillDatabse _fillDatabase = new();

        private EditContext _sqlServerConfigurationEditContext;
        private EditContext _administratorUserCreationModelEditContext;
        private EditContext _fillDatabaseEditContext;

        [Inject]
        private IConfigurationService ConfigurationService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        protected override void OnInitialized()
        {
            _sqlServerConfiguration = new();
            _administratorUserCreationModel = new();
            _fillDatabase = new();

            _sqlServerConfigurationEditContext = new(_sqlServerConfiguration);
            _administratorUserCreationModelEditContext = new(_administratorUserCreationModel);
            _fillDatabaseEditContext = new(_fillDatabase);

            base.OnInitialized();
        }

        private async Task SaveSettings()
        {
            _showSavingAnimation = true;
            StateHasChanged();
            //Validate
            var result = true;
            result &= _sqlServerConfigurationEditContext.Validate();
            result &= _administratorUserCreationModelEditContext.Validate();
            result &= _fillDatabaseEditContext.Validate();
            if (result)
            {
                //Send configuration to server
                InstalationInfo instalationInfo = new()
                {
                    SQLServerConfiguration = _sqlServerConfiguration,
                    AdministratorUserCreationModel = _administratorUserCreationModel,
                    FillDatabse = _fillDatabase
                };
                var response = await ConfigurationService.PostConfigureServer(instalationInfo);
                if (response.IsSuccessStatusCode)
                    NavigationManager.NavigateTo("/", true);
            }

            _showSavingAnimation = false;
            StateHasChanged();
        }
    }
}
