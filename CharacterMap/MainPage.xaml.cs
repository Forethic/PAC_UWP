using CharacterMap.Core;
using CharacterMap.Helpers;
using CharacterMap.ViewModel;
using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Provider;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace CharacterMap
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage
    {
        public MainViewModel MainViewModel { get; set; }

        public MainPage()
        {
            InitializeComponent();
            MainViewModel = DataContext as MainViewModel;
            Loaded += MainPage_Loaded;

        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            CmbFontFamily.SelectedIndex = 0;
        }

        private void SearchBoxUnicode_QuerySubmitted(SearchBox sender, SearchBoxQuerySubmittedEventArgs args)
        {
            var unicodeIndex = SearchBoxUnicode.QueryText;
            int intIndex = ParseHexString(unicodeIndex);

            var ch = MainViewModel.Chars.FirstOrDefault(c => c.UnicodeIndex == intIndex);
            if (ch != null)
            {
                CharGrid.SelectedItem = ch;
            }
        }

        private static int ParseHexString(string hexNumber)
        {
            hexNumber = hexNumber.Replace("x", string.Empty);
            int.TryParse(hexNumber, System.Globalization.NumberStyles.HexNumber, null, out int result);
            return result;
        }

        private void ToggleTheme_Toggled(object sender, RoutedEventArgs e)
        {
            if (ToggleTheme != null)
            {
                RequestedTheme = ToggleTheme.IsOn ? ElementTheme.Dark : ElementTheme.Light;

                if (ToggleTheme.IsOn)
                {
                    UI.ApplyColorToTitleBar(Color.FromArgb(255, 43, 43, 43), Colors.White, Colors.DimGray, Colors.White);
                    UI.ApplyColorToTitleButton(Color.FromArgb(255, 43, 43, 43), Colors.White, Colors.DimGray, Colors.White, Colors.DimGray, Colors.White, Colors.DimGray, Colors.White);
                }
                else
                {

                    UI.ApplyColorToTitleBar(Color.FromArgb(255, 0, 114, 188), Colors.White, Colors.LightGray, Colors.White);
                    UI.ApplyColorToTitleButton(Color.FromArgb(255, 0, 114, 188), Colors.White,
                                               Color.FromArgb(255, 51, 148, 208), Colors.White,
                                               Color.FromArgb(255, 0, 114, 188), Colors.White,
                                               Colors.LightGray, Colors.White);
                }
            }
        }

        private void BtnSelect_Click(object sender, RoutedEventArgs e)
        {
            if (CharGrid?.SelectedItem is Character ch)
            {
                TxtSelected.Text += ch.Char ?? string.Empty;
                TxtXamlCode.Text = $"&#x{ch.UnicodeIndex.ToString("x").ToUpper()};";
                if (CmbFontFamily.SelectedItem is InstalledFont installedFont)
                {
                    TxtFontIcon.Text = $@"<FontIcon FontFamily=""{installedFont.Name}"" Glyph=""&#x{ch.UnicodeIndex.ToString("x").ToUpper()};""/>";
                }
            }
        }

        private async void BtnSavePng_Click(object sender, RoutedEventArgs e)
        {
            var bitmap = new RenderTargetBitmap();
            await bitmap.RenderAsync(GridRenderTarget);

            IBuffer buffer = await bitmap.GetPixelsAsync();
            var stream = buffer.AsStream();
            var fileName = $"{DateTime.Now:yyyy-MM-dd-HHmmss}";
            var result = await SaveStreamToImage(PickerLocationId.PicturesLibrary, fileName, stream, bitmap.PixelWidth, bitmap.PixelHeight);

            if (result != FileUpdateStatus.Complete)
            {
                var dlg = new MessageDialog(result.ToString(), "Oh Shit");
                await dlg.ShowAsync();
            }
        }

        private async Task<FileUpdateStatus> SaveStreamToImage(PickerLocationId location, string fileName, Stream stream, int pixelWidth, int pixelHeight)
        {
            var savePicker = new FileSavePicker
            {
                SuggestedStartLocation = location,
            };

            savePicker.FileTypeChoices.Add("Png Image", new[] { ".png" });
            savePicker.SuggestedFileName = fileName;
            StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                CachedFileManager.DeferUpdates(file);

                using (var fileStream = await file.OpenAsync(FileAccessMode.ReadWrite))
                {
                    BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, fileStream);
                    Stream pixelStream = stream;
                    byte[] pixels = new byte[pixelStream.Length];
                    await pixelStream.ReadAsync(pixels, 0, pixels.Length);
                    encoder.SetPixelData(BitmapPixelFormat.Bgra8,
                                         BitmapAlphaMode.Straight,
                                         (uint)pixelWidth,
                                         (uint)pixelHeight,
                                         96.0,
                                         96.0,
                                         pixels);
                    await encoder.FlushAsync();
                }

                FileUpdateStatus status = await CachedFileManager.CompleteUpdatesAsync(file);
                return status;
            }
            return FileUpdateStatus.Failed;
        }

        private void CharGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CharGrid?.SelectedItem is Character ch)
            {
                TxtPreview.Text = ch.Char ?? string.Empty;
                Txtunicode.Text = "U+" + ch.UnicodeIndex.ToString("x").ToUpper();
            }
        }
        private void TxtXamlCode_GotFocus(object sender, RoutedEventArgs e)
        {
            TxtXamlCode.SelectAll();
        }

        private void TxtFontIcon_GotFocus(object sender, RoutedEventArgs e)
        {
            TxtFontIcon.SelectAll();
        }

        private void BtnCopy_Click(object sender, RoutedEventArgs e)
        {
            Utils.CopyToClipBoard(TxtSelected.Text);
        }

        private void BtnAbout_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AboutPage));
        }

        private void BtnCopyXamlCode_Click(object sender, RoutedEventArgs e)
        {
            Utils.CopyToClipBoard(TxtXamlCode.Text.Trim());
        }

        private void BtnCopyFontIcon_Click(object sender, RoutedEventArgs e)
        {
            Utils.CopyToClipBoard(TxtFontIcon.Text.Trim());
        }
    }
}