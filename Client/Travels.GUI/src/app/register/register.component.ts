import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from '../Services/account.service';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule, HttpClientModule],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  name = '';
  surname = '';
  email = '';
  password = '';

  constructor(
    private accountService: AccountService,
    private router: Router
  ) { }

  register(event: Event) {
    event.preventDefault();

    const userData = {
      name: this.name,
      surname: this.surname,
      email: this.email,
      password: this.password
    };

    console.log('Rejestruję:', userData);

    this.accountService.register(userData).subscribe({
      next: () => {
        alert('Rejestracja powiodła się. Możesz się teraz zalogować.');
        this.router.navigate(['/login']);
      },
      error: (err) => {
        console.error('Błąd rejestracji:', err);
        alert(err.text || 'Coś poszło nie tak podczas rejestracji.');
      }
    });
  }
}
