import { Component } from '@angular/core';
import { GameList } from './game-list/game-list';

@Component({
  selector: 'app-root',
  imports: [GameList],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {}