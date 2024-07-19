import { Component, Input } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import {
  BreadcrumbRouterComponent,
  ContainerComponent,
  HeaderModule,
  HeaderTogglerDirective,
  SidebarToggleDirective
} from '@coreui/angular';

@Component({
  selector: 'app-header-finance',
  standalone: true,
  imports: [
    HeaderModule,
    ContainerComponent,
    SidebarToggleDirective,
    BreadcrumbRouterComponent,
    HeaderTogglerDirective
  ],
  templateUrl: './header-finance.component.html',
  styleUrl: './header-finance.component.css'
})
export class HeaderFinanceComponent {

  firstName: string = '';
  lastName: string = '';

  constructor(private router: Router,
    private dialogRef: MatDialog
  ) { }

  @Input() sidebarId: string = 'sidebar1';

  ngOnInit() {

    const firstName = sessionStorage.getItem('firstname');
    const lastName = sessionStorage.getItem('lastname');

    this.firstName = firstName ? firstName : '';
    this.lastName = lastName ? lastName : '';
  }


  logout(): void {
    this.dialogRef.closeAll(); // Close all dialogs
    sessionStorage.removeItem('userId'); // Remove userId from sessionStorage
    sessionStorage.removeItem('firstname'); // Remove firstname from sessionStorage
    sessionStorage.removeItem('lastname'); // Remove lastname from sessionStorage
    sessionStorage.removeItem('isBudgetSet'); // Remove total amount flag from sessionStorage
    sessionStorage.removeItem('budgetId'); // Remove budgetId from sessionStorage
    this.router.navigate(['/login']); // Back to login page
  }

}
