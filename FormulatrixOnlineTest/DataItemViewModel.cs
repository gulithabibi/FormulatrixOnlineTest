using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Xml;

namespace FormulatrixOnlineTest
{
    public class DataItemViewModel
    {
        public DataItem dataItem { get; set; }
        public object objContent { get; set; }
        public Dictionary<DataItem, object> itemsContext { get; set; }
        public Boolean validate()
        {
            Boolean blnResult = false;
            try
            {
                if (dataItem != null)
                {
                    if (!isDuplicated())
                    {
                        if (dataItem.itemType == 1)
                        {
                            this.objContent = JsonConvert.DeserializeObject(dataItem.itemContent);
                        }
                        else if (dataItem.itemType == 2)
                        {
                            XmlDocument xmlDoc = new XmlDocument();
                            xmlDoc.LoadXml("<root>" + dataItem.itemContent + "</root>");
                            this.objContent = xmlDoc;
                        }                        
                        this.itemsContext.Add(dataItem, objContent);
                        blnResult = true;
                    }
                    else
                    {
                        throw new Exception("Data duplicated !");
                    }
                }
                else
                {
                    throw new Exception("Data item null");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return blnResult;
        }
        private bool isDuplicated()
        {
            bool blnResult = true;
            if (this.itemsContext!=null && this.itemsContext.Count > 0)
            {
                if (this.itemsContext.Where(a => a.Key.itemName == dataItem.itemName && a.Key.itemType == dataItem.itemType).Count() == 0)
                {
                    blnResult = false;
                }
            }
            else
            {                
                blnResult = false;
            }

            return blnResult;
        }
    }
}
