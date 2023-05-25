// package: 
// file: src/app/protos/OrderService.proto

import * as jspb from "google-protobuf";

export class Order extends jspb.Message {
  getCustomerid(): number;
  setCustomerid(value: number): void;

  getProductid(): number;
  setProductid(value: number): void;

  getQuantity(): number;
  setQuantity(value: number): void;

  getOrderprice(): number;
  setOrderprice(value: number): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): Order.AsObject;
  static toObject(includeInstance: boolean, msg: Order): Order.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: Order, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): Order;
  static deserializeBinaryFromReader(message: Order, reader: jspb.BinaryReader): Order;
}

export namespace Order {
  export type AsObject = {
    customerid: number,
    productid: number,
    quantity: number,
    orderprice: number,
  }
}

export class PostResponse extends jspb.Message {
  getSubmitted(): boolean;
  setSubmitted(value: boolean): void;

  getMessage(): string;
  setMessage(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): PostResponse.AsObject;
  static toObject(includeInstance: boolean, msg: PostResponse): PostResponse.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: PostResponse, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): PostResponse;
  static deserializeBinaryFromReader(message: PostResponse, reader: jspb.BinaryReader): PostResponse;
}

export namespace PostResponse {
  export type AsObject = {
    submitted: boolean,
    message: string,
  }
}

