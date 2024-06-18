import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialog} from "@angular/material/dialog";
import {Priority} from "../../../enum/Priority.enum";
import {enumHelper} from "../../../helpers/enum.helper";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {INoteRequestModel} from "../../../models/request/INoteRequest.model";
import {IBaseResponseModel} from "../../../models/response/IBaseResponse.model";
import {INoteResponseModel} from "../../../models/response/INoteResponse.model";
import {HttpStatusCode} from "@angular/common/http";
import {ModalComponent} from "../modal/modal.component";
import {IErrorResponseModel} from "../../../models/response/IErrorResponse.model";
import {NotesService} from "../../../api/services/notes.service";

@Component({
  selector: 'app-create-update',
  templateUrl: './create-update.component.html',
  styleUrls: ['./create-update.component.scss']
})
export class CreateUpdateComponent {

  loading: boolean = false;
  minDate: Date = new Date();

  noteRequestModel!: INoteRequestModel;

  public priority: Priority = Priority.Easy;
  public priorityEnum = Priority;
  public enumValues = enumHelper;


  noteForm = new FormGroup({
    title: new FormControl('', [Validators.required, Validators.minLength(2)]),
    description: new FormControl('', [Validators.required, Validators.minLength(10)]),
    priority: new FormControl('', Validators.required),
    deadline: new FormControl('', Validators.required)
  });

  constructor(@Inject(MAT_DIALOG_DATA) public data: any, private dialog: MatDialog, private noteService: NotesService) {
    this.minDate = new Date();
    this.minDate.setDate(this.minDate.getDate() + 1);
  }

  actionTranslations: {[key: string]: string} = {
    'Create': 'Створити',
    'Update': 'Оновити'
  };

  public onSubmit(): void {
    if (this.noteForm.valid) {
      this.loading = true;
      const request: INoteRequestModel = this.noteRequestModel = this.noteForm.value as INoteRequestModel;
      if(this.data.action === "Create") {
        this.createNote(request);
      }
      else if(this.data.action === 'Update') {
        this.updateNote(request, this.data.noteId);
      }
      else {
        alert("Ok");
      }
    }
  }

  private updateNote(request: INoteRequestModel, noteId: number) {
    this.noteService.updateNote(noteId,request).subscribe({
      next: (): void => {
        const dialogRef = this.dialog.open(ModalComponent, {
          width: '550px',
          height: '350px',
          data: {status: 'Success', message: "Нотатку успішно оновлена"}
        });

        dialogRef.afterClosed().subscribe((_result: any) => {
          console.log('The error modal was closed');
        });
        this.loading = false;
      },
      error: (_error: IErrorResponseModel) => {
        const dialogRef = this.dialog.open(ModalComponent, {
          width: '550px',
          height: '350px',
          data: {status: 'Error', message: `${_error.error.errors[0]}`}
        });

        dialogRef.afterClosed().subscribe((_result: any) => {
          console.log('The error modal was closed');
        });
      }
    });
  }

  private createNote(request: INoteRequestModel): void {
    this.noteService.createNote(request).subscribe({
      next: (_response: IBaseResponseModel<INoteResponseModel>) => {
        if (_response.statusCode === HttpStatusCode.Created) {
          this.dialog.open(ModalComponent, {
            width: '550px',
            height: '350px',
            data: {status: 'Success', message: `${_response.message}`}
          });
        }
        this.loading = false;
      },
      error: (_error: IErrorResponseModel) => {
        if (_error.error.statusCode === HttpStatusCode.BadRequest) {
          const dialogRef = this.dialog.open(ModalComponent, {
            width: '550px',
            height: '350px',
            data: {status: 'Error', message: `${_error.error.errors[0]}`}
          });

          dialogRef.afterClosed().subscribe((_result: any) => {
            console.log('The error modal was closed');
          });
        }
      }
    });
  }
}
