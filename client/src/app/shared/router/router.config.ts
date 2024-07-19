import { Routes } from '@angular/router';
import { LayoutComponent } from '../../core/layout/layout.component';
import { LoginComponent } from '../../../modules/authorization/login/login.component';
import { DashboardComponent } from '../../../modules/dashboard/dashboard.component';
import { IncomesComponent } from '../../../modules/incomes/incomes.component';
import { AuthGuard } from '../../auth.guard';
import { ExpensesComponent } from '../../../modules/expenses/expenses.component';

export const routes: Routes = [
    { path: 'login', component: LoginComponent },
    {
        path: 'layout',
        component: LayoutComponent,
        canActivate: [AuthGuard],
        children: [
            { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
            { path: 'dashboard', component: DashboardComponent },
            { path: 'incomes', component: IncomesComponent },
            { path: 'expenses', component: ExpensesComponent }
        ]
    },
    { path: '', redirectTo: 'login', pathMatch: 'full' },
    { path: '**', redirectTo: 'login', pathMatch: 'full' }
];
