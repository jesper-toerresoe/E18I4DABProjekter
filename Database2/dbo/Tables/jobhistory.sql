create table jobhistory(empno integer references employee (empno),
                        position varchar(30),
                        startdate date, 
                        enddate date,
                        salary  decimal(8,2),
                        primary key (empno, position));