using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator.Challenge.Domain
{
    public class ElevatorConfiguration
    {
        public int TotalFloors { get; set; }
        public int ElevatorMaximumWeight { get; set; }

        public int ElevatorMaximumPassengers { get; set; }

        public int NumberOfPassengerElevator { get; set; }

        public int NumberOfFreightElevator { get; set; }
    }
}
