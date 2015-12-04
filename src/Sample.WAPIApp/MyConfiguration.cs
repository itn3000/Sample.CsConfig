/// <summary>sample configuration class</summary>
public class MyConfiguration
{
    public string a { get; set; }
    public int b { get; set; }
    /// <summary>sub configuration section</summary>
    public class SubConfiguration
    {
        public double c { get; set; }
        public string d { get; set; }
    }
    /// <summary>ASP.NET Logging properties</summary>
    public class LoggingSetting
    {
        public class LoggingLevel
        {
            public string Default { get; set; }
        }
        public LoggingLevel LogLevel { get; set; }
        public bool IncludeScopes { get; set; }

    }
    public SubConfiguration e { get; set; }
    public LoggingSetting Logging { get; set; }
}