import { Component } from '@angular/core';

import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { Doctor } from 'src/app/models/doctor';
import { AccountService } from 'src/app/services/account.service';

import { DoctorService } from 'src/app/services/doctor.service';

 

@Component({

  selector: 'app-add-doctor',

  templateUrl: './add-doctor.component.html',

  styleUrls: ['./add-doctor.component.scss']

})

export class AddDoctorComponent {
  registerForm!:FormGroup;
  submitted:boolean=false;
  msg:string="";
  errorAddMsg:string="";
  private sub$?:Subscription;
  doctor:Doctor=new Doctor();
  role:string="";
  constructor(private fb:FormBuilder,private doctorService:DoctorService,
    private accountService:AccountService,private router:Router)
  {}



  ngOnInit():void{
    if(!sessionStorage.getItem("token"))
    {
      this.router.navigateByUrl("/home");
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
          
           this.router.navigateByUrl("/home");
        }
      }
    )
    this.registerForm= this.fb.group({
      doctorName:[null,[Validators.required]],
    })

    
  }

 

  onSubmit(){
    this.submitted=true;
    console.log(this.submitted);
    this.doctor.doctorName=this.registerForm.controls["doctorName"].value;
    if(this.doctor.doctorName)
    this.sub$=this.doctorService.addDoctor(this.doctor).subscribe({
      next:(data)=>{
        if(data==true)
        {
          console.log("Doctor Added Successfully");
          window.alert("Doctor Added Successfully");
          this.registerForm.reset();
          this.router.navigateByUrl("/home");

        }
        else{

          console.log("Cannot add Doctor");
          alert("Some error occured");
          this.registerForm.reset();
        }
      },
      error:(err)=>{
        console.error(err)
        alert("Error Adding Doctor")
        this.errorAddMsg=err.message();
        this.msg="Some error occured";      
      }
     })
    console.log(this.registerForm);
  }

  get f():{[controlName:string]:AbstractControl}{
    return this.registerForm.controls;
  }

  get isButtonDisabled()
  {
    return (this.submitted || this.f['doctorName'].touched) && this.f['doctorName'].errors
  }
  doReset(){
    this.submitted=false;
    this.registerForm.controls["doctorName"].setErrors(null);
    this.registerForm.controls["doctorName"].updateValueAndValidity();
    this.router.navigateByUrl("/home");
  }

}

 