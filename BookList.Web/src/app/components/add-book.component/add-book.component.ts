import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { BookService } from '../../services/book.service';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BookRequest } from '../../models/book-request';

@Component({
  selector: 'add-book',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './add-book.component.html',
  standalone: true
})
export class AddBookComponent {
  addBookForm: FormGroup;
  errorMessage = '';
  private formBuilder = inject(FormBuilder);
  private bookService = inject(BookService);
  private router = inject(Router);
  
  constructor() {
    this.addBookForm = this.formBuilder.group({
      title: ['', Validators.required],
      author: ['', Validators.required],
      year: ['', [Validators.required]]
    })
  }

  onSubmit() {
    if (this.addBookForm.invalid) {
      this.addBookForm.markAllAsTouched();
      return;
    }
    const payload: BookRequest = {
      title: this.addBookForm.value.title!,
      author: this.addBookForm.value.author!,
      publicationDate: new Date(this.addBookForm.value.year!)
    }

    this.bookService.addBook(payload).subscribe({
      next: () => {
        this.router.navigate(['/']);
      },
      error: () => {
        this.errorMessage = 'Server error';
      }
    });
  }

  cancel() {
    this.router.navigate(['/']);
  }
}
