export interface Book {
  id: number;
  title: string;
  author: string;
  publishedDate: string; // ISO date (yyyy-MM-dd)
  coverImageUrl: string | null;
}

export type BookRequest = Omit<Book, 'id'>;

export interface Quote {
  id: number;
  text: string;
  author: string | null;
  book: string | null;
}

export type QuoteRequest = Omit<Quote, 'id'>;

export interface AuthResponse {
  token: string;
  username: string;
  expiresAt: string;
}
