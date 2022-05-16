using OpenQA.Selenium;
using OpenQA.Selenium.Opera;

const string path = @"D:\log.txt";
//paste link to add bot youtube channel
Console.WriteLine("Enter the Youtube channel link");
var url = Convert.ToString(Console.ReadLine());
url = url + "/videos";


IWebDriver driver = new OperaDriver(@"C:\Users\Emrullah\AppData\Local\Programs\Opera");

driver.Navigate().GoToUrl(url);

//IReadOnlyCollection<IWebElement> follwers = driver.FindElements(By.CssSelector("yt-simple-endpoint.style-scope.ytd-grid-video-renderer"));
Thread.Sleep(2000);
IReadOnlyCollection<IWebElement> follwers = driver.FindElements(By.CssSelector("a[id='video-title']"));

//IWebElement s = driver.FindElement(By.TagName("body"));
//((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight - 150)");
//s.SendKeys(Keys.End);

Thread.Sleep(500);


var scrollheight = "" +             
               "var endpage = window.scrollHeight;" +
               "return endpage;";
//string script = "" +
//    "window.scrollto(0,document.body.scrollheight);" +
//    "return endpage;";
IJavaScriptExecutor jsa = driver as IJavaScriptExecutor;
var endpage = Convert.ToInt32(jsa.ExecuteScript(scrollheight));

while (true)
{
    IJavaScriptExecutor js = driver as IJavaScriptExecutor;
    js.ExecuteScript("window.scrollBy(0,1000)");
    var end = endpage;
    Thread.Sleep(750);
    endpage = Convert.ToInt32(jsa.ExecuteScript(scrollheight));
    if (end == endpage)
        break;
}
var sbHeight = "window.innerHeight * (window.innerHeight / document.body.offsetHeight)";
IJavaScriptExecutor jsaa = driver as IJavaScriptExecutor;
var sayfa = Convert.ToInt32(jsaa.ExecuteScript(sbHeight));



List<string> linkler = new List<string>();
foreach (IWebElement follower in follwers)
{
    Console.WriteLine(follower.Text);
    //Console.WriteLine(follower.GetAttribute("href"));
    linkler.Add(follower.GetAttribute("href"));
}
if(!File.Exists(path))
{
    var myFile = File.Create(path);
    myFile.Close();
}

for(int i =0; i<linkler.Count; i++)
{
   
    driver.Navigate().GoToUrl(linkler[i]);
 
}
File.WriteAllLines(path, linkler);


driver.Close();
driver.Quit();
