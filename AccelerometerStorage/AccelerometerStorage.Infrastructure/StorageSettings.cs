namespace AccelerometerStorage.Infrastructure
{
    public sealed class StorageSettings
    {
        private StorageSettings()
        {
        }

        public string FileStorageRootPath { get; private set; }
    }
}
