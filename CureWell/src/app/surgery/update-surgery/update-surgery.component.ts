import { DatePipe } from '@angular/common';

import { Component, OnInit } from '@angular/core';

import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';

import { ActivatedRoute, Router } from '@angular/router';

import { Surgery } from 'src/app/models/surgery';

import { SurgeryService } from 'src/app/services/surgery.service';

//import { MatchingValidation } from 'src/app/shared/match-validators';

 

@Component({

  selector: 'app-update-surgery',

  templateUrl: './update-surgery.component.html',

  styleUrls: ['./update-surgery.component.scss']

})

export class UpdateSurgeryComponent implements OnInit {

  updateForm! : FormGroup;

  submitted: boolean = false;

  id: number;

  surgery!:Surgery;

 

  constructor(private activatedRoute : ActivatedRoute,

    private surgeryService:SurgeryService,

    private fb : FormBuilder,

    private datePipe : DatePipe,

    private router: Router){

    this.id = this.activatedRoute.snapshot.params["surgeryId"];

  }

 

 

 

  ngOnInit(): void {

    this.surgeryService.getSurgeryById(this.id).subscribe(

      (data) => {this.surgery = data},

      (err) => {console.error(err)},

      () => {this.placeData();}

    )

  }

 

  placeData(){

    this.updateForm = this.fb.group({

      surgeryId:[this.surgery.surgeryId, [Validators.required]],

      doctorId:[this.surgery.doctorId, [Validators.required]],

      surgeryDate:[this.datePipe.transform(this.surgery.surgeryDate,"yyyy-MM-dd"), Validators.required],

      startTime:[this.surgery.startTime, [Validators.required, Validators.min(0), Validators.max(24)]],

      endTime: [this.surgery.endTime, [Validators.required, Validators.min(0), Validators.max(24)]],

      surgeryCategory: [this.surgery.surgeryCategory, Validators.required]

    }

    )

  }

 

 

  onSubmit() {

    this.submitted = true;

    console.log(this.updateForm);

    console.log(this.f['surgeryId']);

 

    this.surgery.startTime = this.f['startTime'].value;

    this.surgery.endTime = this.f['endTime'].value;

   

    this.surgeryService.updateSurgery(this.surgery).subscribe(

      (result) => {

        // Handle the response

        if (result) {

          console.log("Surgery updated successfully");

        } else {

          console.error("Surgery update failed");

        }

      },

      (error) => {

        console.error("Some Error Occured while updating surgery:", error);

      },

      () => {

        this.backToViewSurgery()

      }

    );

   

  }

 

  backToViewSurgery()

  {

    this.router.navigate(['/surgery/view-todays-surgery']);

  }

 

 

  getControl(controlName: string): AbstractControl {

    return this.updateForm.controls[controlName];

  }

 

 

  get f(): { [controlName: string]: AbstractControl } {

    return this.updateForm.controls;

  }

 

  get isButtonDisabled() {

    const f = this.updateForm.controls;

    return (

      ((this.submitted || f['startTime'].touched || f['startTime'].dirty) &&

      f['startTime'].errors) ||

      ((this.submitted || f['endTime'].touched || f['endTime'].dirty) &&

      f['endTime'].errors)

    ) || f['endTime'].value <= f['startTime'].value;

  }

}