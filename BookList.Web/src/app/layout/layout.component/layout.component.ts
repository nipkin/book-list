import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from '../header.component/header.component';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [CommonModule, HeaderComponent, RouterModule],
  templateUrl: './layout.component.html'
})
export class LayoutComponent { }
