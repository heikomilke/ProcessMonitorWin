using System.Diagnostics;
using ProcessMonitorService.Data;

namespace ProcessMonitorService.Service;

/// <summary>
/// knows how to get info about running processes
/// </summary>
public class RunningProcessCollector
{
    public RunningProcessCollection GetAll()
    {
        var startTime = DateTime.UtcNow;


        Process[] processes = Process.GetProcesses();
        List<RunningProcess> metaList = new();
        foreach (var p in processes)
        {
            var meta = new RunningProcess(p.Id, p.ProcessName);
            metaList.Add(meta);
        }

        var all = new RunningProcessCollection(startTime, metaList.ToArray());

        return all;
    }
}