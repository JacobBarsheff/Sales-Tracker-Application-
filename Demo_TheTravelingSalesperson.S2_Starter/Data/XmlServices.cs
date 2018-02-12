using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Demo_TheTravelingSalesperson
{
    public class XmlServices
    {
        public string dataPath = "data.xml";
        #region Methods

        
        public static Salesperson WriteXMLFile(Salesperson salesperson)
        {
            XmlSerializer serialiazer = new XmlSerializer(typeof(Salesperson));

            StreamWriter swriter = new StreamWriter("data.xml");

            using (swriter)
            {
                serialiazer.Serialize(swriter, salesperson);
            }
            return salesperson;
        }

        public static Salesperson ReadXmlFile(Salesperson salesperson)
        {


            XmlSerializer serialiazer = new XmlSerializer(typeof(Salesperson));

            StreamReader sReader = new StreamReader("data.xml");

            using (sReader)
            {
                Object xmlObject = serialiazer.Deserialize(sReader);
                salesperson = (Salesperson)xmlObject;
            }
            return salesperson;
        }
        #endregion
    }
}
