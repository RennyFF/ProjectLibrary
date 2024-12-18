// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/searchs.proto
// </auto-generated>
#pragma warning disable 0414, 1591, 8981, 0612
#region Designer generated code

using grpc = global::Grpc.Core;

namespace ProjectLibrary.Server.Search {
  public static partial class SearchService
  {
    static readonly string __ServiceName = "searchproto.SearchService";

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
    static readonly grpc::Marshaller<global::ProjectLibrary.Server.Search.ReqSCountity> __Marshaller_searchproto_ReqSCountity = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ProjectLibrary.Server.Search.ReqSCountity.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::ProjectLibrary.Server.Search.ResSCountity> __Marshaller_searchproto_ResSCountity = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ProjectLibrary.Server.Search.ResSCountity.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::ProjectLibrary.Server.Search.ReqPagination> __Marshaller_searchproto_ReqPagination = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ProjectLibrary.Server.Search.ReqPagination.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::ProjectLibrary.Server.Search.ResBooksPagination> __Marshaller_searchproto_ResBooksPagination = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ProjectLibrary.Server.Search.ResBooksPagination.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::ProjectLibrary.Server.Search.ResAuthorsPagination> __Marshaller_searchproto_ResAuthorsPagination = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ProjectLibrary.Server.Search.ResAuthorsPagination.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::ProjectLibrary.Server.Search.ResGenresPagination> __Marshaller_searchproto_ResGenresPagination = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ProjectLibrary.Server.Search.ResGenresPagination.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::ProjectLibrary.Server.Search.ReqSCountity, global::ProjectLibrary.Server.Search.ResSCountity> __Method_SearchCountityBook = new grpc::Method<global::ProjectLibrary.Server.Search.ReqSCountity, global::ProjectLibrary.Server.Search.ResSCountity>(
        grpc::MethodType.Unary,
        __ServiceName,
        "SearchCountityBook",
        __Marshaller_searchproto_ReqSCountity,
        __Marshaller_searchproto_ResSCountity);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::ProjectLibrary.Server.Search.ReqSCountity, global::ProjectLibrary.Server.Search.ResSCountity> __Method_SearchCountityGenre = new grpc::Method<global::ProjectLibrary.Server.Search.ReqSCountity, global::ProjectLibrary.Server.Search.ResSCountity>(
        grpc::MethodType.Unary,
        __ServiceName,
        "SearchCountityGenre",
        __Marshaller_searchproto_ReqSCountity,
        __Marshaller_searchproto_ResSCountity);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::ProjectLibrary.Server.Search.ReqSCountity, global::ProjectLibrary.Server.Search.ResSCountity> __Method_SearchCountityAuthor = new grpc::Method<global::ProjectLibrary.Server.Search.ReqSCountity, global::ProjectLibrary.Server.Search.ResSCountity>(
        grpc::MethodType.Unary,
        __ServiceName,
        "SearchCountityAuthor",
        __Marshaller_searchproto_ReqSCountity,
        __Marshaller_searchproto_ResSCountity);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::ProjectLibrary.Server.Search.ReqPagination, global::ProjectLibrary.Server.Search.ResBooksPagination> __Method_SearchBookPagination = new grpc::Method<global::ProjectLibrary.Server.Search.ReqPagination, global::ProjectLibrary.Server.Search.ResBooksPagination>(
        grpc::MethodType.Unary,
        __ServiceName,
        "SearchBookPagination",
        __Marshaller_searchproto_ReqPagination,
        __Marshaller_searchproto_ResBooksPagination);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::ProjectLibrary.Server.Search.ReqPagination, global::ProjectLibrary.Server.Search.ResAuthorsPagination> __Method_SearchAuthorPagination = new grpc::Method<global::ProjectLibrary.Server.Search.ReqPagination, global::ProjectLibrary.Server.Search.ResAuthorsPagination>(
        grpc::MethodType.Unary,
        __ServiceName,
        "SearchAuthorPagination",
        __Marshaller_searchproto_ReqPagination,
        __Marshaller_searchproto_ResAuthorsPagination);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::ProjectLibrary.Server.Search.ReqPagination, global::ProjectLibrary.Server.Search.ResGenresPagination> __Method_SearchGenrePagination = new grpc::Method<global::ProjectLibrary.Server.Search.ReqPagination, global::ProjectLibrary.Server.Search.ResGenresPagination>(
        grpc::MethodType.Unary,
        __ServiceName,
        "SearchGenrePagination",
        __Marshaller_searchproto_ReqPagination,
        __Marshaller_searchproto_ResGenresPagination);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::ProjectLibrary.Server.Search.SearchsReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of SearchService</summary>
    [grpc::BindServiceMethod(typeof(SearchService), "BindService")]
    public abstract partial class SearchServiceBase
    {
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::ProjectLibrary.Server.Search.ResSCountity> SearchCountityBook(global::ProjectLibrary.Server.Search.ReqSCountity request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::ProjectLibrary.Server.Search.ResSCountity> SearchCountityGenre(global::ProjectLibrary.Server.Search.ReqSCountity request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::ProjectLibrary.Server.Search.ResSCountity> SearchCountityAuthor(global::ProjectLibrary.Server.Search.ReqSCountity request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::ProjectLibrary.Server.Search.ResBooksPagination> SearchBookPagination(global::ProjectLibrary.Server.Search.ReqPagination request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::ProjectLibrary.Server.Search.ResAuthorsPagination> SearchAuthorPagination(global::ProjectLibrary.Server.Search.ReqPagination request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::ProjectLibrary.Server.Search.ResGenresPagination> SearchGenrePagination(global::ProjectLibrary.Server.Search.ReqPagination request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static grpc::ServerServiceDefinition BindService(SearchServiceBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_SearchCountityBook, serviceImpl.SearchCountityBook)
          .AddMethod(__Method_SearchCountityGenre, serviceImpl.SearchCountityGenre)
          .AddMethod(__Method_SearchCountityAuthor, serviceImpl.SearchCountityAuthor)
          .AddMethod(__Method_SearchBookPagination, serviceImpl.SearchBookPagination)
          .AddMethod(__Method_SearchAuthorPagination, serviceImpl.SearchAuthorPagination)
          .AddMethod(__Method_SearchGenrePagination, serviceImpl.SearchGenrePagination).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static void BindService(grpc::ServiceBinderBase serviceBinder, SearchServiceBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_SearchCountityBook, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::ProjectLibrary.Server.Search.ReqSCountity, global::ProjectLibrary.Server.Search.ResSCountity>(serviceImpl.SearchCountityBook));
      serviceBinder.AddMethod(__Method_SearchCountityGenre, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::ProjectLibrary.Server.Search.ReqSCountity, global::ProjectLibrary.Server.Search.ResSCountity>(serviceImpl.SearchCountityGenre));
      serviceBinder.AddMethod(__Method_SearchCountityAuthor, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::ProjectLibrary.Server.Search.ReqSCountity, global::ProjectLibrary.Server.Search.ResSCountity>(serviceImpl.SearchCountityAuthor));
      serviceBinder.AddMethod(__Method_SearchBookPagination, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::ProjectLibrary.Server.Search.ReqPagination, global::ProjectLibrary.Server.Search.ResBooksPagination>(serviceImpl.SearchBookPagination));
      serviceBinder.AddMethod(__Method_SearchAuthorPagination, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::ProjectLibrary.Server.Search.ReqPagination, global::ProjectLibrary.Server.Search.ResAuthorsPagination>(serviceImpl.SearchAuthorPagination));
      serviceBinder.AddMethod(__Method_SearchGenrePagination, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::ProjectLibrary.Server.Search.ReqPagination, global::ProjectLibrary.Server.Search.ResGenresPagination>(serviceImpl.SearchGenrePagination));
    }

  }
}
#endregion
