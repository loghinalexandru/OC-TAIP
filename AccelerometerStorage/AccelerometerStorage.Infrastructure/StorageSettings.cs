namespace AccelerometerStorage.Infrastructure
{
    public sealed class StorageSettings
    {
        private StorageSettings()
        {
        }

        public static StorageSettings Dummy => new StorageSettings
        {
            FileStorageRootPath = ".\\dummy-path"
        };

        public string FileStorageRootPath { get; private set; }
    }
}
