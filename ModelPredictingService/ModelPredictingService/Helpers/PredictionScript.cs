﻿using Anotar.NLog;
using ModelPredictingService.Models;
using System.Diagnostics;

namespace ModelPredictingService.Helpers
{
    public class PredictionScript : IPythonScript
    {
        private readonly string _scriptPath;
        private readonly string _username;
        private readonly string _processGuid;

        public PredictionScript(string scriptPath, string username, string processGuid)
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
                                $" --root_to_predict processed_data_{_processGuid}_{_username} " +
                                $" --username {_username} " +
                                $" --models_dir model_{_processGuid}_{_username} " +
                                $" --predictions_dir predictions_{_processGuid}_{_username} "
                }
            };

            process.Start();

            LogTo.Debug(process.StandardOutput.ReadToEnd());
        }
    }
}