import { Component } from '@angular/core';
import { Order } from './generated/src/app/protos/OrderService_pb';
import { grpc } from '@improbable-eng/grpc-web';
import { OrderService } from './generated/src/app/protos/OrderService_pb_service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'gRPC';
  Message = '';
  Error = false;
  OrderSubmitHandler(e: any) {
    e.preventDefault();
    let orderMsg = new Order();
    orderMsg.setCustomerid(e.target.CustomerId.value);
    orderMsg.setProductid(e.target.ProductId.value);
    orderMsg.setQuantity(e.target.Quantity.value);
    orderMsg.setOrderprice(e.target.Price.value);

    grpc.unary(OrderService.PostOrder, {
      request: orderMsg,
      host: 'https://localhost:7121',
      onEnd: (result) => {
        console.log(result);
        const { submitted, message } = result.message?.toObject() as {
          submitted: Boolean;
          message: string;
        };
        if (!submitted) {
          this.Error = false;
        }
        this.Message = message;
      },
    });
  }
}
