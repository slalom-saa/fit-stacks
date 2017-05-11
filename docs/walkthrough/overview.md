# Stacks Walkthrough
For this walkthrough, we will look at the steps for implementing the **Request Product** business process
of **Slalom Rentals**, an online ski shop that rents and sells used skis.  This walkthrough isn't intended to be fully
production ready; it is meant to serve as a good starting point.  While not required, it may be good to have some background
in [Service-Oriented Architecture](https://www.safaribooksonline.com/library/view/service-oriented-architecture-analysis/9780133858709/).  This 
will help in undestanding [service architectures](https://www.safaribooksonline.com/library/view/service-oriented-architecture-analysis/9780133858709/ch04.html#ch04lev2sec6), 
[service layers](https://www.safaribooksonline.com/library/view/service-oriented-architecture-analysis/9780133858709/ch05.html) and
the [design process](https://www.safaribooksonline.com/library/view/Service-Oriented+Architecture:+Analysis+and+Design+for+Services+and+Microservices,+Second+Edition/9780133858709/ch06.html#ch06).

### Part 1: Design
Here we will not build out the requirements, but we will review the expected developer
specification in order to continue with our development.  See [Design Overview](1.%20Design/overview.md).

### Part 2: Development
This section is the largest in this walkthrough.  Here we will take the use cases
and service contracts to:
1. Stub out methods and tests - [readme](2.%20Development/1.%20stub%20methods%20and%20tests.md)
2. Use the document tool to validate against specifications - [readme](2.%20Development/2.%20run%20document%20tool%20to%20validate.md)
3. Implement required functionality and tests
4. Check test reports and create additional reports

### Part 3: Deployment
Here we will deploy our service to an Azure API app and hit endpoints
using Postman.

### Part 4: Monitoring
In the monitor section, we will configure monitoring based on the business 
requirements and specifications - specifically for performance and goal KPIs.
