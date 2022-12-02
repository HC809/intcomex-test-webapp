using AutoMapper;
using intcomex_test_webapp.BLL.DTOs;
using intcomex_test_webapp.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace intcomex_test_webapp.BLL.Services
{
    public interface IContactTypeService
    {
        Task<List<ContactTypeDTO>> GetAll();
    }

    public class ContactTypeService : IContactTypeService
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();
        private readonly IMapper _mapper;

        public ContactTypeService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<List<ContactTypeDTO>> GetAll()
        {
            var contactTypes = await _unitOfWork.ContactTypeRepository.GetAllAsync();

            return _mapper.Map<List<ContactTypeDTO>>(contactTypes);
        }
    }
}
