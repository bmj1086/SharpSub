using System.IO;
using System.Linq;
using SharpSub.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Web;
using System.Diagnostics;
using System.Drawing;

namespace SharpSub.Data.Tests
{
    
    
    /// <summary>
    ///This is a test class for SubsonicRequestTest and is intended
    ///to contain all SubsonicRequestTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SubsonicRequestTest
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
        ///A test for LogIn
        ///</summary>
        [TestMethod()]
        public void LoginTest()
        {
            string serverURL = "bjones.subsonic.org";
            string username = "Guest";
            string password = "notbrett";
            bool expected = true; 
            SubsonicResponse response = SubsonicRequest.Login(serverURL, username, password);
            bool actual = response.Successful;
            Assert.AreEqual(expected, actual);
        }


        /// <summary>
        ///A test for GetArtists
        ///</summary>
        [TestMethod()]
        public void GetAlbumsTest()
        {
            bool expected = true;
            bool actual;
            string serverURL = "bmjones.com:56565/music";
            string username = "Guest";
            string password = "notbrett";
            SubsonicRequest.Login(serverURL, username, password);

            string albumId = "533a5c4d757369635c4161726f6e2047696c6c65737069655c416e7468656d20536f6e6720283230313129";
            var albumSongs = SubsonicRequest.GetAlbumSongs(albumId);
            actual = albumSongs.Count > 0;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetArtistAlbums
        ///</summary>
        [TestMethod()]
        public void GetArtistAlbumsTest()
        {
            bool expected = true;
            bool actual;

            string serverURL = "bmjones.com:56565/music";
            string username = "Guest";
            string password = "notbrett";
            SubsonicRequest.Login(serverURL, username, password);

            Artist artist = SubsonicRequest.GetArtistList().First();
            var artistAlbums = SubsonicRequest.GetArtistAlbums(artist);
            actual = artistAlbums.Count > 0;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetAlbumArt
        ///</summary>
        [TestMethod()]
        public void GetAlbumArtTest()
        {
            string serverURL = "bmjones.com:56565/music";
            string username = "Guest";
            string password = "notbrett";
            SubsonicRequest.Login(serverURL, username, password);

            Artist artist = SubsonicRequest.GetArtistList()[10];
            Album album = SubsonicRequest.GetArtistAlbums(artist).First();
            bool expected = true; //TODO: Initialize to an appropriate value
            Bitmap coverArt = SubsonicRequest.GetAlbumArt(album);
            coverArt.Save("C:/tmp.bmp");
            bool actual = coverArt != null;
            Assert.AreEqual(expected, actual);
        }

    }
}
