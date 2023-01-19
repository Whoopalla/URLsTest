namespace URLsTest.Services {
    public class URLFromTextFileRetriever : IURLRetriever {
        private string _filePath;

        public URLFromTextFileRetriever(string filePath) {
            _filePath = filePath;
        }

        // Url are expected to be valid and separated with colon
        public async Task<List<string>> GetURLsAsync() {
            var text = await File.ReadAllTextAsync(_filePath);
            return text.Split(',').Where(url => !String.IsNullOrEmpty(url)).ToList();
        }
    }
}
