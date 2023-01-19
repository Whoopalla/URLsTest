namespace URLsTest.Client.Models {
    internal class HtmlTags : ObservableCollectionEx<string> {
        public HtmlTags() {
            Add("a");
            Add("div");
            Add("h1");
            Add("p");
        }
    }
}
