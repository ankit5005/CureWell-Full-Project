create table Surgery(
SurgeryId int identity(5000,1) constraint pk_Surgery_SurgeryId primary Key,
DoctorId int constraint fk_Surgery_Doctor_DoctorId foreign Key references Doctor(DoctorId),
SurgeryDate Date not null,
StartTime Decimal(4,2) not null,
EndTime Decimal(4,2) not null,
SurgeryCategory char(3) constraint fk_Surgery_Specialization_SurgeryCategory foreign Key references Specialization(SpecializationCode)
)
