using BusinessObject;

namespace DataAccess
{
    public interface RoleRepository
    {
        List<Role> GetAll();
        void Create(Role role);
        void Update(Role role);
        void Delete(int id);
    }
}
