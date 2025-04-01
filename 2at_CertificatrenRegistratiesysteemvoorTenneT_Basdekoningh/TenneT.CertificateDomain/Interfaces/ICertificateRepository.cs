using TenneT.CertificationDomain.Models;

public interface ICertificateRepository
{
    Task<List<CertificateUpload>> GetPendingUploadsAsync();
    Task UpdateStatusAsync(Guid id, string status, string? comment);
    Task<List<CertificateUpload>> GetExpiredCertificatesAsync(DateTime now);
}
