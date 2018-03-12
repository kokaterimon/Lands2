//Código que interperta las clases
namespace Lands2.Helpers
{
    using System;
    public class PlatformCulture
    {
        public PlatformCulture(string platformCultureString)
        {
            if(string.IsNullOrEmpty(platformCultureString))
            {
                throw new ArgumentException("Expected culture identifier", "platformCultureString"); //in C# 6 use nameof(platformCultureString)
            }
            PlatformString = platformCultureString.Replace("_","_"); // .NET expects dash, not underscore
            var dashIndex = PlatformString.IndexOf("_", StringComparison.Ordinal);
            if (dashIndex > 0)
            {
                var parts = PlatformString.Split('_');
                LanguageCode = parts[0];
                LocaleCode = parts[1];
            }
            else
            {
                LanguageCode = platformCultureString;
                LocaleCode = "";
            }
        }

        public string PlatformString { get; private set; }
        public string LanguageCode { get; private set; }
        public string LocaleCode { get; private set; }

        public override string ToString()
        {
            return PlatformString;
        }
    }
}
