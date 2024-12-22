import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CategoryRoutingModule } from './category-routing.module';
import { AllCategoriesComponent } from './all-categories/all-categories.component';
import { AddCategoryComponent } from './add-category/add-category.component';
import { UpdateCategoryComponent } from './update-category/update-category.component';
import { SharedModule } from '../../shared/shared/shared.module';
import { CategoryClient } from '../../core/services/ElectronicsAppClient';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AllCategoriesComponent,
    AddCategoryComponent,
    UpdateCategoryComponent,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule,
    CategoryRoutingModule,
    SharedModule,
  ],
  providers: [CategoryClient],
})
export class CategoryModule {}
