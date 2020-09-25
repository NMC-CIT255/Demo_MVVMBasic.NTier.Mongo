using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_MVVMBasic.DataAccessLayer
{
    public static class SeedData
    {
        public static List<Widget> GetAllWidgets()
        {
            return new List<Widget>()
            {
                new Widget()
                {
                    Id = 1,
                    Name = "Furry",
                    Color = "Red",
                    UnitPrice = 4.95,
                    CurrentInventory = 10
                },

                new Widget()
                {
                    Id = 2,
                    Name = "Woody",
                    Color = "Brown",
                    UnitPrice = 6.95,
                    CurrentInventory = 15
                },

                new Widget()
                {
                    Id = 3,
                    Name = "Rubbery",
                    Color = "Black",
                    UnitPrice = 3.95,
                    CurrentInventory = 20
                },

                new Widget()
                {
                    Id = 4,
                    Name = "Glassy",
                    Color = "Clear",
                    UnitPrice = 9.95,
                    CurrentInventory = 2
                }
            };
        }

    }
}
