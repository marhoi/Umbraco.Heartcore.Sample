using Refit;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Net;
using System.Threading.Tasks;
using Umbraco.Headless.Client.Net.Delivery;
using Umbraco.Heartcore.Core;

namespace Umbraco.Heartcore.Features.Shared
{
	public class UmbracoContextHelper : IUmbracoContextHelper
	{
		private readonly ContentDeliveryService ContentDelivery;

		public UmbracoContextHelper(ContentDeliveryService contentDelivery)
		{
			ContentDelivery = contentDelivery ?? throw new ArgumentNullException(nameof(contentDelivery));
		}

		public async Task<(IDictionary<string, object> content, HttpStatusCode statusCode)> GetPageById(string id)
		{
			Guid.TryParse(id, out Guid guid);

			if(guid.Equals(Guid.Empty))
			{
				return (ImmutableDictionary<string, object>.Empty, HttpStatusCode.BadRequest);
			}

			try
			{
				var content = await ContentDelivery.Content.GetById(guid);

				// Uncomment below if you want to filter the base properties from Umbraco Heartcore and only return your own custom properties
				//var umbracoProperties = content.Properties.Keys.Where(p => p.StartsWith("_")).ToList();
				//foreach(var key in umbracoProperties)
				//{
				//	content.Properties.Remove(key);
				//}

				return (content.Properties, HttpStatusCode.OK);
			}
			catch(ApiException ae)
			{
				return (ImmutableDictionary<string, object>.Empty, ae.StatusCode);
			}		
		}

		public async Task<(IDictionary<string, object> content, HttpStatusCode statusCode)> GetPageByUrl(string url)
		{
			try
			{
				var content = await ContentDelivery.Content.GetByUrl(url);

				// Uncomment below if you want to filter the base properties from Umbraco Heartcore and only return your own custom properties
				//var umbracoProperties = content.Properties.Keys.Where(p => p.StartsWith("_")).ToList();
				//foreach(var key in umbracoProperties)
				//{
				//	content.Properties.Remove(key);
				//}

				return (content.Properties, HttpStatusCode.OK);
			}
			catch(ApiException ae)
			{
				return (ImmutableDictionary<string, object>.Empty, ae.StatusCode);
			}
		}

		public async Task<(IEnumerable<IDictionary<string, object>> content, HttpStatusCode statusCode)> GetPagesByContentType(string contentTypeAlias)
		{
			try
			{
				var content = await ContentDelivery.Content.GetByType(contentTypeAlias);

				if(content?.Content?.Items.Any() != true)
				{
					return (new List<ImmutableDictionary<string, object>>(), HttpStatusCode.NotFound);
				}

				var pages = new List<IDictionary<string, object>>();
				foreach(var item in content.Content.Items)
				{
					pages.Add(item.Properties);					
				}				

				return (pages, HttpStatusCode.OK);
			}
			catch(ApiException ae)
			{
				return (new List<ImmutableDictionary<string, object>>(), ae.StatusCode);
			}
		}
	}
}
