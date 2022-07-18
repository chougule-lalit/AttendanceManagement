import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AttendanceTypesComponent } from './attendance-types.component';

describe('AttendanceTypesComponent', () => {
  let component: AttendanceTypesComponent;
  let fixture: ComponentFixture<AttendanceTypesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AttendanceTypesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AttendanceTypesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
