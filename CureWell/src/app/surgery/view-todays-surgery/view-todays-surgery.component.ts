import { Component, OnInit } from '@angular/core';

import { Router } from '@angular/router';

import { Surgery } from 'src/app/models/surgery';

import { SurgeryService } from 'src/app/services/surgery.service';

@Component({
  selector: 'app-view-todays-surgery',
  templateUrl: './view-todays-surgery.component.html',
  styleUrls: ['./view-todays-surgery.component.scss']
})


export class ViewTodaysSurgeryComponent implements OnInit {
  surgeryList: Surgery[] | null = [];
  errorMsg: any = null;
  constructor(private surgeryService: SurgeryService, private router: Router) {
  }
  ngOnInit(): void {
    this.getTodaySurgery()
  }
  
  getTodaySurgery(): void {
    this.surgeryService.getSurgeriesForToday().subscribe(value => {
      this.surgeryList = value;
      console.log(this.surgeryList);
      if (this.surgeryList?.length == 0) {
        this.surgeryList = null;
        this.errorMsg = "No Surgery Scheduled For Today";
      }
    },

      error => {
        console.log("Error Occured in view-todays-surgery.component.ts", error)
        this.surgeryList = null;
        this.errorMsg = error;
      },

      () => {
        console.log("Today's Surgery Fetched Successfully");
      }

    );

  }
  editSurgery(surgeryId: number): void {

    this.router.navigate(['/surgery/update-surgery/', surgeryId]);

  }
  getCurrentDate(): number {
    return new Date().getDay(); // A method to get today's date
  }

  getSurgeryDate(s: Surgery): number | undefined {

    return new Date(s.surgeryDate!).getDay();
  }
}