using System.Collections.Generic;

namespace Demo_TheTravelingSalesperson
{

    public class Salesperson
    {
        public enum Gender
        {
            NA,
            Male,
            Female
        }
        #region FIELDS

        private string _firstName;
        private string _lastName;
        private string _accountID;
        private List<string> _citiesVisited;
        private Product _currentStock;
        private string _currentCity = "N/A";
        private int _age;
        private bool _hasPriorSalesExperience;






        #endregion

        #region PROPERTIES
        public bool HasPriorSalesExperience
        {
            get { return _hasPriorSalesExperience; }
            set { _hasPriorSalesExperience = value; }
        }

        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }

        public string CurrentCity
        {
            get { return _currentCity; }
            set { _currentCity = value; }
        }


        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string AccountID
        {
            get { return _accountID; }
            set { _accountID = value; }
        }
      
        public List<string> CitiesVisited
        {
            get { return _citiesVisited; }
            set { _citiesVisited = value; }
        }
        public Product CurrentStock
        {
            get { return _currentStock; }
            set { _currentStock = value; }
        }
        #endregion

        #region CONSTRUCTORS

        public Salesperson()
        {
            _citiesVisited = new List<string>();
            _currentStock = new Product();
        }

        public Salesperson(string firstName, string lastName, string acountID)
        {
            _firstName = firstName;
            _lastName = lastName;
            _accountID = acountID;

            _citiesVisited = new List<string>();
            _currentStock = new Product();
        }
        #endregion

        #region METHODS



        #endregion
    }
}
