import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { HeaderFinanceComponent } from './core/header/header-finance.component';
import { LayoutComponent } from './core/layout/layout.component';
import { SidebarFinanceComponent } from './core/sidebar/sidebar-finance.component';
import { MatDialogModule } from '@angular/material/dialog';
import { DashboardComponent } from '../modules/dashboard/dashboard.component';
import { NgxUiLoaderModule } from 'ngx-ui-loader';
import { MatMomentDateModule } from '@angular/material-moment-adapter';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,
    CommonModule,
    RouterLink,
    RouterLinkActive,
    HeaderFinanceComponent,
    LayoutComponent,
    SidebarFinanceComponent,
    MatDialogModule,
    DashboardComponent,
    NgxUiLoaderModule,
    MatMomentDateModule
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'EDIT 2024';
}
