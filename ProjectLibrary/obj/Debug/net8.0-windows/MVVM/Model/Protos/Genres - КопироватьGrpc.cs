// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: MVVM/Model/Protos/genres - Копировать.proto
// </auto-generated>
#pragma warning disable 0414, 1591, 8981, 0612
#region Designer generated code

using grpc = global::Grpc.Core;

namespace ProjectLibrary.Client.Genre {
  public static partial class GenreService
  {
    static readonly string __ServiceName = "genreproto.GenreService";

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
    static readonly grpc::Marshaller<global::ProjectLibrary.Client.Genre.RequestCountity> __Marshaller_genreproto_RequestCountity = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ProjectLibrary.Client.Genre.RequestCountity.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::ProjectLibrary.Client.Genre.ResponseCountity> __Marshaller_genreproto_ResponseCountity = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ProjectLibrary.Client.Genre.ResponseCountity.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::ProjectLibrary.Client.Genre.RequestGenresByPage> __Marshaller_genreproto_RequestGenresByPage = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ProjectLibrary.Client.Genre.RequestGenresByPage.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::ProjectLibrary.Client.Genre.ResponseGenresByPage> __Marshaller_genreproto_ResponseGenresByPage = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ProjectLibrary.Client.Genre.ResponseGenresByPage.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Google.Protobuf.WellKnownTypes.Empty> __Marshaller_google_protobuf_Empty = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Google.Protobuf.WellKnownTypes.Empty.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::ProjectLibrary.Client.Genre.ResponseGenreNames> __Marshaller_genreproto_ResponseGenreNames = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ProjectLibrary.Client.Genre.ResponseGenreNames.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::ProjectLibrary.Client.Genre.RequestSingleGenre> __Marshaller_genreproto_RequestSingleGenre = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ProjectLibrary.Client.Genre.RequestSingleGenre.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::ProjectLibrary.Client.Genre.ResponseSingleGenre> __Marshaller_genreproto_ResponseSingleGenre = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ProjectLibrary.Client.Genre.ResponseSingleGenre.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::ProjectLibrary.Client.Genre.RequestCountity, global::ProjectLibrary.Client.Genre.ResponseCountity> __Method_GetCountity = new grpc::Method<global::ProjectLibrary.Client.Genre.RequestCountity, global::ProjectLibrary.Client.Genre.ResponseCountity>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetCountity",
        __Marshaller_genreproto_RequestCountity,
        __Marshaller_genreproto_ResponseCountity);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::ProjectLibrary.Client.Genre.RequestGenresByPage, global::ProjectLibrary.Client.Genre.ResponseGenresByPage> __Method_GetGenresByPage = new grpc::Method<global::ProjectLibrary.Client.Genre.RequestGenresByPage, global::ProjectLibrary.Client.Genre.ResponseGenresByPage>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetGenresByPage",
        __Marshaller_genreproto_RequestGenresByPage,
        __Marshaller_genreproto_ResponseGenresByPage);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Google.Protobuf.WellKnownTypes.Empty, global::ProjectLibrary.Client.Genre.ResponseGenreNames> __Method_GetAllGenreNames = new grpc::Method<global::Google.Protobuf.WellKnownTypes.Empty, global::ProjectLibrary.Client.Genre.ResponseGenreNames>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetAllGenreNames",
        __Marshaller_google_protobuf_Empty,
        __Marshaller_genreproto_ResponseGenreNames);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::ProjectLibrary.Client.Genre.RequestSingleGenre, global::ProjectLibrary.Client.Genre.ResponseSingleGenre> __Method_GetSingleGenre = new grpc::Method<global::ProjectLibrary.Client.Genre.RequestSingleGenre, global::ProjectLibrary.Client.Genre.ResponseSingleGenre>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetSingleGenre",
        __Marshaller_genreproto_RequestSingleGenre,
        __Marshaller_genreproto_ResponseSingleGenre);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::ProjectLibrary.Client.Genre.GenresReflection.Descriptor.Services[0]; }
    }

    /// <summary>Client for GenreService</summary>
    public partial class GenreServiceClient : grpc::ClientBase<GenreServiceClient>
    {
      /// <summary>Creates a new client for GenreService</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public GenreServiceClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for GenreService that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public GenreServiceClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected GenreServiceClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected GenreServiceClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::ProjectLibrary.Client.Genre.ResponseCountity GetCountity(global::ProjectLibrary.Client.Genre.RequestCountity request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetCountity(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::ProjectLibrary.Client.Genre.ResponseCountity GetCountity(global::ProjectLibrary.Client.Genre.RequestCountity request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetCountity, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::ProjectLibrary.Client.Genre.ResponseCountity> GetCountityAsync(global::ProjectLibrary.Client.Genre.RequestCountity request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetCountityAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::ProjectLibrary.Client.Genre.ResponseCountity> GetCountityAsync(global::ProjectLibrary.Client.Genre.RequestCountity request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetCountity, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::ProjectLibrary.Client.Genre.ResponseGenresByPage GetGenresByPage(global::ProjectLibrary.Client.Genre.RequestGenresByPage request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetGenresByPage(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::ProjectLibrary.Client.Genre.ResponseGenresByPage GetGenresByPage(global::ProjectLibrary.Client.Genre.RequestGenresByPage request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetGenresByPage, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::ProjectLibrary.Client.Genre.ResponseGenresByPage> GetGenresByPageAsync(global::ProjectLibrary.Client.Genre.RequestGenresByPage request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetGenresByPageAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::ProjectLibrary.Client.Genre.ResponseGenresByPage> GetGenresByPageAsync(global::ProjectLibrary.Client.Genre.RequestGenresByPage request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetGenresByPage, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::ProjectLibrary.Client.Genre.ResponseGenreNames GetAllGenreNames(global::Google.Protobuf.WellKnownTypes.Empty request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetAllGenreNames(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::ProjectLibrary.Client.Genre.ResponseGenreNames GetAllGenreNames(global::Google.Protobuf.WellKnownTypes.Empty request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetAllGenreNames, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::ProjectLibrary.Client.Genre.ResponseGenreNames> GetAllGenreNamesAsync(global::Google.Protobuf.WellKnownTypes.Empty request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetAllGenreNamesAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::ProjectLibrary.Client.Genre.ResponseGenreNames> GetAllGenreNamesAsync(global::Google.Protobuf.WellKnownTypes.Empty request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetAllGenreNames, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::ProjectLibrary.Client.Genre.ResponseSingleGenre GetSingleGenre(global::ProjectLibrary.Client.Genre.RequestSingleGenre request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetSingleGenre(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::ProjectLibrary.Client.Genre.ResponseSingleGenre GetSingleGenre(global::ProjectLibrary.Client.Genre.RequestSingleGenre request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetSingleGenre, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::ProjectLibrary.Client.Genre.ResponseSingleGenre> GetSingleGenreAsync(global::ProjectLibrary.Client.Genre.RequestSingleGenre request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetSingleGenreAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::ProjectLibrary.Client.Genre.ResponseSingleGenre> GetSingleGenreAsync(global::ProjectLibrary.Client.Genre.RequestSingleGenre request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetSingleGenre, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected override GenreServiceClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new GenreServiceClient(configuration);
      }
    }

  }
}
#endregion
