namespace TemplateDDD.Core.Utility
{
    public class ENV
    {
        public static readonly string TEMPLATEDDD = System.Environment.GetEnvironmentVariable("TEMPLATEDDD", EnvironmentVariableTarget.Machine);
    }
}