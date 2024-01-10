import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/service/Auth-service/auth.service';
import { CartService } from 'src/app/service/Cart-services/cart-service.service';

@Component({
  selector: 'app-my-orders',
  templateUrl: './my-orders.component.html',
  styleUrls: ['./my-orders.component.css']
})
export class MyordersComponent implements OnInit {

  orders : any[] = []

  userId : string = ''

  constructor(private cartService : CartService,private authService : AuthService) {}

  ngOnInit(): void {
      this.userId = this.authService.getUserIdFromToken();
      this.cartService.myOrders(this.userId).subscribe(orders => {
        this.orders = orders;
        console.log(this.orders);
      })
      this.orders.sort((a, b) => new Date(b.date).getTime() - new Date(a.date).getTime());
  }
  
}

