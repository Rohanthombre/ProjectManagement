import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { Task } from '../Models/Task';
import { ProjectmanagementService } from '../service/projectmanagement.service';

@Component({
  selector: 'app-add-task',
  templateUrl: './add-task.component.html',
  styleUrls: ['./add-task.component.css']
})
export class AddTaskComponent implements OnInit {
  pageTitle = 'Add Project';

  AddUpdateTaskFormGroup: FormGroup;
  projectName: FormControl;
  taskName: FormControl;
  priority: FormControl;
  parentTask: FormControl;
  startDate: FormControl;
  endDate: FormControl;
  userName: FormControl;


  formSubmitted: boolean;
  isEditMode: boolean;
  editProjectId: number;


  searchForm: FormGroup;
  searchInputControl: FormControl;

  singleSelect: any = [];
  taskConfig = {
    displayKey: "TaskName", //if objects array passed which key to be displayed defaults to description
    search: true,
    placeholder: "Select",
    noResultsFound: 'No results found!',
    searchPlaceholder: 'Search'

  };

  userConfigconfig = {
    displayKey: "FirstName", //if objects array passed which key to be displayed defaults to description
    search: true,
    placeholder: "Select",
    noResultsFound: 'No results found!',
    searchPlaceholder: 'Search'

  };

  projectConfig = {
    displayKey: "ProjectName", //if objects array passed which key to be displayed defaults to description
    search: true,
    placeholder: "Select",
    noResultsFound: 'No results found!',
    searchPlaceholder: 'Search'

  };
  userOptions: any;
  projectOptions: any;
  taskOptions: any;

  errorMessage = '';
  successMessage = '';

  @Input()
  tasks: Task[];

  createOrEditTaskModel: Task = {
    TaskId: null,
    TaskName: "null",
    ParentTask: null,
    Project: null,
    StartDate: null,
    EndDate: null,
    Priority: null,
    Status: null,
    UserId: null
  };


  constructor(private projectManagementService: ProjectmanagementService, private fb: FormBuilder) { }

  ngOnInit() {

    this.projectName = new FormControl('', Validators.required);
    this.taskName = new FormControl('', [Validators.required,
    Validators.minLength(2),
    Validators.maxLength(80)]);
    this.priority = new FormControl('', Validators.required);
    this.endDate = new FormControl('', Validators.required);
    this.priority = new FormControl('', Validators.required);
    this.parentTask = new FormControl('');
    this.startDate = new FormControl('', Validators.required);
    this.endDate = new FormControl('', Validators.required);
    this.userName = new FormControl('', Validators.required);


    this.AddUpdateTaskFormGroup = new FormGroup({
      projectName: this.projectName,
      taskName: this.taskName,
      priority: this.priority,
      parentTask: this.parentTask,
      startDate: this.startDate,
      endDate: this.endDate,
      userName: this.userName
    });

    this.loadProjectDetails();
    this.loadTaskDetails();
    this.loadUserDetails();

  }
  onFormSubmit() {

    this.formSubmitted = true;

    if (this.AddUpdateTaskFormGroup.valid) {

      this.createOrEditTaskModel.Project = this.AddUpdateTaskFormGroup.controls.projectName.value.ProjectId;
      this.createOrEditTaskModel.TaskName = this.AddUpdateTaskFormGroup.controls.taskName.value;
      this.createOrEditTaskModel.Priority = this.AddUpdateTaskFormGroup.controls.priority.value;
      this.createOrEditTaskModel.ParentTask = this.AddUpdateTaskFormGroup.controls.parentTask.value.TaskId;
      this.createOrEditTaskModel.StartDate = this.AddUpdateTaskFormGroup.controls.startDate.value;
      this.createOrEditTaskModel.EndDate = this.AddUpdateTaskFormGroup.controls.endDate.value;
      this.createOrEditTaskModel.UserId = this.AddUpdateTaskFormGroup.controls.userName.value.UserId;

      if (!this.isEditMode) {

        this.CreateTaskService(this.createOrEditTaskModel);
      }
      else {
        this.EditTaskService(this.createOrEditTaskModel);
      }
    }
  }

  CreateTaskService(taskModel: Task) {
    this.projectManagementService.createTask(taskModel).subscribe(
      () => {
        this.successMessage = 'Record saved Successfully';
        window.alert(this.successMessage);
        this.resetForm();

        this.loadProjectDetails();
      },
      error => this.errorMessage = <any>error
    );
  }

  EditTaskService(taskModel: Task) {
    this.projectManagementService.editTask(taskModel).subscribe(
      () => {
        this.successMessage = 'Record saved Successfully';
        window.alert(this.successMessage);
        this.resetForm();

        this.loadProjectDetails();
      },
      error => this.errorMessage = <any>error
    );
  }

  loadUserDetails(): void {
    this.projectManagementService.getAllUsers().subscribe(x => {
      this.userOptions = x;
    },
      error => this.errorMessage = <any>error
    );
  }

  loadProjectDetails(): void {
    this.projectManagementService.getAllProjects().subscribe(x => {
      this.projectOptions = x;
    },
      error => this.errorMessage = <any>error
    );
  }

  loadTaskDetails(): void {
    this.projectManagementService.getAllTask().subscribe(x => {
      this.taskOptions = x;
    },
      error => this.errorMessage = <any>error
    );
  }
  resetForm() {
    // this.newTaskForm.reset();
    this.AddUpdateTaskFormGroup.reset();
  }
}
