import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Router } from '@angular/router';
import { AuthService } from '../auth/auth.service';

@Component({
  selector: 'app-login',
  imports: [CommonModule, FormsModule, HttpClientModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  email = '';
  password = '';

  constructor(
    private http: HttpClient,
    private authService: AuthService,
    private router: Router
  ) {}

  onSubmit() {
    const loginData = { email: this.email, password: this.password };

    this.http.post<{ token: string }>('http://localhost:5190/api/Account/login', loginData)
      .subscribe({
        next: (response) => {
          this.authService.setToken(response.token);
          this.router.navigate(['/home']);
        },
        error: (err) => {
          console.error('Błąd logowania:', err);
          alert('Niepoprawny login lub hasło');
        }
      });
  }
}
