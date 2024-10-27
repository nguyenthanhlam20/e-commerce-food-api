using BusinessObject;
using Repository.implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.implementations
{
    public class CategoryServiceImpl : CategoryService
    {
        private readonly CategoryRepository categoryRepository = new CategoryRepository();
        public List<Category> GetAll()
        {
            return categoryRepository.GetAll();
        }
    }
}
