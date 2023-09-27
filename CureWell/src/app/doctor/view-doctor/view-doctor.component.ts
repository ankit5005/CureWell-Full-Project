import { Component } from '@angular/core';
import { Doctor } from 'src/app/models/doctor';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { DoctorService } from 'src/app/services/doctor.service';
import { Specialization } from 'src/app/models/specialization';
import { DoctorSpecialization } from 'src/app/models/doctor-specialization';
import { SpecializationService } from 'src/app/services/specialization.service';
import { AccountService } from 'src/app/services/account.service';
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
  specializations:Specialization[]=[];
  selectedSpec:string="default";
  role:string="";
  constructor(private route:Router,private doctorService:DoctorService,
    private router:ActivatedRoute
    ,private specializationService:SpecializationService,
    private accountService:AccountService){}
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
    if(specializationCode=="")
    {
      return this.doctorList;
    }
    this.sub$=this.doctorService.getDoctorsBySpecialization(specializationCode).subscribe({
      next:(data)=>{
        this.doctorList=data;
      },
      error:(err)=>{
        console.error(err)
        this.errorMsg="Some error occured";
      }
     })
     return this.doctorList;
  }
  onChange(event:string)
  {
    this.getDoctorBySpecialization(event);
  }

  getAllSpecialization()
  {
    this.sub$=this.specializationService.getSpecialization().subscribe({
      next:(data)=>{
        console.log(data);
        this.specializations=data;
        let specialization=new Specialization();
        specialization.specializationCode="def"
        specialization.specializationName="Select the specialization"
        this.specializations.push(specialization)

      },
      error:(err)=>{
        console.error(err)
        this.errorMsg="Some error occured";
      }
     })
  }

  // get filteredDoctorList(): Doctor[] {
  //   return this.getDoctorBySpecialization(this.selectedSpec)
  // }

  ngOnInit()
  {
    this.sub$=this.accountService.getRole().subscribe(data =>{
      
      console.log(data);
      this.role=data;
    },
    (err) => {
      console.error(err.status);
     // this.statusCode = err.status;
    })
    this.specializationCode=this.router.snapshot.params["specializationCode"];
    console.log(this.specializationCode);
    if(this.specializationCode)
    {
       this.getDoctorBySpecialization(this.specializationCode);
    }
    else{
      this.getDoctor();
    }
    this.getAllSpecialization();
  }

  ngOnDestroy(): void {
    this.sub$?.unsubscribe();
  }
}
