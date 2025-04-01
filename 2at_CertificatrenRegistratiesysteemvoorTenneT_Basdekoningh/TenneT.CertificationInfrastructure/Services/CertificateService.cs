using TenneT.CertificationDomain.Interfaces;
using TenneT.CertificationDomain.Models;
using TenneT.CertificationInfrastructure;
using TenneT.CertificationPortal.ViewModels;

namespace _2at_CertificatrenRegistratiesysteemvoorTenneT_Basdekoningh.TenneT.CertificationInfrastructure.Services;

public class CertificateService : ICertificateService
{
    private readonly CertificationDbContext _context;
    private readonly IBlobStorageService _blobService;

    public CertificateService(CertificationDbContext context, IBlobStorageService blobService)
    {
        _context = context;
        _blobService = blobService;
    }

    public async Task UploadCertificateAsync(CertificateUploadViewModel viewModel)
    {
        var filePath = await _blobService.UploadAsync(viewModel.File);

        var certificate = new CertificateUpload
        {
            Id = Guid.NewGuid(),
            TechnicianId = viewModel.TechnicianId,
            CertificateTypeId = viewModel.CertificateTypeId,
            FilePath = filePath,
            UploadDate = DateTime.UtcNow,
            Status = "Pending"
        };

        _context.CertificateUploads.Add(certificate);
        await _context.SaveChangesAsync();
    }
}
