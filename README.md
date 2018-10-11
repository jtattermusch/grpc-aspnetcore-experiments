# grpc-aspnetcore-experiments
Experimental repo for gRPC + ASP.NET core integration

Shows how to handle gRPC requests with ASP.NET core.

gRPC protocol on top of HTTP/2 is described here:
https://github.com/grpc/grpc/blob/master/doc/PROTOCOL-HTTP2.md


### Running against interop client

Interop tests are used by gRPC team to evaluate interoperability between different gRPC implementations.
As such, they are ideal for testing compliance level for ASP.NET core bindings
https://github.com/grpc/grpc/blob/master/doc/interop-test-descriptions.md

Build interop client for gRPC C# (other language client can be used too)
```
tools/run_tests/run_tests.py -l csharp -c dbg --build_only
src/csharp/Grpc.IntegrationTesting.Client/bin/Debug/netcoreapp1.0
```

Run interop client with the simplest scenario `empty_unary`. Not using TLS yet.
```
dotnet exec Grpc.IntegrationTesting.Client.dll --server_host localhost --server_port 8080 --test_case empty_unary
```
