namespace Lands2.Helpers
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
        public static string Accept
        {
            get { return Resource.Accept; }
        }
        public static string EmailValidation
        {
            get { return Resource.EmailValidation; }
        }
        public static string Error
        {
            get { return Resource.Error; }
        }
        public static string EmailPlaceHolder
        {
            get { return Resource.EmailPlaceHolder; }
        }
        public static string Rememberme
        {
            get { return Resource.Rememberme; }
        }
        public static string Menu
        {
            get { return Resource.Menu; }
        }
        public static string MyProfile
        {
            get { return Resource.MyProfile; }
        }
        public static string Statistics
        {
            get { return Resource.Statistics; }
        }
        public static string LogOut
        {
            get { return Resource.LogOut; }
        }
    }
}
