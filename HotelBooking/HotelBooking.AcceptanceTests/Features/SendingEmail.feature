@email
Feature: SendingEmail

Scenario: Email is in invalid format
	Given the email address input is in invalid format
	When I request for hotel reservation
	Then Booking result includes no operations results
	And Booking result has failure overall result

Scenario: Email is not delivered
	Given the email has not been delivered
	When I request for hotel reservation
	Then Operations succeeds with warning
	And Booking succeeds
	And Warning is logged