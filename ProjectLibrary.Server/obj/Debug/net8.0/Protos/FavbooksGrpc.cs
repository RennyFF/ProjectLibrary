// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/favbooks.proto
// </auto-generated>
#pragma warning disable 0414, 1591, 8981, 0612
#region Designer generated code

using grpc = global::Grpc.Core;

namespace ProjectLibrary.Server.FavoriteBook {
  public static partial class FavoriteBookService
  {
    static readonly string __ServiceName = "favbookproto.FavoriteBookService";

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
    static readonly grpc::Marshaller<global::ProjectLibrary.Server.FavoriteBook.RequestCountity> __Marshaller_favbookproto_RequestCountity = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ProjectLibrary.Server.FavoriteBook.RequestCountity.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::ProjectLibrary.Server.FavoriteBook.ResponseCountity> __Marshaller_favbookproto_ResponseCountity = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ProjectLibrary.Server.FavoriteBook.ResponseCountity.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::ProjectLibrary.Server.FavoriteBook.RequestCheckFavoriteBook> __Marshaller_favbookproto_RequestCheckFavoriteBook = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ProjectLibrary.Server.FavoriteBook.RequestCheckFavoriteBook.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::ProjectLibrary.Server.FavoriteBook.ResponseCheckFavoriteBook> __Marshaller_favbookproto_ResponseCheckFavoriteBook = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ProjectLibrary.Server.FavoriteBook.ResponseCheckFavoriteBook.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::ProjectLibrary.Server.FavoriteBook.RequestChangeFavoriteBook> __Marshaller_favbookproto_RequestChangeFavoriteBook = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ProjectLibrary.Server.FavoriteBook.RequestChangeFavoriteBook.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Google.Protobuf.WellKnownTypes.Empty> __Marshaller_google_protobuf_Empty = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Google.Protobuf.WellKnownTypes.Empty.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::ProjectLibrary.Server.FavoriteBook.RequestFavoriteBookByUser> __Marshaller_favbookproto_RequestFavoriteBookByUser = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ProjectLibrary.Server.FavoriteBook.RequestFavoriteBookByUser.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::ProjectLibrary.Server.FavoriteBook.ResponseFavoriteBookByUser> __Marshaller_favbookproto_ResponseFavoriteBookByUser = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ProjectLibrary.Server.FavoriteBook.ResponseFavoriteBookByUser.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::ProjectLibrary.Server.FavoriteBook.RequestCountity, global::ProjectLibrary.Server.FavoriteBook.ResponseCountity> __Method_GetCountity = new grpc::Method<global::ProjectLibrary.Server.FavoriteBook.RequestCountity, global::ProjectLibrary.Server.FavoriteBook.ResponseCountity>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetCountity",
        __Marshaller_favbookproto_RequestCountity,
        __Marshaller_favbookproto_ResponseCountity);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::ProjectLibrary.Server.FavoriteBook.RequestCheckFavoriteBook, global::ProjectLibrary.Server.FavoriteBook.ResponseCheckFavoriteBook> __Method_CheckFavoriteBook = new grpc::Method<global::ProjectLibrary.Server.FavoriteBook.RequestCheckFavoriteBook, global::ProjectLibrary.Server.FavoriteBook.ResponseCheckFavoriteBook>(
        grpc::MethodType.Unary,
        __ServiceName,
        "CheckFavoriteBook",
        __Marshaller_favbookproto_RequestCheckFavoriteBook,
        __Marshaller_favbookproto_ResponseCheckFavoriteBook);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::ProjectLibrary.Server.FavoriteBook.RequestChangeFavoriteBook, global::Google.Protobuf.WellKnownTypes.Empty> __Method_ChangeFavoriteBook = new grpc::Method<global::ProjectLibrary.Server.FavoriteBook.RequestChangeFavoriteBook, global::Google.Protobuf.WellKnownTypes.Empty>(
        grpc::MethodType.Unary,
        __ServiceName,
        "ChangeFavoriteBook",
        __Marshaller_favbookproto_RequestChangeFavoriteBook,
        __Marshaller_google_protobuf_Empty);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::ProjectLibrary.Server.FavoriteBook.RequestFavoriteBookByUser, global::ProjectLibrary.Server.FavoriteBook.ResponseFavoriteBookByUser> __Method_GetFavoriteBooksByUser = new grpc::Method<global::ProjectLibrary.Server.FavoriteBook.RequestFavoriteBookByUser, global::ProjectLibrary.Server.FavoriteBook.ResponseFavoriteBookByUser>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetFavoriteBooksByUser",
        __Marshaller_favbookproto_RequestFavoriteBookByUser,
        __Marshaller_favbookproto_ResponseFavoriteBookByUser);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::ProjectLibrary.Server.FavoriteBook.FavbooksReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of FavoriteBookService</summary>
    [grpc::BindServiceMethod(typeof(FavoriteBookService), "BindService")]
    public abstract partial class FavoriteBookServiceBase
    {
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::ProjectLibrary.Server.FavoriteBook.ResponseCountity> GetCountity(global::ProjectLibrary.Server.FavoriteBook.RequestCountity request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::ProjectLibrary.Server.FavoriteBook.ResponseCheckFavoriteBook> CheckFavoriteBook(global::ProjectLibrary.Server.FavoriteBook.RequestCheckFavoriteBook request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::Google.Protobuf.WellKnownTypes.Empty> ChangeFavoriteBook(global::ProjectLibrary.Server.FavoriteBook.RequestChangeFavoriteBook request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::ProjectLibrary.Server.FavoriteBook.ResponseFavoriteBookByUser> GetFavoriteBooksByUser(global::ProjectLibrary.Server.FavoriteBook.RequestFavoriteBookByUser request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static grpc::ServerServiceDefinition BindService(FavoriteBookServiceBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_GetCountity, serviceImpl.GetCountity)
          .AddMethod(__Method_CheckFavoriteBook, serviceImpl.CheckFavoriteBook)
          .AddMethod(__Method_ChangeFavoriteBook, serviceImpl.ChangeFavoriteBook)
          .AddMethod(__Method_GetFavoriteBooksByUser, serviceImpl.GetFavoriteBooksByUser).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static void BindService(grpc::ServiceBinderBase serviceBinder, FavoriteBookServiceBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_GetCountity, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::ProjectLibrary.Server.FavoriteBook.RequestCountity, global::ProjectLibrary.Server.FavoriteBook.ResponseCountity>(serviceImpl.GetCountity));
      serviceBinder.AddMethod(__Method_CheckFavoriteBook, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::ProjectLibrary.Server.FavoriteBook.RequestCheckFavoriteBook, global::ProjectLibrary.Server.FavoriteBook.ResponseCheckFavoriteBook>(serviceImpl.CheckFavoriteBook));
      serviceBinder.AddMethod(__Method_ChangeFavoriteBook, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::ProjectLibrary.Server.FavoriteBook.RequestChangeFavoriteBook, global::Google.Protobuf.WellKnownTypes.Empty>(serviceImpl.ChangeFavoriteBook));
      serviceBinder.AddMethod(__Method_GetFavoriteBooksByUser, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::ProjectLibrary.Server.FavoriteBook.RequestFavoriteBookByUser, global::ProjectLibrary.Server.FavoriteBook.ResponseFavoriteBookByUser>(serviceImpl.GetFavoriteBooksByUser));
    }

  }
}
#endregion
