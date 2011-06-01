﻿using SharpSub.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Web;

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
        public void LogInTest()
        {
            string serverURL = "bjones.subsonic.org";
            string username = "Guest";
            string password = HttpUtility.UrlEncode("notbrett");
            bool expected = true; 
            Subsonic.Response response = SubsonicRequest.LogIn(serverURL, username, password);
            bool actual = response.Successful;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for BuildRequestURL
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SharpSub.Data.dll")]
        public void BuildRequestURLTest()
        {
            RequestType requestType = new RequestType(); // TODO: Initialize to an appropriate value
            Dictionary<string, string> additionalParameters = null; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = SubsonicRequest_Accessor.BuildRequestURL(requestType, additionalParameters);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
