GamesAPI may be used to track and compare lists of users' favorite video games.
It Implements 4 flows:
- Search for games
- Create users
- Add games to/remove games from a user's list of favorite games
- Compare two users' lists of favorite games

In order to run the web application you can download the repo and run it in Microsoft Visual Studio.
 Before running it:
 1. Generate a free RAWG API key https://rawg.io/apidocs
 2.  Add a new settings file name "appsettings.Development.json" with the follwing setting:
    {"RAWGSettings": {"RawgApiKey": "<key>" }}
 4. Run the Application, SwaggerUI will open on the browser.
 5. Use SwaggerUI, PostMan or other rest client to call endpoints.

 
