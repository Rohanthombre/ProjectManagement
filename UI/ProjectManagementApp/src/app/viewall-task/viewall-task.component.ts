import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Task } from '../Models/Task';
import { TaskQueryModel } from '../Models/TaskQueryModel';
import { TaskManagerService } from '../task-manager.service';


@Component({
  selector: 'app-viewall-task',
  templateUrl: './viewall-task.component.html',
  styleUrls: ['./viewall-task.component.css']
})
export class ViewallTaskComponent implements OnInit {

  errorMessage = '';
  tasks: Task[];
  title = 'View Task';

  taskName: string;
  parentTaskName: string;
  priorityFrom: number;
  priorityTo: number;
  startDate: Date;
  endDate: Date;


  // taskQueryModel: TaskQueryModel = {
  //   EndDate: null,
  //   ParentTask: null,
  //   PriorityFrom: null,
  //   PriorityTo: null,
  //   StartDate: null,
  //   TaskName: ""
  // };


  // viewQueryForm: FormGroup;
  // taskName: FormControl;
  // parentTaskName: FormControl;
  // priorityFormGroup: FormGroup;
  // priorityFrom: FormControl;
  // priorityTo: FormControl;
  // dateFormGroup: FormGroup;
  // startDate: FormControl;
  // endDate: FormControl;

  constructor(private taskService: TaskManagerService) { }

  ngOnInit() {

    // this.taskName = new FormControl(this.taskQueryModel.TaskName);
    // this.parentTaskName = new FormControl(this.taskQueryModel.ParentTask); 
    // this.priorityFrom = new FormControl(this.taskQueryModel.PriorityFrom);
    // this.priorityTo = new FormControl(this.taskQueryModel.PriorityTo);
    // this.priorityFormGroup = new FormGroup({
    //   priorityFrom: this.priorityFrom,
    //   priorityTo: this.priorityTo
    // });
    // this.startDate = new FormControl(this.taskQueryModel.StartDate);
    // this.endDate = new FormControl(this.taskQueryModel.EndDate);

    // this.dateFormGroup = new FormGroup({
    //   startDate: this.startDate,
    //   endDate: this.endDate
    // });

    // this.viewQueryForm = new FormGroup({
    //   taskName: this.taskName,
    //   priority: this.priorityFormGroup,
    //   parentTask: this.parentTaskName,
    //   dateGroup: this.dateFormGroup
    // });




    this.taskService.getAllTask().subscribe(
      tasks => {
        this.tasks = tasks;
      },
      error => this.errorMessage = <any>error
    );
  }

}
