import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'at-team-member-detail',
  templateUrl: './team-member-detail.component.html',
  styles: [`
    .team-categories{
      margin: 0;
    }
  `]
})
export class TeamMemberDetailComponent implements OnInit {

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit(): void {
  }

}
