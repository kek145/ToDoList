import { NgModule } from "@angular/core";
import {MatMenuModule} from "@angular/material/menu"
import {MatIconModule} from "@angular/material/icon"
import {MatListModule} from "@angular/material/list"
import {MatCardModule} from "@angular/material/card"
import {MatSortModule} from "@angular/material/sort"
import {MatInputModule} from "@angular/material/input"
import {MatBadgeModule} from "@angular/material/badge"
import {MatRadioModule} from "@angular/material/radio"
import {MatTableModule} from "@angular/material/table"
import {MatSelectModule} from "@angular/material/select"
import {MatButtonModule} from "@angular/material/button"
import {MatSliderModule} from "@angular/material/slider"
import {MatDialogModule} from "@angular/material/dialog"
import {MatToolbarModule} from "@angular/material/toolbar"
import {MatSidenavModule} from "@angular/material/sidenav"
import {MatNativeDateModule} from "@angular/material/core"
import {MatCheckboxModule} from "@angular/material/checkbox"
import {MatPaginatorModule} from "@angular/material/paginator"
import {MatDatepickerModule} from "@angular/material/datepicker"
import {MatAutocompleteModule} from "@angular/material/autocomplete"

@NgModule({
exports:[
    MatInputModule,
    MatSelectModule,
    MatAutocompleteModule,
    MatToolbarModule,
    MatMenuModule,
    MatIconModule,
    MatButtonModule,
    MatBadgeModule,
    MatSidenavModule,
    MatListModule,
    MatCardModule,
    MatSliderModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatRadioModule,
    MatCheckboxModule,
    MatDialogModule
]
})
export class MaterialModule{}