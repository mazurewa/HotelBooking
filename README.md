# HotelBooking
Implementation of hotel reservation booking module

# Description

Module enables processing of operations in the order given by selected hotel. Operation execution can result in success, warning or failure, with the latter aborting the booking process.

Some operations would require integration with external systems, however this solution does not include it. Integration with external system is stubbed.

Take a look at acceptance tests written using SpecFlow how the module works and how to use it.

HotelBooking.App is a console application, which calls booking module. Implementation of ILogger interface needs to be passed to the module.