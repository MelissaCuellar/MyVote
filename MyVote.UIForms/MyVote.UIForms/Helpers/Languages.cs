
namespace MyVote.UIForms.Helpers
{
    using Interfaces;
    using Resources;
    using Xamarin.Forms;

    public static class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Accept => Resource.Accept;

        public static string Error => Resource.Error;

        public static string EmailError => Resource.EmailError;

        public static string ErrorPassword => Resource.ErrorPassword;

        public static string ErrorLogin => Resource.ErrorLogin;

        public static string Login => Resource.Login;

        public static string Email => Resource.Email;
        
        public static string Password => Resource.Password;

        public static string Register => Resource.Register;

        public static string EmailPlaceHolder => Resource.EmailPlaceHolder;

        public static string PasswordPlaceHolder => Resource.PasswordPlaceHolder;

        public static string Remember => Resource.Remember;

        public static string RegisterNew => Resource.RegisterNew;

        public static string Firstname => Resource.Firstname;

        public static string Lastname => Resource.Lastname;

        public static string FirtsNamePlaceHolder => Resource.FirtsNamePlaceHolder;

        public static string LastNamePlaceHoler => Resource.LastNamePlaceHoler;

        public static string Country => Resource.Country;

        public static string CountryPlaceHolder => Resource.CountryPlaceHolder;

        public static string City => Resource.City;

        public static string CityPlaceholder => Resource.CityPlaceholder;

        public static string Occupation => Resource.Occupation;

        public static string OccupationPlaceHolder => Resource.OccupationPlaceHolder;

        public static string Phone => Resource.Phone;

        public static string PhonePlaceHolder => Resource.PhonePlaceHolder;

        public static string PasswordConfirmPH => Resource.PasswordConfirmPH;

        public static string ConfirmPassword => Resource.ConfirmPassword;

    }

}
