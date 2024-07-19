import { ExpenseDialogDataModel } from './../../../app/shared/models/expense-dialog-data.model';
import { CommonModule } from '@angular/common';
import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatTooltipModule } from '@angular/material/tooltip';
import { ButtonDirective } from '@coreui/angular';
import { ExpenseViewModel } from '../../../app/shared/models/expense.model';
import { CashflowService } from '../../../app/shared/services/cashflow.service';
import { MatSelectModule } from '@angular/material/select';
import { NgxUiLoaderModule } from 'ngx-ui-loader';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
@Component({
  selector: 'app-expenses-dialog',
  standalone: true,
  imports: [
    MatDialogModule,
    ButtonDirective,
    MatFormFieldModule,
    MatInputModule,
    ReactiveFormsModule,
    CommonModule,
    MatDatepickerModule,
    MatTooltipModule,
    MatSelectModule,
    NgxUiLoaderModule
  ],
  templateUrl: './expenses-dialog.component.html',
  styleUrl: './expenses-dialog.component.css'
})
export class ExpensesDialogComponent implements OnInit, OnDestroy {
  // subs
  subscriptions: Subscription[] = [];

  expenseForm: FormGroup;

  savingFailed: boolean = false;


  constructor(public dialogRef: MatDialogRef<ExpensesDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public dialogData: ExpenseDialogDataModel,
    private fb: FormBuilder,
    private cashflowService: CashflowService,
    private toastr: ToastrService) { }


  ngOnInit(): void {
    this.expenseForm = this.fb.group({
      amount: new FormControl(this.dialogData.expense.amount, [Validators.required, Validators.pattern(/^\d+(\.\d{1,2})?$/)]),
      date: new FormControl(this.dialogData.expense.date ?? new Date(), Validators.required),
      description: new FormControl(this.dialogData.expense.description),
      category: new FormControl(this.dialogData.expense.categoryId, Validators.required)
    })
  }

  addEditExpense(): void {
    if (this.expenseForm.invalid) {
      this.expenseForm.markAllAsTouched();
      return;
    }
    this.dialogData.expense.budgetId = Number(sessionStorage.getItem('budgetId'));

    this.subscriptions.push(this.cashflowService.addEditExpense(({
      budgetId: this.dialogData.expense.budgetId,
      id: this.dialogData.expense.id,
      amount: this.expenseForm.value.amount,
      description: this.expenseForm.value.description,
      date: this.expenseForm.value.date,
      categoryId: this.expenseForm.value.category
    } as ExpenseViewModel))
      .subscribe(res => {
        if (res!!) {
          this.toastr.success("Expense saved successfully.", "Success");
          this.dialogRef.close();
        }
      },
        error => {
          this.savingFailed = true;
        }));
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach((sub) => {
      if (sub && !sub.closed) {
        sub.unsubscribe();
      }
    });
  }
}