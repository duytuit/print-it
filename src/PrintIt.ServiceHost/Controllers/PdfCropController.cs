using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrintIt.Core;

namespace PrintIt.ServiceHost.Controllers
{
    [ApiController]
    [Route("pdf")]
    public class PdfCropController : ControllerBase
    {
        private readonly IPdfCropService _pdfCropService;

        public PdfCropController(IPdfCropService pdfCropService)
        {
            _pdfCropService = pdfCropService;
        }

        [HttpPost]
        [Route("to-png")]
        public IActionResult PdfToPng([FromForm] PdfFromTemplateRequest request)
        {
            if (request.PdfFile == null || request.PdfFile.Length == 0)
            {
                return BadRequest("No PDF file provided.");
            }

            try
            {
                _pdfCropService.CropPdfPageAsync(request.PdfFile, request.OutputPath ??  "/image/convert1.png", request.X ?? 0, request.Y ?? 1300, request.Width ?? 2384, request.Height  ?? 770);
                return StatusCode(200,"thành công");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
    public sealed class PdfFromTemplateRequest
    {
        [Required]
        public IFormFile PdfFile { get; set; }

        public string? OutputPath { get; set; }

        public int? X { get; set; }

        public int? Y { get; set; }

        public int? Width { get; set; }
        public int? Height { get; set; }
    }
}
