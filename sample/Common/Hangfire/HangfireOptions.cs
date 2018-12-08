namespace Common
{
    public class HangfireOptions
    {

        public int WorkerCount { get; set; }
        public string[] QueueNames { get; set; }
        public string ServerName { get; set; }
    }
}
