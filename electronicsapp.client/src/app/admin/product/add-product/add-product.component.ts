import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {
  AddNewProductDto,
  CategoryClient,
  GetCategoryDto,
  ProductClient,
} from '../../../core/services/ElectronicsAppClient';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';

type NewType = OnInit;

@Component({
  selector: 'app-add-product',
  standalone: false,

  templateUrl: './add-product.component.html',
  styleUrl: './add-product.component.css',
})
export class AddProductComponent implements NewType {
  productForm!: FormGroup;
  categories: GetCategoryDto[] = [];

  constructor(
    private fb: FormBuilder,
    private productClient: ProductClient,
    private categoryClient: CategoryClient,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.productForm = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      price: ['', [Validators.required, Validators.pattern('^[0-9]*$')]],
      categoryId: ['', Validators.required],
    });

    this.loadCategories();
  }

  loadCategories(): void {
    this.categoryClient.getAllCategories().subscribe({
      next: (response) => {
        this.categories = response;
      },
      error: (error) => {
        console.error('Error loading categories:', error);
      },
    });
  }

  onSubmit(): void {
    if (this.productForm.valid) {
      const newProduct: AddNewProductDto = this.productForm.value;
      this.productClient.addNewProduct(newProduct).subscribe({
        next: () => {
          Swal.fire('Success', 'Product added successfully', 'success');
          this.router.navigate(['/admin/products/all']);
        },
        error: (error) => {
          console.error('Error adding product:', error);
          Swal.fire(
            'Error',
            'An error occurred while adding the product',
            'error'
          );
        },
      });
    }
  }
}
