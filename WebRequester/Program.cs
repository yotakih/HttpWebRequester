using System;

namespace WebRequester
{
    class Program
    {
        static void Main(string[] args)
        {
            var req = new HttpWebRequester();
            var cnt = req.Get(@"http://localhost:5000/WeatherForecast").Result;
            Console.WriteLine(cnt);
        }
    }
}
