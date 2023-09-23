import { Component } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { Doctor } from 'src/app/models/doctor';
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

  constructor(private fb:FormBuilder,private doctorService:DoctorService)
  {}
  ngOnInit():void{
    this.registerForm= this.fb.group({
      doctorName:[null,[Validators.required]],
    })
  }
  
  onSubmit(){
    this.submitted=true;
    this.doctor.doctorName=this.registerForm.controls["doctorName"].value;
    this.sub$=this.doctorService.addDoctor(this.doctor).subscribe({
      next:(data)=>{
        if(data==true)
        {
          alert("Doctor Added Successfully");
        }
        else{
          alert("Some error occured");
          this.registerForm.reset();
        }
      },
      error:(err)=>{
        console.error(err)
        this.errorAddMsg=err.message();
        this.msg="Some error occured";       

      }
     })
    console.log(this.registerForm);
  }

  get f():{[controlName:string]:AbstractControl}{
    return this.registerForm.controls;
  }

  doReset(){
    this.submitted=false;
    this.registerForm.reset();
   // window.location.reload();
  }
}
