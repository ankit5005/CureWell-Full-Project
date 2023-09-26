import { Component } from '@angular/core';

import {AbstractControl, FormBuilder, FormControl,FormGroup,Validators} from '@angular/forms'
import { AccountService } from '../services/account.service';
import { Subscription } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';

 

@Component({

  selector: 'app-login',

  templateUrl: './login.component.html',

  styleUrls: ['./login.component.scss']

})

export class LoginComponent {
  sub$?: Subscription;
  statusCode?: Number;
  loginForm!: FormGroup;
  loggedIn:boolean=false;
  constructor(private service: AccountService,private fb: FormBuilder,private route:Router) { }

 

  ngOnInit(): void {
    if(this.service.isAuthenticated())
    {
      this.loggedIn=!this.loggedIn;
      this.route.navigateByUrl("/home")
    }
    this.loginForm = this.fb.group({
      emailId: [null, Validators.required],
      password: [null, Validators.required]
    })
  }

  get f(): { [controlName: string]: AbstractControl } {
    return this.loginForm.controls;
  }

  ngOnDestroy(): void {
    this.sub$?.unsubscribe();
  }

  onSubmit() {
   {
    this.sub$ = this.service.login(this.f['emailId'].value, this.f['password'].value).subscribe({
      next: (data) => {
        console.log(data);
        sessionStorage.setItem("token", data.access_token);
        alert("User Logged In Successfully")
        window.location.reload();
        //this.route.navigate(["/home"]);
      },
      error: (err) => {
        alert("Invalid Credentials")
        console.error(err.status);
        this.statusCode = err.status;
      }
    })
    }
  }

}

 