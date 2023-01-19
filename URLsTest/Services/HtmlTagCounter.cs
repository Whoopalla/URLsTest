using HtmlAgilityPack;

namespace URLsTest.Services {
    public class HtmlTagCounter : IHtmlTagCounter {
        public async Task<int> CountAsync(string tag, string url) {
            var web = new HtmlWeb();
            var doc = await web.LoadFromWebAsync(url);
            return doc.DocumentNode.SelectNodes($"//{tag}").Count();
        }
    }
}
