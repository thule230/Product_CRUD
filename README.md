# Product_CRUD

For the purpose of this POC, the auxiliary tables  
(ProductType, CapacityUnity, WeightUnity and ClothingSize) don't have an endpoint that makes changes to them, only queries. To populate them, the "\PopulateAuxTables" endpoint was created, which will populate them with default values, values that can be seen in the get endpoints of each table.

### Before performing actions with products run the endpoint that creates the values in the auxiliary tables
