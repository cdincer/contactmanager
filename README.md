## For Swagger
https://localhost:7197/swagger/index.html

## For getting everything up with Docker compose 
At the root folder of the project "matelso" 
first execute:
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d

### Random testing scenarios
//Test Scenario for Birthday
{
  "salutation": "Mrs",
  "firstname": "TestFirstName1",
  "lastname": "TestLastName1",
  "email": "working@email.com",
  "displayname": "TestingDisplayName1",
  "birthddate": "1999-09-14T10:47:24.285Z",
  "phoneNumber": ""
}

//Test Scenario for DisplayName
{
  "salutation": "Mrs",
  "firstname": "TestFirstName2",
  "lastname": "TestLastName2",
  "email": "working2@email.com",
  "displayname": "",
  "birthddate": "1998-09-12T10:47:24.285Z",
  "phoneNumber": ""
}
