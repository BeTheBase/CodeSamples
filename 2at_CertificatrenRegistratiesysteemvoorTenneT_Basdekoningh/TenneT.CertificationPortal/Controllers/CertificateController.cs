using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TenneT.CertificationDomain.Models;
using TenneT.CertificationDomain.Interfaces;
using TenneT.CertificationPortal.ViewModels;

namespace TenneT.CertificationPortal.Controllers;

[Authorize(Roles = "Technician")]
public class CertificateController : Controller
{
    private readonly ICertificateService _certificateService;
    private readonly ICertificateTypeService _typeService;

    public CertificateController(ICertificateService certificateService, ICertificateTypeService typeService)
    {
        _certificateService = certificateService;
        _typeService = typeService;
    }

    [HttpGet]
    public async Task<IActionResult> Upload()
    {
        var model = new CertificateUploadViewModel
        {
            CertificateTypes = await _typeService.GetCertificateTypeSelectListAsync()
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Upload(CertificateUploadViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.CertificateTypes = await _typeService.GetCertificateTypeSelectListAsync();
            return View(model);
        }

        await _certificateService.UploadCertificateAsync(model);
        TempData["Success"] = "Certificate successfully uploaded.";
        return RedirectToAction(nameof(Upload));
    }
}
