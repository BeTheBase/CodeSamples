using TenneT.CertificationPortal.ViewModels;

namespace TenneT.CertificationDomain.Interfaces;

public interface ICertificateService
{
    Task UploadCertificateAsync(CertificateUploadViewModel viewModel);
}
