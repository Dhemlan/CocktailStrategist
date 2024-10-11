using CocktailStrategist.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using FluentAssertions;
using System.Net;

namespace CocktailStrategist.Tests.Integration
{
    [TestFixture]
    public class IngredientControllerTests
    {
        private HttpClient client;

        [OneTimeSetUp]
        public void FixtureSetUp()
        {
            var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
            });
            client = factory.CreateClient();
        }
        [OneTimeTearDown]
        public void FixtureTearDown() { client.Dispose(); }

        [Test]
        public async Task Get_retrievesSpecifiedIngredient()
        {
            // Arrange
            var ingredient = new Ingredient { Id = Guid.Parse("a7473d26-ab2d-4364-816e-89fe943b1895"), Name = "Lime" };
            var url = $"/ingredient/{ingredient.Id}";

            // Act
            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            var retrievedIngredient = JsonConvert.DeserializeObject<Ingredient>(content);
            retrievedIngredient.Should().BeEquivalentTo(ingredient, o => o.ComparingByMembers<Ingredient>());
        }

        [Test]
        public async Task Get_returns404WithNonExistantID()
        {
            // Arrange
            var ingredient = new Ingredient { Id = Guid.NewGuid(), Name = "Fake" };
            var url = $"/ingredient/{ingredient.Id}";

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.Should().HaveStatusCode(HttpStatusCode.NotFound);
        }
    }
}
