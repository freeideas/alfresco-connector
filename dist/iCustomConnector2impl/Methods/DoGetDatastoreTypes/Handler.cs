using System;
using System.Collections;
using SPWorks.Search.Service.Interfaces;

namespace AiGeneratedConnector.Methods.DoGetDatastoreTypes
{
    public static class Handler
    {
        public static DataStoreTypeReturn DoGetDatastoreTypes(ConnectionInfo conn, Hashtable customparams, DataStoreInfo datastore)
        {
            try
            {
                return Hardcode.GetDatastoreTypes(conn, customparams, datastore);
            }
            catch (Exception ex)
            {
                return new DataStoreTypeReturn
                {
                    error = true,
                    errorMsg = $"Error in DoGetDatastoreTypes: {ex.Message}",
                    datastoretypes = Array.Empty<DataStoreType>()
                };
            }
        }


        private static bool DoGetDatastoreTypes_TEST_()
        {
            var conn = new ConnectionInfo();
            var customparams = new Hashtable();
            var datastore = new DataStoreInfo();
            
            var result = DoGetDatastoreTypes(conn, customparams, datastore);
            
            if (result.error) return false;
            if (result.datastoretypes == null || result.datastoretypes.Length == 0) return false;
            
            // Verify first datastore type
            var contentType = result.datastoretypes[0];
            if (contentType.id != "cm:content") return false;
            if (contentType.name != "Content") return false;
            if (contentType.typedata == null || contentType.typedata.Length == 0) return false;
            
            // Verify task type exists
            bool foundTask = false;
            foreach (var ds in result.datastoretypes)
            {
                if (ds.id == "bpm:task")
                {
                    foundTask = true;
                    if (ds.name != "Task") return false;
                    break;
                }
            }
            if (!foundTask) return false;
            
            return true;
        }


        private static bool DoGetDatastoreTypes_With_Null_Params_TEST_()
        {
            var result = DoGetDatastoreTypes(null, null, null);
            
            // Should still return valid data even with null parameters in hardcode mode
            if (result.error) return false;
            if (result.datastoretypes == null || result.datastoretypes.Length == 0) return false;
            
            return true;
        }


        public static bool RunTests() { return AiGeneratedConnector.Services.LibTest.TestClass(typeof(Handler)); }
    }
}