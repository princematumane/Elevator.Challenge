
using Elevator.Challenge.Domain;
using Elevator.Challenge.Domain.Elevator;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;

namespace Elevator.Challenge.Tests.Domain.Building
{
    public class BuildingTests
    {
        private readonly List<Challenge.Domain.Elevator.Elevator> _elevators;
        private readonly IElevatorDispatcher _elevatorDispatcherMock;
        private readonly ILogger _loggerMock;

        public BuildingTests()
        {
           
            _elevatorDispatcherMock = Substitute.For<IElevatorDispatcher>();
            _loggerMock = Substitute.For<ILogger>();
            _elevators = new List<Challenge.Domain.Elevator.Elevator>();
        }

        [Fact]
        public void RequestElevator_Should_AddLoadIntoAnElevatorAndMove()
        {
            var elevatorConfig = new ElevatorConfiguration
            {
                TotalFloors = 10,
                ElevatorMaximumWeight = 100,
                ElevatorMaximumPassengers = 10,
                NumberOfPassengerElevator = 2,
                NumberOfFreightElevator = 1,
            };
            var mockOptions = Options.Create(elevatorConfig);

            var elevator = Substitute.For<Challenge.Domain.Elevator.Elevator>(1, 10, _loggerMock);
            var request = new ElevatorRequest(1, 6, 5, ElevatorType.Passenger);

            _elevatorDispatcherMock.AssignElevator(Arg.Any<List<Challenge.Domain.Elevator.Elevator>>(), Arg.Any<ElevatorRequest>())
              .Returns(elevator);

            var building = new Challenge.Domain.Building.Building(_elevatorDispatcherMock, _loggerMock, mockOptions);
            building.RequestElevator(request);

            elevator.Received().AddLoad(request.PassengerNumber);
            elevator.Received().MoveToFloorNumber(request.PickUpFloor,false);
            elevator.Received().MoveToFloorNumber(request.DestinationFloor,true);
            elevator.Received().Offload(request.PassengerNumber);
            elevator.Received().SetStationary(request.PassengerNumber);
        }


    }
}