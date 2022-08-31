using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Responses;
using Core.Utilities.Security.Token;
using Entities.Dtos.Auth;
using Entities.Dtos.User;

namespace Business.Concrete
{
    public class AuthService : IAuthService
    {
        private IUserService _userService;
        private ITokenService _tokenService;
        private IMapper _mapper;

        public AuthService(IMapper mapper, ITokenService tokenService, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
            _tokenService = tokenService;
        }
        public async Task<ApiDataResponse<UserDto>> LoginAsync(LoginDto loginDto)
        {
            var user = await _userService.GetAsync(x => x.UserName == loginDto.UserName && x.Password == loginDto.Password);
            if (user ==null)
            {
                return new ErrorApiDataResponse<UserDto>(null, Messages.UserNotFound);
            }
            else
            {
                if (user.Data.TokenExpireDate == null || String.IsNullOrEmpty(user.Data.Token))
                {
                    return await UpdateToken(user);
                }
                if (user.Data.TokenExpireDate   <  DateTime.Now)
                {
                    return await UpdateToken(user);
                }
            }
            return new SuccessApiDataResponse<UserDto>(user.Data, Messages.SystemLoginSuccesful);
        }

        private async Task<ApiDataResponse<UserDto>> UpdateToken(ApiDataResponse<UserDto> user)
        {
            var accessToken = _tokenService.CreateToken(user.Data.Id, user.Data.UserName);
            var userUpdateDto = _mapper.Map<UserUpdateDto>(user.Data);
            userUpdateDto.Token = accessToken.Token;
            userUpdateDto.TokenExpireDate = accessToken.Expiration;
            userUpdateDto.UpdatedUserId = user.Data.Id;
            var resultUserUpdateDto = await _userService.UpdateAsync(userUpdateDto);
            var userDto = _mapper.Map<UserDto>(resultUserUpdateDto.Data);
            return new SuccessApiDataResponse<UserDto>(userDto, Messages.SystemLoginSuccesful);
        }
    }
}
