
CREATE TABLE doctor (
    doctorId INT constraint doctor_doctorId PRIMARY KEY IDENTITY(1001, 1),
    DoctorName VARCHAR(25) NOT NULL)
  
  insert into doctor (DoctorName) values ('Albert'), ('Olivia'), ('Susan');
