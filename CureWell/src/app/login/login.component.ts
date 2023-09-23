import { Component } from '@angular/core';

import {FormControl,FormGroup,Validators} from '@angular/forms'

 

@Component({

  selector: 'app-login',

  templateUrl: './login.component.html',

  styleUrls: ['./login.component.scss']

})

export class LoginComponent {

 

  loginForm=new FormGroup({

    id:new FormControl('',[Validators.required]),

    password:new FormControl('',[Validators.required,Validators.minLength(5)])

  })

  onSubmit() {

   {

      console.warn(this.loginForm.value);

    }

 

  }

  get id (){

      return this.loginForm.get('id')

  }

  get password(){

    return this.loginForm.get('password')

  }

}

 