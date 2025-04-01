namespace TenneT.CertificationDomain.Interfaces;

public interface IAuditLogger
{
    Task LogAsync(string action, Guid certificateId);
}
