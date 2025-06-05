import { Component } from '@angular/core';
import { AccountService } from '../Services/account.service';
import { CommonModule, NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-forgot-password',
  imports: [FormsModule, NgIf],
  templateUrl: './forgot-password-component.component.html',
  styleUrls: ['./forgot-password-component.component.css']
})
export class ForgotPasswordComponent {
  email = '';
  token = '';
  newPassword = '';
  tokenSent = false;

  constructor(private accountService: AccountService) { }

  sendToken() {
    this.accountService.sendPasswordResetLink(this.email).subscribe({
      next: () => {
        alert('Token wysłany na e-mail');
        this.tokenSent = true;
      },
      error: () => alert('Nie udało się wysłać tokena')
    });
  }

  resetPassword() {
    this.accountService.resetPassword(this.token, this.newPassword).subscribe({
      next: () => alert('Hasło zmienione pomyślnie'),
      error: (err) => {
        console.error('Błąd podczas resetowania hasła:', err);
        alert('Status zmiany hasla: ' + (err.error?.message || err.statusText || 'Nieznany błąd') +
          '\nStatus HTTP: ' + err.status);
      }
    });
  }

}
