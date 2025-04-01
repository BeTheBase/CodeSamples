namespace TenneT.CertificationDomain.Models;

public class CertificateUpload
{
    public Guid Id { get; set; }

    public Guid TechnicianId { get; set; }
    public Technician Technician { get; set; }

    public Guid CertificateTypeId { get; set; }
    public CertificateType CertificateType { get; set; }

    public DateTime UploadDate { get; set; }
    public string FilePath { get; set; } = string.Empty;
    public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected, Expired
    public string? Comment { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public DateTime? ExpiryDate { get; set; }
}
