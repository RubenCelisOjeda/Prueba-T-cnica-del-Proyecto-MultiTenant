namespace pruebaAppApi.Domain._2.Repository.Products
{
    public interface IProductsRepository
    {
        public Task<IEnumerable<pruebaAppApi.Domain._1.Entities.Products>> GetAllProduct();
        public Task<pruebaAppApi.Domain._1.Entities.Products> GetProduct(int id);
        public Task<int> AddProduct(pruebaAppApi.Domain._1.Entities.Products itemRequest);
        public Task<int> UpdateProduct(pruebaAppApi.Domain._1.Entities.Products itemRequest);
        public Task<int> DeleteProduct(int id);
    }
}
