# Add Product Use Case

### Name
Add Product

### Actor
Registered User

### Bounded Context
Catalog

### Summary
Adds a product to the product catalog so that a user can search for it and it can be added to a cart, purchased and/or shipped.

### Input

| Name        | Description                              | 
| ----------- | ---------------------------------------- | 
| Name        | The name of the product to add.          | 
| Description | An optional description for the product. |

### Output
Returns the ID of the added product.

### Rules
1. A product with the same name does not already exist.
2. The use must be registered.

### Post Conditions
1. A product with the name and description is added to the product catalog.
2. The product can be found in product search.

### Usage Index
.25n
