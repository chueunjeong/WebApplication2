using Horizon.XmlRpc.Client;
using Horizon.XmlRpc.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Utils
{
    public interface IXml : IXmlRpcProxy
    {

        [XmlRpcMethod("translate")]
        Object SendRequest(XmlRpcStruct xrs);

        [XmlRpcMethod("multitranslate")]
        Object SendRequestMulti(XmlRpcStruct xrs);
    }
    public class XmlRpcAPI
    {
        public Object translateValue(XmlRpcStruct xrs, string url)
        {
            
            // create instance of XML-RPC interface
            IXml proxy = XmlRpcProxyGen.Create<IXml>();
           
            //요청할 url 설정
            proxy.Url = url;
            
            try
            {
                //xml 송신하고 결과 받기
                XmlRpcStruct response = (XmlRpcStruct)proxy.SendRequest(xrs);
               
                return response;

            }

            catch (Exception ex)
            {
                //Console.WriteLine(ex);
                throw ex;
                //return -1;
            }

        }
    }
}
