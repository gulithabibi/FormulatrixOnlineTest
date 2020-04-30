using System;
using FormulatrixOnlineTest;
using Schemas;
namespace UnitTest
{
    public class Test
    {
        static IFormulatrix formulatrix;
        static void Main(string[] args)
        {
            formulatrix = new Program();
            formulatrix.Initialize();

            Test unitTest = new Test();

            unitTest.TestRegister("1. Register new data json","Item1", "{\"to\":\"gulit@gmail.com\",\"title\":\"register json\",\"body\":\"Online test formulatrix\"}", 1);
            unitTest.TestRegister("2. Register new data xml", "Item1", "<to>gulit@gmail.com</to><title>Register XML</title><body>Online test formulatrix</body>", 2);
            unitTest.TestRegister("3. Register new data with duplicated itemName", "Item1", "{\"to\":\"habibi@gmail.com\",\"title\":\"register json Duplicated\",\"body\":\"Online test formulatrix\"}", 1);

            unitTest.TestRetrieve("4. Retrieve data", "Item1");
            unitTest.TestGetType("5. Get type data", "Item1");
            unitTest.TestDeregister("6. Deregister data ", "Item1");
        }

        public void TestRegister(string title,string itemName, string itemContent, int itemType)
        {
            Console.WriteLine(title);
            Console.WriteLine("ItemName : "+itemName);
            Console.WriteLine("ItemType : "+itemType);
            Console.WriteLine("ItemContent : "+itemContent);            

            bool result = false;
            try
            {                
                formulatrix.Register(itemName, itemContent,itemType);                
                result = true;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Message : "+ex.Message);                
            }
            finally
            {
                if (result == true)              
                {
                    Console.WriteLine("Message : Register data success");
                }
            }
            Console.WriteLine(" ");
        }

        public void TestRetrieve(string title, string itemName)
        {
            Console.WriteLine(title);
            Console.WriteLine("ItemName : " + itemName);

            string strResult = string.Empty;
            bool blnResult = false;
            try
            {
                strResult=formulatrix.Retrieve(itemName);
                blnResult = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Message : "+ex.Message);
            }
            finally
            {
                if (blnResult == true)
                {
                    Console.WriteLine("Message : Retrieve data success");
                    Console.WriteLine("ItemContent : "+strResult);
                }
            }
            Console.WriteLine(" ");
        }

        public void TestGetType(string title, string itemName)
        {
            Console.WriteLine(title);
            Console.WriteLine("ItemName : " + itemName);

            int intResult = 0;
            bool blnResult = false;
            try
            {
                intResult = formulatrix.GetType(itemName);
                blnResult = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Message : "+ex.Message);
            }
            finally
            {
                if (blnResult == true)
                {
                    Console.WriteLine("Message : GetType data success, ItemType : "+intResult);                    
                }
            }
            Console.WriteLine(" ");
        }

        public void TestDeregister(string title, string itemName)
        {
            Console.WriteLine(title);
            Console.WriteLine("ItemName : " + itemName);
            
            bool blnResult = false;
            try
            {
                formulatrix.Deregister(itemName);
                blnResult = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Message : "+ex.Message);
            }
            finally
            {
                if (blnResult == true)
                {
                    Console.WriteLine("Message : Deregister data success");                    
                }
            }
            Console.WriteLine(" ");
        }


    }
}
