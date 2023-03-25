using Product_CRUD.Enumerators;

namespace Product_CRUD.BusinessLayer
{
    public static class Clothing
    {
        public static IEnumerable<object> Get(string? descriptionSearch, decimal? minPrice, decimal? maxPrice, int? minQuantity, int? maxQuantity, decimal? minFinalPrice, decimal? maxFinalPrice, int? size = null, IEnumerable<int>? ids = null)
        {
            try
            {
                var results = DataLayer.Clothing.Get(ids, descriptionSearch, minPrice, maxPrice, minQuantity, maxQuantity, size);

                var productTypesIds = results.Select(result => result.ProductTypeId).Distinct().ToList();

                var productTypesResults = DataLayer.ProductType.Get(productTypesIds);

                var sizesIds = results.Select(result => result.ClothingSizeId).Distinct().ToList();

                var sizesResult = DataLayer.ClothingSize.Get(sizesIds);

                var resultsJoin = from clothings in results
                                  join productTypes in productTypesResults
                                  on clothings.ProductTypeId equals productTypes.Id
                                  join sizes in sizesResult
                                  on clothings.ClothingSizeId equals sizes.Id
                                  select new
                                  {
                                      clothings.Id,
                                      clothings.Description,
                                      clothings.Price,
                                      clothings.Quantity,
                                      FinalPrice = Product.GetFinalPrice(clothings.Price, clothings.Quantity, productTypes.TypeTax),
                                      Size = new { sizes.Id, sizes.Description },
                                      ProductType = new { productTypes.Id, productTypes.Description, productTypes.TypeTax }
                                  };

                var finalResults = resultsJoin.ToList();

                if (minFinalPrice.HasValue)
                {
                    finalResults = finalResults.Where(finalResult => finalResult.FinalPrice >= minFinalPrice.Value).ToList();
                }

                if (maxFinalPrice.HasValue)
                {
                    finalResults = finalResults.Where(finalResult => finalResult.FinalPrice <= maxFinalPrice.Value).ToList();
                }

                return finalResults;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void Add(IEnumerable<Models.Clothing> newClothings)
        {
            try
            {
                var distinctsProductTypes = newClothings.Select(clothing => clothing.ProductTypeId).Distinct().ToList();

                if (distinctsProductTypes.Where(productType => productType != ProductType.Clothing.GetId()).Any())
                {
                    throw new ArgumentException("One or more clothings submitted are not Clothing");
                }

                DataLayer.Clothing.Add(newClothings);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void Update(IEnumerable<Models.Clothing> clothingsNewValues)
        {
            try
            {
                var ids = clothingsNewValues.Select(clothing => clothing.Id).ToList();
                var clothingsToUpdate = DataLayer.Clothing.Get(ids);

                foreach (var clothing in clothingsToUpdate)
                {
                    var clothingToUpdateFound = clothingsToUpdate.Where(clothingToUpdate => clothingToUpdate.Id == clothing.Id).Single();
                    if (clothing.ProductTypeId != clothingToUpdateFound.ProductTypeId)
                    {
                        throw new ArgumentException("One or more clothings submitted are trying to change the ProductType");
                    }
                }

                DataLayer.Clothing.Update(clothingsNewValues);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void Delete(IEnumerable<int> ids)
        {
            try
            {
                if (ids.Any())
                {
                    DataLayer.Clothing.Delete(ids);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
