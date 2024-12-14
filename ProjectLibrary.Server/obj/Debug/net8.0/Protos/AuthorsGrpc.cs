// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/authors.proto
// </auto-generated>
#pragma warning disable 0414, 1591, 8981, 0612
#region Designer generated code

using grpc = global::Grpc.Core;

namespace ProjectLibrary.Server.Author {
  public static partial class AuthorService
  {
    static readonly string __ServiceName = "authorproto.AuthorService";

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::ProjectLibrary.Server.Author.RequestCountity> __Marshaller_authorproto_RequestCountity = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ProjectLibrary.Server.Author.RequestCountity.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::ProjectLibrary.Server.Author.ResponseCountity> __Marshaller_authorproto_ResponseCountity = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ProjectLibrary.Server.Author.ResponseCountity.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::ProjectLibrary.Server.Author.RequestAuthorsByPage> __Marshaller_authorproto_RequestAuthorsByPage = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ProjectLibrary.Server.Author.RequestAuthorsByPage.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::ProjectLibrary.Server.Author.ResponseAuthorsByPage> __Marshaller_authorproto_ResponseAuthorsByPage = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ProjectLibrary.Server.Author.ResponseAuthorsByPage.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::ProjectLibrary.Server.Author.RequestSingleAuthor> __Marshaller_authorproto_RequestSingleAuthor = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ProjectLibrary.Server.Author.RequestSingleAuthor.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::ProjectLibrary.Server.Author.ResponseSingleAuthor> __Marshaller_authorproto_ResponseSingleAuthor = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ProjectLibrary.Server.Author.ResponseSingleAuthor.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::ProjectLibrary.Server.Author.RequestCountity, global::ProjectLibrary.Server.Author.ResponseCountity> __Method_GetCountity = new grpc::Method<global::ProjectLibrary.Server.Author.RequestCountity, global::ProjectLibrary.Server.Author.ResponseCountity>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetCountity",
        __Marshaller_authorproto_RequestCountity,
        __Marshaller_authorproto_ResponseCountity);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::ProjectLibrary.Server.Author.RequestAuthorsByPage, global::ProjectLibrary.Server.Author.ResponseAuthorsByPage> __Method_GetAuthorsByPage = new grpc::Method<global::ProjectLibrary.Server.Author.RequestAuthorsByPage, global::ProjectLibrary.Server.Author.ResponseAuthorsByPage>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetAuthorsByPage",
        __Marshaller_authorproto_RequestAuthorsByPage,
        __Marshaller_authorproto_ResponseAuthorsByPage);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::ProjectLibrary.Server.Author.RequestSingleAuthor, global::ProjectLibrary.Server.Author.ResponseSingleAuthor> __Method_GetSingleAuthor = new grpc::Method<global::ProjectLibrary.Server.Author.RequestSingleAuthor, global::ProjectLibrary.Server.Author.ResponseSingleAuthor>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetSingleAuthor",
        __Marshaller_authorproto_RequestSingleAuthor,
        __Marshaller_authorproto_ResponseSingleAuthor);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::ProjectLibrary.Server.Author.AuthorsReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of AuthorService</summary>
    [grpc::BindServiceMethod(typeof(AuthorService), "BindService")]
    public abstract partial class AuthorServiceBase
    {
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::ProjectLibrary.Server.Author.ResponseCountity> GetCountity(global::ProjectLibrary.Server.Author.RequestCountity request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::ProjectLibrary.Server.Author.ResponseAuthorsByPage> GetAuthorsByPage(global::ProjectLibrary.Server.Author.RequestAuthorsByPage request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::ProjectLibrary.Server.Author.ResponseSingleAuthor> GetSingleAuthor(global::ProjectLibrary.Server.Author.RequestSingleAuthor request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static grpc::ServerServiceDefinition BindService(AuthorServiceBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_GetCountity, serviceImpl.GetCountity)
          .AddMethod(__Method_GetAuthorsByPage, serviceImpl.GetAuthorsByPage)
          .AddMethod(__Method_GetSingleAuthor, serviceImpl.GetSingleAuthor).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static void BindService(grpc::ServiceBinderBase serviceBinder, AuthorServiceBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_GetCountity, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::ProjectLibrary.Server.Author.RequestCountity, global::ProjectLibrary.Server.Author.ResponseCountity>(serviceImpl.GetCountity));
      serviceBinder.AddMethod(__Method_GetAuthorsByPage, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::ProjectLibrary.Server.Author.RequestAuthorsByPage, global::ProjectLibrary.Server.Author.ResponseAuthorsByPage>(serviceImpl.GetAuthorsByPage));
      serviceBinder.AddMethod(__Method_GetSingleAuthor, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::ProjectLibrary.Server.Author.RequestSingleAuthor, global::ProjectLibrary.Server.Author.ResponseSingleAuthor>(serviceImpl.GetSingleAuthor));
    }

  }
}
#endregion