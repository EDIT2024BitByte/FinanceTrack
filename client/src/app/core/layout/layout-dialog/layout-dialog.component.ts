import { BudgetViewModel } from './../../../shared/models/budget.model';
import { BudgetService } from './../../../shared/services/budget.service';
import { Component } from '@angular/core';
import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { ButtonDirective } from '@coreui/angular';
import { MatInputModule } from "@angular/material/input";
import { MatFormFieldModule } from "@angular/material/form-field";
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { NgxUiLoaderModule } from 'ngx-ui-loader';

@Component({
  selector: 'app-layout-dialog',
  standalone: true,
  imports: [
    MatDialogModule,
    ButtonDirective,
    MatFormFieldModule,
    MatInputModule,
    ReactiveFormsModule,
    CommonModule,
    NgxUiLoaderModule
  ],
  templateUrl: './layout-dialog.component.html',
  styleUrl: './layout-dialog.component.css'
})
export class LayoutDialogComponent {
  savingFailed: boolean = false;
  budgetForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private budgetService: BudgetService,
    private dialogRef: MatDialogRef<LayoutDialogComponent>) { }

  ngOnInit(): void {
    this.budgetForm = this.formBuilder.group({
      totalAmount: ['', Validators.required]
    });
  }

  saveBudget(): void {
    var userId = sessionStorage.getItem("userId");

    if (userId) {
      this.budgetService.saveBudget(({
        userId: +userId,
        totalAmount: this.budgetForm.value.totalAmount
      } as BudgetViewModel))
        .subscribe(res => {
          if (res!!) {
            sessionStorage.setItem('isBudgetSet', res.toString());
            sessionStorage.setItem('budgetId', res.id.toString());
            this.dialogRef.close();
            location.reload();
          }
        },
          error => {
            this.savingFailed = true;
          })
    }
  }
}
