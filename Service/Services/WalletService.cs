using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Repository.Domain.Models;
using Service.Helpers;
using VWater.Data;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace Service.Wallets
{
    public interface IWalletService
    {
        public IEnumerable<Wallet> GetAll();
        public Wallet GetById(int id);
        public void Create(WalletCreateModel request);
        public void Update(int id, WalletUpdateModel request);
        public void Delete(int id);
    }
    public class WalletService : IWalletService
    {
        private VWaterContext _context;
        private readonly IMapper _mapper;

        public WalletService(VWaterContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IEnumerable<Wallet> GetAll()
        {
            return _context.Wallets.Include(a => a.Shipper).Include(a => a.Transactions);
        }

        public Wallet GetById(int id)
        {
            var wallet = GetWallet(id);
            return wallet;
        }

        public void Create(WalletCreateModel request)
        {
            var wallet = _mapper.Map<Wallet>(request);

            _context.Wallets.AddAsync(wallet);
            _context.SaveChangesAsync();
        }

        public void Update(int id, WalletUpdateModel request)
        {
            var wallet = GetWallet(id);

            _mapper.Map(request, wallet);
            _context.Wallets.Update(wallet);
            _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var wallet = GetWallet(id);
            _context.Wallets.Remove(wallet);
            _context.SaveChangesAsync();
        }

        private Wallet GetWallet(int id)
        {
            var wallet = _context.Wallets.Include(a => a.Shipper).Include(a => a.Transactions)
                .AsNoTracking().FirstOrDefault(a => a.Id == id);
            if (wallet == null) throw new KeyNotFoundException("Wallet not found!");
            return wallet;
        }

    }
}
