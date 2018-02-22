using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentsApp.API.Controllers;
using StudentAppHelper.ModelBindings.Models;

namespace StudentsApp.API.Tests.Account
{
  /// <summary>
  /// Summary description for AccountUnitTest
  /// </summary>
  [TestClass]
  public class AccountUnitTest
  {
    public AccountUnitTest()
    {
      //
      // TODO: Add constructor logic here
      //
    }

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
    // You can use the following additional attributes as you write your tests:
    //
    // Use ClassInitialize to run code before running the first test in the class
    // [ClassInitialize()]
    // public static void MyClassInitialize(TestContext testContext) { }
    //
    // Use ClassCleanup to run code after all tests in a class have run
    // [ClassCleanup()]
    // public static void MyClassCleanup() { }
    //
    // Use TestInitialize to run code before running each test 
    // [TestInitialize()]
    // public void MyTestInitialize() { }
    //
    // Use TestCleanup to run code after each test has run
    // [TestCleanup()]
    // public void MyTestCleanup() { }
    //
    #endregion

    [TestMethod]
    public void TestRegistroAdmin()
    {
      AccountController account = new AccountController();
      //UserRegistrationModel model = new UserRegistrationModel()
      //{
      //  FirstName = "Daniel",
      //  NombreS = "Pruebas",
      //  ApellidoP = "Miranda",
      //  ApellidoS = "Vasquez",
      //  TipoDocumento = TipoDocumento.Cedula,
      //  Identificacion = "1032",
      //  UserName = "BobPatiño5001",
      //  Password = "123",
      //  ConfirmPassword = "123"
      //};
      //account.RegisterAdmin(model);
    }

    [TestMethod]
    public void TestmetodoTopicList()
    {
      AccountController account = new AccountController();
     // account.topicList();
    }
  }
}
