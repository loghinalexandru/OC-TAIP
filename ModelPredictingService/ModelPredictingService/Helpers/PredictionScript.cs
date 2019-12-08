using System.Diagnostics;

namespace ModelPredictingService.Models
{
    public class PredictionScript : IPythonScript
    {
        private readonly string _scriptPath;

        public PredictionScript(string scriptPath)
        {
            _scriptPath = scriptPath;
        }

        public void Run(string pythonPath)
        {
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo(pythonPath, _scriptPath)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            process.Start();
        }
    }
}
