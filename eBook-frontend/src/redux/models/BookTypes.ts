export interface AddBookRequest {
  name: string;
  year: number;
  information: string;
  genre: string;
  authorId: string;
  imageUrl: string;
  raiting: number;
}

export interface EditBookRequest {
  id: string;
  name: string;
  year: number;
  information: string;
  genre: string;
  authorId: string;
  imageUrl: string;
  raiting: number;
}

export interface Book {
  id: string;
  name: string;
  year: number;
  information: string;
  genre: string;
  authorId: string;
  imageUrl: string;
  raiting: number;
}
