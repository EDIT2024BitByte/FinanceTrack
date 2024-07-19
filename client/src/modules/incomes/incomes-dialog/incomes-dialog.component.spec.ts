import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IncomesDialogComponent } from './incomes-dialog.component';

describe('IncomesDialogComponent', () => {
  let component: IncomesDialogComponent;
  let fixture: ComponentFixture<IncomesDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [IncomesDialogComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(IncomesDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
