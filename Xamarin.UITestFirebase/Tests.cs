using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace Xamarin.UITestFirebase
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;

        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
            //app = ConfigureApp.Android.ApkFile(@"..\..\..\IllyaVirych.Droid/bin/Debug\IllyaVirych.Droid.IllyaVirych.Droid.apk").InstalledApp("IllyaVirych.Droid.IllyaVirych.Droid").StartApp();
        }

        [Test]
        public void WelcomeTextIsDisplayed()
        {
            app.Tap(c => c.Button("image_button"));
            //app.Repl();
            //app.Tap(x => x.Marked("More options"));
            //app.Tap(x => x.Text("Add"));
            //app.EnterText(x => x.Id("txtTitle"), "EA");
            //app.DismissKeyboard();
            //app.EnterText(x => x.Id("txtDesc"), "Description");
            //app.DismissKeyboard();
            //app.Tap(x => x.Id("save_button"));
            //app.WaitForElement(x => x.Text("EA"));
        }

        [Test]
        public void Test()
        {
            app.Tap(c => c.Button("image_button"));
            //app.Tap(x => x.Marked("More options"));
            //app.Tap(x => x.Text("Add"));
            //app.EnterText(x => x.Id("txtTitle"), "EA");
            //app.DismissKeyboard();
            //app.EnterText(x => x.Id("txtDesc"), "Description");
            //app.DismissKeyboard();
            //app.Tap(x => x.Id("save_button"));
            //app.WaitForElement(x => x.Text("EA"));
        }
    }
}
