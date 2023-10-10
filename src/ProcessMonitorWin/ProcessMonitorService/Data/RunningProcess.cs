namespace ProcessMonitorService.Data;

public class RunningProcess
{
    public int Id { get; }
    public string ProcessName { get; }
    public string ProcessPath { get; }

    public RunningProcess(int id, string processName, string processPath)
    {
        Id = id;
        ProcessName = processName;
        ProcessPath = processPath;
    }
}