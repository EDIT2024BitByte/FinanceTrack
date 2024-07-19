import { IncomeExpenseTransactionsViewModel } from "./income-expense-transactions";

export interface IncomeExpenseDataStatisticsViewModel {
    incomeSum: number;
    expenseSum: number;
    incomeExpenseTransactions: IncomeExpenseTransactionsViewModel[];
}