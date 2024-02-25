import {Component, AfterViewInit } from '@angular/core';
import { IdentityService } from 'src/api/services/identity.service';
import {MatDialog} from "@angular/material/dialog";
import {ModalComponent} from "../modal/modal.component";
import {CreateUpdateComponent} from "../create-update/create-update.component";
import {AccountService} from "../../../api/services/account.service";
import {IUserFullNameResponseModel} from "../../../models/response/IUserFullNameResponseModel";

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements AfterViewInit {
  public fullName: string = '';
  private isAuthenticated: boolean = false;
  constructor(private identityService: IdentityService, private accountService: AccountService, private dialog: MatDialog) {}

  ngAfterViewInit(): void {
    this.isAuthenticated = this.isUserLogged();
    if(this.isAuthenticated) {
      this.getUserFullName();
    }
  }

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

  private getUserFullName(): void {
    this.accountService.getUserFullName().subscribe({
      next: (_response: IUserFullNameResponseModel) => {
        this.fullName = _response.fullName;
      },
      error: (_error: any): void => {
        alert(_error);
      }
    });
  }
}
