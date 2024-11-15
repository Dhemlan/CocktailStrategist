using CocktailStrategist.Data;
using CocktailStrategist.Data.CreateRequestObjects;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;
using System.Resources;
using System.Text;
using System.Transactions;

namespace CocktailStrategist.Tests.Integration
{
    [TestFixture]
    public class DrinkControllerTests
    {
        private const string BASE_URL = "/drink";
        // Mutating tests will be performed on maiTai but not englishGarden
        private Drink maiTai;
        private readonly Drink englishGarden = new Drink
        {
            Id = Guid.Parse("5f3f5b3d-33a6-4021-9439-51919350d1da"),
            Name = "English Garden",
            //IngredientList = new List<Guid> { Guid.Parse("57295ca5-3e94-403e-acb7-01417ed03c4d") }
        };

        private HttpClient client;
        //private TransactionScope transaction;

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
        [SetUp]
        public void TestSetUp()
        {
            maiTai = new Drink
            {
                Id = Guid.Parse("83174492-bed9-4f09-8a01-c735947eff21"),
                Name = "Mai Tai",
                //IngredientList = new List<Guid> { Guid.Parse("e6db0f8c-43aa-4eed-8e8c-f6cf20615f4b"), Guid.Parse("57295ca5-3e94-403e-acb7-01417ed03c4d") }
            };

        }
        //[TearDown]
        //public void TestTearDown() { transaction.Dispose();}

        [Test]
        public async Task Get_retrievesSpecifiedDrink()
        {
            // Arrange
            var url = $"{BASE_URL}/{maiTai.Id}";

            // Act
            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            response.Should().HaveStatusCode(HttpStatusCode.OK);
            var retrievedDrink = JsonConvert.DeserializeObject<Drink>(content);
            retrievedDrink.Should().BeEquivalentTo(maiTai, o => o.ComparingByMembers<Drink>()); //.Excluding(d => d.IngredientList));
        }

        [Test]
        public async Task Get_responds404WithNonExistantID()
        {
            // Arrange
            var drink = new Drink { Id = Guid.NewGuid(), Name = "Fake" };
            var url = $"{BASE_URL}/{drink.Id}";

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.Should().HaveStatusCode(HttpStatusCode.NotFound);
        }

        [Test]
        public async Task Get_retrievesAllDrinks()
        {
            // Arrange
            var drinks = new List<Drink> { maiTai, englishGarden};

            // Act
            var response = await client.GetAsync(BASE_URL);
            var content = await response.Content.ReadAsStringAsync();
            var retrievedDrinks = JsonConvert.DeserializeObject<List<Drink>>(content);
            
            //Assert
            response.Should().HaveStatusCode(HttpStatusCode.OK);
            retrievedDrinks.Should().BeEquivalentTo(drinks);
        }

        [Test]
        public async Task Post_createsNewDrink()
        {
            // Arrange        
            var drinkRequest = new CreateDrinkRequest {Name = "Test drink", Ingredients = new List<Guid>() };
            var payload = JsonConvert.SerializeObject(drinkRequest);
            var request = new StringContent(payload, Encoding.UTF8, "application/json");

            // Act
            var createResponse = await client.PostAsync(BASE_URL, request);
            var getResponse = await client.GetAsync(BASE_URL);
            var content = await getResponse.Content.ReadAsStringAsync();
            var retrievedDrinks = JsonConvert.DeserializeObject<List<Drink>>(content);
            var retrievedDrink = retrievedDrinks?.First(d => d.Name.Equals(drinkRequest.Name));
            var deleteUrl = $"{BASE_URL}/{retrievedDrink!.Id}";
            await client.DeleteAsync(deleteUrl);

            // Assert
            createResponse.Should().HaveStatusCode(HttpStatusCode.OK);
           // retrievedDrink.Name.Should().BeSame

        }

        [Test]
        public async Task Post_responds400WithMalformedDrink()
        {
            // Arrange
            var drink = new { Id = Guid.NewGuid(), Foo = "bar" };
            var getUrl = $"{BASE_URL}/{drink.Id}";
            var payload = JsonConvert.SerializeObject(drink);
            var request = new StringContent(payload, Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync(BASE_URL, request);
            var getResponse = await client.GetAsync(getUrl);

            // Assert
            response.Should().HaveStatusCode(HttpStatusCode.BadRequest);
            getResponse.Should().HaveStatusCode(HttpStatusCode.NotFound);
        }

        //[Test]
        // error on same name for drink

        [Test]
        public async Task Post_returns400WithDrinkContainingNonExistantIngredient()
        {
            // Arrange
            var drinkRequest = new CreateDrinkRequest { Name = "Test Drink", 
                Ingredients = new List<Guid> { Guid.NewGuid() } };
            var payload = JsonConvert.SerializeObject(drinkRequest);
            var request = new StringContent(payload, Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync(BASE_URL, request);

            // Assert
            response.Should().HaveStatusCode(HttpStatusCode.BadRequest);
        }
        [Test]
        public async Task Update_EditsDrinkName()
        {
            // Arrange
            var url = $"{BASE_URL}/{maiTai.Id}";
            
            maiTai.Name = "Ultimate Mai Tai";
            var payload = JsonConvert.SerializeObject(maiTai);
            var request = new StringContent(payload, Encoding.UTF8, "application/json");

            // Act
            var response = await client.PutAsync(url, request);
            var getResponse = await client.GetAsync(url);
            var content = await getResponse.Content.ReadAsStringAsync();
            var retrievedDrink = JsonConvert.DeserializeObject<Drink>(content);
            
            // Assert
            response.Should().HaveStatusCode(HttpStatusCode.OK);
            retrievedDrink.Should().BeEquivalentTo(maiTai, o => o.ComparingByMembers<Drink>());

            maiTai.Name = "Mai Tai";
            var revertPayload = JsonConvert.SerializeObject(maiTai);
            var revertRequest = new StringContent(revertPayload, Encoding.UTF8, "application/json");
            await client.PutAsync(url, revertRequest);
        }
        [Test]
        public async Task Update_responds404WithNonExistantId()
        {
            // Arrange
            var drink = new Drink { Id = Guid.NewGuid(), Name = "Fake" };
            var url = $"{BASE_URL}/{drink.Id}";
            var payload = JsonConvert.SerializeObject(drink);
            var request = new StringContent(payload, Encoding.UTF8, "application/json");

            // Act
            var response = await client.PutAsync(url, request);

            // Assert
            response.Should().HaveStatusCode(HttpStatusCode.NotFound);
        }
        [Test]
        public async Task Update_responds400WithMalformedDrink()
        {
            // Arrange
            var drink = new { Id = maiTai.Id, Foo = "Bar"};
            var url = $"{BASE_URL}/{maiTai.Id}";
            var payload = JsonConvert.SerializeObject(drink);
            var request = new StringContent(payload, Encoding.UTF8, "application/json");
            // Act
            var response = await client.PutAsync(url, request);
            // Assert
            response.Should().HaveStatusCode(HttpStatusCode.BadRequest);
        }
        [Test]
        public async Task Delete_removesSpecifiedDrink()
        {
            // Arrange
            var drinkRequest = new CreateDrinkRequest { Name = "Test drink", Ingredients = new List<Guid>() };
            var payload = JsonConvert.SerializeObject(drinkRequest);
            var request = new StringContent(payload, Encoding.UTF8, "application/json");
            await client.PostAsync(BASE_URL, request);

            var getResponse = await client.GetAsync(BASE_URL);
            var content = await getResponse.Content.ReadAsStringAsync();
            var retrievedDrinks = JsonConvert.DeserializeObject<List<Drink>>(content);
            var retrievedDrink = retrievedDrinks?.First(d => d.Name.Equals(drinkRequest.Name));

            var deleteUrl = $"{BASE_URL}/{retrievedDrink!.Id}";
            var count = await TestUtils.CountGetRequestContents(BASE_URL, client);

            // Act
            var response = await client.DeleteAsync(deleteUrl);
            getResponse = await client.GetAsync(deleteUrl);
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
            var drink = new Drink { Id = Guid.NewGuid(), Name = "Fake" };
            var count = await TestUtils.CountGetRequestContents(BASE_URL, client);
            var deleteUrl = $"{BASE_URL}/{drink.Id}";

            // Act
            var response = await client.DeleteAsync(deleteUrl);
            var postDeleteCount = await TestUtils.CountGetRequestContents(BASE_URL, client);

            // Assert
            response.Should().HaveStatusCode(HttpStatusCode.NotFound);
            postDeleteCount.Should().Be(count);
        }
    }
}