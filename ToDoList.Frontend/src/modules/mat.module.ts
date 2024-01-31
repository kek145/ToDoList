import { NgModule } from "@angular/core";
import { MatIconModule } from '@angular/material/icon';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { MatInputModule } from '@angular/material/input';
import { MatRadioModule } from '@angular/material/radio';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSelectModule } from '@angular/material/select';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatNativeDateModule } from '@angular/material/core';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatDatepickerModule } from '@angular/material/datepicker';

@NgModule({
    declarations:[
    ],
    imports: [
        MatIconModule,
        MatSortModule,
        MatTableModule,
        MatInputModule,
        MatRadioModule,
        MatButtonModule,
        MatDialogModule,
        MatSelectModule,
        MatToolbarModule,
        MatSnackBarModule,
        MatPaginatorModule,
        MatFormFieldModule,
        MatDatepickerModule,
        MatNativeDateModule
    ]
})

export class MatModule { }