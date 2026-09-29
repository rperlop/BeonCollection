export interface EditionDto {
  id: number;
  gameId: number;
  platform: string;
  region: string;
  type: string;
  year: number | null;
}

export interface GameDto {
  id: number;
  title: string;
  editions: EditionDto[];
}