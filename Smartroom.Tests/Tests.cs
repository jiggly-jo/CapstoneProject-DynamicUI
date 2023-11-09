using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using System.Threading;


namespace Smartroom.Tests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
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

        }

        [Test]
        public void MediaTest()
        {
            app.Tap(c => c.Marked("MediaRow"));

            app.Tap(c => c.Marked("TVOnOff"));
            AppResult[] result = app.Query(c => c.Marked("Message").Text("TV_POWER_FB"));
            Assert.IsTrue(result.Any());

            app.Tap(c => c.Marked("ChannelUp"));
            result = app.Query(c => c.Marked("Message").Text("TV_CH_UP_FB"));
            Assert.IsTrue(result.Any());

            app.Tap(c => c.Marked("ChannelDown"));
            result = app.Query(c => c.Marked("Message").Text("TV_CH_DN_FB"));
            Assert.IsTrue(result.Any());

            app.Tap(c => c.Marked("Channel1"));
            result = app.Query(c => c.Marked("Message").Text("TV_CH1_FB"));
            Assert.IsTrue(result.Any());

            app.Tap(c => c.Marked("Channel2"));
            result = app.Query(c => c.Marked("Message").Text("TV_CH2_FB"));
            Assert.IsTrue(result.Any());

            app.Tap(c => c.Marked("Channel3"));
            result = app.Query(c => c.Marked("Message").Text("TV_CH3_FB"));
            Assert.IsTrue(result.Any());

            app.Tap(c => c.Marked("Channel4"));
            result = app.Query(c => c.Marked("Message").Text("TV_CH4_FB"));
            Assert.IsTrue(result.Any());

            app.Tap(c => c.Marked("Channel5"));
            result = app.Query(c => c.Marked("Message").Text("TV_CH5_FB"));
            Assert.IsTrue(result.Any());

            app.Tap(c => c.Marked("Channel6"));
            result = app.Query(c => c.Marked("Message").Text("TV_CH6_FB"));
            Assert.IsTrue(result.Any());

            app.Tap(c => c.Marked("Channel7"));
            result = app.Query(c => c.Marked("Message").Text("TV_CH7_FB"));
            Assert.IsTrue(result.Any());

            app.Tap(c => c.Marked("Channel8"));
            result = app.Query(c => c.Marked("Message").Text("TV_CH8_FB"));
            Assert.IsTrue(result.Any());

            app.Tap(c => c.Marked("Channel9"));
            result = app.Query(c => c.Marked("Message").Text("TV_CH9_FB"));
            Assert.IsTrue(result.Any());

            app.Tap(c => c.Marked("Channel0"));
            result = app.Query(c => c.Marked("Message").Text("TV_CH0_FB"));
            Assert.IsTrue(result.Any());

            app.Tap(c => c.Marked("VolumeMute"));
            result = app.Query(c => c.Marked("Message").Text("SBAR_VOL_MUTE_IS_ON_FB"));
            AppResult[] result2 = app.Query(c => c.Marked("Message").Text("SBAR_VOL_MUTE_IS_OFF_FB"));
            Assert.IsTrue(result.Any() || result2.Any());

            app.Tap(c => c.Marked("VolumeMute"));

            if(result.Any())
            {
                result2 = app.Query(c => c.Marked("Message").Text("SBAR_VOL_MUTE_IS_OFF_FB"));
                Assert.IsTrue(result2.Any());
            }
            else if (result2.Any())
            {
                result = app.Query(c => c.Marked("Message").Text("SBAR_VOL_MUTE_IS_ON_FB"));
                Assert.IsTrue(result.Any());
            }

        }

        [Test]
        public void DoorTest()
        {
            app.Tap(c => c.Marked("DoorRow"));

            app.Tap(c => c.Marked("DoorClose"));
            AppResult[] result = app.Query(c => c.Marked("Message").Text("DOOR_CLOSE_FB"));
            Assert.IsTrue(result.Any());

            app.Tap(c => c.Marked("DoorPartial"));
            result = app.Query(c => c.Marked("Message").Text("DOOR_OPEN_PARTIAL_FB"));
            Assert.IsTrue(result.Any());

            Thread.Sleep(2000);
            app.Tap(c => c.Marked("DoorOpen"));
            result = app.Query(c => c.Marked("Message").Text("DOOR_OPEN_FB"));
            Assert.IsTrue(result.Any());
        }

        [Test]
        public void LightTest()
        {
            app.Tap(c => c.Marked("LightRow"));

            app.Tap(c => c.Marked("AmbientToggle"));
            Thread.Sleep(4000);
            AppResult[] result = app.Query(c => c.Marked("Message").Text("LTG_ON_0_FB"));
            AppResult[] result2 = app.Query(c => c.Marked("Message").Text("LTG_OFF_0_FB"));
            Assert.IsTrue(result.Any() || result2.Any());
        }

        [Test]
        public void BlindTest()
        {
            app.Tap(c => c.Marked("BlindsRow"));

            app.Tap(c => c.Marked("InteriorOpen"));
            AppResult[] result = app.Query(c => c.Marked("Message").Text("BLINDS_OPAQUE_OPEN_FB"));
            Assert.IsTrue(result.Any());

            app.Tap(c => c.Marked("InteriorLower"));
            result = app.Query(c => c.Marked("Message").Text("BLINDS_OPAQUE_DOWN_FB"));
            Assert.IsTrue(result.Any());

            app.Tap(c => c.Marked("InteriorRaise"));
            result = app.Query(c => c.Marked("Message").Text("BLINDS_OPAQUE_UP_FB"));
            Assert.IsTrue(result.Any());

            app.Tap(c => c.Marked("InteriorClose"));
            result = app.Query(c => c.Marked("Message").Text("BLINDS_OPAQUE_CLOSE_FB"));
            Assert.IsTrue(result.Any());

        }

        [Test]
        public void TempTest()
        {
            app.Tap(c => c.Marked("TempRow"));

            app.Tap(c => c.Marked("TempUp"));
            AppResult[] result = app.Query(c => c.Marked("Message").Text("HVAC_TEMP_UP_FB"));
            Assert.IsTrue(result.Any());
        }
    }
}