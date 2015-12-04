
class MyConfiguration
{
    public string a { get; set; }
    public int b { get; set; }
    public class SubConfiguration
    {
        public double c { get; set; }
        public string d { get; set; }
    }
    public SubConfiguration e { get; set; }
}