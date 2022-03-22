using CharacterMap.Core;
using CharacterMap.Helpers;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Linq;

namespace CharacterMap.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<InstalledFont> _fontList;
        private ObservableCollection<Character> _chars;
        private InstalledFont _selectedFont;

        public ObservableCollection<InstalledFont> FontList
        {
            get => _fontList;
            set
            {
                _fontList = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Character> Chars
        {
            get => _chars;
            set
            {
                _chars = value;
                RaisePropertyChanged();
            }
        }

        public InstalledFont SelectedFont
        {
            get => _selectedFont;
            set
            {
                _selectedFont = value;
                if (_selectedFont != null)
                {
                    var chars = _selectedFont.GetCharacters();
                    Chars = chars.ToObservableCollection();
                }
                RaisePropertyChanged();
            }
        }

        public MainViewModel()
        {
            var fontList = InstalledFont.GetFonts();
            FontList = fontList.OrderBy(f => f.Name).ToObservableCollection();
        }
    }
}