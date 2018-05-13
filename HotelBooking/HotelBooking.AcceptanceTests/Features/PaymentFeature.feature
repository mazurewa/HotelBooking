Feature: PaymentFeature

Scenario: Booking with not authorized payment
	Given the customer credit card is not authorized
	When I request for hotel reservation
	Then Operation fails
	And Booking fails