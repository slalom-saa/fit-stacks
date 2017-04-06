# Add Product Service Contract

### Name
Add Product

### Path 
catalog/products/add

### Version
1

### Timeout
5 seconds

### Summary
Adds a product to the product catalog so that a user can search for it and it can be added to a cart, rented, purchased or shipped.

### Input

| Name        | Description                              | Validation              |
| ----------- | ---------------------------------------- | ----------------------- |
| Name        | The name of the product to add.          | Name must be specified. |
| Description | An optional description for the product. |                         |

### Output
Returns a string representing the ID of the added product.

### Event
Product Added Event

| Name        | Description                              | 
| ----------- | ---------------------------------------- | 
| ID          | The added product ID.                    |
| Name        | The name of the product to add.          | 
| Description | An optional description for the product. |

### Rules 

| Number | Type     | Code                  | End-User Message                             | 
| ------ | -------- | --------------------- | -------------------------------------------- |
| 1      | Security | UserIsEmployee        | You must be an employee to add a product.    |
| 2      | Business | NameNotUnique         | A product with the same name already exists. |