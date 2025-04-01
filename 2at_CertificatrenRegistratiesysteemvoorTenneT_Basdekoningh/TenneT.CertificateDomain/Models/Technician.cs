namespace TenneT.CertificationDomain.Models;

public class Technician
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public ICollection<CertificateUpload> Certificates { get; set; } = new List<CertificateUpload>();
}
