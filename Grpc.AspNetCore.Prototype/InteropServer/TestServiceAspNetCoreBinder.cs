using System;
using System.Net;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using grpc = global::Grpc.Core;

namespace Grpc.Testing
{
    // normally  ServerServiceDefinition definition = TestService.BindService(new TestServiceImpl())
    // is used to bind gRPC services to servers, but ServerServiceDefinition's contents are gRPC specific
    // and aren't exposed publicly. This class is basically copy-pasted code from TestGrpc.cs
    // TODO(jtattermusch): come up with a better solution than manually copy-pasting code
    public class TestServiceAspNetCoreBinder
    {
        // this provides binding between an implementation of the code-generated server side stub for a service
        // and ASP.NET Core RequestHandler
        public static Grpc.AspNetCore.Prototype.ServerServiceDefinition BindService(global::Grpc.Testing.TestService.TestServiceBase serviceImpl)
        {
            // TODO: use a different type so it's usable for ASP.NET core bindings too
            return Grpc.AspNetCore.Prototype.ServerServiceDefinition.CreateBuilder()
                .AddMethod(__Method_EmptyCall, serviceImpl.EmptyCall)
                .AddMethod(__Method_UnaryCall, serviceImpl.UnaryCall)
                .AddMethod(__Method_CacheableUnaryCall, serviceImpl.CacheableUnaryCall)
                //.AddMethod(__Method_StreamingOutputCall, serviceImpl.StreamingOutputCall)
                //.AddMethod(__Method_StreamingInputCall, serviceImpl.StreamingInputCall)
                //.AddMethod(__Method_FullDuplexCall, serviceImpl.FullDuplexCall)
                //.AddMethod(__Method_HalfDuplexCall, serviceImpl.HalfDuplexCall)
                .AddMethod(__Method_UnimplementedCall, serviceImpl.UnimplementedCall).Build();
        }

        public static readonly string __ServiceName = "grpc.testing.TestService";

        public static readonly grpc::Marshaller<global::Grpc.Testing.Empty> __Marshaller_grpc_testing_Empty = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Grpc.Testing.Empty.Parser.ParseFrom);
        public static readonly grpc::Marshaller<global::Grpc.Testing.SimpleRequest> __Marshaller_grpc_testing_SimpleRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Grpc.Testing.SimpleRequest.Parser.ParseFrom);
        public static readonly grpc::Marshaller<global::Grpc.Testing.SimpleResponse> __Marshaller_grpc_testing_SimpleResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Grpc.Testing.SimpleResponse.Parser.ParseFrom);
        public static readonly grpc::Marshaller<global::Grpc.Testing.StreamingOutputCallRequest> __Marshaller_grpc_testing_StreamingOutputCallRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Grpc.Testing.StreamingOutputCallRequest.Parser.ParseFrom);
        public static readonly grpc::Marshaller<global::Grpc.Testing.StreamingOutputCallResponse> __Marshaller_grpc_testing_StreamingOutputCallResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Grpc.Testing.StreamingOutputCallResponse.Parser.ParseFrom);
        public static readonly grpc::Marshaller<global::Grpc.Testing.StreamingInputCallRequest> __Marshaller_grpc_testing_StreamingInputCallRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Grpc.Testing.StreamingInputCallRequest.Parser.ParseFrom);
        public static readonly grpc::Marshaller<global::Grpc.Testing.StreamingInputCallResponse> __Marshaller_grpc_testing_StreamingInputCallResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Grpc.Testing.StreamingInputCallResponse.Parser.ParseFrom);

        public static readonly grpc::Method<global::Grpc.Testing.Empty, global::Grpc.Testing.Empty> __Method_EmptyCall = new grpc::Method<global::Grpc.Testing.Empty, global::Grpc.Testing.Empty>(
            grpc::MethodType.Unary,
            __ServiceName,
            "EmptyCall",
            __Marshaller_grpc_testing_Empty,
            __Marshaller_grpc_testing_Empty);

        public static readonly grpc::Method<global::Grpc.Testing.SimpleRequest, global::Grpc.Testing.SimpleResponse> __Method_UnaryCall = new grpc::Method<global::Grpc.Testing.SimpleRequest, global::Grpc.Testing.SimpleResponse>(
            grpc::MethodType.Unary,
            __ServiceName,
            "UnaryCall",
            __Marshaller_grpc_testing_SimpleRequest,
            __Marshaller_grpc_testing_SimpleResponse);

        public static readonly grpc::Method<global::Grpc.Testing.SimpleRequest, global::Grpc.Testing.SimpleResponse> __Method_CacheableUnaryCall = new grpc::Method<global::Grpc.Testing.SimpleRequest, global::Grpc.Testing.SimpleResponse>(
            grpc::MethodType.Unary,
            __ServiceName,
            "CacheableUnaryCall",
            __Marshaller_grpc_testing_SimpleRequest,
            __Marshaller_grpc_testing_SimpleResponse);

        public static readonly grpc::Method<global::Grpc.Testing.StreamingOutputCallRequest, global::Grpc.Testing.StreamingOutputCallResponse> __Method_StreamingOutputCall = new grpc::Method<global::Grpc.Testing.StreamingOutputCallRequest, global::Grpc.Testing.StreamingOutputCallResponse>(
            grpc::MethodType.ServerStreaming,
            __ServiceName,
            "StreamingOutputCall",
            __Marshaller_grpc_testing_StreamingOutputCallRequest,
            __Marshaller_grpc_testing_StreamingOutputCallResponse);

        public static readonly grpc::Method<global::Grpc.Testing.StreamingInputCallRequest, global::Grpc.Testing.StreamingInputCallResponse> __Method_StreamingInputCall = new grpc::Method<global::Grpc.Testing.StreamingInputCallRequest, global::Grpc.Testing.StreamingInputCallResponse>(
            grpc::MethodType.ClientStreaming,
            __ServiceName,
            "StreamingInputCall",
            __Marshaller_grpc_testing_StreamingInputCallRequest,
            __Marshaller_grpc_testing_StreamingInputCallResponse);

        public static readonly grpc::Method<global::Grpc.Testing.StreamingOutputCallRequest, global::Grpc.Testing.StreamingOutputCallResponse> __Method_FullDuplexCall = new grpc::Method<global::Grpc.Testing.StreamingOutputCallRequest, global::Grpc.Testing.StreamingOutputCallResponse>(
            grpc::MethodType.DuplexStreaming,
            __ServiceName,
            "FullDuplexCall",
            __Marshaller_grpc_testing_StreamingOutputCallRequest,
            __Marshaller_grpc_testing_StreamingOutputCallResponse);

        public static readonly grpc::Method<global::Grpc.Testing.StreamingOutputCallRequest, global::Grpc.Testing.StreamingOutputCallResponse> __Method_HalfDuplexCall = new grpc::Method<global::Grpc.Testing.StreamingOutputCallRequest, global::Grpc.Testing.StreamingOutputCallResponse>(
            grpc::MethodType.DuplexStreaming,
            __ServiceName,
            "HalfDuplexCall",
            __Marshaller_grpc_testing_StreamingOutputCallRequest,
            __Marshaller_grpc_testing_StreamingOutputCallResponse);

        public static readonly grpc::Method<global::Grpc.Testing.Empty, global::Grpc.Testing.Empty> __Method_UnimplementedCall = new grpc::Method<global::Grpc.Testing.Empty, global::Grpc.Testing.Empty>(
            grpc::MethodType.Unary,
            __ServiceName,
            "UnimplementedCall",
            __Marshaller_grpc_testing_Empty,
            __Marshaller_grpc_testing_Empty);

    }
}
