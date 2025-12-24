namespace AutomatedTransportEnquiry.Repositories
{
    public interface IRouteRepository
    {
        Task<IEnumerable<Route>> GetAllAsync();
        Task<Route> GetIdAsync(int id);
        Task<Route> CreateAsync(Route route);
        Task<bool> UpdateAsync(Route route);
        Task<bool> DeleteAsync(int id);


    }
}
