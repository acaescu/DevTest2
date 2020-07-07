using BusinessLayer;
using BusinessLayer.Interfaces;
using DataLayer;
using DataLayer.DataObjects;
using DataLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NUnitTests
{
    [TestFixture]
    public class Tests
    {
        ICustomMembership _membership;
        IDomainContext _db;
        [SetUp]
        public void Setup()
        {
            //TO DO: add some mock data
            _db = new DomainContext();
            var mockHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
            _membership = new CustomMembership(mockHttpContextAccessor, _db);
        }

       [Test]
        public void  TestVerifyLoginWrongUserOrPassword()
        {
            User user;
            bool resultUser = _membership.VerifyLogin("testUser", "password", out user);
            bool resultPassword = _membership.VerifyLogin("client", "testpassword", out user);
            Assert.IsFalse(resultUser);
            Assert.IsFalse(resultPassword);
        }

        [Test]
        public void TestVerifyLoginEmptyUserOrPassword()
        {
            User user;
            bool resultEmptyUser = _membership.VerifyLogin(null, "password", out user);
            bool resultEmptyPassword = _membership.VerifyLogin("client", null, out user);
            bool resultEmptyBoth = _membership.VerifyLogin(null, null, out user);
            Assert.IsFalse(resultEmptyUser);
            Assert.IsFalse(resultEmptyPassword);
            Assert.IsFalse(resultEmptyBoth);
        }

        [Test]
        public void TestVerifyLoginCorrectUserAndPassword()
        {
            User user;
            bool result = _membership.VerifyLogin("client", "password", out user);
            Assert.IsTrue(result);
        }       

    }
}