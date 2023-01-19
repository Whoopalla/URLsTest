using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using URLsTest.Client.Models;
using URLsTest.Client.ViewModels.Base;
using URLsTest.Services;

namespace URLsTest.Client.ViewModels {
    public class URLAnchorsViewModel : ViewModelBase {
        private CancellationTokenSource _cancellationTokenSource;
        private IURLRetriever _urlRetriever;
        private IHtmlTagCounter _tagCounter;

        private string _urlFile = String.Empty;
        private string _message = String.Empty;
        private bool _showDialog;
        private string _tagToSearch;

        public ObservableCollectionEx<TagItem> TagItems { get; private set; }
        public ObservableCollectionEx<string> HtmlTags { get; private set; }

        public ICommand GetURLsFromFileCommand { get; }

        public ICommand CountTagsCommand { get; }

        public ICommand CancelCountingCommand { get; }

        public string SelectedFile {
            get => _urlFile;
            set => SetProperty(ref _urlFile, value);
        }

        public string Message {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public bool ShowDialog {
            get => _showDialog;
            set => SetProperty(ref _showDialog, value);
        }

        public string TagToSearch {
            get => _tagToSearch;
            set => SetProperty(ref _tagToSearch, value);
        }

        public URLAnchorsViewModel(IHtmlTagCounter tagCounter) {
            TagItems = new ObservableCollectionEx<TagItem>();
            HtmlTags = new HtmlTags();

            GetURLsFromFileCommand = new AsyncRelayCommand(GetURLsFromFile);
            CountTagsCommand = new RelayCommand(CountTags);
            CancelCountingCommand = new RelayCommand(CancelCounting);

            _tagCounter = tagCounter;
            _cancellationTokenSource = new CancellationTokenSource();

            TagToSearch = "a";
        }

        private async Task GetURLsFromFile() {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Document";
            dialog.DefaultExt = ".txt";
            dialog.Filter = "Text documents (.txt)|*.txt";

            bool? result = dialog.ShowDialog();

            if (result == true) {
                SelectedFile = dialog.FileName;
                TagItems.Clear();
                _urlRetriever = new URLFromTextFileRetriever(SelectedFile);
                await GetUrls();
            }
        }

        private async Task GetUrls() {
            if (_urlRetriever is null) {
                return;
            }
            List<string> urls = new List<string>();
            urls = await _urlRetriever.GetURLsAsync();
            foreach (var url in urls) {
                TagItems.Add(new TagItem() { Url = url, Count = 0 });
            }
        }

        private async void CountTags() {
            if (TagItems.Count == 0 || IsBusy) {
                return;
            }

            foreach (var tagItem in TagItems) {
                tagItem.ErrorOcurred = false;
            }

            _cancellationTokenSource = new CancellationTokenSource();
            var ct = _cancellationTokenSource.Token;
            try {
                await IsBusyFor(async () =>
                await Parallel.ForEachAsync(TagItems, cancellationToken: ct,
                async (anchor, ct) => {
                    try {
                        anchor.IsCalculating = true;
                        anchor.Count = await _tagCounter.CountAsync(TagToSearch, anchor.Url);
                    }
                    catch (Exception e) {
                        anchor.ErrorOcurred = true;
                    }
                    finally {
                        anchor.IsCalculating = false;
                    }
                }));
            }
            catch (TaskCanceledException e) {

            }
            finally {
                _cancellationTokenSource.Dispose();
                ShowDialog = true;
                if (_cancellationTokenSource.IsCancellationRequested) {
                    Message = "Counting was canceled";
                }
                else {
                    Message = "All tags were counted";
                }
            }

        }

        private void CancelCounting() {
            bool alreadyCancelled = _cancellationTokenSource.IsCancellationRequested;
            try {
                _cancellationTokenSource.Cancel();
            }
            catch (ObjectDisposedException e) {
                return;
            }
            if (!alreadyCancelled) {
                ShowDialog = true;
                Message = "Counting was canceled";
            }
        }
    }
}
