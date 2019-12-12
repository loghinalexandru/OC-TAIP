using ModelTrainingService.Models;
using System.Diagnostics;
using ModelTrainingService.Models.Interfaces;

namespace ModelTrainingService.Helpers
{
    public class FeatureExtractionScript : IPythonScript
    {
        private readonly string _scriptPath;

        public FeatureExtractionScript(string scriptPath)
        {
            _scriptPath = scriptPath;
        }

        public void Run(string pythonPath)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo(pythonPath, _scriptPath)
                {
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
        }
    }
}