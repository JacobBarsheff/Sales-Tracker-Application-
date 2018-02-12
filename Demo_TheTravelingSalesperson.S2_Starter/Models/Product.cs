using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Demo_TheTravelingSalesperson
{
    /// <summary>
    /// Product MVC Model class
    /// </summary>
    public class Product
    {
        #region ENUMERABLES

        public enum ProductType
        {
            None,
            Samsung32,
            Samsung48,
            Samsung60
        }

        #endregion

        #region FIELDS

        private ProductType _type;
        private int _numberOfUnits;
        private bool _onBackorder;
        private double _price;
        private string _manufacturerLocation = "Seoul, South Korea";
        private string _category = "electronics";

        #endregion

        #region PROPERTIES
        public double Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public string Category
        {
            get { return _category = "electronics"; }
            set { _category = value; }
        }

        public string ManufacturerLocation
        {
            get { return _manufacturerLocation = "Seoul, South Korea"; }
            set { _manufacturerLocation = value; }
        }

        public ProductType Type
        {
            get { return _type; }
            set { _type = value; }
        }
      
        public int NumberOfUnits
        {
            get { return _numberOfUnits; }
        }

        public bool OnBackorder
        {
            get { return _onBackorder; }
            set { _onBackorder = value; }
        }

        #endregion

        #region CONSTRUCTORS

        public Product()
        {
            _type = ProductType.None;
            _numberOfUnits = 0;
        }

        public Product(ProductType type, int numberOfUnits)
        {
            _type = type;
            _numberOfUnits = numberOfUnits;
        }

        #endregion

        #region METHODS

        public void AddProducts(int unitsToAdd)
        {
            _numberOfUnits += unitsToAdd;
        }

        public void SubtractProducts(int unitsToSubtract)
        {
            if (_numberOfUnits < unitsToSubtract)
            {
                _onBackorder = true;
            }
            _numberOfUnits -= unitsToSubtract;
        }

        #endregion
    }
}


