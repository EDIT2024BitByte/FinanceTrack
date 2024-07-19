import { ExpenseDialogDataModel } from './../../app/shared/models/expense-dialog-data.model';
import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { ColComponent, RowComponent } from '@coreui/angular';
import { ExpensesDialogComponent } from './expenses-dialog/expenses-dialog.component';
import { GenericConfirmDialogComponent } from '../generic-confirm-dialog/generic-confirm-dialog.component';
import { CategoriesViewModel } from '../../app/shared/models/categories.model';
import { CashflowService } from '../../app/shared/services/cashflow.service';
import { ExpenseViewModel } from '../../app/shared/models/expense.model';
import { NgxUiLoaderModule } from 'ngx-ui-loader';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-expenses',
  standalone: true,
  imports: [
    MatTableModule,
    MatPaginatorModule,
    RowComponent,
    ColComponent,
    NgxUiLoaderModule,
    CommonModule
  ],
  templateUrl: './expenses.component.html',
  styleUrl: './expenses.component.css'
})
export class ExpensesComponent implements OnInit, OnDestroy {

  // subs
  subscriptions: Subscription[] = [];
  
  displayedColumns: string[] = ['no', 'amount', 'date', 'category', 'description', 'edit', 'delete'];
  dataSource = new MatTableDataSource<ExpenseViewModel>();
  @ViewChild(MatPaginator) paginator: MatPaginator;

  categoryList: CategoriesViewModel[];
  expenseData: ExpenseViewModel = {
    id: 0,
    isDeleted: false
  }

  constructor(private dialog: MatDialog,
    private cashflowService: CashflowService,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.subscriptions.push(this.cashflowService.getAllCategories().subscribe(res => {
      if (!!res) {
        this.categoryList = res;
      }
    }));

    this.getAllExpenses();
  }

  getAllExpenses(): void {
    var budgetId = Number(sessionStorage.getItem('budgetId'));

    this.subscriptions.push(this.cashflowService.getAllExpenses(budgetId).subscribe(res => {
      if (!!res) {
        this.dataSource = new MatTableDataSource<ExpenseViewModel>(res);
        this.dataSource.paginator = this.paginator;
      }
    }));
  }

  addExpense(): void {
    var dialogData: ExpenseDialogDataModel = {
      categories: this.categoryList,
      expense: this.expenseData
    };

    this.dialog.open(ExpensesDialogComponent, {
      width: '35%',
      data: dialogData
    }).afterClosed().subscribe(() => {
      this.getAllExpenses();
    });
  }

  editExpense(expense: ExpenseViewModel): void {
    var expenseDataEdit: ExpenseViewModel = {
      id: expense.id,
      amount: expense.amount,
      date: expense.date,
      description: expense.description,
      categoryId: expense.categoryId,
      isDeleted: false
    };

    var dialogData: ExpenseDialogDataModel = {
      categories: this.categoryList,
      expense: expenseDataEdit
    };

    this.dialog.open(ExpensesDialogComponent, {
      width: '35%',
      data: dialogData
    }).afterClosed().subscribe(() => {
      this.getAllExpenses();
    });
  }

  deleteExpense(id: number): void {
    this.dialog.open(GenericConfirmDialogComponent, {
      disableClose: true,
      data: {
        title: 'Delete',
        message: 'Are you sure you want to delete the expense?',
      }
    }).afterClosed().subscribe((confirmed) => {
      if (confirmed) {
        this.subscriptions.push(this.cashflowService.deleteExpense(id).subscribe(
          (res) => {
            if (res) {
              this.toastr.success("Expense deleted successfully.", "Success");
              this.getAllExpenses();
            }
          },
          (error) => {
            this.toastr.error("Failed to delete expense.", "Error");
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
