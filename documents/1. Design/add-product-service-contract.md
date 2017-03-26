# Add Product Service Contract

**Name**:  Add Product

**Path**: catalog/products/add

**Version**: 1

**Timeout**: 5 seconds

**Summary**: Adds a product to the product catalog so that a user can search for it and it can be added to a cart, purchased and/or shipped.

**Input**:

| Name        | Description                              | Validation        |
| ----------- | ---------------------------------------- | ----------------- |
| Name        | The name of the product to add.          | Must not be null. |
| Description | An optional description for the product. |                   |

**Output**: Returns a string representing the ID of the added product.

**Event**: Product Added Event

| Name        | Description                              | 
| ----------- | ---------------------------------------- | 
| ID          | The added product ID.                    |
| Name        | The name of the product to add.          | 
| Description | An optional description for the product. |

**Rules**: 

| Number | Type     | Code                  | Name                    | End-User Message                             | 
| ------ | -------- | --------------------- | ----------------------- | -------------------------------------------- |
| 1      | Security | UserNotRegistered     | user must be registered | You must be registered to submit a product.  |
| 2      | Business | NameNotUnique         | name must be unique     | A product with the same name already exists. |