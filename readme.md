# Automailer
A simple program to send emails

## NachoTacos.Automailer.Api
The front end interface to be consumed by front-end UI

## NachoTacos.Automailer.Data
The data services layer handling all database related tasks

## NachoTacos.Automailer.Domain
The domain layer, the heart of the application

# NachoTacos , They're MY Tacos!

## To run this project after git clone
### Database setup
1. NachosTacos.Automailer.Api project
 a. Make sure the connection string in appsettings.json "AutomailerConnection" is correct
2. Update the database migrations
 a. Open up Package Manager Console
 b. Make sure the default project is set to NachoTacos.Automailer.Data
 c. Run the command 'update-database'
If everything goes well, you should see the AutomailerDB database created

### Test email using smtp4dev
1. Install 'smtp4dev' -- if you haven't done so
 a. From command prompt, run the command:
 ```
 dotnet tool install -g Rnwood.Smtp4dev --version "3.1.0-*"
 ```
 b. Start smtp4dev to capture emails
 ```
 smtp4dev
 ```

