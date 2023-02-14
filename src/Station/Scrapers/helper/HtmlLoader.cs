
namespace gasStation {

    public class HtmlLoader {
        static public string loadHtmlString(string linkString, string fileName) {

            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();

            var myReq = client.GetAsync(linkString);
            
            myReq.Wait();

            var htmlData = myReq.Result;

            var content = htmlData.Content;

            var html = content.ReadAsStringAsync().Result;
            File.WriteAllText(fileName, html);

            return html;
            
        }

    }

}