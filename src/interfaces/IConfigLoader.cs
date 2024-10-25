namespace Nocturnal.interfaces
{
    public interface IConfigLoader
    {
        static abstract ValueTask<bool> LoadConfigFile();
    }
}
