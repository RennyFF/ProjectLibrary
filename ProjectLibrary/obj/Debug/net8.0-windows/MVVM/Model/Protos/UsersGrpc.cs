// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: MVVM/Model/Protos/users.proto
// </auto-generated>
#pragma warning disable 0414, 1591, 8981, 0612
#region Designer generated code

using grpc = global::Grpc.Core;

namespace ProjectLibrary.Client.User {
  public static partial class UserService
  {
    static readonly string __ServiceName = "userproto.UserService";

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
    static readonly grpc::Marshaller<global::ProjectLibrary.Client.User.RequestAuthorize> __Marshaller_userproto_RequestAuthorize = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ProjectLibrary.Client.User.RequestAuthorize.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::ProjectLibrary.Client.User.ResponseAuthorize> __Marshaller_userproto_ResponseAuthorize = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ProjectLibrary.Client.User.ResponseAuthorize.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::ProjectLibrary.Client.User.RequestRegister> __Marshaller_userproto_RequestRegister = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ProjectLibrary.Client.User.RequestRegister.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::ProjectLibrary.Client.User.ResponseRegister> __Marshaller_userproto_ResponseRegister = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ProjectLibrary.Client.User.ResponseRegister.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::ProjectLibrary.Client.User.RequestCheckUnique> __Marshaller_userproto_RequestCheckUnique = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ProjectLibrary.Client.User.RequestCheckUnique.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::ProjectLibrary.Client.User.ResponseCheckUnique> __Marshaller_userproto_ResponseCheckUnique = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ProjectLibrary.Client.User.ResponseCheckUnique.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::ProjectLibrary.Client.User.RequestAuthorize, global::ProjectLibrary.Client.User.ResponseAuthorize> __Method_AuthorizeUser = new grpc::Method<global::ProjectLibrary.Client.User.RequestAuthorize, global::ProjectLibrary.Client.User.ResponseAuthorize>(
        grpc::MethodType.Unary,
        __ServiceName,
        "AuthorizeUser",
        __Marshaller_userproto_RequestAuthorize,
        __Marshaller_userproto_ResponseAuthorize);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::ProjectLibrary.Client.User.RequestRegister, global::ProjectLibrary.Client.User.ResponseRegister> __Method_RegisterUser = new grpc::Method<global::ProjectLibrary.Client.User.RequestRegister, global::ProjectLibrary.Client.User.ResponseRegister>(
        grpc::MethodType.Unary,
        __ServiceName,
        "RegisterUser",
        __Marshaller_userproto_RequestRegister,
        __Marshaller_userproto_ResponseRegister);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::ProjectLibrary.Client.User.RequestCheckUnique, global::ProjectLibrary.Client.User.ResponseCheckUnique> __Method_CheckUniqueField = new grpc::Method<global::ProjectLibrary.Client.User.RequestCheckUnique, global::ProjectLibrary.Client.User.ResponseCheckUnique>(
        grpc::MethodType.Unary,
        __ServiceName,
        "CheckUniqueField",
        __Marshaller_userproto_RequestCheckUnique,
        __Marshaller_userproto_ResponseCheckUnique);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::ProjectLibrary.Client.User.UsersReflection.Descriptor.Services[0]; }
    }

    /// <summary>Client for UserService</summary>
    public partial class UserServiceClient : grpc::ClientBase<UserServiceClient>
    {
      /// <summary>Creates a new client for UserService</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public UserServiceClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for UserService that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public UserServiceClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected UserServiceClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected UserServiceClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::ProjectLibrary.Client.User.ResponseAuthorize AuthorizeUser(global::ProjectLibrary.Client.User.RequestAuthorize request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return AuthorizeUser(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::ProjectLibrary.Client.User.ResponseAuthorize AuthorizeUser(global::ProjectLibrary.Client.User.RequestAuthorize request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_AuthorizeUser, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::ProjectLibrary.Client.User.ResponseAuthorize> AuthorizeUserAsync(global::ProjectLibrary.Client.User.RequestAuthorize request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return AuthorizeUserAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::ProjectLibrary.Client.User.ResponseAuthorize> AuthorizeUserAsync(global::ProjectLibrary.Client.User.RequestAuthorize request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_AuthorizeUser, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::ProjectLibrary.Client.User.ResponseRegister RegisterUser(global::ProjectLibrary.Client.User.RequestRegister request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return RegisterUser(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::ProjectLibrary.Client.User.ResponseRegister RegisterUser(global::ProjectLibrary.Client.User.RequestRegister request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_RegisterUser, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::ProjectLibrary.Client.User.ResponseRegister> RegisterUserAsync(global::ProjectLibrary.Client.User.RequestRegister request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return RegisterUserAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::ProjectLibrary.Client.User.ResponseRegister> RegisterUserAsync(global::ProjectLibrary.Client.User.RequestRegister request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_RegisterUser, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::ProjectLibrary.Client.User.ResponseCheckUnique CheckUniqueField(global::ProjectLibrary.Client.User.RequestCheckUnique request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return CheckUniqueField(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::ProjectLibrary.Client.User.ResponseCheckUnique CheckUniqueField(global::ProjectLibrary.Client.User.RequestCheckUnique request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_CheckUniqueField, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::ProjectLibrary.Client.User.ResponseCheckUnique> CheckUniqueFieldAsync(global::ProjectLibrary.Client.User.RequestCheckUnique request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return CheckUniqueFieldAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::ProjectLibrary.Client.User.ResponseCheckUnique> CheckUniqueFieldAsync(global::ProjectLibrary.Client.User.RequestCheckUnique request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_CheckUniqueField, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected override UserServiceClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new UserServiceClient(configuration);
      }
    }

  }
}
#endregion
