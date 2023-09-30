import { DatePipe } from '@angular/common';

import { Component, OnInit } from '@angular/core';

import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Specialization } from 'src/app/models/specialization';

import { Surgery } from 'src/app/models/surgery';
import { AccountService } from 'src/app/services/account.service';
import { SpecializationService } from 'src/app/services/specialization.service';

import { SurgeryService } from 'src/app/services/surgery.service';

 

@Component({

  selector: 'app-view-todays-surgery',

  templateUrl: './view-todays-surgery.component.html',

  styleUrls: ['./view-todays-surgery.component.scss']

})

export class ViewTodaysSurgeryComponent implements OnInit {

  surgeryList: Surgery[] | null = [];

  errorMsg: any = null;

  categoryFilter: string = "";

  isDropdownOpen: boolean = false;
  specializations:Specialization[]=[];

  categoryOptions: string[] = ["ANE", "CAR", "No Filter"];
  private sub$?:Subscription;
  role:string="";

 

  constructor(private surgeryService: SurgeryService, private router: Router, 
    private datePipe: DatePipe,private accountService:AccountService,private specializationService:
    SpecializationService) {}
  getRole(){
  this.sub$ = this.accountService.getRole().subscribe({
    next: (data) => {
      console.log(data);
      this.role=data;
    },
    error: (err) => {
      console.error(err.status);
     // this.statusCode = err.status;
    }
  })
}


getAllSpecialization()
{
  this.sub$=this.specializationService.getSpecialization().subscribe({
    next:(data)=>{
      console.log(data);
      this.specializations=data;
      let specialization=new Specialization();
        specialization.specializationCode=""
        specialization.specializationName="Select the specialization"
        this.specializations.push(specialization)
    },
    error:(err)=>{
      console.error(err)
      this.errorMsg="Some error occured";
    }
   })
}
ngOnInit(): void {
  this.getTodaySurgery()
  this.getRole();
  this.getAllSpecialization();
}
 
  getTodaySurgery(): void {

    this.surgeryService.getSurgeriesForToday().subscribe(

      (value) => {

        this.surgeryList = value;

        if (this.surgeryList.length === 0) {

          this.surgeryList = null;

          this.errorMsg = "No Surgery Scheduled For Today";

        }

      },

      (error) => {

        console.log("Some Error Occurred while Fetching Today's Surgery List", error);

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

 

  getCurrentDate(): string | null {

    return this.datePipe.transform(new Date(), 'yyyy-MM-dd');

  }

 

  getSurgeryDate(surgeryDate: Date): string | null {

    return this.datePipe.transform(surgeryDate, 'yyyy-MM-dd');

  }

  // Filtered surgery list

  get filteredSurgeryList(): Surgery[] {

    return this.surgeryList!.filter(surgery => {

      // If categoryFilter is empty, show all surgeries

      if (this.categoryFilter.trim() === '') {

        return true;

      }

      // Check if the surgery category contains the filter text (case-insensitive)

      return surgery.surgeryCategory.toLowerCase().includes(this.categoryFilter.toLowerCase());

    });

  }

 

  // Function to get a category-specific background color

  getCategoryColor(category: string): string {

    // You can define colors based on categories or use a default color

    switch (category.toLowerCase()) {

      case 'ane':

        return '#f1c604'; // Blue

      case 'car':

        return '#04b31b'; // Red

      case 'category3':

        return '#2ecc71'; // Green

      // Add more cases for other categories as needed

      default:

        return '#3498db'; // Default to blue

    }

  }

 

  toggleDropdown() {

    this.isDropdownOpen = !this.isDropdownOpen;

  }

}



