using System;
using System.IO;
using System.Collections.Generic;
using Schemas;
using System.Linq;
using System.Xml;
using Newtonsoft.Json;

namespace FormulatrixOnlineTest
{
    public class Program: IFormulatrix, IDisposable
    {
        //Storage Contexts 
        public Dictionary<DataItem, object> itemsContext;       

        public void Initialize()
        {
            itemsContext = new Dictionary<DataItem, object>();
        }

        public void Register(string itemName, string itemContent, int itemType)
        {
            DataItem dataItem = new DataItem();
            dataItem.itemName = itemName;
            dataItem.itemType = itemType;
            dataItem.itemContent = itemContent;

            DataItemViewModel dataItemModel = new DataItemViewModel();
            dataItemModel.dataItem = dataItem;
            dataItemModel.itemsContext = itemsContext;

            if (dataItemModel.validate())
            {
                //Set new storage context
                itemsContext = dataItemModel.itemsContext;                
            }                                 
        }

        public string Retrieve(string itemName)
        {
            string strResult=string.Empty;

            if (this.itemsContext.Count() > 0)
            {
                var itemContext = this.itemsContext.Where(a => a.Key.itemName == itemName);

                if (itemContext.Count() > 0)
                {
                    var doc = itemContext.First();
                    if (doc.Value != null)
                    {
                        if (doc.Key.itemType == 1)
                        {
                            strResult= JsonConvert.SerializeObject(doc.Value);
                        }
                        else if (doc.Key.itemType == 2)
                        {
                            XmlDocument xmlDoc = (XmlDocument)doc.Value;
                            StringWriter sw = new StringWriter();
                            XmlTextWriter xw = new XmlTextWriter(sw);
                            xmlDoc.WriteTo(xw);
                            strResult= sw.ToString();
                        }
                    }
                }
                else
                {
                    throw new Exception("Data not found");
                }
            }            

            return strResult;
        }

        public int GetType(string itemName)
        {
            int intResult =0;

            if (this.itemsContext.Count() > 0)
            {
                var itemContext = this.itemsContext.Where(a => a.Key.itemName == itemName);

                if (itemContext.Count() > 0)
                {
                    intResult = itemContext.First().Key.itemType;
                }
                else
                {
                    throw new Exception("Data not found");
                }
            }

            return intResult;
        }

        public void Deregister(string itemName)
        {
            if (this.itemsContext.Count() > 0)
            {
                var itemContext = this.itemsContext.Where(a => a.Key.itemName == itemName);

                if (itemContext.Count() > 0)
                {
                    var temp = itemContext.First();
                    this.itemsContext.Remove(temp.Key);
                }
                else
                {
                    throw new Exception("Data not found");
                }
            }
        }

        public void Dispose()
        {
            itemsContext.Clear();
        }
    }
}
