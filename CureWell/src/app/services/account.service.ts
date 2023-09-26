import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
 // private user!: User;
  //private baseUrl = "http://localhost:55736/api";
  private baseUrl = "http://localhost:44352";
  private authHeader!: HttpHeaders;


  constructor(private http: HttpClient) {
  let authorizeData = 'Bearer ' + sessionStorage.getItem("token");
    this.authHeader = new HttpHeaders({
      'Content-Type': 'application/x-www-form-urlencoded',
    })
  }


  login(emailId: string, password: string): Observable<any> {
    //this.user = new User();
    // this.user.username = emailId;
    // this.user.password = password;
    const body = new HttpParams()
      .set('username', emailId)
      .set('password', password)
      .set('grant_type', 'password');
    //console.log(this.user);
    return this.http.post(`${this.baseUrl}/token`, body, { headers: this.authHeader });
  }

  getRole():Observable<any>
  {
    return this.http.get(`${this.baseUrl}/api/Home/GetRole`,{ headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization':'Bearer ' + sessionStorage.getItem("token")
    })});
  }
  logout(){
    sessionStorage.removeItem("token");
  }
  
  isAuthenticated():boolean{
    if(sessionStorage.getItem("token")){
      return true;
    }
    return false;
  }
}
