namespace PortaalMonteur.ViewModels;

public class CertificateUploadViewModel
{
    public Guid TechnicianId { get; set; }
    public Guid CertificateTypeId { get; set; }
    public IFormFile File { get; set; }
}
