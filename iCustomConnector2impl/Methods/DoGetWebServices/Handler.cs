using System;
using System.Linq;
using SPWorks.Search.Service.Interfaces;

namespace AiGeneratedConnector.Methods.DoGetWebServices
{
    public static class Handler
    {
        public static WebServiceInfo DoGetWebServices()
        {
            try
            {
                return Hardcode.GetWebServices();
            }
            catch (Exception ex)
            {
                return new WebServiceInfo
                {
                    services = Array.Empty<CustomPair>()
                };
            }
        }


        private static bool DoGetWebServices_TEST_()
        {
            var result = DoGetWebServices();
            
            if (result == null) return false;
            if (result.services == null) return false;
            
            return true;
        }


        private static bool DoGetWebServices_Empty_Services_TEST_()
        {
            var result = DoGetWebServices();
            
            if (result == null) return false;
            if (result.services == null) return false;
            if (result.services.Length != 0) return false;
            
            return true;
        }


        private static bool DoGetWebServices_Services_Array_Type_TEST_()
        {
            var result = DoGetWebServices();
            
            if (result == null) return false;
            if (result.services == null) return false;
            if (!(result.services is CustomPair[])) return false;
            
            return true;
        }


        private static bool DoGetWebServices_Never_Null_TEST_()
        {
            for (int i = 0; i < 5; i++)
            {
                var result = DoGetWebServices();
                if (result == null) return false;
                if (result.services == null) return false;
            }
            
            return true;
        }


        public static bool RunTests() { return AiGeneratedConnector.Services.LibTest.TestClass(typeof(Handler)); }
    }
}