Create database PRNGroup4
Use PRNGroup4

--Bảng Users - Quản lí tài khoản người dùng
Create Table Users (
	UserID INT PRIMARY KEY IDENTITY,
	FullName VARCHAR(150) NOT NULL,
	Email VARCHAR(150) NOT NULL UNIQUE,
	Password VARCHAR(255) NOT NULL,
	Role VARCHAR(20) CHECK (Role IN ('Student', 'Teacher', 'TrafficPolice')) NOT NULL,
	Class VARCHAR(50), -- chỉ dùng cho student
	School VARCHAR(100), -- chỉ dùng cho student
	Phone VARCHAR(15)
);

--Bảng Course - Quản lí khóa học
Create Table Courses (
	CourseID INT Primary Key Identity,
	CourseName Varchar(100) Not Null,
	TeacherID Int Not Null,
	StartDate Date Not Null,
	EndDate Date Not Null,
	Foreign Key (TeacherID) References Users(UserID)
);

--Bảng Registrations - Quản lí đăng kí khóa học/ kì thi
Create Table Registrations (
	RegistrationID Int Primary Key Identity,
	UserID Int Not Null,
	CourseID Int Not Null,
	Status Varchar(20) Check (Status In ('Pending', 'Approved', 'Rejected')) Default 'Pending',
	Comments Text,
	Foreign Key (UserID) References Users(UserID),
	Foreign Key (CourseID) References Courses(CourseID)
);

-- Bảng Exams - Quản lí kì thi
Create Table Exams (
	ExamID Int Primary Key Identity,
	CourseID Int Not Null,
	Date Date Not Null,
	Room Varchar(50) Not Null,
	Foreign Key (CourseID) References Courses(CourseID)
);

-- Bảng Results - Kết quả thi
Create Table Results (
	ResultID Int Primary Key Identity,
	ExamID Int Not Null,
	UserID Int Not Null,
	Score Decimal(5, 2) Not Null,
	PassStatus Bit Not Null,
	Foreign Key (ExamID) References Exams(ExamID),
	Foreign Key (UserID) References Users(UserID)
);

-- Bảng Certificates - Cấp chứng chỉ
Create Table Certificates (
	CertificateID Int Primary Key Identity,
	UserID Int Not Null,
	IssuedDate Date Not Null,
	ExpirationDate Date Not Null,
	CertificateCode Varchar(50) Unique,
	Foreign Key (UserId) References Users(UserID)
);

-- Bảng Attendace - Ghi nhận điểm danh khóa học
Create Table Attendance (
	AttendanceID Int Primary Key Identity,
	CourseID Int Not Null,
	UserID Int Not Null,
	SessionDate Date Not Null,
	Status Varchar(20) Check (Status In ('Present', 'Absent')) Not Null,
	Note Text,
	Foreign Key (CourseID) References Courses(CourseID),
	Foreign Key (UserID) References Users(UserID)
);

INSERT INTO Users (FullName, Email, Password, Role, Class, School, Phone) VALUES
('Nguyễn Văn A', 'a@student.com', 'hashed123', 'Student', '12A1', 'ĐH FPT HN', '0912345678'),
('Trần Thị B', 'b@teacher.com', 'hashed456', 'Teacher', NULL, NULL, '0987654321'),
('Lê Công An', 'csgt@police.com', 'hashed789', 'TrafficPolice', NULL, NULL, '0909123456');

INSERT INTO Courses (CourseName, TeacherID, StartDate, EndDate) VALUES
('Kỹ năng lái xe cơ bản', 2, '2025-07-01', '2025-07-31'),
('Lý thuyết giao thông', 2, '2025-08-01', '2025-08-15');

INSERT INTO Registrations (UserID, CourseID, Status, Comments) VALUES
(1, 1, 'Approved', 'Tham gia đầy đủ'),
(1, 2, 'Pending', 'Đang chờ xác nhận');

INSERT INTO Exams (CourseID, Date, Room) VALUES
(1, '2025-07-30', 'P101'),
(2, '2025-08-15', 'P202');

INSERT INTO Results (ExamID, UserID, Score, PassStatus) VALUES
(1, 1, 8.50, 1),
(2, 1, 7.00, 1);


INSERT INTO Certificates (UserID, IssuedDate, ExpirationDate, CertificateCode) VALUES
(1, '2025-08-01', '2028-08-01', 'CERT-2025-A001');


INSERT INTO Attendance (CourseID, UserID, SessionDate, Status, Note) VALUES
(1, 1, '2025-07-01', 'Present', 'Đi học đúng giờ'),
(1, 1, '2025-07-02', 'Absent', 'Có việc gia đình'),
(1, 1, '2025-07-03', 'Present', 'Tham gia tích cực');

