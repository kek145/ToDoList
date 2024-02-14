import { Component, OnInit } from '@angular/core';
import { IdentityService } from 'src/api/services/identity.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  public data: number = 0;

  constructor(private identityService: IdentityService) { }
  ngOnInit(): void {
    this.identityService.identityUserId().subscribe({
      next: (_response: any) => {
        this.data = _response.id;
      },
      error: (_error: any) => {
        alert("error123!@#");
      }
    });
  }

}
