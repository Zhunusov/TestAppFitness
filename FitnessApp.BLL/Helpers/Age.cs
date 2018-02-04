using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.BLL.Helpers
{
    public class Age
    {
        public static int GetAgeOfUser(DateTime? dateOfBirth)
        {
            if (dateOfBirth != null)
            {
                DateTime now = DateTime.Today;
                int age = now.Year - ((DateTime)dateOfBirth).Year;

                if (dateOfBirth > now.AddYears(-age))
                    age--;

                return age;
            }
            return 0;
        }
    }
}
