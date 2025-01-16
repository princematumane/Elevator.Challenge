# Elevator-Challenge

This application simulates the movement of elevators in a large building, aiming to optimize passenger transportation efficiently. Users interact with a console interface to request an elevator. They can select between two types of elevators:

Passenger Elevator
Freight Elevator
Users will input the following details:

Pick-up floor
Destination floor
Number of passengers waiting on each floor
The application follows Clean Architecture principles, structured into the following layers:

Presentation Layer: Manages user interactions and displays information.
Domain Layer: Contains core business logic and domain entities.
Application Layer: Coordinates the use cases and application flow.
Infrastructure Layer: Handles data storage, external services, and system integration.
This design ensures scalability, maintainability, and separation of concerns.

## Usage

Step-by-step instructions on how to use the application.

```bash
# Clone the repository
git clone [https://github.com/princematumane/Elevator.Challenge.git]

# Open project with Visual Studio or an IDE of your choice
Set Elevator.Challenge.Presentation as Start Up

To customize the app:

Open appsettings.json in the Elevator.Challenge.Presentation root folder.

Find the "ElevatorConfiguration" section:

{
  "ElevatorConfiguration": {
    "TotalElevators": 10,
    "TotalFloors": 10,
    "ElevatorMaximumWeight": 100,
    "ElevatorMaximumPassengers": 10,
    "NumberOfPassengerElevator": 2,
    "NumberOfFreightElevator": 1
  }
}

```
