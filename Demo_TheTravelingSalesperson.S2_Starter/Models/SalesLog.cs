using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_TheTravelingSalesperson
{
    public class SalesLog
    {
        private string _soldProduct;
        private string _boughtProduct;


        private string _dateTimeSold;
        private int _unitsSold;
        private int _unitsBought;
        public int _totalSales { get; set; }
        public int _totalPurchase { get; set; }


        public string BoughtProduct
        {
            get { return _boughtProduct; }
            set { _boughtProduct = value; }
        }

        public int UnitsSold
        {
            get { return _unitsSold; }
            set { _unitsSold = value; }
        }
        public int UnitsBought
        {
            get { return _unitsBought; }
            set { _unitsBought = value; }
        }

        public string DateTimeSold
        {
            get { return _dateTimeSold; }
            set { _dateTimeSold = value; }
        }
        public string SoldProduct
        {
            get { return _soldProduct; }
            set { _soldProduct = value; }
        }

        
    }
}
