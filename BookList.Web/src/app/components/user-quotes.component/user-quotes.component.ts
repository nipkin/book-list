import { QuoteService } from '../../services/quote.service';
import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { QuoteRequest } from '../../models/quote-request';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faQuoteLeft } from '@fortawesome/free-solid-svg-icons';
interface EditableItem {
  id: number
  value: string;
  editing: boolean;
  tempValue: string;
}

@Component({
  selector: 'user-quotes',
  imports: [CommonModule, FormsModule, FontAwesomeModule],
  templateUrl: './user-quotes.component.html',
  standalone: true
})
export class UserQuotesComponent {
  items: EditableItem[] = [];
  newValue = '';
  errorMessage = '';
  faQuoteLeft = faQuoteLeft;

  private quoteService = inject(QuoteService);

  ngOnInit() {
    this.load();
  }

  load() {
    this.quoteService.getUserQuotes().subscribe(data => {
      this.items = data.userQuotes.map(value => ({
        id: value.id,
        value: value.text,
        editing: false,
        tempValue: value.text
      }));
    });
  }

  add() {
    const payload: QuoteRequest = {
      text: this.newValue!
    }

    if (this.items.length >= 5) {
      this.errorMessage = 'Du kan bara ha 5 citat.';
      return;
    }
    if (this.newValue.trim() === '') {
      this.errorMessage = 'Texten för citatet behövs';
      return;
    }
    this.quoteService.addQuote(payload).subscribe(() => {
        this.newValue = '';
        this.load();
    });
  }

  delete(id: number) {
    if (!confirm('Är du säker på att du vill ta bort det här citatet?')) {
      return;
    }

    this.quoteService.deleteQuote(id).subscribe({
      next: () => {
        this.load();
      },
      error: () => {
       this.errorMessage = 'Något gick fel när citatet skulle tas bort';
      }
    });
  }

  edit(item: EditableItem) {
    item.editing = true;
    item.tempValue = item.value;
  }

  save(id: number, item: EditableItem) {
    const payload: QuoteRequest =  {
      text: item.tempValue
    }
    
    this.quoteService.updateQuote(id, payload).subscribe(() => {
      item.value = item.tempValue;
      item.editing = false;
    });
  }

  abort(item: EditableItem) {
    item.tempValue = item.value;
    item.editing = false;
  }
}
