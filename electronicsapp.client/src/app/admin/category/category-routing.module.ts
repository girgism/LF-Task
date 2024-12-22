import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AllCategoriesComponent } from './all-categories/all-categories.component';
import { AddCategoryComponent } from './add-category/add-category.component';
import { UpdateCategoryComponent } from './update-category/update-category.component';

const routes: Routes = [
  { path: 'all', component: AllCategoriesComponent },
  { path: 'add', component: AddCategoryComponent },
  { path: 'update/:id', component: UpdateCategoryComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CategoryRoutingModule {}
