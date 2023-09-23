import { Component } from '@angular/core';
import { Doctor } from 'src/app/models/doctor';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { DoctorService } from 'src/app/services/doctor.service';
@Component({
  selector: 'app-view-doctor',
  templateUrl: './view-doctor.component.html',
  styleUrls: ['./view-doctor.component.scss']
})
export class ViewDoctorComponent {
  private sub$?:Subscription;
  doctorList:Doctor[]=[];
  showMsgDiv:string="";
  errorMsg:string="";
  specializationCode:string="";

  constructor(private route:Router,private doctorService:DoctorService,private router:ActivatedRoute){}
  editDoctorDetails(doctorId:number)
  {
    this.route.navigate(['/doctor/update-doctor',doctorId]);
  }

  getDoctor()
  {
     this.sub$=this.doctorService.getDoctors().subscribe({
      next:(data)=>{
        console.log(data);
        this.doctorList=data;
      },
      error:(err)=>{
        console.error(err)
        this.errorMsg="Some error occured";

      }
     })
  }
  getDoctorBySpecialization(specializationCode:string)
  {
    this.sub$=this.doctorService.getDoctorsBySpecialization(specializationCode).subscribe({
      next:(data)=>{
        console.log(data);
        this.doctorList=data;
      },
      error:(err)=>{
        console.error(err)
        this.errorMsg="Some error occured";
      }
     })
  }

  ngOnInit()
  {
    this.specializationCode=this.router.snapshot.params["specializationCode"];
    console.log(this.specializationCode);
    if(this.specializationCode)
    {
       this.getDoctorBySpecialization(this.specializationCode);
    }
    else{
      this.getDoctor();
    }
  }

  ngOnDestroy(): void {
    this.sub$?.unsubscribe();
  }
}
