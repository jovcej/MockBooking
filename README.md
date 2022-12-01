
1.//REGISTER
API: /api/User/registeruser

reqmodel:
{
  "firstName": "test",
  "lastName": "test",
  "email": "test@test.com",
  "password": "Qwerty1%"
}



2.//LOGIN (Create JWT TOKEN)
API: /api/User/loginuser

reqmodel:
{
    "email": "test@test.com",
    "password": "Qwerty1%"
}


3.//SEARCH
Api: /api/Search

reqModel:
{
  "destination": "SKP",
  "departureAirport": "CPH",
  "fromDate": "2023-11-30T21:31:54.784Z",
  "toDate": "2022-11-29T21:31:54.784Z"
}

4.//BOOK
Api: /api/Book

reqModel:
{
  "optionCode": "feead276-9022-43e0-8628-7f5bfe0e940c",
  "searchReq": {
    "destination": "SKP",
    "departureAirport": "CPH",
    "fromDate": "2022-11-29T21:33:19.381Z",
    "toDate": "2022-11-29T21:33:19.382Z"
}

5.//CHECKSTATUS
Api: api/CheckStatus

reqModel:
{
  "bookingCode": "BDlivz"
}


