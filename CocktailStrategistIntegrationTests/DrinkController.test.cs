using CocktailStrategist.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Text;
using System.Transactions;

namespace CocktailStrategistIntegrationTests
{
    [TestFixture, Rollback]
    public class DrinkControllerTests
    {
        private TransactionScope scope;

        [SetUp]
        public void SetUp()
        {
            scope = new TransactionScope();
            Console.WriteLine("starting scope");
        }

        [TearDown]
        public void TearDown()
        {
            scope.Dispose();
        }

        [Test]
        public async Task Get()
        {
            // Arrange
            var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
            });
            var client = factory.CreateClient();
            var url = "/drink";

            // Act

            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.That(content.Equals("value2"));
        }

        [Test]
        public async Task DrinkControllerCreatesNewDrink()
        {
                // Arrange
                var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
                {
                });
                var client = factory.CreateClient();
                var url = "/drink";

                var drink =  new Drink { Id = Guid.NewGuid(), Name = "Test drink" } ;
                var payload = JsonConvert.SerializeObject(drink);
                var request = new StringContent(payload, Encoding.UTF8, "application/json");

                // Act

                var response = await client.PostAsync(url, request);
                var content = await response.Content.ReadAsStringAsync();

                // Assert
                response.EnsureSuccessStatusCode();

                //Assert.That(content.Equals("value2"));
        }

        // drink with a non-existant ingredient
        //test
    }
}