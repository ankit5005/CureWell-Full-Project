import { Component } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-add-doctor',
  templateUrl: './add-doctor.component.html',
  styleUrls: ['./add-doctor.component.scss']
})
export class AddDoctorComponent {
  registerForm!:FormGroup;
  submitted:boolean=false;

  constructor(private fb:FormBuilder)
  {}
  ngOnInit():void{

    this.registerForm= this.fb.group({
      doctorName:[null,[Validators.required]],
    })
  }
  
  onSubmit(){
    this.submitted=true;
    console.log(this.registerForm);
  }

  get f():{[controlName:string]:AbstractControl}{
    return this.registerForm.controls;
  }

  doReset(){
    this.submitted=false;
    this.registerForm.reset();
    window.location.reload();
  }
}
