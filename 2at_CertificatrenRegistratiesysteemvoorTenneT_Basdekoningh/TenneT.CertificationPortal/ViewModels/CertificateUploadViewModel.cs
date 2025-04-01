using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TenneT.CertificationPortal.ViewModels;

public class CertificateUploadViewModel
{
    [Required]
    public Guid TechnicianId { get; set; }

    [Required]
    public Guid CertificateTypeId { get; set; }

    [Required]
    public IFormFile File { get; set; }

    public List<SelectListItem> CertificateTypes { get; set; } = new();
}
