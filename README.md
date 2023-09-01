## For Swagger
https://localhost:7197/swagger/index.html

## For  setting enviroment up
Just right click on docker-compose file and do docker-compose up with 
VS Code Docker extension. Everything else needed is taken care by ServiceExtensions.cs
It will build a table , insert 3 records in it for example


## Project summary
Everything that is asked of revolves around 4 files Contact.cs , ContractRepository.cs, Dto.cs, and ServiceExtensions.cs.
Contact.cs is where our business entity is and with Getter/Setters. Access is limited with them.

Dto.cs is like the name implies it is responsible for getting things around and enforcing the length/required variables.
Email validity is checked here too but email uniqueness is not here on purpose. 

Creation of postgresql table at startup, setting the table columns is done at ServiceExtensions.cs, Email uniqueness check is done on
postgre side and set on the column here. I always thought for a heavy load operation like this, it's best to use Db side.

ContractRepository.cs is our place where we do our CRUD, everything is facilitated by Dapper,
our connetion string is in appsettings.json and it's delivered everywhere through that.

NotifyHasBirthdaySoon is calculated every time there is a Read operation on that contact
I picked this instead of setting up a BackgroundWorker operation to keep everything simple



### Random testing scenarios
//Third user is at the start up with a set guid for update/get user functions testing if you choose to do so
//it's guid is :4b2056a9-7ee4-47b1-a64f-15770ceab7aa

//Test Scenario for Birthday
</br>
{
  "salutation": "Mrs",
  "firstname": "TestFirstName1",
  "lastname": "TestLastName1",
  "email": "working@email.com",
  "displayname": "TestingDisplayName1",
  "birthdate": "1999-09-14T10:47:24.285Z",
  "phoneNumber": ""
}

//Test Scenario for DisplayName</br>
{
  "salutation": "Mrs",
  "firstname": "TestFirstName2",
  "lastname": "TestLastName2",
  "email": "working2@email.com",
  "displayname": "",
  "birthdate": "1998-09-12T10:47:24.285Z",
  "phoneNumber": "02124669900"
}
