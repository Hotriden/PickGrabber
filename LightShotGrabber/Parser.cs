using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using AngleSharp.Html.Parser;


namespace LightShotGrabber
{
    public class Parser
    {
        public char[] Alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

        public async void ParseFromUrl()
        {
            WebClient wc = new WebClient();
            string firstSymb = "https://prnt.sc/";
            Console.WriteLine("Введите 6 значное значение");
            string enterStr = Console.ReadLine();
            char[] charStr = enterStr.ToUpper().ToArray();
            List<int> numb = new List<int>();
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < Alpha.Length; j++)
                {
                    if (charStr[i] == Alpha[j])
                    {
                        numb.Add(j);
                    }
                }
            }

            for (int a = numb[0]; a < Alpha.Length; a++)
            {
                for (int b = numb[1]; b < Alpha.Length; b++)
                {
                    for (int c = numb[2]; c < Alpha.Length; c++)
                    {
                        for (int d = numb[3]; d < Alpha.Length; d++)
                        {
                            for (int e = numb[4]; e < Alpha.Length; e++)
                            {
                                for (int f = numb[5]; f < Alpha.Length; f++)
                                {
                                    try
                                    {
                                        string NewString = "" + Alpha[a] + Alpha[b] + Alpha[c] + Alpha[d] + Alpha[e] + Alpha[f];
                                        Uri myUri = new Uri(firstSymb + NewString.ToLower());
                                        string path = ParseByAngle(myUri);
                                        wc.Headers.Add("User-Agent: Other");
                                        string fileName = NewString + ".jpg";
                                        await wc.DownloadFileTaskAsync(path, fileName);
                                        Console.WriteLine("Файл {0} загружен", fileName);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public string ParseByAngle(Uri url)
            {
                const string quote = "\"";
                string finalstring = "";
                WebClient wc = new WebClient();
                wc.Headers.Add("User-Agent: Other");
                wc.Encoding = Encoding.UTF8;
                string Response = wc.DownloadString(url);
                var parser = new HtmlParser();
                var search = parser.ParseDocument(Response);
                var result = search.GetElementsByClassName("image-container image__pic js-image-pic").ToArray();

                foreach (var b in result)
                {
                    string patternUrl = "src='".Replace("'", "\"") + "https://image";
                    string[] splitted = b.InnerHtml.Split();
                    string tempUrl = "";
                    for (int i = 0; i < splitted.Length; i++)
                    {
                        if (splitted[i].StartsWith(patternUrl))
                        {
                            tempUrl += splitted[i];
                            break;
                        }
                    }
                string subStringUrl = tempUrl.Remove(0, 5);
                finalstring = subStringUrl.Remove(subStringUrl.IndexOf(quote), 1);
                }
            return finalstring;
        }
    }
}
