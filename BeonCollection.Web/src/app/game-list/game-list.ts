import { Component, OnInit, computed, inject, signal } from '@angular/core';
import { GamesApi } from '../games-api';
import { GameDto } from '../models';
import { GameSpine } from '../game-spine/game-spine';

interface SpineItem {
  gameId: number;
  editionId: number;
  title: string;
  platform: string;
}

@Component({
  selector: 'app-game-list',
  imports: [GameSpine],
  templateUrl: './game-list.html',
  styleUrl: './game-list.css',
})
export class GameList implements OnInit {
  private api = inject(GamesApi);

  games = signal<GameDto[]>([]);
  loading = signal(true);
  error = signal<string | null>(null);

  shelves = computed(() => {
    const byPlatform = new Map<string, SpineItem[]>();

    for (const game of this.games()) {
      for (const edition of game.editions) {
        const item: SpineItem = {
          gameId: game.id,
          editionId: edition.id,
          title: game.title,
          platform: edition.platform,
        };
        const list = byPlatform.get(edition.platform) ?? [];
        list.push(item);
        byPlatform.set(edition.platform, list);
      }
    }

    return Array.from(byPlatform.entries())
      .map(([platform, items]) => ({
        platform,
        items: items.sort((a, b) => a.title.localeCompare(b.title)),
      }))
      .sort((a, b) => a.platform.localeCompare(b.platform));
  });

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

  onSpineClick(item: SpineItem): void {
    console.log('Clic en lomo:', item);
  }
}