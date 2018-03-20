# HotelBooking
Implementation of hotel reservation booking module

# Description

HotelBooking is a console application written in C#. 

Module enables processing of operations in the order specific for particular hotel. Hotel can be configured to not include all available operations, however all required operations must be included to process reservation successfully. Failure of not required operation does not abort the process.

Some operations would require integration with external systems, however this solution does not include it. The external systems are faked.

Take a look at acceptance tests written using SpecFlow how the module works and how to use it.