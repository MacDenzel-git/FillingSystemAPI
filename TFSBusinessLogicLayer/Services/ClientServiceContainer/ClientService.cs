using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechArchDataHandler.AutoMapper;
using TechArchDataHandler.General;
using TFSBusinessLogicLayer.Services.ClientServiceContainer;
using TFSDataAccessLayer.DTO;
using TFSDataAccessLayer.General;
using TFSDataAccessLayer.Models;

namespace TFSBusinessLogicLayer.Services.ClientServiceContainer
{
    public class ClientService : IClientService
    {
        private readonly GenericRepository<Client> _clientRepository;
        public ClientService(GenericRepository<Client> clientRepository)
        {
            _clientRepository = clientRepository;
        }
        public async Task<OutputHandler> Create(ClientDTO clientDTO)
        {
            try
            {
                bool isExist = await _clientRepository.AnyAsync(x => x.ClientName == clientDTO.ClientName && x.DepartmentId == clientDTO.DepartmentId);
                if (isExist)
                {
                    return new OutputHandler
                    {
                        IsErrorOccured = true,
                        Message = $"{clientDTO.ClientName} already exist in the database"

                    };
                }
                //create folder first 
                //var outputHandler = Helper.CreateFolder(clientDTO.ClientName);
                var mapped = new AutoMapper<ClientDTO, Client>().MapToObject(clientDTO);

                await _clientRepository.AddAsync(mapped);
                await _clientRepository.SaveChanges();
                return new OutputHandler
                {
                    IsErrorOccured = false,
                    Message = "Record Created Successfully"
                };
            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }

        }

        public async Task<OutputHandler> Delete(int clientId)
        {

            try
            {
                
                var fileType = await _clientRepository.GetItem(x => x.ClientId == clientId);
                var delete = Helper.DeleteFolder(fileType.ClientName);
                if (delete.IsErrorOccured)
                {
                    return new OutputHandler
                    {
                        IsErrorOccured = true,
                        Message = delete.Message
                    };
                }
                await _clientRepository.DeleteAsync(fileType);
                await _clientRepository.SaveChanges();
                return new OutputHandler
                    {
                        IsErrorOccured = false,
                        Message = "Record Deleted Successfully"
                    };
            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }
        }

        public async Task<ClientDTO> GetClient(int clientId)
        {
            var output = await _clientRepository.GetItem(x => x.ClientId == clientId);

            return new AutoMapper<Client, ClientDTO>().MapToObject(output);
        }

        public async Task<IEnumerable<ClientDTO>> GetClients(int departmentId)
        {
            var clients = await _clientRepository.GetListAsync( x => x.DepartmentId == departmentId);
            return new AutoMapper<Client, ClientDTO>().MapToList(clients);
        }  
        
        public async Task<IEnumerable<ClientDTO>> GetClientsForAdmin()
        {
            var clients = await _clientRepository.GetAll();
            return new AutoMapper<Client, ClientDTO>().MapToList(clients);
        }

        public async Task<OutputHandler> Update(ClientDTO clientDTO)
        {
            try
            {
               var mapped = new AutoMapper<ClientDTO, Client>().MapToObject(clientDTO);
               //var isDirectoryEmpty = Helper.IsFolderEmpty(clientDTO.ClientName);
               // if (!isDirectoryEmpty)
               // {
               //     return new OutputHandler
               //     {
               //         IsErrorOccured = true,
               //         Message = "This Client cannot be updated because it has files attached to it"
               //     };
               // }
                await _clientRepository.UpdateAsync(mapped);
                return new OutputHandler
                {
                    IsErrorOccured = false,
                    Message = "Record updated successfully"
                };
            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }
        }


        //public async Task<OutputHandler> ChangeStatus(int clientId)
        //{
        //    try
        //    {

        //        var fileType = await _clientRepository.GetItem(x => x.ClientId == clientId);
        //        if (fileType.)
        //        {
        //            fileType.IsActive = false;
        //        }
        //        else
        //        {
        //            fileType.IsActive = true;
        //        }

        //        // var mapped = new AutoMapper<clientDTO, Client>().MapToObject(fileType);
        //        await _clientRepository.UpdateAsync(fileType);
        //        await _clientRepository.SaveChanges();

        //        return new OutputHandler
        //        {
        //            IsErrorOccured = false,
        //            Message = "Status updated Successfully"
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return StandardMessages.getExceptionMessage(ex);
        //    }
        //}
    }
}
