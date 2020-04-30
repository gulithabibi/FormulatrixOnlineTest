using System;

namespace Schemas
{
    public interface IFormulatrix
    {        
        void Register(string itemName, string itemContent, int itemType);
        string Retrieve(string itemName);
        int GetType(string itemName);
        void Deregister(string itemName);
        void Initialize();
    }
}
