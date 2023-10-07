# Grocery App Readme

Welcome to the Grocery App! This readme file will help you get started with setting up and running the Grocery App, both for the frontend and the backend. Please follow the steps carefully.

## Prerequisites

Before you begin, ensure you have the following software installed on your system:

### Frontend (Angular)

- **Node.js and npm**: Make sure you have Node.js and npm (Node Package Manager) installed. You can download them from [nodejs.org](https://nodejs.org/).

### Backend (.NET Core)

- **.NET 5.0 SDK**: Install the .NET 5.0 SDK, which is required to build and run the backend API. You can download it from the official [.NET website](https://dotnet.microsoft.com/download/dotnet/5.0).

## Frontend Setup

1. **Navigate to the Frontend Folder**:

   Open a terminal and navigate to the frontend folder of the Grocery App.

   ```bash
   cd grocery-app-frontend

###  Install Dependencies:

Run the following command to install the required dependencies for the frontend.

```bash

npm install
```
Start the Frontend:

Once the dependencies are installed, start the frontend development server.

```bash

ng serve
```
The frontend should now be running at http://localhost:4200/.

*Access the App:*

Open your web browser and go to http://localhost:4200/ to access the Grocery App.

Backend Setup
Navigate to the Backend Folder:

Open a terminal and navigate to the backend folder of the Grocery App.

```bash

cd grocery-app-backend
```
Restore NuGet Packages:

Run the following command to restore the NuGet packages for the backend.

```bash

dotnet restore
```
Run the Backend:

Start the backend API by running the following command:

```bash
dotnet run
```
The backend API should now be running at http://localhost:5000/ (HTTP) and https://localhost:5001/ (HTTPS).

**Database Configuration:**

The Grocery App uses Entity Framework Core with SQL Server for the database. Make sure to configure your database connection in the appsettings.json file.

**Apply Migrations:**

If this is the first time you're running the app or if there are database changes, apply migrations to create the database schema.

```bash
dotnet ef database update
```
This command will create the necessary tables in your database.

Using the Grocery App
Visit http://localhost:4200/ in your web browser to access the Grocery App's front end.

The front end provides features for browsing products, adding them to the cart, and placing orders.

The navbar contains buttons for user authentication, viewing the cart, and placing orders.

As an admin user, you can also add, edit, and remove products.

To log in as an admin, you can use the default admin credentials or create a new admin user through the registration process. The default admin credentials are:

Email: admin@example.com
Password: adminpassword
The cart button in the navbar displays the number of items in your cart.

Default Admin User
If you don't have an admin user in the database, the app will automatically seed an admin user when you run the backend.

Email: admin@example.com
Password: adminpassword
Important Notes
Make sure you have the required software and dependencies installed before running the app.

Remember to configure the database connection string in the appsettings.json file for the backend.

For production use, consider securing your application and database, and follow best practices for deployment.
