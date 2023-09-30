import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Doctor } from '../models/doctor';
@Injectable({
  providedIn: 'root'
})
export class DoctorService {
  private baseUrl = "http://localhost:44352/api/Home"
  private authHeader!: HttpHeaders;


  constructor(private http: HttpClient) {
    //let authorizeData = 'Basic ' + btoa(sessionStorage.getItem('emailId') + ':' + sessionStorage.getItem('password'));
    //let authorizeData = 'Basic ' + btoa('arun@gmail.com' + ':' + 'arun');
    //let authorizeData = 'Bearer ' + sessionStorage.getItem("token");
    //console.log(authorizeData);

    this.authHeader = new HttpHeaders({
      'Content-Type': 'application/json'
    })
  }

  getDoctors(): Observable<Doctor[]> {
    return this.http.get<Doctor[]>(`${this.baseUrl}/AllDoctors`,{ headers: this.authHeader });
  }
  updateDoctorDetails(doctor:Doctor):Observable<any>
  {
    return this.http.put<any>(`${this.baseUrl}/UpdateDoctorDetails`, doctor, { headers: this.authHeader });
  }

  getDoctorsBySpecialization(specializationCode:string): Observable<Doctor[]> {
    return this.http.get<Doctor[]>(`${this.baseUrl}/DoctorBySpecialization/${specializationCode}`,{ headers: this.authHeader });
  }
  addDoctor(doctor: Doctor): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/AddDoctor`, doctor, { headers: this.authHeader });
  }
}
