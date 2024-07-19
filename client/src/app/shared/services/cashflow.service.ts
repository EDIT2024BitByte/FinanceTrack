import { DashboardDateViewModel } from './../models/dashboard-date.model';
import { Injectable } from "@angular/core";
import { environment } from "../../../environment/environment";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { IncomeViewModel } from "../models/income.model";
import { ExpenseViewModel } from "../models/expense.model";
import { CategoriesViewModel } from "../models/categories.model";
import { IncomeExpenseDataStatisticsViewModel } from "../models/income-expense-data-statistics";

const GET_CATEGORIES = '/cashflow/GetAllCategories';
const GET_INCOME = '/cashflow/GetAllIncome';
const GET_EXPENSES = '/cashflow/GetAllExpenses';
const ADD_EDIT_INCOME = '/cashflow/AddEditIncome';
const ADD_EDIT_EXPENSE = '/cashflow/AddEditExpense';
const DELETE_INCOME = '/cashflow/DeleteIncome';
const DELETE_EXPENSE = '/cashflow/DeleteExpense';
const GET_INCOME_EXPENSE_DATA_STATISTICS = '/cashflow/GetIncomeExpenseDataStatistics';
const GET_EXPENSE_BY_DATE = '/cashflow/GetExpenseByDate'

@Injectable({
  providedIn: 'root'
})
export class CashflowService {
  private url: string = environment.webApiCashflowUrl;

  constructor(private http: HttpClient) { }

  getAllCategories(): Observable<CategoriesViewModel[]> {
    return this.http.get<CategoriesViewModel[]>(
        this.url + GET_CATEGORIES
    )
  }

  getAllIncome(budgetId: number): Observable<IncomeViewModel[]> {
    return this.http.get<IncomeViewModel[]>(
        this.url + GET_INCOME + `/${budgetId}`
    )
  }

  getAllExpenses(budgetId: number): Observable<ExpenseViewModel[]> {
    return this.http.get<ExpenseViewModel[]>(
        this.url + GET_EXPENSES + `/${budgetId}`
    )
    }

  getIncomeExpenseDataStatistics(budgetId: number): Observable<IncomeExpenseDataStatisticsViewModel> {
      return this.http.get<IncomeExpenseDataStatisticsViewModel>(
          this.url + GET_INCOME_EXPENSE_DATA_STATISTICS + `/${budgetId}`
      )
  }

  getExpenseByDate(data: DashboardDateViewModel): Observable<ExpenseViewModel[]> {
      return this.http.post<ExpenseViewModel[]>(
          this.url + GET_EXPENSE_BY_DATE, data
      )
  }

  addEditIncome(income: IncomeViewModel): Observable<IncomeViewModel> {
    return this.http.post<IncomeViewModel>(
      this.url + ADD_EDIT_INCOME, income
    )
  }

  addEditExpense(expense: ExpenseViewModel): Observable<ExpenseViewModel> {
    return this.http.post<ExpenseViewModel>(
      this.url + ADD_EDIT_EXPENSE, expense
    )
  }

  deleteIncome(id: number): Observable<number> {
    return this.http.delete<number>(
      this.url + DELETE_INCOME + `/${id}`
    )
  }

  deleteExpense(id: number): Observable<number> {
    return this.http.delete<number>(
      this.url + DELETE_EXPENSE + `/${id}`
    )
  }
}