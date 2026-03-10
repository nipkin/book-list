import { Routes } from '@angular/router';
import { BookListComponent } from './components/book-list.component/book-list.component';
import { AddUserComponent } from './components/add-user.component/add-user.component';
import { AddBookComponent } from './components/add-book.component/add-book.component';
import { UpdateBookComponent } from './components/update-book.component/update-book.component';
import { UserQuotesComponent } from './components/user-quotes.component/user-quotes.component';
import { LoginComponent } from './components/login.component/login.component';
import { LayoutComponent } from './layout/layout.component/layout.component';
import { authGuard, authCheckGuard } from './guards/auth-guard';

export const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    canActivate: [authCheckGuard],
    children: [
      {
        path: '',
        component: BookListComponent
      },
      {
        path: 'add-user',
        component: AddUserComponent
      },
      {
        path: 'add-book',
        component: AddBookComponent,
        canActivate: [authGuard] 
      },
      {
        path: 'update-book/:id',
        component: UpdateBookComponent,
        canActivate: [authGuard] 
      },
      {
        path: 'quotes',
        component: UserQuotesComponent,
        canActivate: [authGuard] 
      },
      {
        path: 'login',
        component: LoginComponent
      },
    ],
  },
  { path: '**', redirectTo: '' },
];
