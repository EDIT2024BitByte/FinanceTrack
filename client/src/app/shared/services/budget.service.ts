import { Injectable } from "@angular/core";
import { environment } from "../../../environment/environment";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { BudgetViewModel } from "../models/budget.model";

const GET_BUDGET = '/budget/GetAll';
const CHECK_BUDGET = '/budget/CheckBudget';
const SAVE_BUDGET = '/budget/SaveBudget';
const GET_BUDGET_BY_USER_ID = '/budget/GetBudgetByUserId';

@Injectable({
  providedIn: 'root'
})
export class BudgetService {
  private url: string = environment.webApiBudgetUrl;

  constructor(private http: HttpClient) { }

  getBudget(): Observable<any> {
    return this.http.get<any>(
      this.url + GET_BUDGET
    )
  }

  checkBudget(userId: number): Observable<boolean> {
    return this.http.get<boolean>(
      this.url + CHECK_BUDGET + `/${userId}`
    )
  }

  saveBudget(budget: BudgetViewModel): Observable<BudgetViewModel> {
    return this.http.post<BudgetViewModel>(
      this.url + SAVE_BUDGET, budget
    )
  }

  getBudgetByUserId(userId: number): Observable<BudgetViewModel> {
    return this.http.get<BudgetViewModel>(
      this.url + GET_BUDGET_BY_USER_ID + `/${userId}`
    )
  }
}