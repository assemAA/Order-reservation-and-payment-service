// package: 
// file: src/app/protos/OrderService.proto

var src_app_protos_OrderService_pb = require("../../../src/app/protos/OrderService_pb");
var grpc = require("@improbable-eng/grpc-web").grpc;

var OrderService = (function () {
  function OrderService() {}
  OrderService.serviceName = "OrderService";
  return OrderService;
}());

OrderService.PostOrder = {
  methodName: "PostOrder",
  service: OrderService,
  requestStream: false,
  responseStream: false,
  requestType: src_app_protos_OrderService_pb.Order,
  responseType: src_app_protos_OrderService_pb.PostResponse
};

exports.OrderService = OrderService;

function OrderServiceClient(serviceHost, options) {
  this.serviceHost = serviceHost;
  this.options = options || {};
}

OrderServiceClient.prototype.postOrder = function postOrder(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(OrderService.PostOrder, {
    request: requestMessage,
    host: this.serviceHost,
    metadata: metadata,
    transport: this.options.transport,
    debug: this.options.debug,
    onEnd: function (response) {
      if (callback) {
        if (response.status !== grpc.Code.OK) {
          var err = new Error(response.statusMessage);
          err.code = response.status;
          err.metadata = response.trailers;
          callback(err, null);
        } else {
          callback(null, response.message);
        }
      }
    }
  });
  return {
    cancel: function () {
      callback = null;
      client.close();
    }
  };
};

exports.OrderServiceClient = OrderServiceClient;

