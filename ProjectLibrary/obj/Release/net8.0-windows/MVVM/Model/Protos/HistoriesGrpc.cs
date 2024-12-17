// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: MVVM/Model/Protos/histories.proto
// </auto-generated>
#pragma warning disable 0414, 1591, 8981, 0612
#region Designer generated code

using grpc = global::Grpc.Core;

namespace ProjectLibrary.Client.History {
  public static partial class HistoryService
  {
    static readonly string __ServiceName = "historyproto.HistoryService";

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
    static readonly grpc::Marshaller<global::ProjectLibrary.Client.History.RequestHistory> __Marshaller_historyproto_RequestHistory = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ProjectLibrary.Client.History.RequestHistory.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::ProjectLibrary.Client.History.ResponseHistory> __Marshaller_historyproto_ResponseHistory = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ProjectLibrary.Client.History.ResponseHistory.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::ProjectLibrary.Client.History.RequestHistory, global::ProjectLibrary.Client.History.ResponseHistory> __Method_GetHistoryCards = new grpc::Method<global::ProjectLibrary.Client.History.RequestHistory, global::ProjectLibrary.Client.History.ResponseHistory>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetHistoryCards",
        __Marshaller_historyproto_RequestHistory,
        __Marshaller_historyproto_ResponseHistory);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::ProjectLibrary.Client.History.HistoriesReflection.Descriptor.Services[0]; }
    }

    /// <summary>Client for HistoryService</summary>
    public partial class HistoryServiceClient : grpc::ClientBase<HistoryServiceClient>
    {
      /// <summary>Creates a new client for HistoryService</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public HistoryServiceClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for HistoryService that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public HistoryServiceClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected HistoryServiceClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected HistoryServiceClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::ProjectLibrary.Client.History.ResponseHistory GetHistoryCards(global::ProjectLibrary.Client.History.RequestHistory request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetHistoryCards(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::ProjectLibrary.Client.History.ResponseHistory GetHistoryCards(global::ProjectLibrary.Client.History.RequestHistory request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetHistoryCards, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::ProjectLibrary.Client.History.ResponseHistory> GetHistoryCardsAsync(global::ProjectLibrary.Client.History.RequestHistory request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetHistoryCardsAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::ProjectLibrary.Client.History.ResponseHistory> GetHistoryCardsAsync(global::ProjectLibrary.Client.History.RequestHistory request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetHistoryCards, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected override HistoryServiceClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new HistoryServiceClient(configuration);
      }
    }

  }
}
#endregion