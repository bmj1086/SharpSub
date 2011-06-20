using System.Linq;
using NAudio.Wave;
using SharpSub.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace SharpSub.Data.Tests
{
    
    
    /// <summary>
    ///This is a test class for MP3Test and is intended
    ///to contain all MP3Test Unit Tests
    ///</summary>
    [TestClass()]
    //[Ignore]
    public class MP3Test
    {
        private TestContext testContextInstance;
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for MP3 Constructor
        ///</summary>
        [TestMethod()]
        public void MP3ConstructorTest()
        {
            string serverURL = "bmjones.com:56565/music";
            string username = "Guest";
            string password = "notbrett";
            SubsonicRequest.Login(serverURL, username, password);
            
            //Random random = new Random();
            //var artistList = SubsonicRequest.GetArtistList();
            //var artist = artistList[random.Next(artistList.Count)];
            var artist = SubsonicRequest.GetArtistList().First();

            Album album = SubsonicRequest.GetArtistAlbums(artist).First();
            Song song = SubsonicRequest.GetAlbumSongs(album.ID).First();

            Mp3Player player = new Mp3Player(song);
            player.Play();
            DateTime tostop = DateTime.Now.AddSeconds(10);
            while (DateTime.Now < tostop){}
            player.Stop();
            Debug.WriteLine("Stopped playback...");
        }

    }
}
