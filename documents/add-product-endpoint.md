# Add Product

**Name**:  Add Product

**Path**:  catalog/products/add

**Version**: 1

**Timeout**: 5 seconds

**Summary**: Adds a product to the product catalog so that a user can search for it and it can be added to a cart, purchased and/or shipped.

**Input**:

| Name | Description                     | Validation        |
| ---- | ------------------------------- | ----------------- |
| Name | The name of the product to add. | Must not be null. |

**Output**: Returns a simple string that represents the ID of the added product.