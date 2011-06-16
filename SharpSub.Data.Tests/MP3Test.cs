﻿using SharpSub.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SharpSub.Data.Tests
{
    
    
    /// <summary>
    ///This is a test class for MP3Test and is intended
    ///to contain all MP3Test Unit Tests
    ///</summary>
    [TestClass()]
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
            bool expected = true;
            bool actual;
            string serverURL = "bmjones.com:56565/music";
            string username = "Guest";
            string password = "notbrett";
            SubsonicRequest.Login(serverURL, username, password);

            Artist artist = SubsonicRequest.GetArtistList()[0];
            Album album = SubsonicRequest.GetArtistAlbums(artist.ID)[0];
            Song song = SubsonicRequest.GetAlbumSongs(album.ID)[0];
            MP3 target = new MP3(song);
        }
    }
}
