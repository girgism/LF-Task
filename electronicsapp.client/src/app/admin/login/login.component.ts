import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../shared/services/auth.services';
import { Router } from '@angular/router';
import { LoginRequest } from '../../core/services/ElectronicsAppClient';

@Component({
  selector: 'app-login',
  standalone: false,

  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent implements OnInit {
  loginFormGroup!: FormGroup;
  errorMessage: string = '';
  error: boolean = false;
  constructor(
    private fb: FormBuilder,
    private _http: HttpClient,
    private _authService: AuthService,
    private _router: Router
  ) {}

  ngOnInit(): void {
    this.loginFormGroup = this.fb.group({
      userName: ['', Validators.required],
      password: ['', Validators.required],
      rememberMe: [false, Validators.required],
    });
    const currentUser = this._authService.isLoggedIn();
    if (currentUser) {
      this._router.navigate(['/admin/dashboard']);
    }
  }

  onSubmit(): void {
    if (this.loginFormGroup.valid) {
      this.errorMessage = '';
      this.LoginUser(this.loginFormGroup.value);
    } else {
      this.errorMessage = 'requiredFieldsMessage';
    }
  }
  LoginUser(formValues: any) {
    var email = formValues.userName;
    var password = formValues.password;
    var loginCredentials = new LoginRequest();
    loginCredentials.email = email;
    loginCredentials.password = password;

    this._authService.login(loginCredentials).subscribe({
      next: (res) => {
        this._router.navigate(['/admin/dashboard']);
      },
      error: (err) => {
        console.log(err);
      },
    });
  }
}
