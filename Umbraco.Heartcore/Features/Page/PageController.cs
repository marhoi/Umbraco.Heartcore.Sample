using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Heartcore.Core;

namespace Umbraco.Heartcore.Page
{
	[Route("api/v1/page")]
	[ApiController]
	public class PageController : ControllerBase
    {
		private readonly IUmbracoContextHelper ContextHelper;
		public PageController(IUmbracoContextHelper contextHelper)
		{
			ContextHelper = contextHelper ?? throw new ArgumentNullException(nameof(contextHelper));
		}

		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		[HttpGet]
		[Route("{id}")]
		public async Task<IActionResult> Get(string id)
		{
			var content = await ContextHelper.GetPageById(id);

			return StatusCode((int)content.statusCode, content.content);
		}
	}
}