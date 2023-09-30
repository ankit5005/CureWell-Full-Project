import { Injectable } from '@angular/core';

import { Specialization } from '../models/specialization';

import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class SpecializationService {
  private apiUrl = 'http://localhost:44352/api/Home';

  constructor(private http: HttpClient) { }
  getSpecialization(): Observable<Specialization[]> {

    return this.http.get<Specialization[]>(this.apiUrl+"/AllSpecializations");
  }
}