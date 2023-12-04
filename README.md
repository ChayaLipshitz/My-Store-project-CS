# Store App

## Description

The Store App is an application that allows customers and managers to manage an online store.  
     
****

## Features  

### Manager   
* Can view, add, update, and delete products.  
* Can track orders and update their status.  

### Customer 
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

## Installation and usage instructions  

* Download the code from the archive.  
* Install Visual Studio 2022 or later.  
* Open the project in Visual Studio.  
* Run the project.  
* Usage instructions  
  
### Manager  
- To log in to your manager account, click the "Admin" button and enter your username and password.  
- To add a product, click the "Add Product" button.  
- To update a product, click the product you want to update.  
- To delete a product, click the product you want to delete.  
- To track orders, click the "Orders" button.  
- To update the status of an order, click the order you want to update.
     
### Customer 
   
- To view your orders, click the "Orders" button, and enter your order ID.
- To create a new order, click the "New Order" button.
- To add a product to an order, click the product you want to add.
- To update the quantity of a product in an order, click the product you want to update.
- To remove a product from an order, click the product you want to remove.
- To submit an order, click the "Submit Order" button.
  
### Simulation 
- To start the simulation, click the "Start Simulation" button in the landing page.
- To stop The simulation, click the "Stop Simulation" button in the simulation page.
- If the simulation system finish to process all the orders, this page will closed by default.
      
***
  
## Screen shots
#### Landing page 
![image](https://github.com/ChayaLipshitz/My-Store-project-CS/assets/113462696/e89fa222-07b8-450f-9dd7-fe82bbc028f7)

#### Product catalog 
![image](https://github.com/ChayaLipshitz/My-Store-project-CS/assets/113462696/ff7d6afa-ff44-4fc2-a56d-f248837d2e62)

#### Cart page 
![image](https://github.com/ChayaLipshitz/My-Store-project-CS/assets/113462696/9ba525bf-66f6-48f3-ab71-2d7b4a0b28a5)

#### New product 
![image](https://github.com/ChayaLipshitz/My-Store-project-CS/assets/113462696/3250bbdb-b8cd-4dfa-819f-26d9ef8152f2)

#### Order window
![image](https://github.com/ChayaLipshitz/My-Store-project-CS/assets/113462696/a2fc72f9-1c5c-43e8-b0f0-8ce6bc19efad)

#### Simulation
![image](https://github.com/ChayaLipshitz/My-Store-project-CS/assets/113462696/cc32c98d-7752-4091-952f-cb3d270356e4)


Thank you for using our Store App!
