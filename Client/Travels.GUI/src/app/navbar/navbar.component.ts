import { AuthService } from './../auth/auth.service';
import { Component, OnInit, inject } from '@angular/core';
import { Router } from '@angular/router';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-navbar',
  imports: [NgIf],
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  isLoggedIn = false;

  private authService = inject(AuthService)
  private router = inject(Router) 

  ngOnInit(): void {
    this.authService.authStatus.subscribe(status => {
      this.isLoggedIn = status;
    });
  }

  logout() {
    this.authService.clearToken();
    this.router.navigate(['/login']);
  }

  checkAccess(route: string) {
    if (!this.isLoggedIn) {
      alert('Musisz być zalogowany, aby uzyskać dostęp do tej strony.');
      return;
    }
    this.router.navigate([route]);
  }
}
