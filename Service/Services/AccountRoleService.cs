using AutoMapper;
using Service.Helpers;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace Service.Services
{
    public interface IAccountRoleService
    {
        public IEnumerable<AccountRole> GetAll();
        public AccountRoleReadModel GetById(int id);
        public void Create(AccountRoleCreateModel request);
        public void Update(int id, AccountRoleUpdateModel request);
        public void Delete(int id);
    }
    public class AccountRoleService : IAccountRoleService
    {
        private VWaterContext _context;
        private readonly IMapper _mapper;

        public AccountRoleService(VWaterContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IEnumerable<AccountRole> GetAll()
        {
            return _context.AccountRoles;
        }

        public AccountRoleReadModel GetById(int id)
        {
            var accountRole = _mapper.Map<AccountRoleReadModel>(GetAccountRole(id));
            return accountRole;
        }

        public void Create(AccountRoleCreateModel request)
        {
            var accountRole = _mapper.Map<AccountRole>(request);

            _context.AccountRoles.AddAsync(accountRole);
            _context.SaveChangesAsync();
        }

        public void Update(int id, AccountRoleUpdateModel request)
        {
            var accountRole = GetAccountRole(id);
            _mapper.Map(request, accountRole);
            _context.AccountRoles.Update(accountRole);
            _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var accountRole = GetAccountRole(id);
            _context.AccountRoles.Remove(accountRole);
            _context.SaveChangesAsync();
        }

        private AccountRole GetAccountRole(int id)
        {
            var accountRole = _context.AccountRoles.Find(id);
            if (accountRole == null) throw new KeyNotFoundException("Account Role not found!");
            return accountRole;
        }
    }
}
