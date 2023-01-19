using System.Threading.Tasks;

namespace URLsTest.Client.ViewModels.Base;

public interface IViewModelBase {

    public bool IsBusy { get; }

    public bool IsInitialized { get; set; }

    Task InitializeAsync();
}