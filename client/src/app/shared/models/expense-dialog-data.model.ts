import { CategoriesViewModel } from "./categories.model";
import { ExpenseViewModel } from "./expense.model";

export interface ExpenseDialogDataModel {
    categories: CategoriesViewModel[];
    expense: ExpenseViewModel;
}