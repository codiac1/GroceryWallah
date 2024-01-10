import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Cart } from 'src/app/models/cartModel';

import { environment } from 'src/environments/environment.development';
import { Product } from 'src/app/models/product';
import { AuthService } from 'src/app/service/Auth-service/auth.service';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  apiUrl = environment.apiUrl;
  cartProductCount: number = 0;

  constructor(private http: HttpClient, private authService: AuthService) {}

  getAllCartItems(userId: string): Observable<Product[]> {
    const newCart: Cart = {
      cartId: '00000000-0000-0000-0000-000000000000',
      userId: userId,
      productId: '00000000-0000-0000-0000-000000000000',
      quantity: 0
    };
    //console.log('Inside CartService id', userId);
    return this.http.post<Product[]>(`${this.apiUrl}/api/Cart/GetItems`, newCart);
  }

  getCartItemCount(): number {
    return this.cartProductCount;
  }

  addProductToCart(newCart: Cart): Observable<any> {
    this.cartProductCount += newCart.quantity;
    return this.http.post(`${this.apiUrl}/api/Cart/addProduct`, newCart);
  }

  updateQuantity(cartRow: Cart): Observable<any> {
    // this.cartProductCount -= cartRow.quantity;
    return this.http.put(`${this.apiUrl}/api/Cart/UpdateQuantity`, cartRow);
  }

  removeProductFromCart(cart: Cart): Observable<any> {
    this.cartProductCount -= cart.quantity;
    return this.http.delete(`${this.apiUrl}/api/Cart/deleteItem`, { body: cart });
  }

  placeOrder(userId : string,allProducts : Product[]) : Observable<string>{
   
    return this.http.post<string>(this.apiUrl + '/cart/placeOrder',{userId,allProducts});
  }

  myOrders(userId : string) : Observable<any[]>{
    return this.http.get<any[]>(`${this.apiUrl}/api/cart/?userId=${userId}`);
  }
}
