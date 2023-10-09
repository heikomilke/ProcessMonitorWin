namespace ProcessMonitorService.Data;

public class RunningProcess
{
    public int Id { get; }
    public string ProcessName { get; }

    public RunningProcess(int id, string processName)
    {
        Id = id;
        ProcessName = processName;
    }
}