using Product_CRUD.Models;

namespace Product_CRUD.DataLayer
{
    public static class Clothing
    {
        public static IEnumerable<Models.Clothing> Get(IEnumerable<int>? ids = null, string? descriptionSearch = null, decimal? minPrice = null, decimal? maxPrice = null, int? minQuantity = null, int? maxQuantity = null, int? size = null)
        {
            try
            {
                using var context = new ApplicationDbContext();

                var query = from clothing in context.Clothings
                            select clothing;

                if (ids != null && ids.Any())
                {
                    query = query.Where(clothing => ids.Contains(clothing.Id));
                }

                if (descriptionSearch != null)
                {
                    query = query.Where(clothing => clothing.Description.Contains(descriptionSearch));
                }

                if (minPrice.HasValue)
                {
                    query = query.Where(clothing => clothing.Price >= minPrice.Value);
                }

                if (maxPrice.HasValue)
                {
                    query = query.Where(clothing => clothing.Price <= maxPrice.Value);
                }

                if (minQuantity.HasValue)
                {
                    query = query.Where(clothing => clothing.Quantity >= minQuantity.Value);
                }

                if (maxQuantity.HasValue)
                {
                    query = query.Where(clothing => clothing.Quantity <= maxQuantity.Value);
                }

                if (size.HasValue)
                {
                    query = query.Where(clothing => clothing.ClothingSizeId == size.Value);
                }

                return query.ToList();
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
                if (AllClothingsAreValid(newClothings))
                {
                    using var context = new ApplicationDbContext();

                    context.Clothings.AddRange(newClothings);
                    context.SaveChanges();
                }
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
                if (AllClothingsAreValid(clothingsNewValues))
                {
                    var context = new ApplicationDbContext();

                    var ids = clothingsNewValues.Select(clothingNewValue => clothingNewValue.Id).ToList();

                    if (ids.Distinct().Count() != ids.Count)
                    {
                        throw new ArgumentException("One or more clothings submitted are repeated");
                    }

                    var clothingsFound = Get(ids);

                    if (ids.Count != clothingsFound.Count())
                    {
                        throw new ArgumentException("One or more clothings submitted were not found");
                    }

                    context.Clothings.UpdateRange(clothingsNewValues);
                    context.SaveChanges();
                }
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
                var context = new ApplicationDbContext();

                var clothingsToDelete = Get(ids);
                context.Clothings.RemoveRange(clothingsToDelete);

                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static bool AllClothingsAreValid(IEnumerable<Models.Clothing> clothingsToValidate)
        {
            var distinctsProductTypes = clothingsToValidate.Select(clothing => clothing.ProductTypeId).Distinct().ToList();

            var productsTypesFounds = ProductType.Get(distinctsProductTypes);

            if (!productsTypesFounds.Any() || distinctsProductTypes.Count != productsTypesFounds.Count())
            {
                throw new ArgumentException("One or more clothings submitted has a invalid ProductType");
            }

            var distinctsSizes = clothingsToValidate.Select(clothing => clothing.ClothingSizeId).Distinct().ToList();

            var sizesFounds = ClothingSize.Get(distinctsSizes);

            if (!sizesFounds.Any() || distinctsSizes.Count != sizesFounds.Count())
            {
                throw new ArgumentException("One or more clothings submitted has a invalid Size");
            }

            return true;
        }
    }
}
