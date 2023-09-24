import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { ViewDoctorComponent } from './doctor/view-doctor/view-doctor.component';
import { AddDoctorComponent } from './doctor/add-doctor/add-doctor.component';
import { UpdateDoctorComponent } from './doctor/update-doctor/update-doctor.component';
import { ViewTodaysSurgeryComponent } from './surgery/view-todays-surgery/view-todays-surgery.component';
import { ViewSpecializationComponent } from './specialization/view-specialization/view-specialization.component';
import { AboutUsComponent } from './about-us/about-us.component';
import { ErrorComponent } from './shared/error/error.component';
import { UpdateSurgeryComponent } from './surgery/update-surgery/update-surgery.component';
import { RegisterComponent } from './register/register.component';
import { AppointmentComponent } from './appointment/appointment.component';

const routes: Routes = [
  {path:"",redirectTo:"/home",pathMatch:"full"},
  {path:"home",component:HomeComponent},
  {path:"login",component:LoginComponent},
  {path:"signup",component:RegisterComponent},
  {path:"doctor/view-doctor",component:ViewDoctorComponent},
  {path:"doctor/view-doctor/:specializationCode",component:ViewDoctorComponent},
  {path:"doctor/add-doctor",component:AddDoctorComponent},
  {path:"doctor/update-doctor/:doctorId",component:UpdateDoctorComponent},
  {path:"surgery/update-surgery",component:UpdateSurgeryComponent},
  {path:"surgery/view-todays-surgery",component:ViewTodaysSurgeryComponent},
  {path:"specialization/view-specialization",component:ViewSpecializationComponent},
  {path:"make-appointment",component:AppointmentComponent},
  {path:"about-us",component:AboutUsComponent},
  {path:"**",component:ErrorComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
