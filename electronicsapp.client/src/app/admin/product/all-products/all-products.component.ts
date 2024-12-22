import { Component, OnInit } from '@angular/core';
import {
  GetProductDto,
  ProductClient,
} from '../../../core/services/ElectronicsAppClient';
import Swal from 'sweetalert2';
import { Router } from '@angular/router';

@Component({
  selector: 'app-all-products',
  standalone: false,

  templateUrl: './all-products.component.html',
  styleUrl: './all-products.component.css',
})
export class AllProductsComponent implements OnInit {
  products: GetProductDto[] = [];

  constructor(private _productClient: ProductClient, private router: Router) {}

  ngOnInit(): void {
    this.getAllProducts();
  }

  getAllProducts() {
    this._productClient.getAllProducts().subscribe({
      next: (response) => {
        this.products = response;
      },
      error: (error) => {
        Swal.fire({
          title: 'Error',
          text: 'An error occurred while loading the data',
          icon: 'error',
        });
      },
    });
  }

  addProduct() {
    this.router.navigate(['/admin/products/add']);
  }
}
