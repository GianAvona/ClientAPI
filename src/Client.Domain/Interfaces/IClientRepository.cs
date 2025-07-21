using System;
using System.Threading.Tasks;
using System.Collections.Generic;

//using Client.Domain.Entities;
using ClientEntity = Client.Domain.Entities.Client;

namespace Client.Domain.Interfaces
{
    public interface IClientRepository
    {
        Task<ClientEntity?> GetByIdAsync(Guid id);              // Buscar cliente pelo ID
        Task<ClientEntity?> GetByEmailAsync(string email);      // Buscar cliente pelo e-mail
        Task<ClientEntity?> GetByCpfAsync(string cpf);          // Buscar cliente pelo CPF
        Task<IEnumerable<ClientEntity>> GetAllAsync();          // Buscar todos os clientes
        Task AddAsync(ClientEntity client);                     // Adicionar novo cliente
        Task UpdateAsync(ClientEntity client);                  // Atualizar cliente existente
        Task DeleteAsync(Guid id);                              // Deletar cliente pelo ID

        //  Validações de unicidade
        Task<bool> ExistsByEmailAsync(string email);
        Task<bool> ExistsByCPFAsync(string cpf);
    }
}
