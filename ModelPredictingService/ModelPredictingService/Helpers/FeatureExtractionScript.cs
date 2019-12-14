using Anotar.NLog;
using ModelPredictingService.Models;
using System.Diagnostics;

namespace ModelPredictingService.Helpers
{
    public class FeatureExtractionScript : IPythonScript
    {
        private readonly string _scriptPath;
        private readonly string _username;

        public FeatureExtractionScript(string scriptPath, string username)
        {
            _scriptPath = scriptPath;
            _username = username;
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
                                $" --raw_data_dir data_{_username} " +
                                $" --save_processed_data processed_data_{_username} "
                }
            };

            process.Start();

            LogTo.Debug(process.StandardOutput.ReadToEnd());
        }
    }
}