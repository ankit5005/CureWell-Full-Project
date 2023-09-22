import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DoctorService } from 'src/app/services/doctor.service';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Doctor } from 'src/app/models/doctor';

@Component({
  selector: 'app-update-doctor',
  templateUrl: './update-doctor.component.html',
  styleUrls: ['./update-doctor.component.scss']
})
export class UpdateDoctorComponent {
  constructor(private route:Router,private fb: FormBuilder,private doctorService:DoctorService,private router:ActivatedRoute){}
  doctorId:number=0;
  doctorForm!: FormGroup; // Using "!" to indicate it will be assigned later

  doctorData!: Doctor; // Using "!" to indicate it will be assigned later
  ngOnInit(){
    this.doctorId=this.router.snapshot.params["doctorId"];
    console.log(this.doctorId);
    // Initialize the form group and doctorData as needed

    this.doctorForm = this.fb.group({

      doctorId: [this.doctorId, [Validators.required]], // Default value for doctorId

      doctorName: [null, [Validators.required]], // Default value for doctorName

    });
    
  }



}
