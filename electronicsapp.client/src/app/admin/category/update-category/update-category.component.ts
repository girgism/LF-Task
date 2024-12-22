import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {
  CategoryClient,
  GetCategoryDto,
  UpdateCategoryDto,
} from '../../../core/services/ElectronicsAppClient';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-update-category',
  standalone: false,

  templateUrl: './update-category.component.html',
  styleUrl: './update-category.component.css',
})
export class UpdateCategoryComponent implements OnInit {
  categoryForm!: FormGroup;
  categoryId: number = 0;

  constructor(
    private fb: FormBuilder,
    private categoryClient: CategoryClient,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    this.categoryId = id !== null ? +id : 0;
    this.categoryForm = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
    });
    this.loadCategory();
  }

  loadCategory(): void {
    this.categoryClient.getCategoryById(this.categoryId).subscribe({
      next: (category: GetCategoryDto) => {
        this.categoryForm.patchValue({
          name: category.name,
          description: category.description,
        });
      },
      error: (error) => {
        console.error('Error loading category:', error);
      },
    });
  }

  onSubmit(): void {
    if (this.categoryForm.valid) {
      const updatedCategory: UpdateCategoryDto = {
        id: this.categoryId,
        ...this.categoryForm.value,
      };

      this.categoryClient.updateCategory(updatedCategory).subscribe({
        next: () => {
          Swal.fire('Success', 'Category updated successfully', 'success');
          this.router.navigate(['/admin/categories/all']);
        },
        error: (error) => {
          console.error('Error updating category:', error);
          Swal.fire(
            'Error',
            'An error occurred while updating the category',
            'error'
          );
        },
      });
    }
  }
}
