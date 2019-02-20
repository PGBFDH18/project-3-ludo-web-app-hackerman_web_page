using System;
using Xunit;
using Hackerman_WebbApp.Controllers;
using RestSharp;
using Moq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hackerman_WebbApp;

namespace ControllerTest
{
    public class UnitTest1
    {
        [Fact]
        public async Task ReadListOfGamesAsync()
        {
            // Arrange

            RestClient client = new RestClient();
            PlayerCounter counter = new PlayerCounter();

            var controller = new LudoController(client, counter);


            // Act

            IActionResult result = await controller.ListGames();

            // Assert

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);



        }
    }
}
