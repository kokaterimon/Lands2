namespace Lands2.Interfaces
{
    using SQLite.Net.Interop;
    public interface IConfig
    {
        string DirectoryDB { get; }
        ISQLitePlatform Platform { get; } //BR: cuáles son las librerías que vas a usar de la base de datos
    }
}
