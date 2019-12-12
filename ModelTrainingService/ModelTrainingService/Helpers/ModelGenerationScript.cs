using Anotar.NLog;
using ModelTrainingService.Models;
using System.Diagnostics;
using ModelTrainingService.Models.Interfaces;

namespace ModelTrainingService.Helpers
{
    public class ModelGenerationScript : IPythonScript
    {
        private readonly string _scriptPath;

        public ModelGenerationScript(string scriptPath)
        {
            _scriptPath = scriptPath;
        }

        public void Run(string pythonPath)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo(pythonPath, _scriptPath)
                {
                    RedirectStandardOutput = true, UseShellExecute = false, CreateNoWindow = true
                }
            };
            process.Start();

            LogTo.Debug(process.StandardOutput.ReadToEnd());
        }
    }
}