import { Routes } from '@angular/router';
import { GameList } from './game-list/game-list';
import { Login } from './login/login';
import { Admin } from './admin/admin';
import { authGuard } from './auth-guard';

export const routes: Routes = [
  { path: '', component: GameList },
  { path: 'login', component: Login },
  { path: 'admin', component: Admin, canActivate: [authGuard] },
];