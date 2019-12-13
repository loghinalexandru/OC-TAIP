namespace ModelPredictingService.Models
{
    public interface IScriptRunner
    {
        void Execute(IPythonScript script);
    }
}