using Microsoft.EntityFrameworkCore;
using TenneT.CertificationDomain.Models;
using TenneT.CertificationDomain.Interfaces;

namespace TenneT.CertificationInfrastructure.Repositories;

public class CertificateRepository : ICertificateRepository
{
    private readonly CertificationDbContext _context;

    public CertificateRepository(CertificationDbContext context)
    {
        _context = context;
    }

    public async Task<List<CertificateUpload>> GetPendingUploadsAsync()
    {
        return await _context.CertificateUploads
            .Include(c => c.Technician)
            .Include(c => c.CertificateType)
            .Where(c => c.Status == "Pending")
            .ToListAsync();
    }

    public async Task UpdateStatusAsync(Guid id, string status, string? comment)
    {
        var cert = await _context.CertificateUploads.FindAsync(id);
        if (cert == null) return;

        cert.Status = status;
        cert.Comment = comment;

        if (status == "Approved")
        {
            cert.ApprovedAt = DateTime.UtcNow;
            cert.ExpiryDate = DateTime.UtcNow.Add(cert.CertificateType.ValidityPeriod);
        }

        await _context.SaveChangesAsync();
    }

    public async Task<List<CertificateUpload>> GetExpiredCertificatesAsync(DateTime now)
    {
        return await _context.CertificateUploads
            .Where(c => c.ExpiryDate.HasValue && c.ExpiryDate <= now && c.Status != "Expired")
            .ToListAsync();
    }
}
