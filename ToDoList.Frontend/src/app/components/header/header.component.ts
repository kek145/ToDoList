import { Component } from '@angular/core';
import { IdentityService } from 'src/api/services/identity.service';
import {MatDialog} from "@angular/material/dialog";
import {ModalComponent} from "../modal/modal.component";
import {CreateUpdateComponent} from "../create-update/create-update.component";

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent {

  constructor(private identityService: IdentityService, private dialog: MatDialog) {}

  protected openModal(): void {
    const dialogRef = this.dialog.open(CreateUpdateComponent, {
      width: '1200px',
      height: '700px',
      data: { action: 'Create' }
    });

    dialogRef.afterClosed().subscribe((_result: any) => {
      console.log('The error modal was closed');
    });
  }

  protected isUserLogged(): boolean {
    return this.identityService.isLogged();
  }

  protected logout(): void {
    localStorage.clear();
  }
}
