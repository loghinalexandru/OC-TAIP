using AccelerometerStorage.Domain;

namespace AccelerometerStorage.Tests.Common
{
    public static class DataFileFactory
    {
        public static DataFile GetDataFile(User user)
        {
            return DataFile.Create("CsvExample.csv", user).Value;
        }
    }
}
