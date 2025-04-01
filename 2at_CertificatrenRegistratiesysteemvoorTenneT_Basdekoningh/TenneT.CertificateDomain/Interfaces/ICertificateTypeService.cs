using Microsoft.AspNetCore.Mvc.Rendering;

namespace TenneT.CertificationDomain.Interfaces;

public interface ICertificateTypeService
{
    Task<List<SelectListItem>> GetCertificateTypeSelectListAsync();
}
