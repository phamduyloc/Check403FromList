using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Check403FromList
{
    class Program
    {
        static void Main(string[] args)
        {
            string strArt =
                @"                                           $$\    " + "\n" +
                @"$$$$$$$\   $$$$$$\   $$$$$$\   $$$$$$\   $$$$$$\  " + "\n" +
                @"$$  ____| $$$ __$$\ $$$ __$$\ $$$ __$$\ $$  __$$\ " + "\n" +
                @"$$ |      $$$$\ $$ |$$$$\ $$ |$$$$\ $$ |$$ /  \__|" + "\n" +
                @"$$$$$$$\  $$\$$\$$ |$$\$$\$$ |$$\$$\$$ |\$$$$$$\  " + "\n" +
                @"\_____$$\ $$ \$$$$ |$$ \$$$$ |$$ \$$$$ | \___ $$\ " + "\n" +
                @"$$\   $$ |$$ |\$$$ |$$ |\$$$ |$$ |\$$$ |$$\  \$$ |" + "\n" +
                @"\$$$$$$  |\$$$$$$  /\$$$$$$  /\$$$$$$  /\$$$$$$  |" + "\n" +
                @" \______/  \______/  \______/  \______/  \_$$  _/ " + "\n" +
                @"                                           \ _/   " + "\n";

            if (args.Length > 0)
            {
                Console.Write("\n\n" + strArt);
                string file = args[0];
                int numPageOpen = int.Parse(args[1]);
                int countLine = 0;
                int count = 0;
                string line;
                if (File.Exists(file))
                {
                    StreamReader reader = new StreamReader(file);
                    while ((line = reader.ReadLine()) != null)
                    {
                        countLine++;
                        if (Page_403("http://" + line))      //http --> Ok
                        {
                            count++;
                            //Console.WriteLine(line + " --- " + countLine +" --- True --- " + count);
                            Process.Start("chrome.exe", line);
                            if (count >= numPageOpen)
                            {
                                count = 0;
                                Console.WriteLine("\n\nNum line readed: " + countLine);
                                Console.WriteLine("\n\nPress <Enter> to continue...");
                                Console.ReadLine();
                                Console.Clear();
                                Console.Write("\n\n" + strArt);
                            }
                        }
                        else
                        {
                            Console.WriteLine(line + " --- " + countLine + " --- False");
                        }
                        //else
                        //{
                        //    if (Page_403("https://" + line))     //https --> Ok
                        //    {
                        //        count++;
                        //        //Console.WriteLine(line + " --- " + countLine +" --- True --- " + count);
                        //        Process.Start("chrome.exe", line);
                        //        if (count >= numPageOpen)
                        //        {
                        //            count = 0;
                        //            Console.WriteLine("\n\nNum line readed: " + countLine);
                        //            Console.WriteLine("\n\nPress <Enter> to continue...");
                        //            Console.ReadLine();
                        //            Console.Clear();
                        //            Console.Write("\n\n" + strArt);
                        //        }
                        //    }
                        //    else
                        //    {
                        //        Console.WriteLine(line + " --- " + countLine + " --- False");
                        //    }
                        //}
                    }
                    reader.Close();
                }

                Console.WriteLine("Done");
                Console.ReadLine();
            }
        }


        public static bool Page_403(String url)
        {
            bool blnResult = false;
            try
            {
                // Creates an HttpWebRequest for the specified URL.
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                // Sends the HttpWebRequest and waits for a response.
                myHttpWebRequest.Timeout = 5000;
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();

                if (myHttpWebResponse.StatusCode == HttpStatusCode.Forbidden)
                {
                    blnResult = true;
                }
                myHttpWebResponse.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(url + "---" + ex.Message);
            }
            return blnResult;
        }
    }
}
