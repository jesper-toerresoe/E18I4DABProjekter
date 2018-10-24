create table empcourse(empno integer references employee (empno), 
                       courseno integer references course (courseno),
                       primary key (empno, courseno));