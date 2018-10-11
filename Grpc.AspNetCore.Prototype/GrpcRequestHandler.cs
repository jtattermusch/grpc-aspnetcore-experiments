using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Grpc.Testing;

namespace Grpc.AspNetCore.Prototype
{
    public class GrpcRequestHandler
    {
        public static async Task HandleRequest(HttpContext context)
        {
            var request = context.Request;
            DumpRequestDetails(request);
            
            // TODO(jtattermusch): check that content type is application/grpc

            // TODO: bind the service as part of initialization
            var serverServiceDefinition = TestServiceAspNetCoreBinder.BindService(new TestServiceImpl());

            var method = context.Request.Path;

            // From registered call handlers, choose the right one based on :path header
            // and invoke it.
            IServerCallHandler callHandler;
            if (serverServiceDefinition.CallHandlers.TryGetValue(method, out callHandler))
            {
                await callHandler.HandleCall(context);
            }
            else
            {
                // TODO(jtattermusch): grpc server invokes unimplemented method call handler here...
                Console.WriteLine("no handler available");
            }


            // while(true)
            // {
            //     var msg = await StreamUtils.ReadMessageAsync(context.Request.Body);

            //     if (msg != null)
            //     {
            //         Console.WriteLine("Read message of size " + msg.Length);

            //         //var parsedMsg = Grpc.Testing.SimpleRequest.Parser.ParseFrom(msg);
            //         //Console.WriteLine("Parsed message: " + parsedMsg);
            //         //Console.WriteLine("Parsed message: " + parsedMsg.Payload.Body.Length);
            //     }
            //     else
            //     {
            //         Console.WriteLine("No more request messages.");
            //         break;
            //     }
            // }
            
        }

        static void DumpRequestDetails(HttpRequest request)
        {
            // values of these fields are pretty much given for gRPC requests.
            Console.WriteLine("Method: " + request.Method);
            Console.WriteLine("Scheme: " + request.Scheme);
            Console.WriteLine("Protocol: " + request.Protocol);
            Console.WriteLine("ContentType: " + request.ContentType);

            // low priority: can be set explicitly by gRPC client, but usually the value is not important.
            Console.WriteLine("Host: " + request.Host);

            // For gRPC in format "/ServiceName/MethodName"
            Console.WriteLine("Path: " + request.Path);  // TODO: there's a PathBase too
            
            foreach (var headerEntry in request.Headers)
            {
                // only "User-Agent" is interesting here
                Console.WriteLine("header " + headerEntry.Key + " " + headerEntry.Value);
            }
        }
    }
}
