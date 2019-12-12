namespace ModelTrainingService.Models
{
    public sealed class Options
    {
        public string StorageEndpoint { get; set; }
        public string DataPreprocessingScriptPath { get; set; }
        public string ModelGenerationScriptPath { get; set; }
        public string PythonFullPath { get; set; }
        public int WorkerRefreshTime { get; set; }
    }
}