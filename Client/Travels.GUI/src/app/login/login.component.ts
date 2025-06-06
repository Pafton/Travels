import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../auth/auth.service';
import { AccountService } from '../Services/account.service';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  email = '';
  password = '';
  forgotEmail = '';
  resetToken = '';
  newPassword = '';
  resetMode = false;


  private http = inject(HttpClient);
  private authService = inject(AuthService);
  private router = inject(Router);
  private accountService = inject(AccountService)


  onSubmit() {
    const loginData = { email: this.email, password: this.password };

    this.accountService.login(loginData).subscribe({
      next: (token: string) => {
        this.authService.setToken(token);
        this.router.navigate(['/home']);
      },
      error: (err) => {
        console.error('Błąd logowania:', err);
        alert('Niepoprawny login/hasło lub nieaktywne konto');
      }
    });

  }

  goToForgotPassword() {
    this.router.navigate(['/forgot-password']);
  }
}
