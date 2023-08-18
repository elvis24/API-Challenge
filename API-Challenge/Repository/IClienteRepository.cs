using API_Challenge.Modelos.DTO;

namespace API_Challenge.Repository
{
    public interface IClienteRepository
    {
        Task<List<ClienteDto>> GetClientes();
        Task<ClienteDto> GetClienteById(int id);
        Task<ClienteDto> CrearUpdate(ClienteDto clienteDto);
        Task<bool> DeleteCliente(int id);
    }
}
