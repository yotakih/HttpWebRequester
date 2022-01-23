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
		// [TestMethod]
		// public void SetProxyTest()
		// {
		// 	var r = new HttpWebRequester();
		// 	var p = "http://localhost:8080/";
		// 	var u = "https://www.google.com/?hl=ja";
		// 	r.SetProxy(p);
		// 	Assert.AreEqual(p, r.Proxy.GetProxy(new Uri(u)).ToString());
		// }
		// [TestMethod]
		// public void SetProxyTest2()
		// {
		// 	var r = new HttpWebRequester();
		// 	var p = "http://localhost:8080/";
		// 	var u = new Uri("https://www.google.com/?hl=ja");
		// 	r.SetProxy(p, "usr", "pass");
		// 	var c = r.Proxy.Credentials.GetCredential(u, "Basic");
		// 	Assert.IsTrue("usr" == c.UserName && "pass" == c.Password);
		// }

		[TestMethod]
		public void GetTest()
		{
			var r = new HttpWebRequester();
			var u = "https://www.google.com/?hl=ja";
			var c = r.Get(u).Result;
			Assert.IsTrue(c.Length > 0);
		}
		[TestMethod]
		public void GetBadUrlTest()
		{
			var r = new HttpWebRequester();
			var u = "http://192.168.1.21:5555";
			Assert.ThrowsException<Exception>(() => {
				var c = r.Get(u).Result;
			});
		}
	}
}
