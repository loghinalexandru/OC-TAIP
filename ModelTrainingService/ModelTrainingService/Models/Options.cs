namespace ModelTrainingService.Models
{
    public class Options
    {
        public string StorageEndpoint { get; set; }
        public string ScriptPath { get; set; }
        public int WorkerRefreshTime { get; set; }
    }
}