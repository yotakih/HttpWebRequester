using System;
using System.Net;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebRequester;

namespace WebRequesterTests
{
	[TestClass]
	public class WebRequesterTests
	{
		[TestMethod]
		public void GetTest()
		{
			var r = new HttpWebRequester();
			var u = "http://localhost:3000";
			var c = r.Get(u).Result;
			Assert.AreEqual(c.StatusCode, HttpStatusCode.OK);
		}
		[TestMethod]
		public void PostTest()
		{
			var url = "http://localhost:3000";
			var data = HttpWebRequester.ConvPostJsonData(@"{ ""hoge"": ""ほげ"" }");
			Console.WriteLine(data);
			var req = new HttpWebRequester();
			var res = req.Post(url, data).Result;
			Console.WriteLine(HttpWebRequester.ContentFromRes(res));
			Assert.AreEqual(res.StatusCode, HttpStatusCode.OK);
		}
		[TestMethod]
		public void ContentFromResTest()
		{
			var req = new HttpWebRequester();
			var url = "http://www.google.com/?hl=ja";
			var res = req.Get(url).Result;
			var s = HttpWebRequester.ContentFromRes(res, "utf-8");
			Assert.IsTrue(s.Length > 0);
		}
		[TestMethod]
		public void GetBadUrlTest()
		{
			var r = new HttpWebRequester();
			var u = "http://192.168.1.21:5555";
			Assert.ThrowsException<Exception>(() =>
			{
				var c = r.Get(u).Result;
			});
		}
	}
}
