Feature: ProduceBookingResult

Scenario: Successful reservation
	Given the hotel is in database
	When I request for hotel reservation
	Then Booking result includes all opeartions results
	And Booking result has success overall result

Scenario: Failed reservation - hotel is not in database
	Given the hotel is not in database
	When I request for hotel reservation
	Then Booking result includes no operations results
	And Booking result has failure overall result

Scenario: Failed reservation - email is in invalid format
	Given the email address input is in invalid format
	When I request for hotel reservation
	Then Booking result includes no operations results
	And Booking result has failure overall result
	
Scenario: Failed reservation - hotel does not have all required operations included
	Given the hotel does not have all required operations included
	When I request for hotel reservation
	Then Booking result includes no operations results
	And Booking result has failure overall result