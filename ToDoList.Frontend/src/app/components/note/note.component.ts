import { Component } from '@angular/core';
import {MatDialog} from "@angular/material/dialog";
import {Priority} from "../../../enum/Priority.enum";
import { enumHelper } from "../../../helpers/enum.helper";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {NotesService} from "../../../api/services/notes.service";
import {INoteRequestModel} from "../../../models/request/INoteRequest.model";
import {IBaseResponseModel} from "../../../models/response/IBaseResponse.model";
import {INoteResponseModel} from "../../../models/response/INoteResponse.model";
import {IErrorResponseModel} from "../../../models/response/IErrorResponse.model";
import {ModalComponent} from "../modal/modal.component";
import {HttpStatusCode} from "@angular/common/http";

@Component({
  selector: 'app-note',
  templateUrl: './note.component.html',
  styleUrls: ['./note.component.scss']
})
export class NoteComponent {
  loading: boolean = false;

  public priority: Priority = Priority.Easy;
  public priorityEnum = Priority;
  public enumValues = enumHelper;

  noteRequestModel!: INoteRequestModel;

  noteForm = new FormGroup({
    title: new FormControl('', [Validators.required, Validators.minLength(2)]),
    description: new FormControl('', [Validators.required, Validators.minLength(10)]),
    priority: new FormControl('', Validators.required),
    deadline: new FormControl('', Validators.required)
  });

  constructor(private noteService: NotesService, private dialog: MatDialog) { }


  onSubmit(): void {
    if(this.noteForm.valid) {
      this.loading = true;
      const request: INoteRequestModel = this.noteRequestModel = this.noteForm.value as INoteRequestModel;
      this.noteService.createNote(request).subscribe({
        next: (_response: IBaseResponseModel<INoteResponseModel>) => {
          if (_response.statusCode === HttpStatusCode.Created) {
            this.dialog.open(ModalComponent, {
              width: '550px',
              height: '350px',
              data: { status: 'Success', message: `${_response.message}` }
            });
          }
          this.loading = false;
        },
        error: (_error: IErrorResponseModel) => {
          if(_error.error.statusCode === HttpStatusCode.BadRequest) {
            const dialogRef = this.dialog.open(ModalComponent, {
              width: '550px',
              height: '350px',
              data: { status: 'Error', message: `${_error.error.errors[0]}` }
            });

            dialogRef.afterClosed().subscribe((_result: any) => {
              console.log('The error modal was closed');
            });
          }
        }
      });
    }

  }
}
