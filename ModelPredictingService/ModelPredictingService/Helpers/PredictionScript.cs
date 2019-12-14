using Anotar.NLog;
using ModelPredictingService.Models;
using System.Diagnostics;

namespace ModelPredictingService.Helpers
{
    public class PredictionScript : IPythonScript
    {
        private readonly string _scriptPath;
        private readonly string _username;

        public PredictionScript(string scriptPath, string username)
        {
            _scriptPath = scriptPath;
            _username = username;
        }

        public void Run(string pythonPath)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo(pythonPath, _scriptPath)
                {
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    Arguments = $"--root_to_predict processed_data_{_username} " +
                                $"--username {_username} " +
                                $"--models_dir models_{_username} " +
                                $"--predictions_dir predictions_{_username}"
                }
            };

            process.Start();

            LogTo.Debug(process.StandardOutput.ReadToEnd());
        }
    }
}