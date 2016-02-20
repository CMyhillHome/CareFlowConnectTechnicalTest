This assessment uses the assessment database, credentials for which can be found here: https://passwords.doccom.local/pid=611

You can populate an empty database using the script PopulateDatabase.sql. This will create the user, two tables, and some data for the tables.

This is intended to be used as a home-based assessment for senior level candidates. There are three branches of interest:

linq-to-objects
In this assessment the candidate has to implement DI for the web API project, provide an in-memory implementation of an Entity Framework data context, and write unit tests for the web API controller using the in-memory version. I anticipate this would be very challenging.

di-container
In this assessment the in-memory implementation of the Entity Framework data context is already there. The candidate still has to implement DI for the web API project and write unit tests for the web API controller using the in-memory version. This should take less time.

solution
This contains a completed assessment, just to prove it can be done!

In order to get a candidate to do the home-based assessment, you will need to do the following:
1. Ensure there is a database for the API to connect to. The connection string in the REST API application expects a database called 'developerassessment' on a server called 'wbp9ox7b5l', so if you don't use that database, you'll need to update the connection string.
3. Ensure the instructions in the file 'Instructions for the candidate.txt' are sufficient.
4. Zip up the solution (i.e. the folder 'PaientsAndEpisodes') and send it to the candidate. The .git file is above this folder, so you don't have to worry that the candidate will be able to access the repository.