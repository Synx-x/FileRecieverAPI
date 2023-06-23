using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;

namespace FileReceiverAPI.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class FileController : ControllerBase {
		private readonly IWebHostEnvironment _environment;

		public FileController(IWebHostEnvironment environment) {
			_environment = environment;
		}

		[HttpPost]
		public async Task<IActionResult> ReceiveFileAsync() {

			using (var reader = new StreamReader(Request.Body, Encoding.UTF8)) {
				string requestBody = await reader.ReadToEndAsync();

				string fileName = "file.csv";
				string rootPath = _environment.ContentRootPath;
				string filePath = Path.Combine(rootPath, fileName); 
				
				using (var writer = new StreamWriter(filePath)) {
					await writer.WriteAsync(requestBody);
				}

				return Ok("File received and saved as CSV successfully.");
			}

		}
	}
}
