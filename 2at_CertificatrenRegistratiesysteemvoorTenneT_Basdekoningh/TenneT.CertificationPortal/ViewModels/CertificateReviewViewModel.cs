namespace TenneT.CertificationPortal.ViewModels;

public class CertificateReviewViewModel
{
    public Guid UploadId { get; set; }
    public string TechnicianName { get; set; }
    public string CertificateType { get; set; }
    public DateTime UploadDate { get; set; }
    public string Status { get; set; }
    public string? Comment { get; set; }
    public string DownloadUrl { get; set; }
}
