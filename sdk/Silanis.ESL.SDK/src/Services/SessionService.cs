using System;
using Newtonsoft.Json;
using Silanis.ESL.SDK.Internal;

namespace Silanis.ESL.SDK.Services
{
	/// <summary>
	/// The SessionService class provides a method to create a session token for a signer.
	/// </summary>
	public class SessionService
	{
		private string apiToken;
		private UrlTemplate template;

		/// <summary>
		/// Initializes a new instance of the <see cref="Silanis.ESL.SDK.SessionService"/> class.
		/// </summary>
		/// <param name="apiToken">API token.</param>
		/// <param name="baseUrl">Base URL.</param>
		public SessionService (string apiToken, string baseUrl)
		{
			this.apiToken = apiToken;
			template = new UrlTemplate (baseUrl);
		}

		/// <summary>
		/// Creates a session token for a signer and returns the session token.
		/// </summary>
		/// <returns>The session token for signer.</returns>
		/// <param name="packageId">The package id.</param>
		/// <param name="signer">The signer to create a session token for.</param>
		public SessionToken CreateSessionToken (PackageId packageId, string signerId)
		{
			string path = template.UrlFor (UrlTemplate.SESSION_PATH)
                .Replace ("{packageId}", packageId.Id)
                .Replace ("{signerId}", signerId)
                .Build ();

			try {
				string response = Converter.ToString (HttpMethods.PostHttp (apiToken, path, new byte[0]));
				return JsonConvert.DeserializeObject<SessionToken> (response);
			} catch (Exception e) {
				throw new EslException ("Could not create a session token for signer." + " Exception: " + e.Message);
			}
		}
	}
}

