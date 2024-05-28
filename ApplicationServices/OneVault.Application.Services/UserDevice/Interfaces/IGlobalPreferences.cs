namespace OneVault.ApplicationServices.UserDevices.Interfaces
{
    public interface IGlobalPreferences
    {
        void Save(string key, string value);
        string Get(string key);
    }
}
