GearGo 

GearGo is a web app for a small equipment-rental business. Customers browse equipment (camping gear, power tools, event equipment) and request to rent it. Staff and managers run the business behind the scenes.

Project status: early design stage. This README explains what we are building and how it fits together, so you can get started quickly.

Table of contents
What problem does it solve?
Who uses it?
Features
How it works (architecture)
Database design
Getting started
Project structure
Working with Git
Glossary
What problem does it solve?

Right now, checking what is available and asking to rent it is a manual job: messages, calls and notes. That wastes time and makes double-bookings easy.

GearGo puts everything in one place:

Customers can see what is available and ask to rent it online.
Staff can review requests and keep equipment records up to date.
Managers can control the catalogue and user accounts.

Not included in version 1: online payments, delivery scheduling, damage/insurance handling and invoicing.

Who uses it?

There are three roles. Each role can do everything the one above it in the table can do for browsing, plus more.

Role	Who they are	What they can do
Customer	Someone who wants to rent equipment	Browse equipment, submit and track their own rental requests
Staff	Employee running day-to-day rentals	Everything a customer can see, plus manage equipment and process all rental requests
Manager	Owner or supervisor	Everything staff can do, plus manage users, roles and the catalogue
Features
Customer
Register and log in
Browse and search the equipment catalogue
View equipment details (description, daily rate, availability)
Submit a rental request (item, quantity, start and end dates)
View and cancel your own pending requests
Staff
Add and edit equipment
Mark equipment as available, unavailable or under maintenance
Review incoming rental requests
Approve or reject requests (the system checks for date clashes)
Record when equipment is collected and returned
Manager
Create, deactivate and reactivate user accounts
Assign roles (Customer, Staff, Manager)
Manage categories and archive or restore equipment
Cancel or change any rental request
See an overview of requests and popular equipment
How it works (architecture)

GearGo follows the MVC pattern (Model-View-Controller) with a separate layer for business rules. A request travels like this:

Browser
Controller
Service / Business Logic
Data Access
Database
View
Layer	Its job	Example
Browser	Shows pages and sends requests	Catalogue page, request form
Controller	Receives the request, checks who the user is, picks what to do next	RentalRequestController
Service	Holds the business rules	"A rental cannot overlap another approved rental for the same item"
Data Access	Talks to the database so nothing else has to	EquipmentRepository
Database	Stores the data	Tables for users, equipment and requests
View	Turns data into the HTML page you see	Equipment list page

Why separate the layers? Each layer has one job. That makes the code easier to read, test and change. For example, you can change how data is stored without touching the pages.

Database design

Three tables to start with. Customers, staff and managers all live in one User table, separated by a Role column.

submits
reviews
is requested in
USER
int
UserId
PK
string
FullName
string
Email
string
PasswordHash
string
Phone
string
Role
bool
IsActive
datetime
CreatedAt
RENTAL_REQUEST
int
RequestId
PK
int
CustomerId
FK
int
EquipmentId
FK
int
Quantity
date
StartDate
date
EndDate
string
Status
string
Notes
datetime
CreatedAt
int
ReviewedById
FK
datetime
ReviewedAt
EQUIPMENT
int
EquipmentId
PK
string
Name
string
Description
string
Category
decimal
DailyRate
int
QuantityTotal
string
Condition
bool
IsAvailable
bool
IsArchived

Request status flow: Pending → Approved → Collected → Returned. A request can also end as Rejected or Cancelled.

Known simplification: one request covers one type of equipment. Renting several items in one request would need an extra RentalRequestItem table later.

Getting started
What you need
Git
The .NET SDK
A code editor such as Visual Studio, VS Code or Rider
Run it
bash
# 1. Get the code
git clone https://github.com/FusionFlame01/GearGo-MVC
cd GearGo

# 2. Install dependencies
dotnet restore

# 3. Start the app
dotnet run --project GearGo

Then open the address shown in the terminal (usually https://localhost:xxxx).

Settings and secrets

Never commit passwords or connection strings. Keep them out of the repository using .NET User Secrets:

bash
dotnet user-secrets init --project GearGo
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "<your-connection-string>" --project GearGo
Project structure

A typical MVC layout looks like this (your folders may differ slightly):

GearGo/
├── Controllers/    # Handle requests, call services
├── Models/         # Data classes (User, Equipment, RentalRequest)
├── Views/          # Pages the user sees
├── Services/       # Business rules
├── Data/           # Database access
├── wwwroot/        # CSS, JavaScript, images
└── Program.cs      # App starts here

Folders like bin/ and obj/ are build output. They are listed in .gitignore and should never be committed.

Working with Git
Pull the latest code before you start: git pull
Make a branch for your work: git checkout -b feature/short-name
Commit small and often with a clear message: git commit -m "Add equipment search"
Push your branch: git push -u origin feature/short-name
Open a pull request and ask a teammate to review it.

Tips:

If Git says a path "is ignored by one of your .gitignore files", it is doing its job. Do not force-add it.
Never commit secrets, bin/ or obj/.
Glossary
Term	Meaning
MVC	Model-View-Controller, a way to organise a web app into data, pages and request handling
Controller	Code that receives a web request and decides what happens
Service	Code that holds the business rules
Repository / Data Access	Code that reads and writes the database
ERD	Entity Relationship Diagram, a picture of your tables and how they connect
Primary key (PK)	A column that uniquely identifies each row
Foreign key (FK)	A column that points to a row in another table
Role	A label (Customer, Staff, Manager) that decides what a user may do
Use case	One thing a user wants to achieve, such as "submit a rental request"
