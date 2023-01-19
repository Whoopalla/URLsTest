namespace URLsTest.Services {
    public interface IURLRetriever {
        Task<List<string>> GetURLsAsync();
    }
}
