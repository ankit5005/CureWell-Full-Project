import { Component } from '@angular/core';

import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { Specialization } from '../models/specialization';

import { SpecializationService } from '../services/specialization.service';

import { DoctorService } from '../services/doctor.service';

import { AccountService } from '../services/account.service';

import { Doctor } from '../models/doctor';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Appointment } from '../models/appointment';

 

@Component({

  selector: 'app-appointment',

  templateUrl: './appointment.component.html',

  styleUrls: ['./appointment.component.scss']

})

export class AppointmentComponent {

  private sub$?:Subscription;

  doctorList:Doctor[]=[];

  specializations:Specialization[]=[];
  userEmail:string="";
  errorMsg:string="";
  appointmentForm: FormGroup;
  selectedSpec:string="default";
  submitted: boolean = false;

  constructor(private route:Router,private fb: FormBuilder,private doctorService:DoctorService,

    private router:ActivatedRoute

    ,private specializationService:SpecializationService,

    private accountService:AccountService)
    {
      this.appointmentForm = this.fb.group({

        specializationCode: [null, Validators.required],
  
        doctorName: [null, Validators.required],
  
        date: [null, Validators.required],
  
        patientName: [null, Validators.required],
  
        message: [null, Validators.required],
  
      });
    }
  
  getEmail(){
    this.sub$ = this.accountService.getEmail().subscribe({
      next: (data) => {
        console.log(data);
        this.userEmail=data;
      },
      error: (err) => {
        console.error(err.status);
      }
    })
  }
  ngOnInit(){

    this.getAllSpecialization();
    this.getEmail();
  }

  getAllSpecialization()
  {
    this.sub$=this.specializationService.getSpecialization().subscribe({

      next:(data)=>{

        console.log(data);

        this.specializations=data;
        let specialization=new Specialization();
        specialization.specializationCode="def"
        specialization.specializationName="Select the specialization"
        this.specializations.push(specialization)

      },
      error:(err)=>{
        console.error(err)
        this.errorMsg="Some error occured";
      }
     })
  }
  getDoctorBySpecialization(specializationCode:string)
  {
    this.sub$=this.doctorService.getDoctorsBySpecialization(specializationCode).subscribe({
      next:(data)=>{
        console.log(data);
        this.doctorList=data;
      },
      error:(err)=>{
        console.error(err)
        this.errorMsg="Some error occured";
      }
     })
  }

  onChange(event:string)
  {
    this.getDoctorBySpecialization(event);

  }
  get f(): { [controlName: string]: AbstractControl } {
    return this.appointmentForm.controls;
  }

  get isButtonDisabled()
  {
    return ((this.submitted || this.f['specializationCode'].touched) && this.f['specializationCode'].errors)||
    ((this.submitted || this.f['doctorName'].touched) && this.f['doctorName'].errors)||
    ((this.submitted || this.f['date'].touched) && this.f['date'].errors)||
    ((this.submitted || this.f['patientName'].touched) && this.f['patientName'].errors)||
    ((this.submitted || this.f['message'].touched) && this.f['message'].errors)
  }
  onSubmit() {

    let appointment=new Appointment()
    appointment={specializationCode:this.f["specializationCode"].value,emailId:this.userEmail,
  doctorName:this.f["doctorName"].value,description:this.f["message"].value,patientName:this.f["patientName"].value,
appointmentDate:this.f["date"].value}
    this.sub$=this.accountService.addAppointment(appointment).subscribe({
      next:(data)=>{
        if(data==true)
        {
          window.alert("Appointment Request Send");
          this.appointmentForm.reset();
          this.route.navigateByUrl("/home");

        }
        else{
          alert("Some error occured");
          this.appointmentForm.reset();
        }
      },
      error:(err)=>{
        console.error(err)
        alert("Error registering user")     
      }
     })
  }

}