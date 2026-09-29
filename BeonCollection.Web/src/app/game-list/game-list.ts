import { Component, OnInit, inject, signal } from '@angular/core';
import { GamesApi } from '../games-api';
import { GameDto } from '../models';

@Component({
  selector: 'app-game-list',
  imports: [],
  templateUrl: './game-list.html',
  styleUrl: './game-list.css',
})
export class GameList implements OnInit {
  private api = inject(GamesApi);

  games = signal<GameDto[]>([]);
  loading = signal(true);
  error = signal<string | null>(null);

  ngOnInit(): void {
    this.api.getGames().subscribe({
      next: games => {
        this.games.set(games);
        this.loading.set(false);
      },
      error: () => {
        this.error.set('No se ha podido cargar la colección.');
        this.loading.set(false);
      },
    });
  }
}