import { CategoriesViewModel } from "./categories.model";

export interface ExpenseViewModel {
    id: number;
    budgetId?: number;
    amount?: number;
    date?: Date;
    description?: string;
    categoryId?: number;
    category?: CategoriesViewModel;
    isDeleted: boolean;
}