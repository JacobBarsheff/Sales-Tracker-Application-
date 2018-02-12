using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_TheTravelingSalesperson
{
    class InitializeDataFileXml
    {
        public Salesperson InitializeSalesperson()
        {
            Salesperson salesperson = new Salesperson()
            {
                FirstName = "John",
                LastName = "Doe",
                AccountID = "123",
                CurrentStock = new Product(Product.ProductType.None, 0),
                CitiesVisited = new List<string>()
                {
                    "City 1",
                    "City 2",
                    "City 3"

                }
            };
            return salesperson;
        }
        public void SeedDataFile()
        {
            XmlServices xmlServices = new XmlServices();
            XmlServices.WriteXMLFile(InitializeSalesperson());
        }
    }
}
