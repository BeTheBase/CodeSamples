namespace ExterneToegangAPI.ViewModels;

public class CertificateDto
{
    public string CertificateType { get; set; }
    public DateTime ApprovedAt { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public string DownloadUrl { get; set; }
}
