using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace URLsTest.Client.Models {
    public class TagItem : INotifyPropertyChanged {
        private string _url;
        private int _count;
        private bool _errorOcurred;
        private bool _isCalculating;

        public string Url {
            get { return _url; }
            set {
                _url = value;
                OnPropertyChanged("Url");
            }
        }

        public int Count {
            get { return _count; }
            set {
                _count = value;
                OnPropertyChanged("Count");
            }
        }

        public bool ErrorOcurred {
            get { return _errorOcurred; }
            set {
                _errorOcurred = value;
                OnPropertyChanged("ErrorOcurred");
            }
        }

        public bool IsCalculating {
            get { return _isCalculating; }
            set {
                _isCalculating = value;
                OnPropertyChanged("IsCalculating");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public override string ToString() {
            return $"URL: {this.Url} Count: {this.Count} {(ErrorOcurred ? "Error occured" : "")}";
        }
    }
}
