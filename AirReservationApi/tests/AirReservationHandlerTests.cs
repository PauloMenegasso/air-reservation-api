using AutoBogus;
using NSubstitute;
using Xunit;
using AirReservationApi.Domain;
using AirReservationApi.handlers;
using System.Threading.Tasks;
using System.Linq;
using System;
using AirReservationApi.Infra.Repositiory;

namespace tests
{
    public class AirReservationHandlerTests
    {
        private readonly IAirReservationRepository airReservationRepository;
        private readonly AirReservationApi.Infra.Logging.ILogger logger;
        private readonly IAirReservationHandler handler;

        public AirReservationHandlerTests()
        {
            this.airReservationRepository = Substitute.For<IAirReservationRepository>();
            this.logger = Substitute.For<AirReservationApi.Infra.Logging.ILogger>();
            this.handler = new AirReservationHandler(this.airReservationRepository, this.logger);
        }

        [Fact]
        public async Task When_tring_to_insert_an_invalid_air_reservation_should_return_error_message()
        {
            //Arrange
            var airReservation = MockInvalidAirReservation();

            //Act
            var result = await this.handler.InsertAirReservation(airReservation);

            //Assert
            Assert.Equal(result, "Invalid air reservation");
        }

        [Fact]
        public async Task When_tring_to_insert_an_valid_air_reservation_and_success_should_return_error_message()
        {
            //Arrange
            var airReservation = MockValidAirReservation();

            //Act
            var result = await this.handler.InsertAirReservation(airReservation);

            //Assert
            this.logger.Received(1).Information(Arg.Any<string>(), Arg.Any<object[]>());
            await this.airReservationRepository.Received(1).SaveChanges();
            await this.airReservationRepository.Received(1).Add<AirReservation>(Arg.Any<AirReservation>());
            Assert.Equal(result, "Air reservation inserted!");
        }

        [Fact]
        public async Task When_removing_air_reservation_given_that_it_fails_should_log_and_return_error()
        {
            //Arrange
            this.airReservationRepository.RemoveOne(Arg.Any<int>()).Returns(false);

            //Act
            var result = await this.handler.RemoveReservation(Arg.Any<int>());

            //Assert
            this.logger.Received(1).Error(Arg.Any<string>());
            Assert.Equal(result, "Error while removing air reservation");
        }

        [Fact]
        public async Task When_removing_air_reservation_given_that_it_succeds_should_log_and_return_success()
        {
            //Arrange
            this.airReservationRepository.RemoveOne(1).Returns(true);

            //Act
            var result = await this.handler.RemoveReservation(1);

            //Assert
            Assert.Equal(result, "Removed with success");
        }

        [Fact]
        public async Task When_searching_a_specific_reservation_given_that_it_is_not_found_should_log_Warning()
        {
            //Arrange > Act
            var result = await this.handler.GetAnAirReservation(1);

            //Assert
            this.logger.Received(1).Warning(Arg.Any<string>());
        }

        [Fact]
        public async Task When_searching_a_specific_reservation_given_that_it_is_found_should_log_Information_and_return_the_air_reservation()
        {
            //Arrange 
            var airReservation = MockValidAirReservation();

            this.airReservationRepository.GetOne<AirReservation>(1).Returns(airReservation);
            // Act
            var result = await this.handler.GetAnAirReservation(1);

            //Assert
            this.logger.Received(1).Information(Arg.Any<string>(), airReservation);
            Assert.Equal(result, airReservation);
        }

        private AirReservation MockInvalidAirReservation()
            => new AutoFaker<AirReservation>()
                .RuleFor(a => a.NumberOfAdultPassagers, -1);

        private AirReservation MockValidAirReservation()
        {
            var ARandomDate = new DateTime(2022, 03, 24, 01, 00, 00);

            return new AutoFaker<AirReservation>()
                .RuleFor(a => a.ArrivalDatetime, ARandomDate)
                .RuleFor(a => a.DepartureDatetime, ARandomDate.AddDays(-1))
                .RuleFor(a => a.FlightNumber, 1231)
                .RuleFor(a => a.NumberOfAdultPassagers, 1);
        }

    }
}