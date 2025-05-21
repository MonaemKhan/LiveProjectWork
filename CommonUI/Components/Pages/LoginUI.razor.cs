using CentralModels.Administration;
using System.ComponentModel.DataAnnotations;

namespace CommonUI.Components.Pages
{
    public partial class LoginUI
    {
        private LoginModel loginModel = new LoginModel();
        private string message = string.Empty;
        private string passwordInputType = "password";
        private string eyeIconClass = "fas fa-eye";
        private string companyName = "MonaemKhan";

        protected override void OnInitialized()
        {
            companyName = _config["AppName"];
            base.OnInitialized();
        }

        private void TogglePasswordVisibility()
        {
            if (passwordInputType == "password")
            {
                passwordInputType = "text";
                eyeIconClass = "fas fa-eye-slash";
            }
            else
            {
                passwordInputType = "password";
                eyeIconClass = "fas fa-eye";
            }
        }

        private async Task HandleLogin()
        {
            loginModel.projectName = GlobalVariable.projectName;
            message = await _http.getLoginResponce(loginModel);
            if (string.IsNullOrEmpty(message))
            {
                _nav.NavigateTo("/counter");
            }
        }
    }
}
