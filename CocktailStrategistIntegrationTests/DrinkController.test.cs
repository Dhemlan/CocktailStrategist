using CocktailStrategist.Data;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Transactions;

namespace CocktailStrategist.Tests.Integration
{
    [TestFixture]
    public class DrinkControllerTests
    {
        private readonly Guid MAI_TAI_ID = Guid.Parse("7fa85f64-5717-4562-b3fc-2c963f66afa6");
        private const string BASE_URL = "/drink";

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
        public void FixtureTearDown() { client.Dispose(); }
        //[SetUp]
        //public void TestSetUp()
        //{
        //    transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        //}
        //[TearDown]
        //public void TestTearDown() { transaction.Dispose();}

        [Test]
        public async Task Get_retrievesSpecifiedDrink()
        {
            // Arrange
            var drink = new Drink { Id = MAI_TAI_ID, Name = "Mai Tai" };
            var url = BASE_URL + $"/{drink.Id}";

            // Act
            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            var retrievedDrink = JsonConvert.DeserializeObject<Drink>(content);
            retrievedDrink.Should().BeEquivalentTo(drink, o => o.ComparingByMembers<Drink>());
        }

        [Test]
        public async Task Get_responds404WithNonExistantID()
        {
            // Arrange
            var drink = new Drink { Id = Guid.NewGuid(), Name = "Fake" };
            var url = BASE_URL + $"/{drink.Id}";

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.Should().HaveStatusCode(HttpStatusCode.NotFound);
        }

        [Test]
        public async Task Post_createsNewDrink()
        {
            // Arrange        
            var drink = new Drink { Id = Guid.NewGuid(), Name = "Test drink" };
            var payload = JsonConvert.SerializeObject(drink);
            var request = new StringContent(payload, Encoding.UTF8, "application/json");

            // Act
            var createResponse = await client.PostAsync(BASE_URL, request);

            var getUrl = BASE_URL + $"/{drink.Id}";
            var getResponse = await client.GetAsync(getUrl);
            var content = await getResponse.Content.ReadAsStringAsync();
            var retrievedDrink = JsonConvert.DeserializeObject<Drink>(content);
            await client.DeleteAsync(getUrl);

            // Assert
            createResponse.EnsureSuccessStatusCode();
            retrievedDrink.Should().BeEquivalentTo(drink, o => o.ComparingByMembers<Drink>());

        }

        [Test]
        public async Task Post_responds400WithMalformedDrink()
        {
            // Arrange
            var drink = new { Id = Guid.NewGuid(), Foo = "bar" };
            var getUrl = BASE_URL + $"/{drink.Id}";
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
        //public async Task Post_returns400WithDrinkContainingNonExistantIngredient()
        //{

        //}
        [Test]
        public async Task Put_UpdatesDrinkName()
        {
            // Arrange
            var drink = new Drink { Id = MAI_TAI_ID, Name = "Ultimate Mai Tai" };
            var url = BASE_URL + $"/{drink.Id}";
            var payload = JsonConvert.SerializeObject(drink);
            var request = new StringContent(payload, Encoding.UTF8, "application/json");
            
            var revert = new Drink { Id = MAI_TAI_ID, Name = "Mai Tai" };
            var revertPayload = JsonConvert.SerializeObject(revert);
            var revertRequest = new StringContent(revertPayload, Encoding.UTF8, "application/json");

            // Act
            var response = await client.PutAsync(url, request);
            var getResponse = await client.GetAsync(url);
            var content = await getResponse.Content.ReadAsStringAsync();
            var retrievedDrink = JsonConvert.DeserializeObject<Drink>(content);
            await client.PutAsync(url, revertRequest);

            // Assert
            response.EnsureSuccessStatusCode();
            retrievedDrink.Should().BeEquivalentTo(drink, o => o.ComparingByMembers<Drink>());
        }
        [Test]
        public async Task Put_responds404WithNonExistantId()
        {
            // Arrange
            var drink = new Drink { Id = Guid.NewGuid(), Name = "Fake" };
            var url = BASE_URL + $"/{drink.Id}";
            var payload = JsonConvert.SerializeObject(drink);
            var request = new StringContent(payload, Encoding.UTF8, "application/json");

            // Act
            var response = await client.PutAsync(url, request);

            // Assert
            response.Should().HaveStatusCode(HttpStatusCode.NotFound);
        }
        [Test]
        public async Task Put_responds400WithMalformedDrink()
        {
            // Arrange
            var drink = new { Id = MAI_TAI_ID, Foo = "Bar"};
            var url = BASE_URL + $"/{drink.Id}";
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
            var drink = new Drink { Id = Guid.NewGuid(), Name = "Test drink" };
            var payload = JsonConvert.SerializeObject(drink);
            var request = new StringContent(payload, Encoding.UTF8, "application/json");
            await client.PostAsync(BASE_URL, request);
            var count = await CountGetRequestContents(BASE_URL, client);

            var deleteUrl = BASE_URL + $"/{drink.Id}";
            // Act
            var response = await client.DeleteAsync(deleteUrl);
            var getResponse = await client.GetAsync(deleteUrl);
            var postDeleteCount = await CountGetRequestContents(BASE_URL, client);

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
            var count = await CountGetRequestContents(BASE_URL, client);
            var deleteUrl = BASE_URL + $"/{drink.Id}";

            // Act
            var response = await client.DeleteAsync(deleteUrl);
            var postDeleteCount = await CountGetRequestContents(BASE_URL, client);

            // Assert
            response.Should().HaveStatusCode(HttpStatusCode.NotFound);
            postDeleteCount.Should().Be(count);
        }

        private async Task<int?> CountGetRequestContents(string url, HttpClient client)
        {
            var getResponse = await client.GetAsync(url);
            var content = await getResponse.Content.ReadAsStringAsync();
            var retrievedList = JsonConvert.DeserializeObject<IEnumerable<Drink>>(content);
            return retrievedList?.Count();
        }
}
}