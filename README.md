# Documentation of API-endpoints

## Customers
* GET/api/Customers/getAllCustomers: Will list all existing customers.
* POST/api/Customers/addCustomer: Will add a new customer, when filling in required fields.
* GET/api/Customers/getCustomer/{customerId}: Will find a specific customer and their info, when inputting customer ID.
* GET/api/Customers/getCustomerByPhoneNo/{phoneNo}: Will also find a specific customer and their info, when inputting a customer's phone number.
* PUT/api/Customers/updateCustomer/{customerId}: Will update a customer, when inputting their customer ID and filling in available fields.
* DELETE/api/Customers/deleteCustomer/{customerId}: Will delete a certain user, when inputting their customer ID.

## Menu
* GET/api/Menu/getAllDishes: Will list all existing dishes.
* POST/api/Menu/addDish: Will add a new dish, when filling in required fields.
* GET/api/Menu/getDish/{dishId}: Will get a certain dish's info, when inputting dish ID.
* GET/api/Menu/getDishByName/{name}: Will get a certain dish's info, when inputting dish name.
* PUT/api/Menu/updateDish/{dishId}: Will update dish info, when inputting dish ID and filling in available fields.
* DELETE/api/Menu/deleteDish/{dishId}: Will delete a certain dish, when inputting its ID.

## Reservations
* GET/api/Reservations/getAllReservations: Will list all existing reservations.
* GET/api/Reservations/getReservation/{reservationId}: Will fetch a certain reservations info, when inputting reservation ID.
* PUT/api/Reservations/updateReservation/{reservationId}: Will update reservation info, when inputting reservation ID and filling in available fields.
* DELETE/api/Reservations/deleteReservation/{reservationId}: Will delete a certain reservation, when inputting its ID.
* GET/api/Reservations/getAvailableTablesForReservation: Will return all available tables for reservation - taking seats and time schedule into consideration.
* POST/api/Reservations/makeReservation: Will allow one to make a reservation.

## Tables
* GET/api/Tables/getAllTables: Will list all existing tables.
* POST/api/Tables/addTable: Will add a new table, when filling in required fields.
* GET/api/Tables/getTable/{tableId}: Will fetch a certain table's info, when inputting table ID.
* PUT/api/Tables/updateTable/{tableId}: Will update a table's info, when inputting table ID and filling in available fields.
* DELETE/api/Tables/deleteTable/{tableId}: Will delete a certain table, when inputting its ID.

  ## Initial version of ER-diagram:
<img width="595" alt="InitialERdiagram" src="https://github.com/user-attachments/assets/4ef5127e-a8c4-46a0-8120-1b00002482ed">
