using TenneT.CertificationPortal.ViewModels;
using TenneT.CertificationPortal.ViewModels;

namespace TenneT.CertificationDomain.Interfaces;

public interface ICertificateReviewService
{
    Task<List<CertificateReviewViewModel>> GetPendingUploadsAsync();
    Task MarkAsApprovedAsync(Guid id, string? comment);
    Task MarkAsRejectedAsync(Guid id, string? comment);
}
