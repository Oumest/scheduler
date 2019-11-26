Use scheduler;

CREATE TABLE users (
id int PRIMARY KEY IDENTITY (1,1) NOT NULL,
username nvarchar(180),
email nvarchar(180),
"enabled" smallint,
salt nvarchar(255) NOT NULL,
password nvarchar(255),
last_login datetime2(0),
confirmation_token nvarchar(180) NOT NULL,
password_requested_at datetime2(0) NOT NULL,
roles nvarchar(MAX),
alias nvarchar(60),
registration_date datetime2(0) NOT NULL,
title nvarchar(50),
avatar nvarchar(255)
);

CREATE TABLE teams(
id int PRIMARY KEY IDENTITY (1,1) NOT NULL,
teamlead_id int,
name nvarchar(100)
);

CREATE TABLE project_teams(
project_id int PRIMARY KEY IDENTITY (1,1) NOT NULL,
team_id int
);

CREATE TABLE user_preferences(
id int PRIMARY KEY IDENTITY(1,1) NOT NULL,
"user_id" int NOT NULL,
name nvarchar(50),
value nvarchar(255) NOT NULL
);

CREATE TABLE user_teams(
"user_id" int PRIMARY KEY IDENTITY (1,1) NOT NULL,
team_id int
);

CREATE TABLE projects(
id int PRIMARY KEY IDENTITY (1,1) NOT NULL,
customer_id int,
"name" nvarchar(150),
order_number nvarchar(255) NOT NULL,
comment nvarchar(MAX) NOT NULL,
visible smallint,
fixed_rate float NOT NULL,
hourly_rate float NOT NULL,
budget float,
time_budget int,
color nvarchar(7)
);

CREATE TABLE timesheet(
id int PRIMARY KEY IDENTITY(1,1) NOT NULL,
"user_id" int,
activity_id int,
project_id int,
start_time datetime2(0),
end_time datetime2(0) NOT NULL,
duration int NOT NULL,
"description" nvarchar(MAX) NOT NULL,
rate float,
exported smallint,
fixed_rate float NOT NULL,
hourly_rate float NOT NULL,
);

CREATE TABLE activities(
id int PRIMARY KEY IDENTITY(1,1) NOT NULL,
project_id int NOT NULL,
"name" nvarchar(150),
comment nvarchar(MAX) NOT NULL,
visible smallint,
fixed_rate float NOT NULL,
hourly_rate float NOT NULL,
budget float,
time_budget float,
color nvarchar(7)
);

CREATE TABLE timesheet_tags(
timesheet_id int PRIMARY KEY IDENTITY (1,1),
tag_id int
);

CREATE TABLE tags(
id int PRIMARY KEY IDENTITY (1,1) NOT NULL,
"name" nvarchar(100)
);

CREATE TABLE customers(
id int PRIMARY KEY IDENTITY(1,1),
name nvarchar(150),
number nvarchar(50) NOT NULL,
comment nvarchar(MAX) NOT NULL,
company nvarchar(255) NOT NULL,
contact nvarchar(255) NOT NULL,
"address" nvarchar(MAX) NOT NULL,
country nvarchar(2),
currency nvarchar(3),
phone nvarchar(255) NOT NULL, 
mobile nvarchar(255) NOT NULL,
email nvarchar(255) NOT NULL,
fixed_rate float NOT NULL,
hourly_rate float NOT NULL,
budget float,
time_budget int
);

CREATE TABLE customer_teams(
customer_id int PRIMARY KEY IDENTITY (1,1),
team_id int
)


ALTER TABLE teams ADD FOREIGN KEY (teamlead_id) REFERENCES users(id);

ALTER TABLE project_teams ADD FOREIGN KEY (project_id) REFERENCES projects(id);

ALTER TABLE project_teams ADD FOREIGN KEY (team_id) REFERENCES teams(id);

ALTER TABLE user_preferences ADD FOREIGN KEY (user_id) REFERENCES users(id);

ALTER TABLE user_teams ADD FOREIGN KEY (user_id) REFERENCES users(id);

ALTER TABLE user_teams ADD FOREIGN KEY (team_id) REFERENCES teams(id);

ALTER TABLE projects ADD FOREIGN KEY (customer_id) REFERENCES customers(id);

ALTER TABLE timesheet ADD FOREIGN KEY (activity_id) REFERENCES activities(id);

ALTER TABLE timesheet ADD FOREIGN KEY (project_id) REFERENCES projects(id);

ALTER TABLE timesheet ADD FOREIGN KEY (user_id) REFERENCES users(id);

ALTER TABLE activities ADD FOREIGN KEY (project_id) REFERENCES projects(id);

ALTER TABLE timesheet_tags ADD FOREIGN KEY (timesheet_id) REFERENCES timesheet(id);

ALTER TABLE timesheet_tags ADD FOREIGN KEY (tag_id) REFERENCES tags(id);

ALTER TABLE customer_teams ADD FOREIGN KEY (customer_id) REFERENCES customers(id);

ALTER TABLE customer_teams ADD FOREIGN KEY (team_id) REFERENCES teams(id);