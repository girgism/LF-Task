import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import {
  AddCategoryDto,
  CategoryClient,
} from '../../../core/services/ElectronicsAppClient';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-add-category',
  standalone: false,

  templateUrl: './add-category.component.html',
  styleUrl: './add-category.component.css',
})
export class AddCategoryComponent implements OnInit {
  categoryForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private categoryClient: CategoryClient,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.categoryForm = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
    });
  }

  onSubmit(): void {
    if (this.categoryForm.valid) {
      const newCategory: AddCategoryDto = this.categoryForm.value;
      this.categoryClient.addNewCategory(newCategory).subscribe({
        next: () => {
          Swal.fire({
            title: 'Added',
            text: 'The Category Added',
            icon: 'success',
          });
          this.router.navigate(['/admin/categories/all']);
        },
        error: (error) => {
          Swal.fire({
            title: 'Error',
            text: error,
            icon: 'error',
          });
          console.error('Error creating category:', error);
        },
      });
    }
  }
}
