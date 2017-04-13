# Add Product Use Case

### Name
Add Product

### Actor
Product Approver

### Bounded Context
Catalog

### Summary
Adds a product to the product catalog so that a user can search 
for it and it can be added to a cart, rented, purchased and shipped.

### Input

| Name        | Description                              | 
| ----------- | ---------------------------------------- | 
| Name        | The name of the product to add.          | 
| Description | An optional description for the product. |

### Output
Returns the ID of the added product.

### Rules
1. A product with the same name does not already exist.
2. The user must be an employee.

### Post Conditions
1. A product with the name and description is added to the product catalog.
2. The product can be found in product search.
3. The response comes back in less than .5 seconds.
4. The Product Added Event is raised.

### Usage Index
.01n
