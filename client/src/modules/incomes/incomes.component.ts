import { CashflowService } from './../../app/shared/services/cashflow.service';
import { Component, ViewChild, PLATFORM_ID, OnInit, OnDestroy } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { ColComponent, RowComponent } from '@coreui/angular';
import { GenericConfirmDialogComponent } from '../generic-confirm-dialog/generic-confirm-dialog.component';
import { IncomesDialogComponent } from './incomes-dialog/incomes-dialog.component';
import { IncomeViewModel } from '../../app/shared/models/income.model';
import { NgxUiLoaderModule } from 'ngx-ui-loader';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-incomes',
  standalone: true,
  imports: [
    MatTableModule,
    MatPaginatorModule,
    RowComponent,
    ColComponent,
    NgxUiLoaderModule,
    CommonModule
  ],
  templateUrl: './incomes.component.html',
  styleUrl: './incomes.component.css'
})

export class IncomesComponent implements OnInit, OnDestroy {
  // subs
  subscriptions: Subscription[] = [];
  
  displayedColumns: string[] = ['no', 'amount', 'date', 'description', 'edit', 'delete'];
  dataSource = new MatTableDataSource<IncomeViewModel>();
  @ViewChild(MatPaginator) paginator: MatPaginator;

  incomeData: IncomeViewModel = {
    id: 0,
    isDeleted: false
  }

  constructor(private dialog: MatDialog,
    private cashflowService: CashflowService,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.getAllIncome();
  }

  getAllIncome(): void {
    var budgetId = Number(sessionStorage.getItem('budgetId'));
    
    this.subscriptions.push(this.cashflowService.getAllIncome(budgetId).subscribe(res => {
      if (!!res) {
        this.dataSource = new MatTableDataSource<IncomeViewModel>(res);
        this.dataSource.paginator = this.paginator;
      }
    }));
  }

  addIncome(): void {
    this.dialog.open(IncomesDialogComponent, {
      width: '35%',
      data: this.incomeData
    }).afterClosed().subscribe(() => {
      this.getAllIncome();
    });
  }

  editIncome(income: IncomeViewModel): void {
    var incomeDataEdit: IncomeViewModel = {
      id: income.id,
      amount: income.amount,
      date: income.date,
      description: income.description,
      isDeleted: false
    };

    this.dialog.open(IncomesDialogComponent, {
      width: '35%',
      data: incomeDataEdit
    }).afterClosed().subscribe(() => {
      this.getAllIncome();
    });
  }

  deleteIncome(id: number): void {
    this.dialog.open(GenericConfirmDialogComponent, {
      disableClose: true,
      data: {
        title: 'Delete',
        message: 'Are you sure you want to delete income?',
      }
    }).afterClosed().subscribe((confirmed) => {
      if (confirmed) {
        this.subscriptions.push(this.cashflowService.deleteIncome(id).subscribe(
          (res) => {
            if (res) {
              this.toastr.success("Income deleted successfully.", "Success");
              this.getAllIncome();
            }
          },
          (error) => {
            this.toastr.error("Failed to delete income.", "Error");
          }
        ));
      }
    });
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach((sub) => {
      if (sub && !sub.closed) {
        sub.unsubscribe();
      }
    });
  }
}