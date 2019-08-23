import { Component, OnInit, Input } from '@angular/core';
import { TaskQueryModel } from '../Models/taskquerymodel';
import { FormGroup, FormControl } from '@angular/forms';
import { Project } from '../Models/project';
import { ProjectmanagementService } from '../service/projectmanagement.service';
import { Task } from '../Models/Task';

@Component({
  selector: 'app-view-task',
  templateUrl: './view-task.component.html',
  styleUrls: ['./view-task.component.css']
})
export class ViewTaskComponent implements OnInit {
 
  searchForm: FormGroup;
  searchInputControl: FormControl;

  @Input()
  tasks: Task[];

  sortBy: String = "StartDate";

  searchUserInputValue:string="";
  
  errorMessage = '';
  successMessage = '';

  constructor(private projectManagementService: ProjectmanagementService) { 
    this.initFormsControl();
  }

  ngOnInit() {
  }

  private initFormsControl() {
    this.searchInputControl = new FormControl(this.searchUserInputValue);
    

    this.searchForm = new FormGroup({
      searchInputControl: this.searchInputControl
    });

    this.searchForm.valueChanges.subscribe(x=>
      {
        this.searchUserInputValue=this.searchInputControl.value;
      });
  }

  loadTaskDetails(): void {
    this.projectManagementService.getAllTask().subscribe(x => {
      this.tasks = x;
    },
      error => this.errorMessage = <any>error
    );
  }

}
