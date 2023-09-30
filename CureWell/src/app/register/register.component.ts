import { Component } from '@angular/core';

import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../services/account.service';
import { Subscription } from 'rxjs';
import { User } from '../models/user';
import { Router } from '@angular/router';

 

@Component({

  selector: 'app-register',

  templateUrl: './register.component.html',

  styleUrls: ['./register.component.scss']

})

export class RegisterComponent {

  // Create a FormGroup to manage form controls
  registrationForm: FormGroup;
  submitted: boolean = false;
  mobileNoRegex: string = "^[0-9]{10,10}$";
  emailRegex: string = "^[a-zA-Z]+@[a-z0-9]+\\.[a-z]{2,4}$";
  password:string="(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]{6,16}"
  private sub$?:Subscription;

  constructor(private fb: FormBuilder,private accountService:AccountService,private router:Router) {

    // Initialize the form controls with validation rules

    this.registrationForm = this.fb.group({

      userName: [null, [Validators.required,Validators.minLength(3)]],

      email: [null, [Validators.required, Validators.pattern(this.emailRegex)]],

      age: [null, Validators.required],

      phoneNo: [null, [Validators.required, Validators.minLength(10), 
        Validators.maxLength(10), Validators.pattern(this.mobileNoRegex)]],

      address: [null, Validators.required],

      password: [null,[Validators.required,Validators.minLength(8),Validators.maxLength(25),
        Validators.pattern(this.password)]]

    });

  }

 

  // Convenience getters for form controls

  // get f() {

  //   return this.registrationForm.controls;

  // }

  get f(): { [controlName: string]: AbstractControl } {
    return this.registrationForm.controls;
  }
 
  get isButtonDisabled()
  {
    return ((this.submitted || this.f['userName'].touched) && this.f['userName'].errors)||
    ((this.submitted || this.f['email'].touched) && this.f['email'].errors)||
    ((this.submitted || this.f['age'].touched) && this.f['age'].errors)||
    ((this.submitted || this.f['phoneNo'].touched) && this.f['phoneNo'].errors)||
    ((this.submitted || this.f['address'].touched) && this.f['address'].errors)||
    ((this.submitted || this.f['password'].touched) && this.f['password'].errors)
  }

  onSubmit() {
    let user:User=new User();
    user.userName=this.f['userName'].value
    user.emailId=this.f['email'].value
    user.age=this.f['age'].value
    user.phoneNumber=this.f['phoneNo'].value
    user.address=this.f['address'].value
    user.password=this.f['password'].value

    this.sub$=this.accountService.addUser(user).subscribe({
      next:(data)=>{
        if(data==true)
        {
          window.alert("Registration Successful");
          this.registrationForm.reset();
          this.router.navigateByUrl("/login");

        }
        else{
          alert("Some error occured");
          this.registrationForm.reset();
        }
      },
      error:(err)=>{
        console.error(err)
        alert("Error registering user")     
      }
     })
  }
}