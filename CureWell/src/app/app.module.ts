import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { FooterComponent } from './footer/footer.component';
import { AboutUsComponent } from './about-us/about-us.component';
import { ViewDoctorComponent } from './doctor/view-doctor/view-doctor.component';
import { UpdateDoctorComponent } from './doctor/update-doctor/update-doctor.component';
import { AddDoctorComponent } from './doctor/add-doctor/add-doctor.component';
import { ViewSpecializationComponent } from './specialization/view-specialization/view-specialization.component';
import { ViewTodaysSurgeryComponent } from './surgery/view-todays-surgery/view-todays-surgery.component';
import { UpdateSurgeryComponent } from './surgery/update-surgery/update-surgery.component';
import { ErrorComponent } from './shared/error/error.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {HttpClientModule } from "@angular/common/http";
import { RegisterComponent } from './register/register.component';
import { AppointmentComponent } from './appointment/appointment.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    FooterComponent,
    AboutUsComponent,
    ViewDoctorComponent,
    UpdateDoctorComponent,
    AddDoctorComponent,
    ViewSpecializationComponent,
    ViewTodaysSurgeryComponent,
    UpdateSurgeryComponent,
    ErrorComponent,
    HomeComponent,
    LoginComponent,
    RegisterComponent,
    AppointmentComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
