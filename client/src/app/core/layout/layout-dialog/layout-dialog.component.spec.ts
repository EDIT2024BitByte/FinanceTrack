import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LayoutDialogComponent } from './layout-dialog.component';

describe('LayoutDialogComponent', () => {
  let component: LayoutDialogComponent;
  let fixture: ComponentFixture<LayoutDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LayoutDialogComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LayoutDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
