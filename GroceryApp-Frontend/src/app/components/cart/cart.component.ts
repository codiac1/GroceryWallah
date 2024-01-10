import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/models/User';
import { Cart } from 'src/app/models/cartModel';
import { Product } from 'src/app/models/product';
import { AuthService } from 'src/app/service/Auth-service/auth.service';
import { CartService } from 'src/app/service/Cart-services/cart-service.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  cartProducts: Product[] = [];
  userId: string = "";
  myOrderItems: any[] = [];

  constructor(private cartService: CartService, private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
     this.userId = this.authService.getUserIdFromToken();
     this.getCartItems(this.userId);
  }

  getCartItems(userId: string): void {
    this.cartService.getAllCartItems(userId)
      .subscribe((items: Product[]) => {
        this.cartProducts = items;
        console.log(this.cartProducts);
      });
  }

  updateQuantity(productId: string, quantity: number): void {
    var cartRow: Cart = {
      cartId: "00000000-0000-0000-0000-000000000000",
      userId: this.userId,
      productId: productId,
      quantity: quantity
    }
    this.cartService.updateQuantity(cartRow)
      .subscribe(() => {
        this.getCartItems(this.userId);
      });
  }

  removeFromCart(productId: string): void {
    var cartRow: Cart = {
      cartId: "00000000-0000-0000-0000-000000000000",
      userId: this.userId,
      productId: productId,
      quantity: 0
    }
    this.cartService.removeProductFromCart(cartRow)
      .subscribe(() => {
        this.getCartItems(this.userId);
      });
  }

  placeOrder(){
    this.cartService.placeOrder(this.userId,this.cartProducts ).subscribe(()=>{   
    })
    for (var i =0 ; i< this.cartProducts.length ; i++){
      this.removeFromCart(this.cartProducts[i].productId);
    }
    
    alert("Order Successful !");
    this.router.navigate(["/myOrders"]);
  }

}
