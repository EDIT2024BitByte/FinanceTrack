import { Component, OnInit, Renderer2 } from '@angular/core';
import { SidebarFinanceComponent } from '../sidebar/sidebar-finance.component';
import { RouterOutlet } from '@angular/router';
import { HeaderFinanceComponent } from '../header/header-finance.component';
import { ContainerComponent, ShadowOnScrollDirective } from '@coreui/angular';
import { BudgetService } from '../../shared/services/budget.service';
import { LayoutDialogComponent } from './layout-dialog/layout-dialog.component';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { DashboardComponent } from '../../../modules/dashboard/dashboard.component';
import { NgxUiLoaderModule } from 'ngx-ui-loader';

@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [
    SidebarFinanceComponent,
    RouterOutlet,
    HeaderFinanceComponent,
    ShadowOnScrollDirective,
    ContainerComponent,
    DashboardComponent,
    NgxUiLoaderModule
  ],
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css']
})
export class LayoutComponent implements OnInit {

  constructor(
    private budgetService: BudgetService,
    private dialog: MatDialog,
    private renderer: Renderer2
  ) { }

  ngOnInit(): void {
    this.checkBudget();
  }

  checkBudget(): void {
    if (typeof sessionStorage !== 'undefined') {
      var userId = sessionStorage.getItem('userId');

      if (!!!sessionStorage.getItem('isBudgetSet') && !!userId) {
        this.budgetService.checkBudget(+userId)
          .subscribe(res => {
            if (!res) {
              const dialogConfig = new MatDialogConfig();
              dialogConfig.width = '40%';
              dialogConfig.disableClose = true;
              const dialogRef = this.dialog.open(LayoutDialogComponent, dialogConfig);

              this.renderer.addClass(document.body, 'dialog-open');

              dialogRef.afterClosed().subscribe(() => {
                this.renderer.removeClass(document.body, 'dialog-open');
              });
            }
          });
      }
    }
  }
}
