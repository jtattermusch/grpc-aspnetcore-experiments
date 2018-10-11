using System;
using System.Net;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Grpc.Core;
using Grpc.Core.Testing;

namespace Grpc.AspNetCore.Prototype
{
    // this file inspired by https://github.com/grpc/grpc/blob/master/src/csharp/Grpc.Core/Internal/ServerCallHandler.cs

    public interface IServerCallHandler
    {
        Task HandleCall(HttpContext httpContext);
    }

    public class UnaryServerCallHandler<TRequest, TResponse> : IServerCallHandler
        where TRequest : class
        where TResponse : class
    {
        readonly Method<TRequest, TResponse> method;
        readonly UnaryServerMethod<TRequest, TResponse> handler;

        public UnaryServerCallHandler(Method<TRequest, TResponse> method, UnaryServerMethod<TRequest, TResponse> handler)
        {
            this.method = method;
            this.handler = handler;
        }

        public async Task HandleCall(HttpContext httpContext)
        {
            httpContext.Response.ContentType = "application/grpc";
            httpContext.Response.Headers.Append("grpc-encoding", "identity");

            var requestPayload = await StreamUtils.ReadMessageAsync(httpContext.Request.Body);
            // TODO: make sure the payload is not null
            var request =  method.RequestMarshaller.Deserializer(requestPayload);

            // TODO: make sure there are no more request messages.
            
            var serverCallContext = HandlerUtils.NewContext();
            var response = await handler(request, serverCallContext);
            
            // TODO: make sure the response is not null
            var responsePayload = method.ResponseMarshaller.Serializer(response);

            await StreamUtils.WriteMessageAsync(httpContext.Response.Body, responsePayload, 0, responsePayload.Length);

            httpContext.Response.AppendTrailer("grpc-status", ((int) StatusCode.OK).ToString());
        }

    }

    // TODO(jtattermusch): add handlers for client streaming, server streaming and duplex streaming too.

    internal static class HandlerUtils
    {
        public static ServerCallContext NewContext()
        {
            // TODO: populate the context
            return TestServerCallContext.Create(null, null, default(DateTime), new Metadata(), CancellationToken.None,
            null, null, null, null, null, null);
            //Create(string method, string host, DateTime deadline, Metadata requestHeaders, CancellationToken cancellationToken,
            //string peer, AuthContext authContext, ContextPropagationToken contextPropagationToken,
            //Func<Metadata, Task> writeHeadersFunc, Func<WriteOptions> writeOptionsGetter, Action<WriteOptions> writeOptionsSetter)


            
        }
    }
}
