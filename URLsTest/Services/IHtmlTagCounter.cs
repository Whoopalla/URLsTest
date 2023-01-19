namespace URLsTest.Services {
    public interface IHtmlTagCounter {
        Task<int> CountAsync(string tag, string url);
    }
}
