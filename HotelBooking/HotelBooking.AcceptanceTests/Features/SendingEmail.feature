Feature: SendingEmail

Scenario: Failed reservation - email is in invalid format
	Given the email address input is in invalid format
	When I request for hotel reservation
	Then Booking result includes no operations results
	And Booking result has failure overall result