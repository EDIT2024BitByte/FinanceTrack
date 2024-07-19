import { Component } from '@angular/core';
import {
  SidebarBrandComponent,
  SidebarComponent,
  SidebarFooterComponent,
  SidebarHeaderComponent,
  SidebarNavComponent,
  SidebarNavHelper,
  SidebarToggleDirective,
  SidebarTogglerDirective
} from '@coreui/angular';
import { IconModule } from '@coreui/icons-angular';
import { CommonModule } from '@angular/common';
import { NgxUiLoaderModule } from 'ngx-ui-loader';

@Component({
  selector: 'app-sidebar-finance',
  standalone: true,
  imports: [
    SidebarComponent,
    SidebarNavComponent,
    SidebarHeaderComponent,
    SidebarFooterComponent,
    SidebarBrandComponent,
    SidebarToggleDirective,
    SidebarTogglerDirective,
    IconModule,
    CommonModule,
    NgxUiLoaderModule
  ],
  templateUrl: './sidebar-finance.component.html',
  styleUrls: ['./sidebar-finance.component.css'],
  providers: [
    SidebarNavHelper
  ]
})
export class SidebarFinanceComponent {

  constructor() { }

  navItems = [
    {
      name: 'Dashboard',
      url: '/layout/dashboard',
      icon: 'cil-chart'
    },
    {
      name: 'Income',
      url: '/layout/incomes',
      icon: 'cil-vertical-align-bottom'
    },
    {
      name: 'Expense',
      url: '/layout/expenses',
      icon: 'cil-vertical-align-top'
    }
  ];

}
