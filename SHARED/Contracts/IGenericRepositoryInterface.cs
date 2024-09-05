using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SHARED.DTOs.ServiceResponses;

namespace SHARED.Contracts
{
    public interface IGenericRepositoryInterface<T>
    {
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        Task<GeneralApiResponse> Insert(T item);
        Task<GeneralApiResponse> Update(T item);
        Task<GeneralApiResponse> DeleteById(int id);
    }
}
