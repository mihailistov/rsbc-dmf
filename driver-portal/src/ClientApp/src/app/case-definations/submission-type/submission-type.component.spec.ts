import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubmissionTypeComponent } from './submission-type.component';

describe('SubmissionTypeComponent', () => {
  let component: SubmissionTypeComponent;
  let fixture: ComponentFixture<SubmissionTypeComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SubmissionTypeComponent]
    });
    fixture = TestBed.createComponent(SubmissionTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
