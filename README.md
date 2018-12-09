# LunchTime
Service oriented website created for displaying menus in a readable manner

Features:
- Easily add a new restaurants menu.
- This menu can be either in image, pdf or text format.
- Caching will make sure the website is only scanned once a day.

Upcoming features:
- Groups where people can vote
- Filtering on location
- User added websites with admin approval
- Wizard for adding new websites
- Report button for wrongly added website

Installation:
- Compile and run LunchTime.Services (These are the services and backend)
- Configure LunchTime.Web appsettings.json for the correct Url to LunchTime.Services
- Compile and run LunchTime.Web (This is the actuall website)
- Feel free to create your own page that utilizes the Services layer
