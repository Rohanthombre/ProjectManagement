import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddUpdateTaskComponent } from './addupdate-task.component';
import { Router, ActivatedRoute, RouterModule } from '@angular/router';
//import { TaskManagerService } from '../task-manager.service';

import { AppModule } from '../app.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { RouterTestingModule } from '@angular/router/testing';
import { ITaskService } from '../ITaskService';
import { TaskserviceServiceFake } from '../task-manager.service.service.mock';

describe('AddUpdateTaskComponent', () => {
  let component: AddUpdateTaskComponent;
  let fixture: ComponentFixture<AddUpdateTaskComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({

      providers: [{ provide: ITaskService, useClass: TaskserviceServiceFake}],

      imports: [
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        //BsDatepickerModule,
        BsDatepickerModule.forRoot(),
        RouterModule,
        RouterTestingModule
      ],


      declarations: [AddUpdateTaskComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddUpdateTaskComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
