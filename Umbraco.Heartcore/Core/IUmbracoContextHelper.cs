using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Umbraco.Heartcore.Core
{
	public interface IUmbracoContextHelper
	{
		Task<(IDictionary<string, object> content, HttpStatusCode statusCode)> GetPageById(string id);
		Task<(IDictionary<string, object> content, HttpStatusCode statusCode)> GetPageByUrl(string url);
		Task<(IEnumerable<IDictionary<string, object>> content, HttpStatusCode statusCode)> GetPagesByContentType(string contentTypeAlias);

	}
}
