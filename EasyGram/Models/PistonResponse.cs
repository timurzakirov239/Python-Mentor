namespace EasyGram.Models
{
    public class PistonResponse
    {
        public RunResult run { get; set; }
    }

    public class RunResult
    {
        public string stdout { get; set; }
        public string stderr { get; set; }
        public int code { get; set; }
    }
}
