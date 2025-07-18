using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ClientEntity = Client.Domain.Entities.Client;
using Client.Domain.Interfaces;
using Client.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Client.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ClientDbContext _context;

        public ClientRepository(ClientDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ClientEntity client)
        {
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ClientEntity>> GetAllAsync()
        {
            return await _context.Clients.AsNoTracking().ToListAsync();
        }

        public async Task<ClientEntity?> GetByCpfAsync(string cpf)
        {
            return await _context.Clients.AsNoTracking()
                .FirstOrDefaultAsync(c => c.CPF == cpf);
        }

        public async Task<ClientEntity?> GetByEmailAsync(string email)
        {
            return await _context.Clients.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<ClientEntity?> GetByIdAsync(Guid id)
        {
            return await _context.Clients.FindAsync(id);
        }

        public async Task UpdateAsync(ClientEntity client)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
        }
    }
}
