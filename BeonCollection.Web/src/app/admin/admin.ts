import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { Auth } from '../auth';
import { GamesApi } from '../games-api';

@Component({
  selector: 'app-admin',
  imports: [FormsModule],
  templateUrl: './admin.html',
  styleUrl: './admin.css',
})
export class Admin {
  private auth = inject(Auth);
  private router = inject(Router);
  private gamesApi = inject(GamesApi);

  title = '';
  saving = signal(false);
  error = signal<string | null>(null);
  success = signal<string | null>(null);

  logout(): void {
    this.auth.logout();
    this.router.navigateByUrl('/login');
  }

  createGame(): void {
    this.error.set(null);
    this.success.set(null);
    this.saving.set(true);

    this.gamesApi.createGame(this.title).subscribe({
      next: game => {
        this.saving.set(false);
        this.success.set(`Juego "${game.title}" creado con id ${game.id}.`);
        this.title = '';
      },
      error: err => {
        this.saving.set(false);
        this.error.set(
          err.status === 401
            ? 'Tu sesión ha caducado, vuelve a iniciar sesión.'
            : 'No se ha podido crear el juego.'
        );
      },
    });
  }
}