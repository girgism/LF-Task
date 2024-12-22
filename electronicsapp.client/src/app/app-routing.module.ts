import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { authGuard } from './shared/services/auth.guard';
import { LoginComponent } from './admin/login/login.component';

const routes: Routes = [
  { path: '', component: LoginComponent },
  {
    path: 'admin',
    loadChildren: () =>
      import('./admin/admin.module').then((m) => m.AdminModule),
  },
  {
    path: 'admin/categories',
    canActivate: [authGuard],
    loadChildren: () =>
      import('./admin/category/category.module').then((m) => m.CategoryModule),
  },
  {
    path: 'admin/products',
    canActivate: [authGuard],
    loadChildren: () =>
      import('./admin/product/product.module').then((m) => m.ProductModule),
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
