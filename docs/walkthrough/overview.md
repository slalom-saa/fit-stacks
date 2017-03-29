# Stacks Walkthrough
For this walkthrough we will look at the steps for implementing the Make Reservation feature
of Slalom Rentals.

### Design
Here we will use the business reqiurements to create developer specifications using use cases
and service contracts.  

### Develop
First, we will stub out all the needed code to validate against the design.  Then,
we will add the implementation details and used our stubbed tests.

### Deploy
Here we will deploy our service to an Azure API app and hit its endpoints
using Postman.

### Monitor
In the monitor section, we will configure monitoring based on the business 
requirements and specifications.

### Version or Retire




## Release Phases
Every agile project has three basic build phases per release and each of those
phases has a focus: velocity, accuracy, optimization.  You may have
heard this in other ways like make it work, make it right, make it fast.

### Make it Work
The goal of this phase is to get working pieces of software out in
front of focus groups as soon as possible.  This doesn't mean that 
requirements should not be provided, but the requirements may be
lean at this stage and churn is expected.

### Make it Right
By the time you get to this phase, you should have received enough feedback
and have implemented that feedback.  You will have a code base
with plenty of tech-debt and bugs.  The goal of the phase is to make sure that
most of the tech-debt is paid off and all (ideally) accuracy related bugs are fixed.

### Make it Fast
This phase is all about architecture optimization.  The previous phases should have
produced modular pieces of implementation that can be...