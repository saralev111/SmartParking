Parking Management System
Project Overview

This is a server-side project built with ASP.NET Core, designed to manage a parking lot.
The system allows managing parking lots, parking spots, vehicles, and calculating payments based on the duration of the parking.

Project Description

The system simulates a real-world parking lot.
When a car enters, it is registered in the system and assigned a parking spot.
When it exits, the system calculates the payment according to the time spent.

The system includes three main entities:

Parking – represents the parking lot.

Spot – represents an individual parking spot.

Car – represents the vehicle.

Database Structure
1. Parking
Column    Description
id    Unique identifier for the parking lot
name    Parking lot name
location    Address or location
total_spots    Total number of parking spots
available_spots    Number of available spots
price_per_hour    Parking fee per hour
2. Spot
Column    Description
id    Unique identifier for the spot
parking_id    Foreign key referencing Parking
spot_number    Parking spot number
is_occupied    Indicates whether the spot is occupied
vehicle_license_num    Foreign key referencing Car
3. Car
Column    Description
license_num (PK)    Car license number (unique identifier)
owner_name    Full name of the car owner
entry_time    Time of entry
exit_time    Time of exit
total_payment    Total payment for the parking session
Main API Endpoints
Method    Route    Description
GET    /api/parking    Get all parking lots
POST    /api/parking    Add a new parking lot
GET    /api/spots    Retrieve all parking spots
POST    /api/cars/enter    Register a car entry
POST    /api/cars/exit    Register a car exit and calculate payment
GET    /api/logs    Retrieve parking logs
Business Logic

Car Entry:
The system finds the first available spot, marks it as occupied, and stores the car’s entry time.

Car Exit:
The system calculates the total time spent, computes the payment according to the hourly rate, and frees the parking spot.

Technologies Used:

1. ASP.NET Core 8.0

2. Entity Framework Core

3. SQL Server

4. Swagger
