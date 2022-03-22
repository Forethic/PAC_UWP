using System;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.ViewManagement;

namespace CharacterMap.Helpers
{
    public class Mobile
    {
        public static void SetWindowsMobileStatusBarColor(Color? backgroundColor, Color? foregroundColor)
        {
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                StatusBar forCurrentView = StatusBar.GetForCurrentView();
                forCurrentView.BackgroundColor = backgroundColor;
                forCurrentView.ForegroundColor = foregroundColor;
                forCurrentView.BackgroundOpacity = 1.0;
            }
        }

        public static async Task HideWindowsMobileStatusBar()
        {
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                await StatusBar.GetForCurrentView().HideAsync();
            }
        }
    }
}