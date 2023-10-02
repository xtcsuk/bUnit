using Bunit.Demo.Pages;
using Bunit.TestDoubles;

namespace Bunit.Demo.Tests.Pages
{
    public class TestAuthorizationTest : TestContext
    {
        [Fact]
        public void Should_Display_UnAuthorized_When_Not_Authenticated_And_NotAuthorized()
        {
            // arrange, act
            this.AddTestAuthorization();

            var cut = RenderComponent<TestAuthorization>();

            // assert
            cut.MarkupMatches("<p><strong>You are not authorized.</strong></p>");
        }

        [Fact]
        public void Should_Display_Authorizing_When_Authenticated_And_In_Authorizing_State()
        {
            // arrange, act
            var authContext = this.AddTestAuthorization();
            authContext.SetAuthorizing();

            var cut = RenderComponent<TestAuthorization>();

            // assert
            cut.MarkupMatches("<p><strong>Authorizing.</strong></p>");
        }

        [Fact]
        public void Should_Display_Authorized_When_Authenticated_And_Authorized()
        {
            // arrange, act
            var authContext = this.AddTestAuthorization();
            authContext.SetAuthorized("Test User");
            authContext.SetRoles("TestRole");

            var cut = RenderComponent<TestAuthorization>();

            // assert
            cut.MarkupMatches("<p><strong>You are authorized.</strong></p>");
        }
    }
}
