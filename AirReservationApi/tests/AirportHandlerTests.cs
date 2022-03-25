using AutoBogus;
using NSubstitute;
using Xunit;
using AirReservationApi.Domain;
using AirReservationApi.handlers;
using System.Threading.Tasks;
using System.Linq;
using AirReservationApi.Infra.Repositiory;

namespace tests
{
    public class AirportHandlerTests
    {
        private readonly IAirportRepository airportRepository;
        private readonly AirReservationApi.Infra.Logging.ILogger logger;
        private readonly IAirportHandler handler;
        public AirportHandlerTests()
        {
            this.airportRepository = Substitute.For<IAirportRepository>();
            this.logger = Substitute.For<AirReservationApi.Infra.Logging.ILogger>();

            this.handler = new AirportHandler(this.airportRepository, this.logger);
        }

        [Fact]
        public async Task When_getting_all_airports_should_log_and_return_an_ordered_by_name_list()
        {
            //Arrange
            var airports = new AutoFaker<Airport>()
                .RuleFor(a => a.Code, "ABC")
                .Generate(10);

            var orderedAirports = airports.OrderBy(a => a.Name);

            this.airportRepository.GetAll().Returns(airports);

            //Act
            var result = await this.handler.GetAllAirports();

            //Assert
            this.logger.Received(1).Information(Arg.Any<string>(), Arg.Any<object[]>());
            Assert.Equal(result, orderedAirports);
        }

        [Fact]
        public async Task When_inserting_an_new_airport_given_that_the_format_is_invalid_should_not_call_repository_and_should_log_warning()
        {
            //Arrange
            var airport = new AutoFaker<Airport>()
                .RuleFor(a => a.Code, "ABCD");

            //Act
            var result = await this.handler.InsertOneAirport(airport);
            //Assert

            this.logger.Received(1).Warning(Arg.Any<string>(), Arg.Any<object[]>());
            this.airportRepository.DidNotReceive().InsertOne(Arg.Any<Airport>());
            Assert.Equal(result, "Invalid airport format");
        }

        [Fact]
        public async Task When_inserting_an_new_airport_given_that_the_DB_response_is_success_should_log_and_return_success()
        {
            //Arrange
            var airport = new AutoFaker<Airport>()
                .RuleFor(a => a.Code, "ABC");

            //Act
            var result = await this.handler.InsertOneAirport(airport);

            //Assert
            this.logger.Received(1).Information(Arg.Any<string>());
            Assert.Equal(result, "Airport inserted successfully");
        }

        [Fact]
        public async Task When_removing_an_airport_given_that_removing_fails_should_log_and_return_fail()
        {
            //Arrange
            this.airportRepository.Remove(Arg.Any<string>()).Returns(false);

            //Act
            var result = await this.handler.RemoveOneAirport(Arg.Any<string>());

            //Assert
            this.logger.Received(1).Error(Arg.Any<string>());
            Assert.Equal(result, "Error while deleting airport");
        }

        [Fact]
        public async Task When_removing_an_airport_given_that_removing_is_successful_should_log_and_return_success()
        {
            //Arrange
            this.airportRepository.Remove(Arg.Any<string>()).Returns(true);

            //Act
            var result = await this.handler.RemoveOneAirport("anyCode");

            //Assert
            // this.logger.Received(1).Information(Arg.Any<string>(), Arg.Any<object[]>());
            Assert.Equal(result, "Airport deleted successfully");
        }

        [Fact]
        public async Task When_updating_airpot_given_that_format_is_invalid_should_log_warning_and_return()
        {
            //Arrange
            var airport = new AutoFaker<Airport>()
                .RuleFor(a => a.Code, "ABCD");

            //Act   
            var result = await this.handler.Update("AnyCode", airport);

            //Assert
            this.logger.Received(1).Warning(Arg.Any<string>());
            Assert.Equal(result, "Invalid airport format");
        }


        [Fact]
        public async Task When_updating_airpot_given_that_format_is_valid_and_insert_fails_should_log_error_and_return()
        {
            //Arrange
            var airport = new AutoFaker<Airport>()
                .RuleFor(a => a.Code, "ABC");

            this.airportRepository.Update(Arg.Any<string>(), airport).Returns(false);

            //Act   
            var result = await this.handler.Update("AnyCode", airport);

            //Assert
            this.logger.Received(1).Error(Arg.Any<string>());
            Assert.Equal(result, "Error while updating airport");
        }
    }
}