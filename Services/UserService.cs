using LibreriaMilo.Data;
using LibreriaMilo.Models;
using LibreriaMilo.Response;

namespace LibreriaMilo.Services;

public class UserService
{
    private readonly MysqlDbContext _context;

    public UserService(MysqlDbContext context)
    {
        _context = context;
    }

    public ServiceResponse<IEnumerable<User>> GetAllUsers()
    {
        var users = _context.Users.ToList();

        return new ServiceResponse<IEnumerable<User>>()
        {
            Data = users,
            IsSuccess = true,

        };
    }

    public ServiceResponse<User> SaveUser(User user)
    {
        _context.Users.Add(user);
        var result = _context.SaveChanges();
        if (result > 0)
        {
            return new ServiceResponse<User>()
            {
                IsSuccess = true,
                Data = user,
                Message = "Userio guardado exitosamente",
            };
        }

        return new ServiceResponse<User>()
        {
            IsSuccess = false,
            Message = "Userio no se guardo exitosamente",
            Data = user,
        };
    }

    public ServiceResponse<User> GetUserById(int id)
    {
        var user = _context.Users.Find(id);
        if (user != null)
        {
            return new ServiceResponse<User>()
            {
                IsSuccess = true,
                Data = user
            };
        }

        return new ServiceResponse<User>()
        {
            IsSuccess = false,
            Data =  null
        };
    }

    public ServiceResponse<User> DeleteUser(int id)
    {
       var userDelete = _context.Users.Find(id);
       if (userDelete != null)
       {
           _context.Users.Remove(userDelete);
           _context.SaveChanges(); 
           return new ServiceResponse<User>()
           {
               IsSuccess = true,
               Message = "Userio eliminado exitosamente",
               Data = userDelete
           };
       }

       return new ServiceResponse<User>()
       {
           IsSuccess = false,
           Message = "No existe tal usuario",
           Data = null
       };

    }

    public ServiceResponse<User> UpdateUser(int id, User user)
    {
        var updateUser = _context.Users.Find(id);
        if (updateUser != null)
        {
            updateUser.Name = user.Name;
            updateUser.LastName = user.LastName;
            updateUser.Telephone = user.Telephone;
            updateUser.Email = user.Email;
            updateUser.UpdatedAt = DateTime.Now;
        
            _context.SaveChanges();
        
            return new ServiceResponse<User>()
            {
                IsSuccess = true,
                Message = "Usuario actualizado exitosamente",
                Data = updateUser
            };
        }

        return new ServiceResponse<User>()
        {
            IsSuccess = false,
            Message = "No existe tal usuario",
            Data = null
        };
    }
}