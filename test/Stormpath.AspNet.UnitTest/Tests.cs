using System.Collections.Generic;
using System.Net.Http;
using FluentAssertions;
using Moq;
using Stormpath.Owin.Abstractions;
using Stormpath.SDK.Account;
using Xunit;

namespace Stormpath.AspNet.UnitTest
{
    public class Tests
    {
        [Fact]
        public void Test1() 
        {
            var request = new System.Net.Http.HttpRequestMessage();

            var account = new Mock<IAccount>();
            account.Setup(a => a.Email).Returns("test@example.com");

            request.SetStormpathAccount(account.Object);

            request.GetStormpathAccount().Should().NotBeNull();
        }
    }

    public static class RequestExtensions
    {
        public static void SetStormpathAccount(this HttpRequestMessage request, IAccount account)
        {
            if (request.GetOwinContext() == null)
            {
                request.SetOwinEnvironment(new Dictionary<string, object>());
            }

            request.GetOwinContext().Set(OwinKeys.StormpathUser, account);
            request.GetOwinContext().Set(OwinKeys.StormpathClient, account.Client);
        }
    }
}
