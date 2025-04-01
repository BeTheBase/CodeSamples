namespace Domein.Models;

public class CertificateUpload
{
    public Guid Id { get; set; }
    public Guid TechnicianId { get; set; }
    public Guid CertificateTypeId { get; set; }
    public string FilePath { get; set; }
    public string Status { get; set; }
    public DateTime UploadDate { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public string? Comment { get; set; }
}
