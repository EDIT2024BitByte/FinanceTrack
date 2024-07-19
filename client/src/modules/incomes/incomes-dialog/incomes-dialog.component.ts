import { IncomeViewModel } from './../../../app/shared/models/income.model';
import { CommonModule } from '@angular/common';
import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { ButtonDirective } from '@coreui/angular';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatTooltipModule } from '@angular/material/tooltip';
import { CashflowService } from '../../../app/shared/services/cashflow.service';
import { NgxUiLoaderModule } from 'ngx-ui-loader';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-incomes-dialog',
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
    NgxUiLoaderModule
  ],
  templateUrl: './incomes-dialog.component.html',
  styleUrl: './incomes-dialog.component.css'
})
export class IncomesDialogComponent implements OnInit, OnDestroy {
  // subs
  subscriptions: Subscription[] = [];

  incomeForm: FormGroup;

  savingFailed: boolean = false;

  constructor(public dialogRef: MatDialogRef<IncomesDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public dialogData: IncomeViewModel,
    private fb: FormBuilder,
    private cashflowService: CashflowService,
    private toastr: ToastrService) { }


  ngOnInit(): void {
    this.incomeForm = this.fb.group({
      amount: new FormControl(this.dialogData.amount, [Validators.required, Validators.pattern(/^\d+(\.\d{1,2})?$/)]),
      date: new FormControl(this.dialogData.date ?? new Date(), Validators.required),
      description: new FormControl(this.dialogData.description)
    })
  }

  addEditIncome(): void {

    if (this.incomeForm.invalid) {
      this.incomeForm.markAllAsTouched();
      return;
    }

    this.dialogData.budgetId = Number(sessionStorage.getItem('budgetId'));

    this.subscriptions.push(this.cashflowService.addEditIncome(({
      budgetId: this.dialogData.budgetId,
      id: this.dialogData.id,
      amount: this.incomeForm.value.amount,
      description: this.incomeForm.value.description,
      date: this.incomeForm.value.date
    } as IncomeViewModel))
      .subscribe(res => {
        if (res!!) {
          this.toastr.success("Income saved successfully.", "Success");
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