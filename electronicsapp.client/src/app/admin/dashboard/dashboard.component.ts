import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../shared/services/auth.services';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  standalone: false,

  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css',
})
export class DashboardComponent implements OnInit {
  constructor(
    private router: Router,
    private _authService: AuthService,
    private _router: Router
  ) {}
  ngOnInit(): void {
    const currentUser = this._authService.isLoggedIn();
    if (!currentUser) {
      this._router.navigate(['/admin/login']);
    }
  }
  navigateToCategories(): void {
    this.router.navigate(['/admin/categories/all']);
  }

  navigateToProducts(): void {
    this.router.navigate(['/admin/products/all']);
  }
}
