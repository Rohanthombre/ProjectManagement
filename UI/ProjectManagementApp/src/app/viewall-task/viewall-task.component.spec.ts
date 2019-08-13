import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewallTaskComponent } from './viewall-task.component';
import { TaskManagerService } from '../task-manager.service';
import { AppModule } from '../app.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { TaskQueryPipe } from '../task-query.pipe';
import { RouterModule } from '@angular/router';
import { ITaskService } from '../ITaskService';
import { TaskserviceServiceFake } from '../task-manager.service.service.mock';

describe('ViewallTaskComponent', () => {
  let component: ViewallTaskComponent;
  let fixture: ComponentFixture<ViewallTaskComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      
      providers: [{ provide: ITaskService, useClass: TaskserviceServiceFake}],
      
      imports: [
        
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        //BsDatepickerModule,
        BsDatepickerModule.forRoot(),
        RouterModule
      ],
      

      declarations: [ ViewallTaskComponent , TaskQueryPipe]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewallTaskComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
