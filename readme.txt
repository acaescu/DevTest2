I concentrated mostly on the authentication and authorization part. 
There are 2 types of users, one admin and one client. Each of them will be redirected to their own section of the application and they will only be able to see the sections 
that they are allowed to see, as defined in the data.

To test, you can use 

USER: client
PASS: password
for the client user, and

USER: admin
PASS: password
for the admin user.

The admin section is empty for now, while the client section shows a bills list, where you can click to pay unpaid bills.
There are a few unit tests added.

I know there are a lot of things left to do, next i would probably touch global error handling and logging