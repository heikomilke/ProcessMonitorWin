using Microsoft.AspNetCore.SignalR;

namespace ProcessMonitorUI.Hubs;

public record PingMessage(DateTime DateCreated, long Sequence);

public class SelfTestHub : Hub
{
    public String DefaultRoute = "/api/hub/self-test";

    private readonly ILogger<ProcessMonitorHub> _logger;
    private readonly IHostApplicationLifetime _lt;
    private bool _started;
    private long _sequence;

    public SelfTestHub(ILogger<ProcessMonitorHub> logger, IHostApplicationLifetime lt)
    {
        _logger = logger;
        _lt = lt;
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
        _logger.LogInformation("Staring the self-test hub loop");
        while (true)
        {
            await Task.Delay(1000, _lt.ApplicationStopping);
            if (Clients==null)
                continue;
            await Clients.All.SendAsync("Ping", new PingMessage(DateTime.UtcNow, _sequence++), _lt.ApplicationStopping);
        }
    }
}