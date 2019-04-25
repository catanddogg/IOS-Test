using System;
using System.IO;
using System.Reflection;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace Xamarin.UITestFirebase
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            string currentFile = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            FileInfo fi = new FileInfo(currentFile);
            string dir = fi.Directory.Parent.Parent.Parent.FullName;
            // PathToAPK is a property or an instance variable in the test class
            var PathToAPK = Path.Combine(dir, "IllyaVirych.Droid", "bin", "Release", "IllyaVirych.Droid.IllyaVirych.Droid.apk");
            if (platform == Platform.Android)
            {
                return ConfigureApp
                    .Android
                    .ApkFile(PathToAPK)
                    .DeviceSerial("00e0ed388958ad")
                    .StartApp();
            }
            return ConfigureApp.iOS.StartApp();
        }
    }
}