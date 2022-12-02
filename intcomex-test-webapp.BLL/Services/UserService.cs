using AutoMapper;
using intcomex_test_webapp.BLL.DTOs;
using intcomex_test_webapp.BLL.Helpers;
using intcomex_test_webapp.BLL.Models;
using intcomex_test_webapp.DAL;
using intcomex_test_webapp.DL.Entities;

namespace intcomex_test_webapp.BLL.Services
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAll();
        Task<UserDTO> GetById(int id);
        Task<ResponseModel> Create(UserDTO user);
        Task<ResponseModel> Delete(int id);
    }

    public class UserService : IUserService
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();
        private readonly IMapper _mapper;
        private readonly IRegexUtilitiesService _regexService;

        public UserService(IMapper mapper, IRegexUtilitiesService regexService)
        {
            _mapper = mapper;
            _regexService = regexService;
        }

        public async Task<List<UserDTO>> GetAll()
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync(includeProperties: "ContactTypeNoNavigation");

            return _mapper.Map<List<UserDTO>>(users).ToList();
        }

        public async Task<UserDTO> GetById(int id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);

            return _mapper.Map<UserDTO>(user);
        }

        public async Task<ResponseModel> Create(UserDTO user)
        {
            var response = new ResponseModel();

            try
            {
                //Validate username exist
                bool usernameExist = await _unitOfWork.UserRepository.ExistEntityByFilter(q => q.Username == user.Username);
                if (usernameExist)
                {
                    response.Message = "El usuario ya existe.";
                    return response;
                }

                //Validate username format
                if (!user.Username.StartsWith("xmx"))
                {
                    response.Message = "El usuario debe iniciar con las letras 'xmx'.";
                    return response;
                }

                //Validate if email exist
                bool emailExist = await _unitOfWork.UserRepository.ExistEntityByFilter(q => q.Email == user.Email);
                if (emailExist)
                {
                    response.Message = "El correo electrónico ya esta siendo utilizado por otro usuario.";
                    return response;
                }

                //Validate email format
                if (!_regexService.IsValidEmail(user.Email))
                {
                    response.Message = "Formato de correo electrónico no válido.";
                    return response;
                }

                User userEntity = _mapper.Map<User>(user);
                await _unitOfWork.UserRepository.InsertAsync(userEntity);
                await _unitOfWork.SaveAsync();

                response.Message = "Usuario creado exitosamente!";
                response.IsSuccess = true;
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }

            return response;
        }


        public async Task<ResponseModel> Delete(int id)
        {
            var response = new ResponseModel();

            try
            {
                var user = await _unitOfWork.UserRepository.GetByIdAsync(id);

                if (user == null)
                {
                    response.Message = $"El usuario con código {id} no existe.";
                    return response;
                }

                await _unitOfWork.UserRepository.DeleteByIdAsync(id);
                await _unitOfWork.SaveAsync();

                response.Message = "Usuario eliminado exitosamente!";
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }

            return response;
        }
    }
}
