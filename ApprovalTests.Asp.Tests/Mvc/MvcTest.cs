﻿using System.Web.Mvc;
using ApprovalTests.Asp.Mvc;
using ApprovalTests.Reporters;
using ApprovalUtilities.Asp.Mvc;
using CassiniDev;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcApplication1;
using MvcApplication1.Controllers;
using MvcApplication1.Models;

namespace ApprovalTests.Asp.Tests.Mvc
{
    [TestClass]
    [UseReporter(typeof (DiffReporter), typeof (FileLauncherReporter))]
    public class MvcTest
    {
        private readonly CassiniDevServer server = new CassiniDevServer();

        [TestInitialize]
        public void Setup()
        {
            PortFactory.MvcPort = 11625;
            this.server.StartServer(MvcApplication.Path, PortFactory.MvcPort, "/", "localhost");
        }


        [TestMethod]
        public void TestMvcPage()
        {
            MvcApprovals.VerifyMvcPage<TestableExampleController>(c => c.TestName);
        }


        [TestCleanup]
        public void TearDown()
        {
            this.server.StopServer();
        }
    }

    public class TestableExampleController : TestableController<CoolController>
    {
        public TestableExampleController(CoolController t)
            : base(t)
        {
        }

        public ActionResult TestName()
        {
            return ControllerUnderTest.SaveName(new Person { Name = "Henrik" });
        }
    }
}