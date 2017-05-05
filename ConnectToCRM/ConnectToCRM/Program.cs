using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.ServiceModel.Description;
using System.Linq;
using SendMessage;
using System.Threading;

namespace ConnectToCRM
{
    class Program
    {
        public static void Main(string[] args)
        {

            ClientCredentials clientCredentials = new ClientCredentials();
            clientCredentials.UserName.UserName = "ankverma19@inifinite.onmicrosoft.com";
            clientCredentials.UserName.Password = "race@2007";
            Uri serviceUri = new Uri("https://inifinite.api.crm.dynamics.com/XRMServices/2011/Organization.svc");
            OrganizationServiceProxy proxy = new OrganizationServiceProxy(serviceUri, null, clientCredentials, null);
            proxy.EnableProxyTypes();
            IOrganizationService serviceProxy = proxy;
            Guid userid = ((WhoAmIResponse)serviceProxy.Execute(new WhoAmIRequest())).UserId;
            Console.WriteLine(userid);
            OrganizationServiceContext orgContext = new OrganizationServiceContext(serviceProxy);
            var data = from a in orgContext.CreateQuery("new_vendor_details_json") select a["new_sms_settings"];
            foreach (var settings in  data)
            {
                new_vendor vendor = new new_vendor();
                vendor.Id = Guid.NewGuid();
                vendor.new_vendorId = Guid.NewGuid();
                vendor.new_Vendor_Authorization = "Test";
                vendor.new_Vendor_Credentials = "Test";
                vendor.new_Vendor_Name = "Test";
                //vendor.new_Vendor_Request_Mode = new OptionSetValue(1);
                vendor.new_Vendor_Status = true;
                vendor.new_Vendor_URL = settings.ToString();
                vendor.new_vendeos = "Test Vendor";
                Guid vendorId  = serviceProxy.Create(vendor);
                Console.WriteLine("Vendor Details Obtained [ " + vendorId + " ] ");
            }
            Thread.Sleep(5000);
            Console.ReadKey();
        }
    }
}
