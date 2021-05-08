using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using UploadFilesServer.Context;
using UploadFilesServer.Models;

namespace UploadFilesServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly FileContext _context;

        public FilesController(FileContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("get_all")]
        public IActionResult GetAllFiles()
        {
            try
            {
                var files = _context.Files.ToList();

                return Ok(files);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet]
        [Route("get_current_file_types")]
        public IActionResult GetCurrentFileTypes()
        {
            try
            {
                var files = _context.Files.ToList();
                var fileTypes = files.Select(x => x.FileType).Distinct().ToList();

                return Ok(fileTypes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPost]
        [Route("create")]
        public IActionResult CreateFile([FromBody]File file)
        {
            try
            {
                if (file == null)
                {
                    return BadRequest("File object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                
                int.TryParse(_configuration["fileUploadRestrictions:maxFileSizeInBytes"], out int maxFileSize);

                if (file.FileSize > maxFileSize)
                {
                    return Problem(detail: $"File size cannot be bigger than limit ({maxFileSize} bytes)", title: "Error");
                }

                var allowedFileTypesString = _configuration["fileUploadRestrictions:allowedFileTypes"];
                var allowedFileTypes = allowedFileTypesString.Split('|').ToList();

                if (!allowedFileTypes.Contains(file.FileType))
                {
                    return Problem(detail: $"Only files with these types can be uploaded: {allowedFileTypesString}", title: "Error");
                }

                file.Id = Guid.NewGuid();
                file.UploadDate = DateTime.UtcNow;
                _context.Add(file);
                _context.SaveChanges();

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
