using System.Diagnostics;
using Anotar.NLog;
using ModelPredictingService.Models;

namespace ModelPredictingService.Helpers
{
    public class PredictionScript : IPythonScript
    {
        private readonly string _scriptPath;
        private readonly string _useranme;

        public PredictionScript(string scriptPath, string username)
        {
            _scriptPath = scriptPath;
            _useranme = username;
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
                    Arguments = $"--username {_useranme}"
                }
            };

            process.Start();

            LogTo.Debug(process.StandardOutput.ReadToEnd());
        }
    }
}
