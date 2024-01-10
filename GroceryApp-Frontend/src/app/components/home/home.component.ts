import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Product } from 'src/app/models/product';
import { ProductService } from 'src/app/service/Product-service/product.service'

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit {
  categories: string[] | undefined;
  selectedCategory: string = ''; 
  searchText: string = ''; 
  products!: any[] ; 
  filteredProducts: any[] | undefined; // Filtered products based on selected category and search keyword

  constructor(private productService : ProductService, private router :Router) { }

  ngOnInit(): void {
    //this.categories = ['Juice', 'Choaclate', 'Jam'];  
    this.getProducts(); 
  }

  getProducts(): void {
    this.productService.getAllProducts().subscribe((products: any[] ) => {
    this.products = products;
    this.filteredProducts = products;
    this.categories =  [...new Set(products.map(product => product.category))];
    console.log(this.products);
    this.filteredProducts = products;
    });
  }

  navigateToProductDetails(product: Product): void {
    this.router.navigate(['/product-details', product.productId]);
  }
  
  updateProducts() {
    this.filteredProducts = this.getFilteredProducts();
  }

  getFilteredProducts() {
    return this.products.filter(product => {
      let matchesSearchText = true;
      let matchesCategory = true;
      if(this.searchText) {
        matchesSearchText = product.name.includes(this.searchText) || product.description.includes(this.searchText);
      }
      if(this.selectedCategory) {
        matchesCategory = product.category === this.selectedCategory;
      }
      return matchesCategory && matchesSearchText;

    });

  }

}
