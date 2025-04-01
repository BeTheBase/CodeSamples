using TenneT.CertificationDomain.Interfaces;

namespace TenneT.CertificationInfrastructure.Logging;

public class AuditLogger : IAuditLogger
{
    private readonly string _logFilePath = "Logs/audit.log";

    public async Task LogAsync(string action, Guid certificateId)
    {
        var message = $"{DateTime.UtcNow:s} | {action} | Certificate ID: {certificateId}";
        Directory.CreateDirectory(Path.GetDirectoryName(_logFilePath)!);
        await File.AppendAllTextAsync(_logFilePath, message + Environment.NewLine);
    }
}
