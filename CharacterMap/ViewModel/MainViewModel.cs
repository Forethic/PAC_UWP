using CharacterMap.Core;
using CharacterMap.Helpers;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Linq;

namespace CharacterMap.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<AlphaKeyGroup<InstalledFont>> _groupedFontList;
        private ObservableCollection<InstalledFont> _fontList;
        private ObservableCollection<Character> _chars;
        private InstalledFont _selectedFont;
        private bool _showSymbolFontsOnly;

        public ObservableCollection<AlphaKeyGroup<InstalledFont>> GroupedFontList
        {
            get => _groupedFontList;
            set
            {
                _groupedFontList = value;
                RaisePropertyChanged();
            }
        }

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

        public bool ShowSymbolFontsOnly
        {
            get => _showSymbolFontsOnly;
            set
            {
                _showSymbolFontsOnly = value;
                FilterFontList(value);
                RaisePropertyChanged();
            }
        }

        private void FilterFontList(bool isSymbolFontOnly)
        {
            var fontList = InstalledFont.GetFonts();

            var newList = fontList.Where(f => f.IsSymbolFont || !isSymbolFontOnly)
                                  .OrderBy(f => f.Name)
                                  .ToObservableCollection();
            FontList = newList;
        }

        public MainViewModel()
        {
            var fontList = InstalledFont.GetFonts();
            FontList = fontList.OrderBy(f => f.Name).ToObservableCollection();
            CreateFontListGroup();
        }

        private void CreateFontListGroup()
        {
            var list = AlphaKeyGroup<InstalledFont>.CreateGroups(FontList, f => f.Name.Substring(0, 1), true);
            GroupedFontList = list.ToObservableCollection();
        }
    }
}