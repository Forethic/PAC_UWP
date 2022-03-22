using System;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage.Streams;

namespace CharacterMap.Helpers
{
    public class Utils
    {
        public static string Architecture => Package.Current.Id.Architecture.ToString();

        public static void CopyToClipBoard(string str)
        {
            DataPackage dataPackage = new DataPackage();
            dataPackage.RequestedOperation = DataPackageOperation.Copy;
            dataPackage.SetText(str);
            Clipboard.SetContent(dataPackage);
        }

        public static async Task<byte[]> ConvertImagetoByte(IRandomAccessStream fileStream)
        {
            DataReader reader = new DataReader(fileStream.GetInputStreamAt(0uL));
            await reader.LoadAsync((uint)fileStream.Size);
            byte[] array = new byte[fileStream.Size];
            reader.ReadBytes(array);
            return array;
        }

        public static string GetAppVersion()
        {
            PackageVersion version = Package.Current.Id.Version;
            return $"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        }

        public static string GetAppDisplayName()
        {
            return Package.Current.DisplayName;
        }

        public static string GetAppPublisher()
        {
            return Package.Current.PublisherDisplayName;
        }
    }
}