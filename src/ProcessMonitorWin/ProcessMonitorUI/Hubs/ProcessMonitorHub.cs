using Microsoft.AspNetCore.SignalR;
using ProcessMonitorService.Service;

namespace ProcessMonitorUI.Hubs;

public class ProcessMonitorHub : Hub
{
    public String DefaultRoute = "/api/hub/process-monitor";

    private readonly ILogger<ProcessMonitorHub> _logger;
    private readonly IHostApplicationLifetime _lt;
    private readonly RunningProcessCollector _collector;
    private bool _started;

    public ProcessMonitorHub(ILogger<ProcessMonitorHub> logger, IHostApplicationLifetime lt,
        RunningProcessCollector collector)
    {
        _logger = logger;
        _lt = lt;
        _collector = collector;
    }

    public void Start()
    {
        if (_started)
        {
            return;
        }
        Task.Factory.StartNew(StartTicking);
        _started = true;
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

    public override Task OnConnectedAsync()
    {
        _logger.LogInformation("Client connected.");

        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        _logger.LogInformation("Client disconnected. exception: "+ exception);
        return base.OnDisconnectedAsync(exception);
    }
}