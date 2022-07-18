import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AttendanceTypeFormComponent } from './attendance-type-form.component';

describe('AttendanceTypeFormComponent', () => {
  let component: AttendanceTypeFormComponent;
  let fixture: ComponentFixture<AttendanceTypeFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AttendanceTypeFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AttendanceTypeFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
