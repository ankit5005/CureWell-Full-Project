import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../services/account.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent {
  private sub$?:Subscription;
  role:string="";
  loggedIn:boolean=false;

  constructor(private route:Router,private accountService:AccountService,
    private router:ActivatedRoute){}
 
  getRole(){
    this.sub$ = this.accountService.getRole().subscribe({
      next: (data) => {
        console.log(data);
        this.role=data;
      },
      error: (err) => {
        console.error(err.status);
       // this.statusCode = err.status;
      }
    })
  }
  logout(){
    this.accountService.logout();
    window.location.reload();

  }
  ngOnInit()
  {
    if(this.accountService.isAuthenticated())
    {
      this.loggedIn=!this.loggedIn;
    
    }
    this.getRole();
  }
    ngOnDestroy(): void {
      this.sub$?.unsubscribe();
    }
  
  }

  
