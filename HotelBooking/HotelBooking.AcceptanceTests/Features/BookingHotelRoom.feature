Feature: BookingHotelRoom

Scenario: No rooms available
	Given No rooms are available
	When I request for hotel reservation
	Then Operation fails
	And Booking fails
	And Abortion is logged
