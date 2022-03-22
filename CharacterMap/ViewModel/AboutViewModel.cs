using CharacterMap.Helpers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace CharacterMap.ViewModel
{
    public class AboutViewModel : ViewModelBase
    {
        public string DisplayName => Utils.GetAppDisplayName();
        public string Publisher => Utils.GetAppPublisher();
        public string Version => Utils.GetAppVersion();
        public string Architecture => Utils.Architecture;

        public RelayCommand CommandReview { get; set; }

        public AboutViewModel()
        {
            CommandReview = new RelayCommand(async () => { await Tasks.OpenStoreReviewAsync(); });
        }
    }
}