import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GameSpine } from './game-spine';

describe('GameSpine', () => {
  let component: GameSpine;
  let fixture: ComponentFixture<GameSpine>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GameSpine],
    }).compileComponents();

    fixture = TestBed.createComponent(GameSpine);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
