export class Cart {
    cartId: string;
    userId: string;
    productId: string;
    quantity: number;
    
    constructor(cartId: string, userId: string, productId: string, quantity: number) {
      this.cartId = cartId;
      this.userId = userId;
      this.productId = productId;
      this.quantity = quantity;
    }
  }
  