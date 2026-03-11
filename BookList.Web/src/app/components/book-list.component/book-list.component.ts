import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { BookService } from '../../services/book.service';
import { AuthService } from '../../services/auth.service';
import { BookResponse } from '../../models/book-response';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faBook } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'book-list',
  imports: [CommonModule, RouterModule, FontAwesomeModule],
  templateUrl: './book-list.component.html',
  standalone: true
})
export class BookListComponent implements OnInit {
  books: BookResponse[] = [];
  faBook = faBook;
  errorMessage = '';

  private bookService = inject(BookService);
  private router = inject(Router);
  public auth = inject(AuthService);

  ngOnInit() {
    this.loadBooks();
  }

  loadBooks() {
    this.bookService.getAll().subscribe({
      next: (data) => {
        this.books = data;
      },
      error: () => {
        this.errorMessage = 'Något gick fel när böckerna skulle hämtas.';
      }
    });
  }

  editBook(id: number) {
    this.router.navigate(['/update-book', id]);
  }

  deleteBook(id: number) {

    if (!confirm('Är du säker på att du vill ta bort boken?')) {
      return;
    }

    this.bookService.deleteBook(id).subscribe({
      next: () => {
        this.loadBooks();
      },
      error: () => {
        this.errorMessage = 'Något gick fel när boken skulle tas bort';
      }
    });
  }
}
