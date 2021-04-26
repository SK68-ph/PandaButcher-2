using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoUpdaterDotNET;


namespace PandaButcher_2
{
    class Program
    {

        // Instantiate random number generator.  
        static private readonly Random _random = new Random();

        // Generates a random number within a range.      
        static private int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }
        class Account
        {
            public void rand()
            {
                string[] fnames = { "Angel", "John", "Paul", "Angelica", "Christian", "Nicole", "Justine", "Angela", "John", "Mark", "Mary", "Joy", "John", "Lloyd", "Mariel", "Jerome", "Jasmine", "Adrian", "Mary", "Grace", "John", "Michael", "Kimberly", "Angelo", "Stephanie", "Justin", "Christine", "John", "Carlo", "Michelle", "James", "Jessa", "Mae", "Mark", "Jenny", "Kenneth", "Angeline", "Jayson", "Erica", "Mark", "Anthony", "Marvin", "Martin", "Bea", "Daniel", "Janelle", "John", "Rey", "Kyla", "Ryan" };
                string[] lnames = { "Valencia", "Reyes", "Cruz", "Bautista", "Del Rosario", "Gonzales", "Ramos", "Aquino", "García", "Dela Cruz", "Soledad", "Perez", "Calague", "Mendoza", "Fernandez" };
                fname = fnames[RandomNumber(0, fnames.Length - 1)];
                lname = lnames[RandomNumber(0, lnames.Length - 1)];
                pass = RandomNumber(233764, 203982320).ToString();
            }


            public string[] genEmailNew(string baseEmail)
            {
                List<string> email = new List<string>();
                for (int i = 0; i < 10000; i++)
                {
                    email.Add(baseEmail + "+" + i + "@gmail.com");
                }
                string[] MyRandomArray = email.OrderBy(x => _random.Next()).ToArray();
                return MyRandomArray;
            }

            public string[] genEmailNew(string baseEmail,int start, int end)
            {
                List<string> email = new List<string>();
                for (int i = start; i < end; i++)
                {
                    email.Add(baseEmail + "+" + i + "@gmail.com");
                }
                string[] MyRandomArray = email.OrderBy(x => _random.Next()).ToArray();
                return MyRandomArray;
            }

            private string _email;
            private string _fname;
            private string _lname;
            private string _pass;
            public string email { get { return _email; } set { _email = value; } }
            public string fname { get { return _fname; } set { _fname = value; } }
            public string lname { get { return _lname; } set { _lname = value; } }
            public string pass { get { return _pass; } set { _pass = value; } }

            public string Export()
            {
                return "Email: " + email + " Password: " + pass;
            }

            public void SplitRaw(string data)
            {
                if (data.Contains('-'))
                {
                    string[] temp = data.Split('-');
                    email = temp[0];
                    pass = temp[1];
                }
                else if (data.Contains('-'))
                {
                    string[] temp = data.Split(' ');
                    email = temp[0];
                    pass = temp[1];
                }
                    
            }

            public override string ToString()
            {
                return email + "-" + pass;
            }
        }

        class Data
        {
            private string[] useragent = { "Mozilla / 5.0(Windows NT 10.0; Win64; x64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 90.0.4430.85 Safari / 537.36", "Mozilla / 5.0(Windows NT 10.0; WOW64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 90.0.4430.85 Safari / 537.36", "Mozilla / 5.0(Windows NT 10.0) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 90.0.4430.85 Safari / 537.36", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4363.0 Safari/537.36", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36 FS", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.66 Safari/537.36", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36" };
            public  string GetUserAgent()
            {
                return useragent[RandomNumber(0,useragent.Length - 1)];
            }

            public string curGenPath = "";
            public string curGenName = "";
            public string GetDataFile()
            {
                    Thread t = new Thread((ThreadStart)(() => {
                    OpenFileDialog openFile = new OpenFileDialog();
                    openFile.FileName = "Emails.txt";
                    openFile.Filter = "Text|*.txt|All|*.*";
                    if (openFile.ShowDialog() == DialogResult.OK)
                    {
                        curGenName = openFile.FileName;
                        curGenPath = Directory.GetParent(curGenName).FullName;

                    }
                    else
                    {
                        Console.WriteLine("Invalid file");
                        Environment.Exit(-1);
                    }
                }));
                t.SetApartmentState(ApartmentState.STA);
                t.Start();
                t.Join();
                return curGenName;
            }

            private  string exportPath = "";
            private  string exportName = "";
            private  void GetExportPath()
            {
                Thread t = new Thread(() =>
                {
                    using (var sfd = new SaveFileDialog())
                    {
                        sfd.FileName = DateTime.Now.ToString("MMMM-d-") + "Butcher";
                        sfd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                        sfd.FilterIndex = 1;
                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            exportPath = sfd.FileName;
                            exportName = Path.GetFileName(sfd.FileName);
                        }
                        else
                        {
                            Console.WriteLine("Invalid file");
                            Environment.Exit(-1);
                        }
                    }
                });
                t.SetApartmentState(ApartmentState.STA);
                t.Start();
                t.Join();
            }

            public  void ExportData(string data)
            {
                if (string.IsNullOrEmpty(exportPath) || string.IsNullOrEmpty(exportName))
                {
                    GetExportPath();
                }
                StreamWriter file = new StreamWriter(exportPath, true);
                file.WriteLine(data);
                file.Close();
            }
        }


        static string TimeStamp;
        static void Main(string[] args)
        {
            Updater();
            while (true)
            {
                int count = 0;
                int err_count = 0;
                int warn_count = 0;
                Account account = new Account();
                Stopwatch stopWatch = new Stopwatch();
                Data data1 = new Data();

                // Selection 
                Console.Clear();
                Console.WriteLine("Modes");
                Console.WriteLine("1 = Register Account");
                Console.WriteLine("1a = New method(+)");
                Console.WriteLine("2 = Check Butcher");
                Console.WriteLine("3 = Export Butcher");
                Console.Write("Enter Mode :");
                string selection = Console.ReadLine();
                TimeStamp = DateTime.Now.ToString("MMMM-d-hh-mm-ss-tt");

                string[] lines = { };
                if (selection.Equals("1a"))
                {
                    Console.Write("Enter email:");
                    var baseEmail = Console.ReadLine();
                    Console.Write("Enter Start Count:");
                    var numStart = int.Parse( Console.ReadLine());
                    Console.Write("Enter End Count:");
                    var numEnd = int.Parse(Console.ReadLine());
                    lines = account.genEmailNew(baseEmail,numStart,numEnd);
                }
                else if(selection.Equals("1") || selection.Equals("2") || selection.Equals("3"))
                {
                    lines = File.ReadAllLines(data1.GetDataFile());
                }

                for (var i = 0; i < lines.Length; i++)
                {
                    string data = lines[i].Trim(' ');
                    if (string.IsNullOrEmpty(data))
                    {
                        continue;
                    }

                    if (data.Contains('#') || selection.Equals("3"))
                    {
                        if (data.Contains("OK") && selection.Equals("3"))
                        {
                            account.SplitRaw(lines[i + 1]);
                            data1.ExportData(account.Export());
                            count++;
                            i++;
                        }
                        continue;
                    }

                    if (selection.Equals("1") || selection.Equals("1a"))
                    {
                        account.email = data;
                    }
                    if (selection.Equals("2"))
                    {
                        account.SplitRaw(data);
                    }
                    ChromeOptions options = new ChromeOptions();
                    options.AddUserProfilePreference("profile.default_content_setting_values.images", 2);
                    string curUserAgent = data1.GetUserAgent();
                    options.AddArguments("--guest", "--headless", "--user-agent=" + curUserAgent, "--disable-blink-features=AutomationControlled", "--blink-settings=imagesEnabled=false", "--disable-gpu", "--disable-software-rasterizer", "--disable-extensions", "--log-level=3");

                    using (IWebDriver driver = new ChromeDriver(options))
                    {
                        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                        stopWatch.Start();
                        driver.Navigate().GoToUrl("https://www.foodpanda.ph");
                        try
                        {
                            wait.Until(webDriver => driver.FindElement(By.CssSelector("h1")).Displayed);
                            driver.FindElement(By.XPath("/html/body/div[1]/header[1]/div[3]/div[1]/a[1]")).Click();
                            Thread.Sleep(100);
                            wait.Until(webDriver => driver.FindElement(By.CssSelector("h2")).Displayed); ;
                            driver.FindElement(By.XPath("/html/body/div[1]/div[1]/main[1]/div[1]/div[1]/div[2]/button[1]")).Click();
                            Thread.Sleep(100);
                            wait.Until(webDriver => driver.FindElement(By.CssSelector("input")).Displayed);
                            Thread.Sleep(100);
                            _ = driver.Manage().Timeouts().ImplicitWait;
                            driver.FindElement(By.Name("email")).SendKeys(account.email + OpenQA.Selenium.Keys.Enter);
                            Thread.Sleep(1000);
                            _ = driver.Manage().Timeouts().ImplicitWait;
                            if (selection.Equals("1") || selection.Equals("1a"))
                            {
                                account.rand();
                                Console.WriteLine("Creating account - {0} with UAgent - {1}", account.email, curUserAgent);
                                driver.FindElement(By.Name("first_name")).SendKeys(account.fname);
                                Thread.Sleep(100);
                                _ = driver.Manage().Timeouts().ImplicitWait;
                                driver.FindElement(By.Name("last_name")).SendKeys(account.lname);
                                Thread.Sleep(100);
                                _ = driver.Manage().Timeouts().ImplicitWait;
                                driver.FindElement(By.Name("password")).SendKeys(account.pass);
                                Thread.Sleep(100);
                                _ = driver.Manage().Timeouts().ImplicitWait;
                                driver.FindElement(By.Name("password")).SendKeys(OpenQA.Selenium.Keys.Enter);
                                Thread.Sleep(2000);
                                _ = driver.Manage().Timeouts().ImplicitWait;
                                if (!driver.Url.Equals("https://www.foodpanda.ph/"))
                                {
                                    throw new NoSuchElementException();
                                }
                                StreamWriter file = new StreamWriter(Path.Combine(data1.curGenPath, TimeStamp + "-GenAcc.txt"), append: true);
                                count++;
                                file.WriteLine(account.ToString());
                                file.Close();
                                Console.WriteLine("Done");
                            }
                            else if (selection.Equals("2"))
                            {
                                Console.WriteLine("Checking account {0} with UAgent - {1}", account.email , curUserAgent);
                                driver.FindElement(By.Name("_password")).SendKeys(account.pass);
                                Thread.Sleep(100);
                                _ = driver.Manage().Timeouts().ImplicitWait;
                                driver.FindElement(By.Name("_password")).SendKeys(OpenQA.Selenium.Keys.Enter);
                                Thread.Sleep(2000);
                                _ = driver.Manage().Timeouts().ImplicitWait;
                                if (!driver.Url.Equals("https://www.foodpanda.ph/"))
                                {
                                    throw new NoSuchElementException();
                                }
                                driver.Navigate().GoToUrl("https://www.foodpanda.ph/vouchers");
                                Thread.Sleep(1000);
                                if (!driver.Url.Equals("https://www.foodpanda.ph/vouchers"))
                                {
                                    throw new NoSuchElementException();
                                }
                                _ = driver.Manage().Timeouts().ImplicitWait;
                                IList<IWebElement> elements = driver.FindElements(By.XPath("/html/body/div[13]/div[1]/main/section/div/div/div[1]/ul/li"));
                                StreamWriter file = new StreamWriter(Path.Combine(data1.curGenPath, TimeStamp + "-CheckedAcc.txt") , append: true);
                                if (elements.Count >= 2)
                                {
                                    count++;
                                    file.WriteLine("#Status - OK, Butcher Count = " + elements.Count);
                                    file.WriteLine(account.ToString());
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("Done - Status = " + " OK");
                                    Console.ResetColor();
                                }
                                if (elements.Count == 1)
                                {
                                    count++;
                                    file.WriteLine("#Status - OK");
                                    file.WriteLine(account.ToString());
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("Done - Status = " + " OK");
                                    Console.ResetColor();
                                }
                                else if (elements.Count == 0)
                                {

                                    count++;
                                    file.WriteLine("#Status - NULL");
                                    file.WriteLine(account.ToString());
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Done - Status = " + "NULL");
                                    Console.ResetColor();
                                }
                                else
                                {
                                    warn_count++;
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Warning Account login failed At line - {0}, Time - {1}", i, TimeStamp);
                                    Console.ResetColor();
                                }
                                file.Close();
                            }

                        }
                        catch (WebDriverTimeoutException wdte)
                        {
                            err_count++;
                            Console.WriteLine("ERROR {0} website response timed out, Time - {1} At line - {2}", wdte.Message, TimeStamp, i);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Website not responding.");
                            Console.ResetColor();
                        }
                        catch (NoSuchElementException nse)
                        {
                            warn_count++;
                            Console.WriteLine("Warning {0} At line failed to login- {1}, Time - {2}", nse.Message, i, TimeStamp);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Account register/login failed");
                            Console.ResetColor();

                        }
                        catch (StaleElementReferenceException serf) 
                        {
                            err_count++;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("FATAL ERROR {0} , Time - {1} At line - {2}", serf.Message, TimeStamp, i);
                            Console.ResetColor();
                        }
                        catch (WebDriverException wde)
                        {
                            err_count++;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("ERROR {0} Website did not respond, Time - {1} At line - {2}", wde.Message, TimeStamp, i);
                            Console.ResetColor();
                        }
                        catch (Exception exp)
                        {
                            err_count++;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Unknown ERROR {0} Execution Result = {1}, Time - {2} At line - {3}", exp.Message, exp.HResult, TimeStamp, i);
                            Console.ResetColor();
                        }
                        finally
                        {
                            stopWatch.Stop();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Elapsed Time {0} \n", stopWatch.ElapsedMilliseconds);
                            stopWatch.Reset();
                            Console.ResetColor();
                            driver.Close();
                        }
                    }
                }
                Console.WriteLine("Done. Total process = {0}, Total warning = {1}, Total Errors = {2}", count, warn_count, err_count);
                Console.ReadKey();
            }
            
            
        }


        public static void Updater()
        {
            Thread apt = new Thread((ThreadStart)(() => {
                AutoUpdater.Start("https://updater-panda.s3.ap-east-1.amazonaws.com/updater.xml");
                AutoUpdater.RunUpdateAsAdmin = true;
                AutoUpdater.Synchronous = true;
                AutoUpdater.DownloadPath = Environment.CurrentDirectory;
                var currentDirectory = new DirectoryInfo(Environment.CurrentDirectory);
                AutoUpdater.InstallationPath = currentDirectory.FullName;
            }));
            apt.SetApartmentState(ApartmentState.STA);
            apt.Start();
            apt.Join();
        }
    }
}
