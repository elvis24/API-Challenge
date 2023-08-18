using API_Challenge.Data;
using API_Challenge.Modelos;
using API_Challenge.Modelos.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API_Challenge.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _db;
        private IMapper _mapper;  

        public ClienteRepository(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ClienteDto> CrearUpdate(ClienteDto clienteDto)
        {
            Cliente cliente = _mapper.Map<ClienteDto,Cliente>(clienteDto);  
            if(cliente.Id>0)
                _db.clientes.Update(cliente);
            else
                await _db.clientes.AddAsync(cliente);

            await _db.SaveChangesAsync();
            return _mapper.Map<Cliente,ClienteDto>(cliente);
        }

        public async Task<bool> DeleteCliente(int id)
        {
            try
            {
                Cliente cliente = await _db.clientes.FindAsync(id);
                if (cliente == null)
                {
                    return false;
                }
                _db.clientes.Remove(cliente);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ClienteDto> GetClienteById(int id)
        {
            Cliente cliente = await _db.clientes.FindAsync(id);
            return _mapper.Map<ClienteDto>(cliente);
        }

        public async Task<List<ClienteDto>> GetClientes()
        {
            List<Cliente> lista = await _db.clientes.ToListAsync();
            return _mapper.Map<List<ClienteDto>>(lista);
        }
    }
}
