using System.ComponentModel.DataAnnotations;
using AutoMapper;
using MyBook.BLL.DTOModels;
using MyBook.BLL.Interfaces;
using MyBook.DAL.Entities;
using MyBook.DAL.Interfaces;

namespace MyBook.BLL.Services;

public class SubscriptionService : ISubscriptionService
{
        IUnitOfWork Database { get; set; }

        public SubscriptionService(IUnitOfWork database)
        {
            Database = database;
        }

        public async Task<IEnumerable<SubscriptionDTO>> GetSubs()
        {
            var subs = await Database.Subscriptions.GetAll();

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Subscription, SubscriptionDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Subscription>, List<SubscriptionDTO>>(subs);
        }

        public async Task<SubscriptionDTO> GetSub(string id)
        {
            var sub = await Database.Subscriptions.Get(Guid.Parse(id));

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Subscription, SubscriptionDTO>()).CreateMapper();
            return mapper.Map<Subscription, SubscriptionDTO>(sub);
        }

        public async Task Create(SubscriptionDTO subDTO)
        {
            var subs = await Database.Subscriptions.GetAll();

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SubscriptionDTO, Subscription>()).CreateMapper();
            var sub = mapper.Map<SubscriptionDTO, Subscription>(subDTO);

            if(subs.Contains(sub))
                throw new ValidationException("Такой план уже существует");

            await Database.Subscriptions.Create(sub);
            Database.Save();
        }

        public async Task Delete(string id)
        {
            await Database.Subscriptions.Delete(Guid.Parse(id));
            Database.Save();
        }

        public async Task Update(SubscriptionDTO editedSub)
        {

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SubscriptionDTO, Subscription>()).CreateMapper();
            var sub = mapper.Map<SubscriptionDTO, Subscription>(editedSub);

            await Database.Subscriptions.Update(sub);
            Database.Save();
        }
}