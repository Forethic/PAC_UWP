using System;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.System;

namespace CharacterMap.Helpers
{
    public class Tasks
    {
        public static async Task OpenStoreReviewAsync()
        {
            string familyName = Package.Current.Id.FamilyName;
            await Launcher.LaunchUriAsync(new Uri("ms-windows-store://review/?PFN=" + familyName));
        }
    }
}