use scheduler_v2;

CREATE TABLE users (
  id int PRIMARY KEY IDENTITY (1,1) NOT NULL, 
  username nvarchar(180) UNIQUE NOT NULL,
  email nvarchar(180) UNIQUE NOT NULL,
  "enabled" smallint,
  salt nvarchar(255) NOT NULL,
  "password" nvarchar(255) NOT NULL,
  last_login datetime2(0),
  confirmation_token nvarchar(180),
  password_requested_at datetime2(0) NOT NULL,
  roles nvarchar(MAX),
  alias nvarchar(60),
  registration_date datetime2(0) NOT NULL,
  title nvarchar(50),
  avatar nvarchar(255),
);

CREATE TABLE timesheet(
  id int PRIMARY KEY IDENTITY (1,1) NOT NULL,
  user_id int NOT NULL,
  activity_id int NOT NULL,
  project_id int NOT NULL,
  start_time datetime2(0) NOT NULL,
  end_time datetime2(0),
  duration int,
  "description" nvarchar(MAX),
  rate float,
  exported smallint,
  fixed_rate float,
  hourly_rate float
);

CREATE TABLE customers(
  id int PRIMARY KEY IDENTITY (1,1) NOT NULL,
  "name" nvarchar(150) NOT NULL,
  number nvarchar(50),
  comment nvarchar(MAX),
  company nvarchar(255),
  contact nvarchar(255),
  "address" nvarchar(MAX),
  country nvarchar(2) NOT NULL,
  currency nvarchar(3) NOT NULL,
  phone nvarchar(255),
  mobile nvarchar(255),
  email nvarchar(255),
  fixed_rate float,
  hourly_rate float,
  budget float NOT NULL,
  time_budget int NOT NULL
);

CREATE TABLE projects(
  id int PRIMARY KEY IDENTITY (1,1) NOT NULL,
  customer_id int NOT NULL,
  "name" nvarchar(150),
  order_number nvarchar(255),
  comment nvarchar(MAX),
  visible smallint NOT NULL,
  fixed_rate float,
  hourly_rate float,
  budget float,
  time_budget int,
  color nvarchar(7)
);

CREATE TABLE activities(
  id int PRIMARY KEY IDENTITY (1,1) NOT NULL,
  project_id int NOT NULL,
  "name" nvarchar(150) NOT NULL,
  comment nvarchar(MAX),
  visible smallint NOT NULL,
  fixed_rate float,
  hourly_rate float,
  budget float,
  time_budget int,
  color nvarchar(7)
);

CREATE TABLE teams(
  id int PRIMARY KEY IDENTITY (1,1) NOT NULL,
  teamlead_id int NOT NULL,
  "name" nvarchar(100) NOT NULL
);

CREATE TABLE tags(
  id int PRIMARY KEY IDENTITY (1,1) NOT NULL,
  "name" nvarchar(100) NOT NULL
);

CREATE TABLE user_teams(
  user_id int,
  team_id int 
);

CREATE TABLE project_teams(
  project_id int,
  team_id int
);

CREATE TABLE customer_teams(
  customer_id int,
  team_id int 
);

CREATE TABLE timesheet_tags(
  timesheet_id int,
  tag_id int 
);

CREATE TABLE user_preferences(
  id int PRIMARY KEY IDENTITY (1,1) NOT NULL,
  user_id int NOT NULL,
  "name" nvarchar (100) NOT NULL,
  "value" nvarchar (255)
);

ALTER TABLE timesheet ADD FOREIGN KEY (user_id) REFERENCES users (id);
ALTER TABLE timesheet ADD FOREIGN KEY (project_id) REFERENCES projects (id);
ALTER TABLE timesheet ADD FOREIGN KEY (activity_id) REFERENCES activities (id);

ALTER TABLE projects ADD FOREIGN KEY (customer_id) REFERENCES customers (id);

ALTER TABLE activities ADD FOREIGN KEY (project_id) REFERENCES projects (id);

ALTER TABLE teams ADD FOREIGN KEY (teamlead_id) REFERENCES users (id);

ALTER TABLE user_teams ADD FOREIGN KEY (user_id) REFERENCES users (id);
ALTER TABLE user_teams ADD FOREIGN KEY (team_id) REFERENCES teams (id);

ALTER TABLE customer_teams ADD FOREIGN KEY (customer_id) REFERENCES customers (id);
ALTER TABLE customer_teams ADD FOREIGN KEY (team_id) REFERENCES teams (id);

ALTER TABLE project_teams ADD FOREIGN KEY (project_id) REFERENCES projects (id);
ALTER TABLE project_teams ADD FOREIGN KEY (team_id) REFERENCES teams (id);

ALTER TABLE timesheet_tags ADD FOREIGN KEY (timesheet_id) REFERENCES timesheet(id);
ALTER TABLE timesheet_tags ADD FOREIGN KEY (tag_id) REFERENCES tags(id);

ALTER TABLE user_preferences ADD FOREIGN KEY (user_id) REFERENCES users(id);