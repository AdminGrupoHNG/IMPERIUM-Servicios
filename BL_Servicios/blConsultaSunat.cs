using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using BL_Servicios;
using System.Configuration;

namespace BL_Servicios
{
    public class blConsultaSunat
    {
        public static string PathChromeDriver = "";

        public class TransactionEN
        {
            public string Estado { get; set; }
            public string Mensaje { get; set; }
            public object Data { get; set; }
            public string FileName { get; set; }
        }

        public static TransactionEN createEntity
        {
            get
            {
                TransactionEN objE = new TransactionEN();
                objE.Estado = "NO";
                objE.Mensaje = "NO";
                objE.Data = null;
                objE.FileName = null;
                return objE;
            }
        }
        public async Task<TransactionEN> DownloadUrlAsync(string url)
        {
            TransactionEN objE = createEntity;
            try
            {
                using (var wc = new WebClient())
                {
                    var s = await wc.DownloadDataTaskAsync(url);
                    objE.Data = s;
                    objE.Estado = "SI";
                    objE.FileName = GetFileName(url);
                }
            }
            catch (Exception ex)
            {
                SetExcepcion(ex, objE);
            }
            return objE;
        }

        private static string GetFileName(string LocalPath)
        {
            return System.IO.Path.GetFileName(LocalPath);
        }

        private static void SetExcepcion(Exception ex, TransactionEN objE)
        {
            objE.Estado = "NO";
            objE.Mensaje = ex.Message;
        }

        public static ChromeDriverService CreateChromeDriverService()
        {
            blEncrypta blEncryp = new blEncrypta();
            PathChromeDriver = blEncryp.Desencrypta(ConfigurationManager.AppSettings[blEncryp.Encrypta("RutaChromeDriver")].ToString());

            var chromeDriverService = OpenQA.Selenium.Chrome.ChromeDriverService.CreateDefaultService(PathChromeDriver);
            chromeDriverService.HideCommandPromptWindow = true;
            chromeDriverService.SuppressInitialDiagnosticInformation = true;
            return chromeDriverService;
        }

        private static ChromeOptions CreateChromeOptions(bool HideChrome)
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--start-maximized");
            if (HideChrome)
            {
                chromeOptions.AddArgument("--window-position=-32000,-32000");
                chromeOptions.AddArguments(new List<string>()
        {
            "--silent-launch",
            "--no-startup-window",
            "no-sandbox",
            "headless"
        });
                chromeOptions.AddArguments(new List<string>()
        {
            "headless"
        });
            }
            return chromeOptions;
        }

        public ChromeDriver CreateChromeDriver(bool HideChrome)
        {
            var chromeDriverService = CreateChromeDriverService();
            ChromeOptions chromeOptions = CreateChromeOptions(HideChrome);
            ChromeDriver driver = new ChromeDriver(chromeDriverService, chromeOptions);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            return driver;
        }

        public byte[] getScreenshot(IWebDriver driver)
        {
            ITakesScreenshot screenshotDriver = driver as ITakesScreenshot;
            Screenshot screenshot = screenshotDriver.GetScreenshot();
            return screenshot.AsByteArray;
        }

        public void ValidatePath(string FilePath)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(FilePath);
                if (fileInfo.Exists)
                    throw new Exception($"Ya existe el archivo: {fileInfo}");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
