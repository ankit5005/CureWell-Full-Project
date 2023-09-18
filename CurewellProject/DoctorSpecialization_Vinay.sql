CREATE TABLE DoctorSpecialization(
	DoctorId INT CONSTRAINT fk_DoctorSpecialization_Doctor_DoctorId foreign Key references Doctor(DoctorId),
	SpecializationCode Char(3) CONSTRAINT fk_DoctorSpecialization_Specialization_SpecializationCode foreign Key references Specialization(SpecializationCode),
	SpecializationDate DATE NOT NULL,
	Constraint ck_DoctorSpecialization_DoctorId_SpecializationCode PRIMARY KEY (DoctorId,SpecializationCode)
)
INSERT INTO DoctorSpecialization(DoctorId,SpecializationCode,SpecializationDate) Values(1001,'ANE','2010-01-01'),(1002,'CAR','2010-01-01'),(1003,'CAR','2010-01-01')
Select * from DoctorSpecialization
