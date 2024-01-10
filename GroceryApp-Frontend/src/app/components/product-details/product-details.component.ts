import { Component } from '@angular/core';
import { Product } from 'src/app/models/product';
import { ProductService } from 'src/app/service/Product-service/product.service';
import { ActivatedRoute } from '@angular/router';
import { CartService } from 'src/app/service/Cart-services/cart-service.service';
import { AuthService } from 'src/app/service/Auth-service/auth.service';
import { Cart } from 'src/app/models/cartModel';
import { Router } from '@angular/router';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent {
  product!: Product;
  userId: string = "";
  quantity = 1;

  constructor(
    private authService: AuthService,
    private route: ActivatedRoute,
    private productService: ProductService,
    private cartService: CartService,
    private router: Router
  ) {}

  ngOnInit() {
    this.userId = this.authService.getUserIdFromToken();
    this.route.paramMap.subscribe(params => {
      const productId = params.get('productId');
      if (productId) {
        this.productService.getProductById(productId).subscribe(
          (product: Product) => {
            this.product = product;
            console.log(product);
          },
          (error: any) => {
            console.error(error);
          }
        );
      } else {
        console.log("Invalid productId");
      }
    });
  }

  decreaseQuantity() {
    if (this.quantity > 1) {
      this.quantity--;
    }
  }

  increaseQuantity() {
    this.quantity++;
  }

  addToCart() {
    if (this.authService.isLoggedIn()) {
      var newCart: Cart = {
        cartId: "00000000-0000-0000-0000-000000000000",
        userId: this.userId,
        productId: this.product.productId,
        quantity: this.quantity,
      };
      this.cartService.addProductToCart(newCart).subscribe(() => {
        this.cartService.getAllCartItems(this.userId).subscribe(() => {
          this.router.navigate(['/cart']);
        });
      });
    } else {
      this.router.navigate(['/login']);
    }
  }
}
