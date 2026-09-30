import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { GameDto } from './models';

const API_URL = 'https://localhost:7276/api';

@Injectable({ providedIn: 'root' })
export class GamesApi {
  private http = inject(HttpClient);

  getGames(): Observable<GameDto[]> {
    return this.http.get<GameDto[]>(`${API_URL}/games`);
  }

  createGame(title: string): Observable<GameDto> {
    return this.http.post<GameDto>(`${API_URL}/games`, { title });
  }
}