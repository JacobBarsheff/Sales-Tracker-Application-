using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Demo_TheTravelingSalesperson
{

    public class Controller
    {
        #region FIELDS

        private bool _usingApplication;
        private ConsoleView _consoleView;
        private Salesperson _salesperson;
        private SalesLog _salesLog;
        private Product _product;
        private XmlServices _xml;
        private List<string> _salesLogList;
        private List<string> _purchaseLogList;


        #endregion

        #region PROPERTIES


        #endregion

        #region CONSTRUCTORS

        public Controller()
        {
            InitializeController();

            //
            // instantiate a Salesperson object
            //
            _salesperson = new Salesperson();

            //
            // instantiate a ConsoleView object
            //
            _consoleView = new ConsoleView();

            //
            //
            //
            _salesLog = new SalesLog();

            //
            //
            //
            _salesLogList = new List<string>();

            //
            //
            //
            _purchaseLogList = new List<string>();

            //
            //
            //
            _product = new Product();
            //
            //
            //
            _xml = new XmlServices();
       
            //
            // begins running the application UI
            //
            ManageApplicationLoop();
        }

        #endregion

        #region METHODS

        /// <summary>
        /// initialize the controller 
        /// </summary>
        private void InitializeController()
        {
            _usingApplication = true;
        }

        private void ManageApplicationLoop()
        {
            MenuOption userMenuChoice;

            _consoleView.DisplayWelcomeScreen();

            


            
            
            //
            // application loop
            //
            while (_usingApplication)
            {

                //
                // get a menu choice from the ConsoleView object
                //
                userMenuChoice = _consoleView.DisplayGetUserMenuChoice();

                //
                // menu structure with correlating methods
                //
                switch (userMenuChoice)
                {
                    case MenuOption.None:
                        break;
                    case MenuOption.SetUpAccount:
                        SetUpAccount();
                        LogPurchase();
                        break;
                    case MenuOption.Travel:
                        Travel();
                        break;
                    case MenuOption.Buy:
                        Buy();
                        LogPurchase();
                        break;
                    case MenuOption.Sell:
                        Sell();
                        LogItem();
                        break;
                    case MenuOption.ChangeItem:
                        ChangeItem();
                        LogPurchase();
                        break;
                    case MenuOption.DisplayInventory:
                        DisplayInventory();
                        break;
                    case MenuOption.DisplayCities:
                        DisplayCities();
                        break;
                    case MenuOption.DisplayAccountInfo:
                        DisplayAccountInfo();
                        break;
                    case MenuOption.DisplayStats:
                        DisplayLog();
                        break;
                    case MenuOption.DisplayPurchaseLog:
                        DisplayPurchaseLog();
                        break;
                    case MenuOption.AccountEdit:
                        EditAccount();
                        break;
                    case MenuOption.Save:
                        WriteXMLFile(_salesperson, _xml);
                        break;
                    case MenuOption.Load:
                        _salesperson = ReadXmlFile(_salesperson);
                        break;
                    case MenuOption.Exit:
                        _usingApplication = false;
                        break;
                    default:
                        break;
                }
            }

            _consoleView.DisplayClosingScreen();

            //
            // end the application
            //
            Environment.Exit(1);
        }
        private void SetUpAccount()
        {
            // setup initial salesperson account
            //
            _salesperson = _consoleView.DisplaySetupAccount(_salesLog, _product);
            
        }
        private void Travel()
        {
            string nextCity = _consoleView.DisplayGetNextCity();
            //if nextCity is NOT blank, then add it to the list. 
            if (nextCity != "")
            {
                _salesperson.CitiesVisited.Add(nextCity);
                _salesperson.CurrentCity = nextCity;
            }
        }

        private void Buy()
        {
            int numberOfUnits = _consoleView.DisplayGetNumberOfUnitsToBuy(_salesperson.CurrentStock);
            _salesperson.CurrentStock.AddProducts(numberOfUnits);
            _salesLog.UnitsBought = numberOfUnits;
            _salesLog._totalPurchase = _salesLog._totalPurchase + numberOfUnits;

        }


        private void Sell()
        {
            int numberOfUnits = _consoleView.DisplayGetNumberOfUnitsToSell(_salesperson.CurrentStock);
            _salesperson.CurrentStock.SubtractProducts(numberOfUnits);
            _salesLog.UnitsSold = numberOfUnits;
            _salesLog._totalSales = _salesLog._totalSales + numberOfUnits;



            if (_salesperson.CurrentStock.OnBackorder)
            {
                _consoleView.DisplayBackorderNotification(_salesperson.CurrentStock, numberOfUnits);
            }
        }

        private void DisplayInventory()
        {
            _consoleView.DisplayInventory(_salesperson.CurrentStock);
        }


        private void DisplayCities()
        {
            _consoleView.DisplayCitiesTraveled(_salesperson);
        }

        private void DisplayAccountInfo()
        {
            _consoleView.DisplayAccountInfo(_salesperson);
        }

        private void LogItem()
        {
            string record;           
            DateTime localDate = DateTime.Now;
            record = (")" + (_salesLog.SoldProduct = _salesperson.CurrentStock.Type.ToString()) +"...." + _salesLog.UnitsSold.ToString() + "........." + localDate.ToString() +"........" + _salesperson.CurrentCity);            
            _salesLogList.Add(record);          
        }

        private void DisplayLog()
        {
            _consoleView.DisplayLog(_salesLogList, _salesLog);
        }

        private void LogPurchase()
        {
            string record;
            DateTime localDate = DateTime.Now;
            record = (")" + (_salesLog.BoughtProduct = _salesperson.CurrentStock.Type.ToString()) + "...." + _salesLog.UnitsBought.ToString() + "............" + localDate.ToString() + "........" + _salesperson.CurrentCity);
            _purchaseLogList.Add(record);

        }

        private void DisplayPurchaseLog()
        {
            _consoleView.DisplayPurchaseLog(_purchaseLogList, _salesLog);
        }
        private void ChangeItem()
        {
           _consoleView.ChangeItem(_salesperson, _salesLog);

        }
        private void EditAccount()
        {
            Salesperson salesperson = _consoleView.DisplayEditAccount(_salesperson);
        }
        private static void WriteXMLFile(Salesperson salesperson, XmlServices _xml)
        {
            salesperson = XmlServices.WriteXMLFile(salesperson);
            
        }

        public static Salesperson ReadXmlFile(Salesperson salesperson)
        {
            salesperson = XmlServices.ReadXmlFile(salesperson);
            return salesperson;
        }
        #endregion
    }
}