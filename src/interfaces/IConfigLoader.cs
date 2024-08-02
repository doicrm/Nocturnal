namespace Nocturnal.src.interfaces
{
    public interface IConfigLoader
    {
        static abstract ValueTask<bool> LoadConfigFile();
    }
}
