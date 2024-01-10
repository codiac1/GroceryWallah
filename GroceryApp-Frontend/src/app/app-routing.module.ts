import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { SignupComponent } from './components/sign-up/sign-up.component';
import { ProductDetailsComponent } from './components/product-details/product-details.component';
import { CartComponent } from './components/cart/cart.component';
import { MyordersComponent } from './components/my-orders/my-orders.component';

const routes: Routes = [
  {path : "" , component : HomeComponent},
  {path : "home", component : HomeComponent},
  {path : "login" , component : LoginComponent},
  {path : "signup" , component : SignupComponent},
  {path: 'product-details/:productId', component: ProductDetailsComponent },
  {path: 'cart', component: CartComponent},
  {path: 'myOrders', component: MyordersComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
