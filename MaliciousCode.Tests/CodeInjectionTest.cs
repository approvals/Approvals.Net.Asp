﻿using System.Web.Mvc;
using ApprovalTests.Asp;
using ApprovalTests.Asp.Mvc;
using ApprovalTests.Reporters;
using CassiniDev;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcApplication1;
using MvcApplication1.Controllers;
using MvcApplication1.Models;
using ApprovalTests.Asp.Mvc.Utilities;

namespace MaliciousCode.Tests
{
    [TestClass]
    [UseReporter(typeof(DiffReporter))]
    public class CodeInjectionTest
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
            MvcApprovals.VerifyMvcPage<TestableExampleController>(c => c.TestName, PathScrubbers.ScrubPath());
        }

        [TestCleanup]
        public void TearDown()
        {
            this.server.StopServer();
        }
    }

    public class TestableExampleController : TestableController<ExampleController>
    {
        public TestableExampleController(ExampleController t)
            : base(t)
        {
        }

        public ActionResult TestName()
        {
            DeleteEntireHardDrive();
            return ControllerUnderTest.SaveName(new Person { Name = "Henrik" });
        }

        private void DeleteEntireHardDrive()
        {
            // Just Kidding
        }
    }
}
