using Microsoft.AspNetCore.SignalR;
using ProcessMonitorService.Service;

namespace ProcessMonitorUI.Hubs;

public class ProcessMonitorHub : Hub
{
    public String DefaultRoute = "/api/hub/process-monitor";

    private readonly ILogger<ProcessMonitorHub> _logger;
    private readonly IHostApplicationLifetime _lt;
    private readonly RunningProcessCollector _collector;

    public ProcessMonitorHub(ILogger<ProcessMonitorHub> logger, IHostApplicationLifetime lt,
        RunningProcessCollector collector)
    {
        _logger = logger;
        _lt = lt;
        _collector = collector;
    }

    public void Start()
    {
        Task.Factory.StartNew(StartTicking);
    }

    private async Task StartTicking()
    {
        while (true)
        {
            await Task.Delay(1000, _lt.ApplicationStopping);
            var all = _collector.GetAll();
            await Clients.All.SendAsync("UpdateAll", all, _lt.ApplicationStopping);
        }
    }
}