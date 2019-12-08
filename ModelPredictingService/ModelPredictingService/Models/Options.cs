using System;
using System.Collections.Generic;
using System.Text;

namespace ModelPredictingService.Models
{
    public class Options
    {
        public string StorageEndpoint { get; set; }
        public string QueueEndpoint { get; set; }
        public string QueueName { get; set; }
        public string DataPreprocessingScriptPath { get; set; }
        public string ModelPredictionScriptPath { get; set; }
        public string PythonFullPath { get; set; }
        public int WorkerRefreshTime { get; set; }
    }
}
