import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/service/Auth-service/auth.service';
import { CartService } from 'src/app/service/Cart-services/cart-service.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {
//LoggedIn:boolean = false;
constructor(private router: Router, private authService: AuthService, private cartService: CartService){}

login():void{
  this.router.navigate(['/login']);
}

signup():void{
  this.router.navigate(['/signup']);
}

isLoggedIn(): boolean {
  return this.authService.isLoggedIn();
}

getUsername(): string {
  return this.authService.getUsernameFromToken();
}

logout(): void {
  this.authService.logout()
}

getCartItemCount(): number{
  return this.cartService.getCartItemCount(); 
}

viewCart():void{
  this.router.navigate(['/cart']);
}

myOrder(){
  this.router.navigate(['/myOrders'])
}
}
