using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TenneT.CertificationDomain.Interfaces;
using TenneT.CertificationPortal.ViewModels;

namespace TenneT.CertificationPortal.Controllers;

[Authorize(Roles = "Admin, Validator")]
public class AdminController : Controller
{
    private readonly ICertificateReviewService _reviewService;

    public AdminController(ICertificateReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpGet]
    public async Task<IActionResult> Dashboard()
    {
        var list = await _reviewService.GetPendingUploadsAsync();
        return View(list);
    }

    [HttpPost]
    public async Task<IActionResult> Approve(Guid id, string? comment)
    {
        await _reviewService.MarkAsApprovedAsync(id, comment);
        return RedirectToAction(nameof(Dashboard));
    }

    [HttpPost]
    public async Task<IActionResult> Reject(Guid id, string? comment)
    {
        await _reviewService.MarkAsRejectedAsync(id, comment);
        return RedirectToAction(nameof(Dashboard));
    }
}
