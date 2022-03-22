using Windows.UI;
using Windows.UI.ViewManagement;

namespace CharacterMap.Helpers
{
    public class UI
    {
        public enum NotificationAudioNames
        {
            Default,
            IM,
            Mail,
            Reminder,
            SMS,
            Looping_Alarm,
            Looping_Alarm2,
            Looping_Alarm3,
            Looping_Alarm4,
            Looping_Alarm5,
            Looping_Alarm6,
            Looping_Alarm7,
            Looping_Alarm8,
            Looping_Alarm9,
            Looping_Alarm10,
            Looping_Call,
            Looping_Call2,
            Looping_Call3,
            Looping_Call4,
            Looping_Call5,
            Looping_Call6,
            Looping_Call7,
            Looping_Call8,
            Looping_Call9,
            Looping_Call10
        }

        public static void ApplyColorToTitleBar(Color? titleBackgroundColor, Color? titleForegroundColor, Color? titleInactiveBackgroundColor, Color? titleInactiveForegroundColor)
        {
            ApplicationView forCurrentView = ApplicationView.GetForCurrentView();
            forCurrentView.TitleBar.BackgroundColor = titleBackgroundColor;
            forCurrentView.TitleBar.ForegroundColor = titleForegroundColor;
            forCurrentView.TitleBar.InactiveBackgroundColor = titleInactiveBackgroundColor;
            forCurrentView.TitleBar.InactiveForegroundColor = titleInactiveForegroundColor;
        }

        public static void ApplyColorToTitleButton(Color? titleButtonBackgroundColor, Color? titleButtonForegroundColor, Color? titleButtonHoverBackgroundColor, Color? titleButtonHoverForegroundColor, Color? titleButtonPressedBackgroundColor, Color? titleButtonPressedForegroundColor, Color? titleButtonInactiveBackgroundColor, Color? titleButtonInactiveForegroundColor)
        {
            ApplicationView forCurrentView = ApplicationView.GetForCurrentView();
            forCurrentView.TitleBar.ButtonBackgroundColor = titleButtonBackgroundColor;
            forCurrentView.TitleBar.ButtonForegroundColor = titleButtonForegroundColor;
            forCurrentView.TitleBar.ButtonHoverBackgroundColor = titleButtonHoverBackgroundColor;
            forCurrentView.TitleBar.ButtonHoverForegroundColor = titleButtonHoverForegroundColor;
            forCurrentView.TitleBar.ButtonPressedBackgroundColor = titleButtonPressedBackgroundColor;
            forCurrentView.TitleBar.ButtonPressedForegroundColor = titleButtonPressedForegroundColor;
            forCurrentView.TitleBar.ButtonInactiveBackgroundColor = titleButtonInactiveBackgroundColor;
            forCurrentView.TitleBar.ButtonInactiveForegroundColor = titleButtonInactiveForegroundColor;
        }

    }
}