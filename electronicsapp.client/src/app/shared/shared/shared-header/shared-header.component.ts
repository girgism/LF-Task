import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.services';

@Component({
  selector: 'app-shared-header',
  standalone: false,

  templateUrl: './shared-header.component.html',
  styleUrl: './shared-header.component.css',
})
export class SharedHeaderComponent implements OnInit {
  constructor(private _authService: AuthService) {}
  ngOnInit(): void {}

  logOut() {
    this._authService.logout();
  }
}
