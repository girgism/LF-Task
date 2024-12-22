import { Component, OnInit } from '@angular/core';
import {
  CategoryClient,
  GetCategoryDto,
} from '../../../core/services/ElectronicsAppClient';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-all-categories',
  standalone: false,

  templateUrl: './all-categories.component.html',
  styleUrl: './all-categories.component.css',
})
export class AllCategoriesComponent implements OnInit {
  categories: GetCategoryDto[] = [];
  constructor(private _categoryCliet: CategoryClient, private router: Router) {}
  ngOnInit(): void {
    this.getAllCategories();
  }

  getAllCategories() {
    this._categoryCliet.getAllCategories().subscribe({
      next: (response) => {
        this.categories = response;
      },
      error: (error) => {
        Swal.fire({
          title: 'Error',
          text: 'An Error while load the data',
          icon: 'error',
        });
      },
    });
  }
  addCategory() {
    this.router.navigate(['/admin/categories/add']);
  }

  editCategory(id: number) {
    this.router.navigate(['/admin/categories/update', id]);
  }

  deleteCategory(id: number) {
    if (confirm('Are you sure you want to delete this category?')) {
      this._categoryCliet.deleteCategory(id).subscribe({
        next: () => {
          Swal.fire({
            title: 'Delete?',
            text: 'That Category Deleted Sccessfully',
            icon: 'success',
          });
          this.getAllCategories();
        },
        error: (error) => {
          Swal.fire({
            title: 'Error',
            text: error,
            icon: 'error',
          });
          console.error('Error deleting category:', error);
        },
      });
    }
  }
}
