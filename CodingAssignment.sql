-- Coding Challenges - PetPals, The Pet Adoption Platform-- Tasks/*1. Provide a SQL script that initializes the database for the Pet Adoption Platform ”PetPals”.*/if exists (select * from sys.databases where name = 'PetPals') drop database PetPals;create database PetPals;use PetPals;/*2. Create tables for pets, shelters, donations, adoption events, and participants.  3. Define appropriate primary keys, foreign keys, and constraints. */-- Pets table

drop table if exists shelters;
create table Shelters (
    ShelterID int primary key identity,
    Name varchar(255),
    Location varchar(255)
);

drop table if exists pets;
create table Pets (
    PetID int primary key identity,
    Name varchar(255),
    Age int,
    Breed varchar(255),
    Type varchar(255),
    AvailableForAdoption bit,
	ShelterID int,
    foreign key (ShelterID) references Shelters(ShelterID) on delete cascade
);


drop table if exists Donations;
create table Donations (
    DonationID int primary key identity,
    DonorName varchar(255),
    DonationType varchar(255),
    DonationAmount decimal,
    DonationItem varchar(255),
    DonationDate date,
	ShelterID int,
    foreign key (ShelterID) references Shelters(ShelterID) on delete cascade
);

drop table if exists AdoptionEvents;
create table AdoptionEvents (
    EventID int primary key identity,
    EventName varchar(255),
    EventDate datetime,
    Location varchar(255)
);


drop table if exists Participants;
create table Participants (
    ParticipantID int primary key identity,
    ParticipantName varchar(255),
    ParticipantType varchar(255),
    EventID int,
    foreign key (EventID) references AdoptionEvents(EventID) on delete cascade
);
drop table if exists adoption;
create table Adoption (
    AdoptionID int primary key identity,
    PetID int,
    ParticipantID int,
    AdoptionDate date,
    foreign key (PetID) references Pets(PetID),
    foreign key (ParticipantID) references Participants(ParticipantID)
);

-- Shelters
insert into Shelters (Name, Location) values
('Happy Paws Shelter', 'Chennai'),
('Paw Haven', 'Delhi'),
('Furry Friends Sanctuary', 'Chennai'),
('Caring Tails Shelter', 'Hyderabad'),
('Pet Paradise', 'Chennai');

-- Pets
insert into Pets (Name, Age, Breed, Type, AvailableForAdoption, ShelterID) values
('Tommy', 2, 'Labrador Retriever', 'Dog', 0, 1),
('Whiskers', 1, 'Persian', 'Cat', 0, 2),
('Buddy', 3, 'Golden Retriever', 'Dog', 0, 3),
('Mittens', 2, 'Siamese', 'Cat', 1, 4),
('Rocky', 4, 'German Shepherd', 'Dog', 1, 5),
('Lucy', 2, 'Labrador Retriever', 'Dog', 1, 2);

-- Donations
insert into Donations (DonorName, DonationType, DonationAmount, DonationItem, DonationDate, ShelterID) values
('Amit Kumar', 'Monetary', 500, NULL, '2024-01-23', 1),
('Priya Patel', 'In-kind', NULL, 'Blankets', '2024-01-24', 2),
('Raj Singh', 'Monetary', 1000, NULL, '2024-01-25', 3),
('Anjali Shah', 'In-kind', NULL, 'Pet Food', '2024-01-26', 4),
('Sandeep Kapoor', 'Monetary', 750, NULL, '2024-01-27', 5);

-- AdoptionEvents
insert into AdoptionEvents (EventName, EventDate, Location) values
('Adoption Day', '2024-02-01 15:00:00', 'Chennai'),
('Furry Fiesta', '2024-02-15 14:30:00', 'Chennai'),
('Paw Parade', '2024-03-05 13:45:00', 'Chennai');

-- Participants
insert into Participants (ParticipantName, ParticipantType, EventID) values
('Sara Verma', 'Visitor', 1),
('Rahul Sharma', 'Volunteer', 2),
('Neha Patel', 'Visitor', 1),
('Aryan Kapoor', 'Adopter', 3),
('Simran Singh', 'Volunteer', 2);

insert into Adoption (PetID, ParticipantID, AdoptionDate)
values
    (1, 1, '2024-02-15'), 
    (2, 2, '2024-02-01'), 
    (3, 3, '2024-03-05');

/*5. Write an SQL query that retrieves a list of available pets (those marked as available for adoption)
from the "Pets" table. Include the pet's name, age, breed, and type in the result set. Ensure that
the query filters out pets that are not available for adoption*/
select name,age,breed,type  from Pets
where AvailableForAdoption=1;

/*6. Write an SQL query that retrieves the names of participants (shelters and adopters) registered
for a specific adoption event. Use a parameter to specify the event ID. Ensure that the query
joins the necessary tables to retrieve the participant names and types.*/

declare @EventID int;
set @EventID =1;
select ParticipantName from Participants
where EventID = @EventID;

/*7. Create a stored procedure in SQL that allows a shelter to update its information (name and
location) in the "Shelters" table. Use parameters to pass the shelter ID and the new information.
Ensure that the procedure performs the update and handles potential errors, such as an invalid
shelter ID.*/

-- Question on stored procedure is skipped

/*8. Write an SQL query that calculates and retrieves the total donation amount for each shelter (by
shelter name) from the "Donations" table. The result should include the shelter name and the
total donation amount. Ensure that the query handles cases where a shelter has received no
donations*/
--alter table donations
--add ShelterID int;

select Shelters.Name, sum(Donations.DonationAmount) as TotalDonationAmount from Shelters
left join Donations on Shelters.ShelterID=Donations.ShelterID
group by Shelters.name;

/*9. Write an SQL query that retrieves the names of pets from the "Pets" table that do not have an
owner (i.e., where "OwnerID" is null). Include the pet's name, age, breed, and type in the result
set.*/
select name,age,breed,type from pets
where AvailableForAdoption = 1;

/*10. Write an SQL query that retrieves the total donation amount for each month and year from the "Donations" table. The result should include the month-year and the
corresponding total donation amount. Ensure that the query handles cases where no donations
were made in a specific month-year.*/

select format(donationdate,'MMMM') as Month, year(donationdate) as Year, sum(donationamount) as TotalDonationAmount from donations
group by format(donationdate,'MMMM'), year(donationdate);

/*11. Retrieve a list of distinct breeds for all pets that are either aged between 1 and 3 years or older
than 5 years*/
select distinct breed from pets
where (age between 1 and 3) or (age>5);

/*12. Retrieve a list of pets and their respective shelters where the pets are currently available for
adoption.*/
select p.petid, p.name as PetName, p.breed, p.type, s.shelterid, s.name as ShelterName from pets p
join shelters s on s.ShelterID = p.shelterid
where p.AvailableForAdoption=1;

/*13. Find the total number of participants in events organized by shelters located in specific city.
Example: City=Chennai*/
select count(distinct Participants.ParticipantID) as TotalParticipants from Participants
join AdoptionEvents on Participants.EventID = AdoptionEvents.EventID
join Shelters on AdoptionEvents.Location = Shelters.Location
where Shelters.Location = 'Chennai';


/*14. Retrieve a list of unique breeds for pets with ages between 1 and 5 years.*/
select distinct breed from pets
where age between 1 and 5;

/*15. Find the pets that have not been adopted by selecting their information from the 'Pet' table.*/
select * from pets
where AvailableForAdoption=1;

/*16. Retrieve the names of all adopted pets along with the adopter's name from the 'Adoption' and
'User' tables.*/
select Pets.Name AS PetName, Participants.ParticipantName AS AdopterName, Adoption.AdoptionDate from Adoption
join Pets ON Adoption.PetID = Pets.PetID
join Participants ON Adoption.ParticipantID = Participants.ParticipantID;


/*17. Retrieve a list of all shelters along with the count of pets currently available for adoption in each
shelter.*/
select s.ShelterID, s.Name as ShelterName, count(p.PetID) as AvailablePetsCount from Shelters S
left join Pets P ON S.ShelterID = P.ShelterID AND P.AvailableForAdoption = 1
group by s.ShelterID,s.name;

--18. Find pairs of pets from the same shelter that have the same breed.
select p1.petid , p1.ShelterID, p1.name, p1.Breed from Shelters s
join pets p1 on s.ShelterID = p1.ShelterID
join pets p2 on p1.Breed=p2.Breed and p1.PetID != p2.PetID;


--19. List all possible combinations of shelters and adoption events.
select s.shelterid, s.name as ShelterName, a.eventid, a.eventname from shelters s
cross join AdoptionEvents a;

--20. Determine the shelter that has the highest number of adopted pets.*/
select top 1 shelterId, count(*) as AdoptedPetsCount from pets 
where AvailableForAdoption = 1
group by shelterid
order by AdoptedPetsCount desc;