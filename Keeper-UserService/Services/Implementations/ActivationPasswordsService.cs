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

        public async Task<ServiceResponse<ActivationPasswords?>> GetByEmailAsync(string email)
        {
            ActivationPasswords? password = await _repository.GetByEmailAsync(email);

            if (password == null)
                return ServiceResponse<ActivationPasswords>.Fail(default, 404, $"There is no password for {email}.");
        
            return ServiceResponse<ActivationPasswords?>.Success(password);
        }


        public async Task<ServiceResponse<ActivationPasswords>> CreateAsync(string email)
        {
            string newPassword = "";
            Random rnd = new Random();

            for (int i = 0; i < 6; i++)
            {
                newPassword += rnd.Next(0, 10).ToString();
            }

            ActivationPasswords? password = await _repository.GetByEmailAsync(email);

            if (password != null)
            {
                password = await _repository.UpdateAsync(new ActivationPasswords()
                {
                    Id = password.Id,
                    Email = email,
                    Password = newPassword,
                    CreatedAt = DateTime.UtcNow
                });

                return ServiceResponse<ActivationPasswords>.Success(password);
            }

            password = await _repository.CreateAsync(new ActivationPasswords()
            {
                Email = email,
                Password = newPassword,
                CreatedAt = DateTime.UtcNow
            });

            return ServiceResponse<ActivationPasswords>.Success(password);
        }
    }
}
