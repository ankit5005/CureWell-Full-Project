import { Injectable } from "@angular/core";

import { Surgery } from "../models/surgery";

import { Observable } from "rxjs";

import { HttpClient } from '@angular/common/http';

 

 

@Injectable({

    providedIn: 'root'

  })

 

export class SurgeryService {

    private baseUrl = "http://localhost:44352/api/home";

 

    constructor(private http : HttpClient){}

 

    getSurgeriesForToday() : Observable<Surgery[]>{

        return this.http.get<Surgery[]>(`${this.baseUrl}/allSurgeries`);

    }

 

    getSurgeryById(surgeryId: number) : Observable<Surgery>{

        return this.http.get<Surgery>(`${this.baseUrl}/GetSurgeryById/${surgeryId}`);

    }

    updateSurgery(surgery : Surgery) : Observable<boolean>{

        return this.http.put<boolean>(`${this.baseUrl}/UpdateSurgery`, surgery);

    }

 

}