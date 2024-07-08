using Microsoft.AspNetCore.Components;

namespace CMTools_2_0.Services
{
    public class ErrorHandlingService
    {
        private readonly NavigationManager _navigationManager;

        public ErrorHandlingService(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }

        public void HandleError(Exception ex)
        {
            _navigationManager.NavigateTo($"/Error?message={Uri.EscapeDataString(ex.Message)}");
        }
    }
}
