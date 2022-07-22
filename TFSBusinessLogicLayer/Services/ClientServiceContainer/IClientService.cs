using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechArchDataHandler.General;
using TFSDataAccessLayer.DTO;

namespace TFSBusinessLogicLayer.Services.ClientServiceContainer
{
    public interface IClientService
    {
        Task<OutputHandler> Create(ClientDTO clientDTO);
        Task<OutputHandler> Update(ClientDTO clientDTO);
        Task<OutputHandler> Delete(int clientId);
        Task<IEnumerable<ClientDTO>> GetClients(int department); //always get clients for the particular 
        Task<IEnumerable<ClientDTO>> GetClientsForAdmin(); //always get clients for the particular 
        Task<ClientDTO> GetClient(int clientId);

    }
}
