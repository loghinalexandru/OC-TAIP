using Anotar.NLog;
using ModelPredictingService.Models;
using System.Diagnostics;

namespace ModelPredictingService.Helpers
{
    public class FeatureExtractionScript : IPythonScript
    {
        private readonly string _scriptPath;
        private readonly string _username;
        private readonly string _processGuid;

        public FeatureExtractionScript(string scriptPath, string username, string processGuid)
        {
            _scriptPath = scriptPath;
            _username = username;
            _processGuid = processGuid;
        }

        public void Run(string pythonPath)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo(pythonPath)
                {
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    Arguments = _scriptPath +
                                $" --raw_data_dir data_{_processGuid}_{_username} " +
                                $" --save_processed_data processed_data_{_processGuid}_{_username} "
                }
            };

            process.Start();

            LogTo.Debug(process.StandardOutput.ReadToEnd());
        }
    }
}