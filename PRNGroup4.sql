CREATE DATABASE PRNGroup4;
USE PRNGroup4;

-- Bảng Users - Quản lí tài khoản người dùng
CREATE TABLE Users (
	UserID INT PRIMARY KEY IDENTITY,
	FullName NVARCHAR(150) NOT NULL,
	Email NVARCHAR(150) NOT NULL UNIQUE,
	Password NVARCHAR(255) NOT NULL,
	Role NVARCHAR(20) CHECK (Role IN (N'Student', N'Teacher', N'TrafficPolice', N'Admin')) NOT NULL,
	Class NVARCHAR(50),  -- chỉ dùng cho student
	School NVARCHAR(100), -- chỉ dùng cho student
	Phone NVARCHAR(15)
);

-- Bảng Courses - Quản lí khóa học
CREATE TABLE Courses (
	CourseID INT PRIMARY KEY IDENTITY,
	CourseName NVARCHAR(100) NOT NULL,
	TeacherID INT NOT NULL,
	StartDate DATE NOT NULL,
	EndDate DATE NOT NULL,
	FOREIGN KEY (TeacherID) REFERENCES Users(UserID)
);

-- Bảng Registrations - Quản lí đăng kí khóa học/kì thi
CREATE TABLE Registrations (
	RegistrationID INT PRIMARY KEY IDENTITY,
	UserID INT NOT NULL,
	CourseID INT NOT NULL,
	Status NVARCHAR(20) CHECK (Status IN (N'Pending', N'Approved', N'Rejected')) DEFAULT N'Pending',
	Comments NVARCHAR(MAX),
	FOREIGN KEY (UserID) REFERENCES Users(UserID),
	FOREIGN KEY (CourseID) REFERENCES Courses(CourseID)
);

-- Bảng Exams - Quản lí kì thi
CREATE TABLE Exams (
	ExamID INT PRIMARY KEY IDENTITY,
	CourseID INT NOT NULL,
	Date DATE NOT NULL,
	Room NVARCHAR(50) NOT NULL,
	FOREIGN KEY (CourseID) REFERENCES Courses(CourseID)
);

-- Bảng Results - Kết quả thi
CREATE TABLE Results (
	ResultID INT PRIMARY KEY IDENTITY,
	ExamID INT NOT NULL,
	UserID INT NOT NULL,
	Score DECIMAL(5, 2) NOT NULL,
	PassStatus BIT NOT NULL,
	FOREIGN KEY (ExamID) REFERENCES Exams(ExamID),
	FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- Bảng Certificates - Cấp chứng chỉ
CREATE TABLE Certificates (
	CertificateID INT PRIMARY KEY IDENTITY,
	UserID INT NOT NULL,
	IssuedDate DATE NOT NULL,
	ExpirationDate DATE NOT NULL,
	CertificateCode NVARCHAR(50) UNIQUE,
	FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- Bảng Attendance - Ghi nhận điểm danh khóa học
CREATE TABLE Attendance (
	AttendanceID INT PRIMARY KEY IDENTITY,
	CourseID INT NOT NULL,
	UserID INT NOT NULL,
	SessionDate DATE NOT NULL,
	Status NVARCHAR(20) CHECK (Status IN (N'Present', N'Absent')) NOT NULL,
	Note NVARCHAR(MAX),
	FOREIGN KEY (CourseID) REFERENCES Courses(CourseID),
	FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- Dữ liệu mẫu
INSERT INTO Users (FullName, Email, Password, Role, Class, School, Phone) VALUES
(N'Nguyễn Văn A', N'a@student.com', N'hashed123', N'Student', N'12A1', N'ĐH FPT HN', N'0912345678'),
(N'Trần Thị B', N'b@teacher.com', N'hashed456', N'Teacher', NULL, NULL, N'0987654321'),
(N'Lê Công An', N'csgt@police.com', N'hashed789', N'TrafficPolice', NULL, NULL, N'0909123456');

INSERT INTO Courses (CourseName, TeacherID, StartDate, EndDate) VALUES
(N'Kỹ năng lái xe cơ bản', 2, '2025-07-01', '2025-07-31'),
(N'Lý thuyết giao thông', 2, '2025-08-01', '2025-08-15');

INSERT INTO Registrations (UserID, CourseID, Status, Comments) VALUES
(1, 1, N'Approved', N'Tham gia đầy đủ'),
(1, 2, N'Pending', N'Đang chờ xác nhận');

INSERT INTO Exams (CourseID, Date, Room) VALUES
(1, '2025-07-30', N'P101'),
(2, '2025-08-15', N'P202');

INSERT INTO Results (ExamID, UserID, Score, PassStatus) VALUES
(1, 1, 8.50, 1),
(2, 1, 7.00, 1);

INSERT INTO Certificates (UserID, IssuedDate, ExpirationDate, CertificateCode) VALUES
(1, '2025-08-01', '2028-08-01', N'CERT-2025-A001');

INSERT INTO Attendance (CourseID, UserID, SessionDate, Status, Note) VALUES
(1, 1, '2025-07-01', N'Present', N'Đi học đúng giờ'),
(1, 1, '2025-07-02', N'Absent', N'Có việc gia đình'),
(1, 1, '2025-07-03', N'Present', N'Tham gia tích cực');
