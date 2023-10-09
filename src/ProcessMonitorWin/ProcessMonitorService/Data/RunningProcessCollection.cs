namespace ProcessMonitorService.Data;

public class RunningProcessCollection
{
    public DateTime DateCreated { get; }
    public RunningProcess[] Data { get; }


    public RunningProcessCollection(DateTime dateCreated, RunningProcess[] data)
    {
        DateCreated = dateCreated;
        Data = data;
    }
}