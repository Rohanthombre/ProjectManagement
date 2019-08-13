import { Component, OnInit } from '@angular/core';
import { Task } from '../Models/Task';
import { CreateTaskModel } from '../Models/CreateTaskModel';
import { FormBuilder, Validators } from '@angular/forms';
import { TaskManagerService } from '../task-manager.service';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormControl } from '@angular/forms';
import { EditTaskComponent } from '../edit-task/edit-task.component';
import { EditTaskModel } from '../Models/EditTaskModel';


@Component({
  selector: 'app-addupdate-task',
  templateUrl: './addupdate-task.component.html',
  styleUrls: ['./addupdate-task.component.css']
})
export class AddUpdateTaskComponent implements OnInit {

  pageTitle = '';
  // newTaskForm: any;
  editTask: Task;
  isEditMode: boolean;
  parentTasks: Task[];
  // newTask: Task;
  formSubmitted: boolean;
  errorMessage = '';
  successMessage = '';
  TaskId: any;
  priorityMin = 0;
  priorityMax = 30;

  createTaskModel: CreateTaskModel = {
    TaskName: "",
    ParentTaskId: null,
    Priority: null,
    StartDate: null,
    EndDate: null
  };

  editTaskModel: EditTaskModel = {
    TaskId : null,
    TaskName: "",
    ParentTaskId: null,
    Priority: null,
    StartDate: null,
    EndDate: null
  };

  AddUpdateTaskFormGroup: FormGroup;
  taskName: FormControl;
  parentTaskId: FormControl;
  priority: FormControl;
  startDate: FormControl;
  endDate: FormControl;

  constructor(private taskService: TaskManagerService, private router: Router, private route: ActivatedRoute) { }


  ngOnInit() {

    this.taskService.getAllTask().subscribe(
      tasks => {
        this.parentTasks = tasks;
      },
      error => this.errorMessage = <any>error
    );

    
    this.TaskId = this.route.snapshot.paramMap.get('id');


    if (this.isEmpty(this.TaskId)) {
      this.pageTitle = "Add Task"

      this.taskName = new FormControl('',
        [Validators.required,
        Validators.minLength(2),
        Validators.maxLength(80)]);
      this.parentTaskId = new FormControl('');
      this.priority = new FormControl(0, Validators.required);
      this.startDate = new FormControl('', Validators.required);
      this.endDate = new FormControl('', Validators.required);


      this.AddUpdateTaskFormGroup = new FormGroup({
        taskName: this.taskName,
        parentTaskId: this.parentTaskId,
        priority: this.priority,
        startDate: this.startDate,
        endDate: this.endDate
      })

    }
    else {

      this.pageTitle = "Edit Task"

      this.isEditMode = true;


      this.taskName = new FormControl('',
        [Validators.required,
        Validators.minLength(2),
        Validators.maxLength(80)]);
      this.parentTaskId = new FormControl('');
      this.priority = new FormControl(0, Validators.required);
      this.startDate = new FormControl('', Validators.required);
      this.endDate = new FormControl('', Validators.required);

      this.AddUpdateTaskFormGroup = new FormGroup({
        taskName: this.taskName,
        parentTaskId: this.parentTaskId,
        priority: this.priority,
        startDate: this.startDate,
        endDate: this.endDate
      })

      this.taskService.getTaskById(this.TaskId).subscribe(
        result1 => {
          
          this.AddUpdateTaskFormGroup.controls.taskName.setValue(result1.TaskName);
          this.AddUpdateTaskFormGroup.controls.priority.setValue(result1.Priority);

          this.AddUpdateTaskFormGroup.controls.parentTaskId.setValue(result1.ParentTask.TaskId);

          this.AddUpdateTaskFormGroup.controls.startDate.setValue(result1.StartDate);

          this.AddUpdateTaskFormGroup.controls.endDate.setValue(result1.EndDate);

        }
      )

    
    }

  }
  isEmpty(val) {
    return (val === undefined || val == null || val.length <= 0) ? true : false;
  }

  onFormSubmit() {

    this.formSubmitted = true;

    if (this.AddUpdateTaskFormGroup.valid) {


      if (!this.isEditMode) {
        // this.newTask = new Task();

        this.createTaskModel.TaskName = this.AddUpdateTaskFormGroup.controls.taskName.value;
        this.createTaskModel.ParentTaskId = this.AddUpdateTaskFormGroup.controls.parentTaskId.value;
        this.createTaskModel.Priority = this.AddUpdateTaskFormGroup.controls.priority.value;
        this.createTaskModel.StartDate = this.AddUpdateTaskFormGroup.controls.startDate.value;
        this.createTaskModel.EndDate = this.AddUpdateTaskFormGroup.controls.endDate.value;
        this.CreateTaskService(this.createTaskModel);

      }
      else {
        this.editTaskModel.TaskId = this.TaskId;
        
      this.editTaskModel.TaskName = this.AddUpdateTaskFormGroup.controls.taskName.value;
      this.editTaskModel.ParentTaskId = this.AddUpdateTaskFormGroup.controls.parentTaskId.value;
      this.editTaskModel.Priority = this.AddUpdateTaskFormGroup.controls.priority.value;
      this.editTaskModel.StartDate = this.AddUpdateTaskFormGroup.controls.startDate.value;
      this.editTaskModel.EndDate = this.AddUpdateTaskFormGroup.controls.endDate.value;
        this.EditTaskService(this.editTaskModel)
      }

    }
  }

  CreateTaskService(createTaskModel: CreateTaskModel) {
    this.taskService.createTask(createTaskModel).subscribe(
      () => {
        this.successMessage = 'Record saved Successfully';
        window.alert(this.successMessage);
        //this.resetForm();
        this.router.navigate(["/viewall-task"]);
      }
    );
  }

  EditTaskService(editTaskModel: EditTaskModel) {
    this.taskService.editTask(editTaskModel).subscribe(
      () => {
        this.successMessage = 'Record saved Successfully';
        window.alert(this.successMessage);
        
        //this.resetForm();
        this.router.navigate(["/viewall-task"]);
      }
    );
  }


  resetForm() {
    // this.newTaskForm.reset();
    this.AddUpdateTaskFormGroup.reset();
  }
}
