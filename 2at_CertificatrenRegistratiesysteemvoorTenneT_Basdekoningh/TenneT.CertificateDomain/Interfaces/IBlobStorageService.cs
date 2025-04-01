namespace TenneT.CertificationDomain.Interfaces;

public interface IBlobStorageService
{
    Task<string> UploadAsync(IFormFile file);
    string GenerateDownloadLink(string filePath);
}
