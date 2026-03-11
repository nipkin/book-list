import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { BookService } from '../../services/book.service';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BookRequest } from '../../models/book-request';

@Component({
  selector: 'update-book.component',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './update-book.component.html',
  standalone: true
})
export class UpdateBookComponent {
  updateBookForm: FormGroup;
  errorMessage = '';
  successMessage = '';
  bookId?: number;

  private formBuilder = inject(FormBuilder);
  private bookService = inject(BookService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  constructor() {
    this.updateBookForm = this.formBuilder.group({
      title: ['', Validators.required],
      author: ['', Validators.required],
      year: ['', [Validators.required]]
    });
  }

  ngOnInit() {
    this.bookId = Number(this.route.snapshot.paramMap.get('id'));
    this.bookService.getBook(this.bookId).subscribe({
      next: (book) => {
        this.updateBookForm.patchValue({
          title: book.title,
          author: book.author,
          year: new Date(book.publicationDate).toISOString().split('T')[0]
        });
      },
      error: () => {
        this.errorMessage = 'Could not fetch book data';
      }
    });
  }

  onSubmit() {
    if (this.updateBookForm.invalid) {
      this.updateBookForm.markAllAsTouched();
      return;
    }

    const payload: BookRequest = {
      title: this.updateBookForm.value.title!,
      author: this.updateBookForm.value.author!,
      publicationDate: new Date(this.updateBookForm.value.year!)
    };

    if (this.bookId) {
      this.bookService.updateBook(this.bookId, payload).subscribe({
        next: () => {
          this.router.navigate(['/']);
        },
        error: () => {
          this.errorMessage = 'Server error';
        }
      });
    }
  }

  cancel() {
    this.router.navigate(['/']);
  }
}
