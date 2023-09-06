# Store App

## Description

The Store App is an application that allows customers and managers to manage an online store.  
****

## Features  

### Manager:  
* Can view, add, update, and delete products.  
* Can track orders and update their status.  

### Customer:
* Can view their orders.
* Can create new orders.

### Simulation

The Store App includes a simulation feature that simulates the order processing system.   
Every random interval of seconds, the system takes the order that has been waiting the longest and processes it, advancing its status from ORDERED to SHIPPED or from SHIPPED to DELIVERED.

The simulation is a helpful tool for testing the performance of the order processing system. It can also be used to evaluate different order processing strategies.


****
## Technologies

* Languages: C#, WPF, XML  
* Architecture: 3-tier model  
* Design patterns: FACTORY, OBSERVER, SINGLETON, etc.  
* Parallelism: THREADS  
* LINQ queries   

****

## Installation instructions  

* Download the code from the archive.  
* Install Visual Studio 2022 or later.  
* Open the project in Visual Studio.  
* Run the project.  
* Usage instructions  

### Manager:  
- To log in to your manager account, click the "Admin" button and enter your username and password.  
- To add a product, click the "Add Product" button.  
- To update a product, click the product you want to update.  
- To delete a product, click the product you want to delete.  
- To track orders, click the "Orders" button.  
- To update the status of an order, click the order you want to update.
   
### Customer:  
 
- To view your orders, click the "Orders" button, and enter your order ID.
- To create a new order, click the "New Order" button.
- To add a product to an order, click the product you want to add.
- To update the quantity of a product in an order, click the product you want to update.
- To remove a product from an order, click the product you want to remove.
- To submit an order, click the "Submit Order" button.

### Simulation:
- To start the simulation, click the "Start Simulation" button in the landing page.
- To stop The simulation, click the "Stop Simulation" button in the simulation page.
- If the simulation system finish to pocess the all orders, this page sill closed by default.

***
  

Thank you for using our Store App!
