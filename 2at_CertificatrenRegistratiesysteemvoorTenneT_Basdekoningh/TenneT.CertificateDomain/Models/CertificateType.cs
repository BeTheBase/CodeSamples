namespace TenneT.CertificationDomain.Models;

public class CertificateType
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public TimeSpan ValidityPeriod { get; set; }

    public ICollection<CertificateUpload> Uploads { get; set; } = new List<CertificateUpload>();
}
