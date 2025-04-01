using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using TenneT.CertificationDomain.Interfaces;

namespace TenneT.CertificationInfrastructure.Storage;

public class AzureBlobStorageService : IBlobStorageService
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly string _containerName = "certificates";

    public AzureBlobStorageService(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }

    public async Task<string> UploadAsync(IFormFile file)
    {
        var container = _blobServiceClient.GetBlobContainerClient(_containerName);
        await container.CreateIfNotExistsAsync();

        var blobName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        var blobClient = container.GetBlobClient(blobName);

        await using var stream = file.OpenReadStream();
        await blobClient.UploadAsync(stream, overwrite: true);

        return blobName;
    }

    public string GenerateDownloadLink(string filePath)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
        var blobClient = containerClient.GetBlobClient(filePath);

        if (!blobClient.CanGenerateSasUri)
            throw new InvalidOperationException("Blob SAS generation is not supported.");

        var sasUri = blobClient.GenerateSasUri(BlobSasPermissions.Read, DateTimeOffset.UtcNow.AddHours(1));
        return sasUri.ToString();
    }
}
