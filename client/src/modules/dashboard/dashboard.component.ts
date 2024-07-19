import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { ChartjsComponent } from '@coreui/angular-chartjs';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { BehaviorSubject, Subscription, filter, forkJoin, mergeMap } from 'rxjs';
import { MatCardModule } from '@angular/material/card';
import { BorderDirective, ButtonDirective, ButtonGroupComponent, CardBodyComponent, CardComponent, CardHeaderComponent, ColComponent, RowComponent, TableDirective, TemplateIdDirective, WidgetStatAComponent } from '@coreui/angular';
import { BudgetService } from '../../app/shared/services/budget.service';
import { NgxUiLoaderModule } from 'ngx-ui-loader';
import { CashflowService } from '../../app/shared/services/cashflow.service';
import { IncomeExpenseTransactionsViewModel } from '../../app/shared/models/income-expense-transactions';
import { CategoriesViewModel } from '../../app/shared/models/categories.model';
import { ExpenseViewModel } from '../../app/shared/models/expense.model';
import { ChartOptions } from 'chart.js';
import { BudgetViewModel } from '../../app/shared/models/budget.model';
import { IncomeExpenseDataStatisticsViewModel } from '../../app/shared/models/income-expense-data-statistics';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css',
  standalone: true,
  imports: [
    ChartjsComponent,
    CommonModule,
    FormsModule,
    MatDatepickerModule,
    MatFormFieldModule,
    MatInputModule,
    MatCheckboxModule,
    MatCardModule,
    WidgetStatAComponent,
    ColComponent,
    TemplateIdDirective,
    TableDirective,
    CardComponent,
    CardBodyComponent,
    RowComponent,
    ReactiveFormsModule,
    ButtonGroupComponent,
    ButtonDirective,
    BorderDirective,
    CardHeaderComponent,
    NgxUiLoaderModule
  ]
})

export class DashboardComponent implements OnInit, OnDestroy {
  // subs
  subscriptions: Subscription[] = [];
  
  constructor(private budgetService: BudgetService,
    private cashflowService: CashflowService,
    private formBuilder: FormBuilder) { }
  totalIncome: any;
  totalExpense: any;
  totalAmount: any;

  categories: CategoriesViewModel[];
  expense: ExpenseViewModel[];
  dateForm: FormGroup;

  categoryIds: number[] = [];
  userId: number;
  budgetId: number;

  tableData: IncomeExpenseTransactionsViewModel[];

  weekExpense: ExpenseViewModel[];

  expense$ = new BehaviorSubject<any>({
    labels: ['Rent and utilities', 'Food and drink', 'Transportation', 'Health', 'Entertainment', 'Gifts', 'Clothing', 'Other'],
    datasets: [{
      backgroundColor: ['rgba(255, 105, 180, 0.5)', 'rgba(151, 187, 205, 0.5)', 'rgba(255, 99, 132, 0.5)', 'rgba(54, 162, 235, 0.5)', 'rgba(75, 192, 192, 0.5)', 'rgba(255, 159, 64, 0.5)', 'rgba(139, 195, 74, 0.5)', 'rgba(204, 255, 0, 0.5)'],
      data: [0, 0, 0, 0, 0, 0, 0, 0]
    }]
  });

  weekExpense$ = new BehaviorSubject<any>({
    labels: ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday'],
    datasets: [
      {
        label: 'Rent and utilities',
        backgroundColor: 'rgba(255, 105, 180, 0.5)',
        data: []
      },
      {
        label: 'Food and drink',
        backgroundColor: 'rgba(151, 187, 205, 0.5)',
        data: []
      },
      {
        label: 'Transportation',
        backgroundColor: 'rgba(255, 99, 132, 0.5)',
        data: []
      },
      {
        label: 'Health',
        backgroundColor: 'rgba(54, 162, 235, 0.5)',
        data: []
      },
      {
        label: 'Entertainment',
        backgroundColor: 'rgba(75, 192, 192, 0.5)',
        data: []
      },
      {
        label: 'Gifts',
        backgroundColor: 'rgba(255, 159, 64, 0.5)',
        data: []
      },
      {
        label: 'Clothing',
        backgroundColor: 'rgba(139, 195, 74, 0.5)',
        data: []
      },
      {
        label: 'Other',
        backgroundColor: 'rgba(204, 255, 0, 0.5)',
        data: []
      }
    ]
  });

  chartOptions: ChartOptions = {
    plugins: {
      title: {
        display: true,
        text: 'Weekly Expenses',
        font: {
          size: 20
        }
      }
    },
    events: [
      "mouseout",
      "click",
      "touchstart",
      "touchmove",
      "touchend"
    ]
  };

  ngOnInit(): void {
    const userID = sessionStorage.getItem('userId');
    this.userId = Number(userID);
    this.buildForm();

    this.getCategories();
    this.getTotalAmount(this.userId);
  }

  buildForm(): void {
    this.dateForm = this.formBuilder.group({
      startDate: [null],
      endDate: [null]
    });
  }

  getCategories(): void {
    this.subscriptions.push(this.cashflowService.getAllCategories()
      .subscribe(res => {
        if (res != null) {
          this.categories = res;
        }
      }));
  }

  getTotalAmount(userId: number): void {

    this.subscriptions.push(
      this.budgetService.getBudgetByUserId(userId)
        .pipe(
          filter(res => res !== null),
          mergeMap((res: BudgetViewModel) => {
           
            this.totalAmount = res.totalAmount;
            this.budgetId = res.id;
            sessionStorage.setItem('budgetId', res.id.toString());

            const rangeStartDate = new Date('2024-07-22');  // July 22, 2024
            const rangeEndDate = new Date('2024-07-28');    // July 28, 2024

            var incomeExpenseStat = this.cashflowService.getIncomeExpenseDataStatistics(this.budgetId);
            var expenseWeek = this.cashflowService.getExpenseByDate({ startDate: rangeStartDate,  endDate: rangeEndDate, budgetId: this.budgetId });
            var expenseByDate = this.cashflowService.getExpenseByDate({ startDate: this.dateForm?.value.startDate, endDate: this.dateForm?.value.endDate, budgetId: this.budgetId });

            return forkJoin([incomeExpenseStat, expenseWeek, expenseByDate]);
          })
        ).subscribe((x) => {
          //incomeExpenseStat
          if(x[0]){
            this.getIncomeExpenseDataStatistics(x[0]);
          }

          //expenseWeek
          if(x[1] != null){
            this.getExpenseForThisWeek(x[1]);
          }

          //expenseByDate
          if(x[2] != null){
            this.getExpenseByDate(x[2]);
          }
        })
    );
  }

  getIncomeExpenseDataStatistics(data: IncomeExpenseDataStatisticsViewModel): void {
    this.totalIncome = data.incomeSum;
    this.totalExpense = data.expenseSum;
    this.tableData = data.incomeExpenseTransactions;
  }

  getExpenseForThisWeek(data: ExpenseViewModel[]): void {
    this.weekExpense = data;
    this.updateThisWeekChartData();
  }

  getExpenseByDate(data: ExpenseViewModel[]): void {
    this.expense = data;
    if (this.categoryIds.length > 0) {
      this.categoryIds.forEach(element => {
        this.updateChartData(element, true);
      });
    }
  }

  onDatePickerClosed(): void {
    this.subscriptions.push(this.cashflowService.getExpenseByDate({
      startDate: this.dateForm?.value.startDate,
      endDate: this.dateForm?.value.endDate,
      budgetId: this.budgetId
    }).subscribe(res => {
      if (res != null) {
        this.getExpenseByDate(res);
      }
    }));
  }

  updateChartData(category: number, isChecked: boolean): void {
    const expense = this.expense$.value;
    const data = expense.datasets[0].data;

    if (isChecked) {
      this.categoryIds.push(category);
    } else {
      this.categoryIds = this.categoryIds.filter(id => id !== category);
    }

    switch (category) {
      case 1:
        data[0] = isChecked && this.expense.find(x => x.categoryId == 1) ?
          this.expense.filter(x => x.categoryId == 1).reduce((sum, current) => sum + (current.amount || 0), 0) : 0;
        break;
      case 2:
        data[1] = isChecked && this.expense.find(x => x.categoryId == 2) ?
          this.expense.filter(x => x.categoryId == 2).reduce((sum, current) => sum + (current.amount || 0), 0) : 0;
        break;
      case 3:
        data[2] = isChecked && this.expense.find(x => x.categoryId == 3) ?
          this.expense.filter(x => x.categoryId == 3).reduce((sum, current) => sum + (current.amount || 0), 0) : 0;
        break;
      case 4:
        data[3] = isChecked && this.expense.find(x => x.categoryId == 4) ?
          this.expense.filter(x => x.categoryId == 4).reduce((sum, current) => sum + (current.amount || 0), 0) : 0;
        break;
      case 5:
        data[4] = isChecked && this.expense.find(x => x.categoryId == 5) ?
          this.expense.filter(x => x.categoryId == 5).reduce((sum, current) => sum + (current.amount || 0), 0) : 0;
        break;
      case 6:
        data[5] = isChecked && this.expense.find(x => x.categoryId == 6) ?
          this.expense.filter(x => x.categoryId == 6).reduce((sum, current) => sum + (current.amount || 0), 0) : 0;
        break;
      case 7:
        data[6] = isChecked && this.expense.find(x => x.categoryId == 7) ?
          this.expense.filter(x => x.categoryId == 7).reduce((sum, current) => sum + (current.amount || 0), 0) : 0;
        break;
      case 8:
        data[7] = isChecked && this.expense.find(x => x.categoryId == 8) ?
          this.expense.filter(x => x.categoryId == 8).reduce((sum, current) => sum + (current.amount || 0), 0) : 0;
        break;
      case 9:
        data[8] = isChecked && this.expense.find(x => x.categoryId == 9) ?
          this.expense.filter(x => x.categoryId == 9).reduce((sum, current) => sum + (current.amount || 0), 0) : 0;
        break;
    }

    this.expense$.next({ ...expense });
  }

  handleChartRef($chartRef: any) { }

  updateThisWeekChartData(): void {

    var categoryList = [1, 2, 3, 4, 5, 6, 7, 8];

    const monday = new Date('2024-07-22');
    const tuesday = new Date('2024-07-23');
    const wednesday = new Date('2024-07-24');
    const thursday = new Date('2024-07-25');
    const friday = new Date('2024-07-26');
    const saturday = new Date('2024-07-27');
    const sunday = new Date('2024-07-28');

    var dayList = [monday, tuesday, wednesday, thursday, friday, saturday, sunday];

    const weekExpense = this.weekExpense$.value;
    const dataset = weekExpense.datasets;

    var index = 0;
    categoryList.forEach(cat => {
      dayList.forEach(day => {
        var barData = this.weekExpense
          .filter(x => x.categoryId == cat)
          .filter(x => {
            const date = new Date(x.date as string | number | Date);
            return new Date(date.getFullYear(), date.getMonth(), date.getDate()).getTime() === new Date(day.getFullYear(), day.getMonth(), day.getDate()).getTime();
          })
          .reduce((sum, current) => sum + (current.amount || 0), 0);

        dataset[index].data.push(barData);
      });
      index++;
    });

    this.weekExpense$.next({ ...weekExpense });
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach((sub) => {
      if (sub && !sub.closed) {
        sub.unsubscribe();
      }
    });
  }
}