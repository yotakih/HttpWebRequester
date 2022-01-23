using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CommonMDLProj;

namespace WebRequester
{
	public class HttpWebRequester
	{
		#region "static variant"
		private static HttpClient _client = null;
		#endregion
		// #region "Setting Proxy"
		// /// <summary>
		// /// 
		// /// </summary>
		// public IWebProxy Proxy = null;
		// /// <summary>
		// /// 
		// /// </summary>
		// /// <param name="proxyurl"></param>
		// public void SetProxy(string proxyurl)
		// {
		// 	this.Proxy = new WebProxy(proxyurl);
		// }
		// /// <summary>
		// /// 
		// /// </summary>
		// /// <param name="proxyurl"></param>
		// /// <param name="authuser"></param>
		// /// <param name="authpass"></param>
		// public void SetProxy(string proxyurl, string authuser, string authpass)
		// {
		// 	this.SetProxy(proxyurl);
		// 	var credential = new NetworkCredential(authuser, authpass);
		// 	this.Proxy.Credentials = credential;
		// }
		// #endregion
		#region "Public Interface"
		/// <summary>
		/// 
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public async Task<string> Get(string url, string encstr = "utf-8", int timeout = 3000)
		{
			var client = this.BuildHttpClient();
			try
			{
				var msg = new HttpRequestMessage
				{
					Method = HttpMethod.Get,
					RequestUri = new Uri(url),
				};
				var res = await client.SendAsync(msg);
				var ret = "";
				using (var st = new StreamReader(res.Content.ReadAsStream(), CommonMdl.GetEncoding(encstr)))
				{
					ret = st.ReadToEnd();
				}
				return ret;
			}
			catch (Exception e)
			{
				Console.WriteLine($"ErrorMessage: {e.Message}");
				Console.WriteLine($"StackTrace: {e.StackTrace}");
				throw;
			}
			finally
			{
				// HttpClientはDisposeせずアプリケーション内で使いまわす仕様
				// client.Dispose();
			}
		}
		#endregion
		#region "Private"
		private HttpClient BuildHttpClient()
		{
			if (_client is null)
			{
				var proxy = WebRequest.GetSystemWebProxy();
				HttpClientHandler handler = new HttpClientHandler()
				{
					Proxy = proxy,
				};
				Console.WriteLine($"Proxy: {handler.Proxy}");
				_client = new HttpClient(handler);
				_client.Timeout = TimeSpan.FromMilliseconds(3000);
			}
			return _client;
		}
		#endregion
	}
}
