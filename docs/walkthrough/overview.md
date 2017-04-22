# Stacks Walkthrough
For this walkthrough, we will look at the steps for implementing the Request Product business process
of Slalom Rentals, an online ski shop that rents and sells used skis.  This walkthrough isn't intended to be fully
production ready; it is meant to serve as a good structural example.  While not required if the
intent is to go through the action of building, it may be helpful to have some background
information on Service-Oriented Architecture and Microservices.  See learning paths.

### Part 1: Design
Here we will use the business requirements outlined in the Slalom Rentals case
study to create developer specifications using use cases
and service contracts.  See the [design overview](1.%20Design/overview.md).

### Part 2: Development
This is the biggest piece in this walkthrough.  Here we will take the use cases
and service contracts to:
1. Stub out methods and tests - [here](2.%20Development/1.%20stub%20methods%20and%20tests.md)
2. Use the document tool to validate against specifications - [here](2.%20Development/2.%20run%20document%20tool%20to%20validate.md)
3. Implement required functionality and tests
4. Check test reports and create additional reports

### Part 3: Deployment
Here we will deploy our service to an Azure API app and hit endpoints
using Postman.

### Part 4: Monitoring
In the monitor section, we will configure monitoring based on the business 
requirements and specifications - specifically for performance and goal KPIs.
