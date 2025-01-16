
using Elevator.Challenge.Domain.Elevator;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Elevator.Challenge.Domain.Building
{
    public class Building
    {
        private readonly ElevatorConfiguration _config;

        private readonly List<Elevator.Elevator> _elevators = new();
        private readonly IElevatorDispatcher _elevatorDispatcher;
        private readonly ILogger _logger;

        public int TotalFloors { get; private set; }
        public int TotalElevators { get; private set; }

        public Building(IElevatorDispatcher elevatorDispatcher, ILogger logger, IOptions<ElevatorConfiguration> options)
        {
            _config = options.Value;
            _logger = logger;
            AddElevators();
            TotalFloors = _config.TotalFloors;
            TotalElevators = _elevators.Count;
           
            _elevatorDispatcher = elevatorDispatcher;
        }

        private void AddElevators()
        {
            for (int i = 1; i <= _config.NumberOfPassengerElevator; i++) {
                _elevators.Add(new PassengerElevator(i, _config.ElevatorMaximumPassengers, _logger));
            }

            for (int i = _config.NumberOfPassengerElevator; i <= _config.NumberOfPassengerElevator + _config.NumberOfFreightElevator; i++)
            {
                _elevators.Add(new FreightElevator(3, _config.ElevatorMaximumWeight, _logger));
            }

        }

        public void RequestElevator(ElevatorRequest request)
        {
            _logger.LogInformation("RequestElevator() Invoked");
            try
            {
                var elevator = _elevatorDispatcher.AssignElevator(_elevators, request);
                if (elevator != null)
                {
                    var status = elevator.Status == ElevatorStatus.Moving ? " Moving from" : "Stationery at" ;
                    Console.WriteLine($"\nFloor selection registered, Elevator Id {elevator.Id} {status} floor {elevator.CurrentFloor}");
                    Console.WriteLine($"");
                    elevator.AddLoad(request.PassengerNumber);
                    elevator.MoveToFloorNumber(request.PickUpFloor,false);

                    elevator.MoveToFloorNumber(request.DestinationFloor,true);

                    elevator.Offload(request.PassengerNumber);
                    elevator.SetStationary(request.PassengerNumber);
                }
                else
                {
                    Console.WriteLine("No available elevators at the moment.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ERROR at {typeof(Building)} RequestElevator() {ex.Message}",ex.InnerException);
            }
        }

        public void ShowElevatorStatus()
        {
            _logger.LogInformation("ShowElevatorStatus() Invoked");
            try
            {
                Console.WriteLine("\n ID  Type        Floor      Status        Direction         Capacity");
                Console.WriteLine("--------------------------------------------------------------------------");
                foreach (var elevator in _elevators)
                {
                    Console.WriteLine(elevator);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ERROR at {typeof(Building)} ShowElevatorStatus() {ex.Message}", ex.InnerException);
            }
        }
    }
}
