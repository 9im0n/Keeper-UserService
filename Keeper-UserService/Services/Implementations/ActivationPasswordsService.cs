using Keeper_ApiGateWay.Models.Services;
using Keeper_UserService.Models.Db;
using Keeper_UserService.Repositories.Interfaces;
using Keeper_UserService.Services.Interfaces;

namespace Keeper_UserService.Services.Implementations
{
    public class ActivationPasswordsService : IActivationPasswordService
    {
        private readonly IActivationPasswordsRepository _repository;

        public ActivationPasswordsService(IActivationPasswordsRepository repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse<ActivationPasswords?>> GetByUserIdAsync(Guid id)
        {
            ActivationPasswords password = await _repository.GetByUserIdAsync(id);

            if (password == null)
                return ServiceResponse<ActivationPasswords>.Fail(default, 404, $"There is no password with user id: {id}");
        
            return ServiceResponse<ActivationPasswords>.Success(password);
        }


        public async Task<ServiceResponse<ActivationPasswords?>> CreateAsync(Users user)
        {
            string newPassword = "";
            Random rnd = new Random();

            for (int i = 0; i < 6; i++)
            {
                newPassword += rnd.Next(0, 10).ToString();
            }

            ActivationPasswords password = await _repository.GetByUserIdAsync(user.Id);

            if (password != null)
            {
                password = await _repository.UpdateAsync(new ActivationPasswords()
                {
                    UserId = user.Id,
                    User = user,
                    Password = newPassword,
                    CreatedAt = DateTime.UtcNow
                });
            }

            password = await _repository.CreateAsync(new ActivationPasswords()
            {
                UserId = user.Id,
                User = user,
                Password = newPassword,
                CreatedAt = DateTime.UtcNow
            });

            return ServiceResponse<ActivationPasswords>.Success(password);
        }
    }
}
