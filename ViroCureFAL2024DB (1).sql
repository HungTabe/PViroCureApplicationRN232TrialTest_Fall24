USE master
GO

CREATE DATABASE ViroCureFAL2024DB
GO
USE ViroCureFAL2024DB
GO
-- Table user
CREATE TABLE ViroCureUser (
    user_id INT PRIMARY KEY ,
    email VARCHAR(255) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
    role int NOT NULL DEFAULT 2
);
-- 1: admin, 2 benh nhan, 3 bac si
GO
-- Add data to table  user
INSERT INTO ViroCureUser (user_id,email, password, role) VALUES
(1,'john.doe@example.com', 'password123', 2),
(2,'jane.smith@example.com', 'password456', 2),
(3,'mike.lee@example.com', 'password789', 2),
(4,'admin.sys@example.com', 'password789', 1),
(5,'doctor@example.com', 'password789', 3);
GO
-- Bảng person
CREATE TABLE Person (
    person_id INT PRIMARY KEY ,
    fullname VARCHAR(255) NOT NULL,
    birth_day DATE NOT NULL,
    phone VARCHAR(15) NOT NULL,
    user_id INT,
    FOREIGN KEY (user_id) REFERENCES ViroCureUser(user_id)
);
GO
INSERT INTO Person (person_id,fullname, birth_day, phone, user_id) VALUES
(1,'John Doe', '1990-05-15', '1234567890', 1),
(2,'Jane Smith', '1985-10-22', '0987654321', 2),
(3,'Mike Lee', '1992-03-08', '1122334455', 3);
GO
CREATE TABLE Virus (
    virus_id INT PRIMARY KEY ,
    virus_name VARCHAR(255) NOT NULL,
    treatment VARCHAR(255)
);
GO
INSERT INTO virus (virus_id,virus_name, treatment) VALUES
(1,'COVID-19', 'Remdesivir'),
(2,'Influenza', 'Tamiflu'),
(3,'Chickenpox', 'Acyclovir');
GO
CREATE TABLE person_virus (
    person_id INT,
    virus_id INT,
    resistance_rate FLOAT,  -- Tỉ lệ kháng thuốc
    PRIMARY KEY (person_id, virus_id),
    FOREIGN KEY (person_id) REFERENCES person(person_id),
    FOREIGN KEY (virus_id) REFERENCES virus(virus_id)
);
GO
INSERT INTO person_virus (person_id, virus_id, resistance_rate) VALUES
(1, 1, 0.2),  -- John Doe mắc COVID-19 với tỉ lệ kháng thuốc 20%
(1, 2, 0.0),  -- John Doe mắc Influenza với tỉ lệ kháng thuốc 0%
(2, 1, 0.5),  -- Jane Smith mắc COVID-19 với tỉ lệ kháng thuốc 50%
(3, 3, 0.1);  -- Mike Lee mắc Chickenpox với tỉ lệ kháng thuốc 10%
