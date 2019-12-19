using AccelerometerStorage.Domain;

namespace AccelerometerStorage.Tests.Common
{
    public static class DataFileFactory
    {
        public static DataFile GetDataFile(User user)
        {
            return DataFile.Create("CsvExample.csv", user, FileType.Input).Value;
        }

        public static DataFile GetModelFile(User user)
        {
            return DataFile.Create("Model.h5", user, FileType.Model).Value;
        }
    }
}
