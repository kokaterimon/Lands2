namespace Lands2.Interfaces
{
    //Para obtener el idioma del teléfono, que es diferente en Android/IOs
    using System.Globalization;
    public interface ILocalize
    {
        CultureInfo GetCurrentCultureInfo();
        void SetLocale(CultureInfo ci);
    }
}
