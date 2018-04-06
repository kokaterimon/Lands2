[assembly: Xamarin.Forms.Dependency(typeof(Lands2.iOS.Implementations.Config))]
namespace Lands2.iOS.Implementations
{
    using System;
    using Interfaces;
    using SQLite.Net.Interop;

    public class Config : IConfig
    {
        private string directoryDB;
        private ISQLitePlatform platform;

        public string DirectoryDB
        {
            get
            {
                if(string.IsNullOrEmpty(directoryDB))
                {
                    var directory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    directoryDB = System.IO.Path.Combine(directory, "..", "Library");
                }
                return directoryDB;
            }
        }

        public ISQLitePlatform Platform
        {
            get
            {
                if(Platform == null)
                {
                    Platform = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
                }
                return Platform;
            }
        }
    }
}
