using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace Service.Services
{
    public interface IAccountRoleService
    {
        public IEnumerable<AccountRole> GetAll();
        public AccountRole GetById(int id);
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
            return _context.AccountRoles.Include(a => a.RoleAccounts);
        }

        public AccountRole GetById(int id)
        {
            var accountRole = GetAccountRole(id);
            return accountRole;
        }

        public void Create(AccountRoleCreateModel request)
        {
            var accountRole = _mapper.Map<AccountRole>(request);

            _context.AccountRoles.Add(accountRole);
            _context.SaveChanges();
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
            var accountRole = _context.AccountRoles.Include(a => a.RoleAccounts).AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (accountRole == null) throw new KeyNotFoundException("Account Role not found!");
            return accountRole;
        }
    }
}
