import { Component } from '@angular/core';
import { Specialization } from 'src/app/models/specialization';
import { SpecializationService } from 'src/app/services/specialization.service';

@Component({
  selector: 'app-view-specialization',
  templateUrl: './view-specialization.component.html',
  styleUrls: ['./view-specialization.component.scss']
})
export class ViewSpecializationComponent {
  specializations: Specialization[] = [];
  constructor(private specializationService:SpecializationService ) { }
  
  ngOnInit(): void {
    this.specializationService.getSpecialization().subscribe((data) => {
      this.specializations = data;
      console.log(data);
    });
  }
}
