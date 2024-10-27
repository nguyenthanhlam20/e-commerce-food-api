using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.implementations
{
    public class CategoryRepository
    {
        private static List<Category> categories = new List<Category>();

        public List<Category> GetAll()
        {
            try
            {
                using (var context = new DBContext())
                {
                    categories = context.Categories.ToList();
                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return categories;
        }
    }
}
