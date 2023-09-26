import { Component } from '@angular/core';

import { ActivatedRoute, Router } from '@angular/router';

import { DoctorService } from 'src/app/services/doctor.service';

import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

import { Doctor } from 'src/app/models/doctor';

import { Subscription } from 'rxjs';
import { AccountService } from 'src/app/services/account.service';

 

@Component({

  selector: 'app-update-doctor',

  templateUrl: './update-doctor.component.html',

  styleUrls: ['./update-doctor.component.scss']

})

export class UpdateDoctorComponent {

  constructor(private route:Router,private fb: FormBuilder,private doctorService:DoctorService,
    private router:ActivatedRoute,private accountService:AccountService){}

  doctorId:number=0;

  submitted:boolean=false;

  doctorForm!: FormGroup; // Using "!" to indicate it will be assigned later

  private sub$?:Subscription;

  doctorData!: Doctor; // Using "!" to indicate it will be assigned later
  role:string="";
  ngOnInit(){
    if(!sessionStorage.getItem("token"))
    {
      this.route.navigateByUrl("/home");
    }
    this.sub$=this.accountService.getRole().subscribe(data =>{
      
      console.log(data);
      this.role=data;
    },
    (err) => {
      console.error(err.status);
     // this.statusCode = err.status;
    },
    () => {
      if(!sessionStorage.getItem("token") || this.role!='Admin')
      {
        console.log("helloo");
        
         this.route.navigateByUrl("/home");
      }
    }
  )

    this.doctorId=this.router.snapshot.params["doctorId"];

    console.log(this.doctorId);

    // Initialize the form group and doctorData as needed

 

    this.doctorForm = this.fb.group({

 

      doctorId: [this.doctorId, [Validators.required]], // Default value for doctorId

 

      doctorName: [null, [Validators.required]], // Default value for doctorName

 

    });

  }

  get f():{[controlName:string]:AbstractControl}{
    return this.doctorForm.controls;
  }

  updateDoctorDetails(){

    this.submitted=true;

    const doctorId = this.doctorForm.get('doctorId')?.value;

    const updatedName = this.doctorForm.get('doctorName')?.value;

    this.doctorData=new Doctor();

    this.doctorData.doctorId=doctorId;

    this.doctorData.doctorName=updatedName;
    

    this.sub$=this.doctorService.updateDoctorDetails(this.doctorData).subscribe(

      (data) => {

        if(data==true){

          window.alert('Doctor details updated successfully');

          this.doctorForm.reset();
          this.doctorForm.controls["doctorName"].setErrors(null);
          this.route.navigateByUrl("/home")
      

        }

        else{
          
          alert("Error Adding Doctor")
          console.log("Error adding Doctor");

        }

      },

      (err) => {

        console.log(err);

        window.alert("Some error occured");

      }

    );

 

  }

  doReset(){

    this.submitted=false;

    this.doctorForm.reset();
    this.doctorForm.controls["doctorName"].setErrors(null);
    window.location.reload();

  }

}

 