Feature: PaymentFeature
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

Scenario: Failed reservation - booking with not authorized payment
	Given the customer credit card is not authorized
	When I request for hotel reservation
	Then Booking result includes no operations results
	And Booking result has failure overall result