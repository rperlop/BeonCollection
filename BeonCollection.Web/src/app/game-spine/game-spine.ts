import { Component, Input, computed } from '@angular/core';
import { getPlatformStyle, getBodyPalette } from '../platform-styles';

@Component({
  selector: 'app-game-spine',
  imports: [],
  templateUrl: './game-spine.html',
  styleUrl: './game-spine.css',
})
export class GameSpine {
  @Input({ required: true }) title!: string;
  @Input({ required: true }) platform!: string;

  platformStyle = computed(() => getPlatformStyle(this.platform));
  bodyPalette = computed(() => getBodyPalette(this.title));
}