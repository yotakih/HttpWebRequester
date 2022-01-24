using System;
using System.Collections.Generic;
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
		#region "Public Interface"
		/// <summary>
		/// 
		/// </summary>
		/// <param name="url"></param>
		/// <param name="header"></param>
		/// <returns></returns>
		public async Task<HttpResponseMessage> Get(string url, Dictionary<string, string> header = null)
		{
			var client = this.BuildHttpClient();
			try
			{
				var msg = new HttpRequestMessage
				{
					Method = HttpMethod.Get,
					RequestUri = new Uri(url),
				};
				if (header is not null)
					foreach (var k in header.Keys)
						msg.Headers.Add(k, header[k]);
				return await client.SendAsync(msg);
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
		public async Task<HttpResponseMessage> Post(string url, HttpContent postData, Dictionary<string, string> header = null)
		{
			var client = this.BuildHttpClient();
			try
			{
				var msg = new HttpRequestMessage
				{
					Method = HttpMethod.Post,
					RequestUri = new Uri(url),
				};
				if (header is not null)
					foreach (var k in header.Keys)
						msg.Headers.Add(k, header[k]);
				msg.Content = postData;
				return await client.SendAsync(msg);
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
		/// <summary>
		/// 
		/// </summary>
		/// <param name="res"></param>
		/// <param name="encstr"></param>
		/// <returns></returns>
		public static string ContentFromRes(HttpResponseMessage res, string encstr = "utf-8")
		{
			try
			{
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
			}
		}
		public static StringContent ConvPostJsonData(string json, string enc = "utf-8")
		{
			return new StringContent(json, CommonMdl.GetEncoding(enc), @"application/json");
		}
		#endregion
		#region "Private"
		/// <summary>
		/// HttpClientを生成
		/// </summary>
		/// <returns></returns>
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
