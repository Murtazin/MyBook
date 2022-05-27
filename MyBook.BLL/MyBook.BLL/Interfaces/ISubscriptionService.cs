using MyBook.BLL.DTOModels;

namespace MyBook.BLL.Interfaces;

public interface ISubscriptionService
{
    Task<IEnumerable<SubscriptionDTO>> GetSubs();
    Task<SubscriptionDTO> GetSub(string id);
    Task Create(SubscriptionDTO sub);
    Task Delete(string id);
    Task Update(SubscriptionDTO editedSub);
}