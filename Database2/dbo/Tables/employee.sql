create table employee(
        empno integer primary key,
        surname varchar(15),
        forenames varchar(30),
        dob date,
        address varchar(50),
        telno varchar(20),
        depno integer 
              references department (depno)
);