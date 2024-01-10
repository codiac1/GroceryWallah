export interface Product {
    productId: string;
    name: string;
    description: string;
    category: string;
    quantity : number;
    imageLink: string;
    price: number;
    discount?:number;
    specification?:string;
  }
  