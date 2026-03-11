import { Component, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faUser, faSignOutAlt } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-header',
  imports: [CommonModule, RouterModule, FontAwesomeModule],
  templateUrl: './header.component.html',
  standalone: true
})
export class HeaderComponent {
  constructor(public auth: AuthService) {
    const saved = localStorage.getItem('darkMode') === 'true';
    this.darkMode = signal(saved);
    document.documentElement.setAttribute('data-bs-theme', saved ? 'dark' : 'light');
  }

  isNavOpen = signal(false);
  darkMode = signal(false);

  faUser = faUser;
  faSignOutAlt = faSignOutAlt;

  toggleNav() {
    this.isNavOpen.update(open => !open);
  }

  logout() {
    this.auth.logout().subscribe();
  }

  toggleDarkMode() {
    this.darkMode.update(v => !v);
    localStorage.setItem('darkMode', String(this.darkMode()));
    document.documentElement.setAttribute(
      'data-bs-theme',
      this.darkMode() ? 'dark' : 'light'
    );
  }
}
