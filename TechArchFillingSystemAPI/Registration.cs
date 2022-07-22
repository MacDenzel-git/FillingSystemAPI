using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFSBusinessLogicLayer.Services.ClientServiceContainer;
using TFSBusinessLogicLayer.Services.CompanyServiceContainer;
using TFSBusinessLogicLayer.Services.CompanySubsidiaryServiceContainer;
using TFSBusinessLogicLayer.Services.DepartmentServiceContainer;
using TFSBusinessLogicLayer.Services.FileDetailServiceContainer;
using TFSBusinessLogicLayer.Services.FileTypeCategoryServiceContainer;
using TFSBusinessLogicLayer.Services.FileTypeServiceContainer;
using TFSBusinessLogicLayer.Services.SubsidiaryCompanyServiceContainer;
using TFSDataAccessLayer.General;
using TFSDataAccessLayer.Models;

namespace TechArchFillingSystemAPI
{
    public static class Registration
    {
        public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<GenericRepository<Client>>();
            serviceCollection.AddScoped<GenericRepository<Company>>();
            serviceCollection.AddScoped<GenericRepository<SubsidiaryCompany>>();
            serviceCollection.AddScoped<GenericRepository<Department>>();
            serviceCollection.AddScoped<GenericRepository<FileDetail>>();
                  serviceCollection.AddScoped<GenericRepository<FileTypeCategory>>();
                  return serviceCollection.AddScoped<GenericRepository<FileType>>();
        }

        public static IServiceCollection AddServices(this IServiceCollection service)
        {
            service.AddScoped<ICompanyService, CompanyService>();
            service.AddScoped<ISubsidiaryCompanyService, SubsidiaryCompanyService>();
            service.AddScoped<IDepartmentService, DepartmentService>();
            service.AddScoped<ICompanyService, CompanyService>();
            service.AddScoped<IClientService, ClientService>();
            service.AddScoped<IFileDetailService, FileDetailService>();
            service.AddScoped<IFileTypeCategoryService, FileTypeCategoryService>();
            return service.AddScoped<IFileTypeService, FileTypeService>();
        }
    }
}
