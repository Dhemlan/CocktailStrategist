using CocktailStrategist.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using FluentAssertions;
using System.Net;
using System.Text;
using CocktailStrategist.Data.Enum;

namespace CocktailStrategist.Tests.Integration
{
    [TestFixture]
    public class IngredientControllerTests
    {
        private const string BASE_URL = "/ingredient";
        private Ingredient lime = new Ingredient
        {
            Id = Guid.Parse("57295ca5-3e94-403e-acb7-01417ed03c4d"),
            Name = "Lime",
            isAvailable = true,
            Category = IngredientCategory.Citrus
        };
        private Ingredient orgeat = new Ingredient
        {
            Id = Guid.Parse("e6db0f8c-43aa-4eed-8e8c-f6cf20615f4b"),
            Name = "Orgeat",
            isAvailable = true,
            Category = IngredientCategory.SyrupsAndSweeteners
        };

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
        public void FixtureTearDown() {
            client.Dispose(); }

        [Test]
        public async Task Get_retrievesSpecifiedIngredient()
        {
            // Arrange
            var url = $"{BASE_URL}/{lime.Id}";

            // Act
            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            var retrievedIngredient = JsonConvert.DeserializeObject<Ingredient>(content);
            retrievedIngredient.Should().BeEquivalentTo(lime, o => o.ComparingByMembers<Ingredient>());
        }

        [Test]
        public async Task Get_returns404WithNonExistantID()
        {
            // Arrange
            var ingredient = new Ingredient { Id = Guid.NewGuid(), Name = "Fake" };
            var url = $"{BASE_URL}/{ingredient.Id}";

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.Should().HaveStatusCode(HttpStatusCode.NotFound);
        }

        [Test]
        public async Task Get_retrievesAllIngredients()
        {
            // Arrange
            var ingredients = new List<Ingredient> {lime, orgeat};

            // Act
            var response = await client.GetAsync(BASE_URL);
            var content = await response.Content.ReadAsStringAsync();
            var retrievedIngredients = JsonConvert.DeserializeObject<List<List<Ingredient>>>(content);

            //Assert
            response.Should().HaveStatusCode(HttpStatusCode.OK);
            retrievedIngredients.Should().BeEquivalentTo(ingredients);
        }

        [Test]
        public async Task Post_createsNewIngredient()
        {
            // Arrange        
            var ingredient = new Ingredient { Id = Guid.NewGuid(), Name = "Test ingredient" };
            var payload = JsonConvert.SerializeObject(ingredient);
            var request = new StringContent(payload, Encoding.UTF8, "application/json");

            // Act
            var createResponse = await client.PostAsync(BASE_URL, request);

            var getUrl = $"{BASE_URL}/{ingredient.Id}";
            var getResponse = await client.GetAsync(getUrl);
            var content = await getResponse.Content.ReadAsStringAsync();
            var retrievedIngredient = JsonConvert.DeserializeObject<Ingredient>(content);
            await client.DeleteAsync(getUrl);

            // Assert
            createResponse.Should().HaveStatusCode(HttpStatusCode.OK);
            retrievedIngredient.Should().BeEquivalentTo(ingredient, o => o.ComparingByMembers<Ingredient>());
        }

        [Test]
        public async Task Post_responds400WithMalformedIngredient()
        {
            // Arrange
            var ingredient = new { Id = Guid.NewGuid(), Foo = "bar" };
            var getUrl = $"{BASE_URL}/{ingredient.Id}";
            var payload = JsonConvert.SerializeObject(ingredient);
            var request = new StringContent(payload, Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync(BASE_URL, request);
            var getResponse = await client.GetAsync(getUrl);

            // Assert
            response.Should().HaveStatusCode(HttpStatusCode.BadRequest);
            getResponse.Should().HaveStatusCode(HttpStatusCode.NotFound);
        }

        [Test]
        public async Task Update_EditsIngredientName()
        {
            // Arrange
            lime.Name = "Tahitian Lime";
            var url = $"{BASE_URL}/{lime.Id}";
            var payload = JsonConvert.SerializeObject(lime);
            var request = new StringContent(payload, Encoding.UTF8, "application/json");

            // Act
            var response = await client.PutAsync(url, request);
            var getResponse = await client.GetAsync(url);
            var content = await getResponse.Content.ReadAsStringAsync();
            var retrievedIngredient = JsonConvert.DeserializeObject<Ingredient>(content);
           
            // Assert
            response.Should().HaveStatusCode(HttpStatusCode.OK);
            retrievedIngredient.Should().BeEquivalentTo(lime, o => o.ComparingByMembers<Ingredient>());

            lime.Name = "Lime";
            payload = JsonConvert.SerializeObject(lime);
            request = new StringContent(payload, Encoding.UTF8, "application/json");
            await client.PutAsync(url, request);
        }
        [Test]
        public async Task Update_responds404WithNonExistantId()
        {
            // Arrange
            var ingredient = new Ingredient { Id = Guid.NewGuid(), Name = "Fake" };
            var url = $"{BASE_URL}/{ingredient.Id}";
            var payload = JsonConvert.SerializeObject(ingredient);
            var request = new StringContent(payload, Encoding.UTF8, "application/json");

            // Act
            var response = await client.PutAsync(url, request);

            // Assert
            response.Should().HaveStatusCode(HttpStatusCode.NotFound);
        }
        [Test]
        public async Task Update_responds400WithMalformedIngredient()
        {
            // Arrange
            var ingredient = new { Id = lime.Id, Foo = "Bar" };
            var url = $"{BASE_URL}/{ingredient.Id}";
            var payload = JsonConvert.SerializeObject(ingredient);
            var request = new StringContent(payload, Encoding.UTF8, "application/json");
            // Act
            var response = await client.PutAsync(url, request);
            // Assert
            response.Should().HaveStatusCode(HttpStatusCode.BadRequest);
        }
        [Test]
        public async Task Delete_removesSpecifiedIngredient()
        {
            // Arrange
            var ingredient = new Ingredient { Id = Guid.NewGuid(), Name = "Test ingredient" };
            var payload = JsonConvert.SerializeObject(ingredient);
            var request = new StringContent(payload, Encoding.UTF8, "application/json");
            await client.PostAsync(BASE_URL, request);
            var count = await TestUtils.CountGetRequestContents(BASE_URL, client);

            var deleteUrl =$"{BASE_URL}/{ingredient.Id}";
            // Act
            var response = await client.DeleteAsync(deleteUrl);
            var getResponse = await client.GetAsync(deleteUrl);
            var postDeleteCount = await TestUtils.CountGetRequestContents(BASE_URL, client);

            // Assert
            response.Should().HaveStatusCode(HttpStatusCode.OK);
            getResponse.Should().HaveStatusCode(HttpStatusCode.NotFound);
            postDeleteCount.Should().Be(count - 1);
        }

        [Test]
        public async Task Delete_responds404WithNonExistantId()
        {
            // Arrange
            var ingredient = new Ingredient { Id = Guid.NewGuid(), Name = "Fake" };
            var count = await TestUtils.CountGetRequestContents(BASE_URL, client);
            var deleteUrl = $"{BASE_URL}/{ingredient.Id}";

            // Act
            var response = await client.DeleteAsync(deleteUrl);
            var postDeleteCount = await TestUtils.CountGetRequestContents(BASE_URL, client);

            // Assert
            response.Should().HaveStatusCode(HttpStatusCode.NotFound);
            postDeleteCount.Should().Be(count);
        }
    }
}
