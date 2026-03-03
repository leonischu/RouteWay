🚀 Booking System

  A simple and secure vehicle/seat booking system with JWT authentication and a clean layered backend structure.

Project Features

      🔐 User Authentication using JWT

      👤 Secure Login & Registration  

      🎟️ Vehicle / Seat Booking

      📋 View Bookings

      ❌ Cancel/Delete Booking

      🔄 REST API Integration

      📦 Clean Layered Backend Structure

      🔑 AuthGuard for route protection

      🧑‍💼 Admin Seeding

🏗️ Project Architecture
    🔹 Frontend – Angular

      Angular Components & Services

      API calls using HttpClient

      Form validation

      Dynamic booking display

      Token-based authentication handling

      AuthGuard for protecting routes:

      AuthGuard ensures that only authenticated users can access certain routes.

      Redirects users to the login page if they are not authenticated.

🔹 Backend – ASP.NET Core

      RESTful Web API

      Business logic for the booking system

      JWT token generation & validation

      Secure protected endpoints

      Dapper for database operations

      Admin Seeding:
      An admin user is seeded in the database on startup to ensure that there is always an administrative account for system management.

🗄️ Database Access – Dapper

        Dapper is used instead of Entity Framework as a lightweight ORM.

        High-performance data access with raw SQL queries.

        Manual query control for better flexibility.

        CRUD operations using Dapper.

    
🔐 Authentication – JWT

        JWT (JSON Web Token) is used for secure authentication:

        User logs in.

        Server validates credentials.

        JWT token is generated and stored on the frontend.
  
        Token is sent in the Authorization header with API requests.

        Backend validates token before granting access.

        Protected routes require a valid JWT token.

  How the System Works

    User Registration/Login
            User registers or logs in, generating a JWT token.

     Searching for Transport
                User searches for available vehicles/seats.

    Booking Request
                 Booking request is sent to the backend.

     Processing Booking
                Backend processes the booking logic and stores booking data using Dapper.
   
    UI Update
               Frontend displays booking confirmation.

    Admin Seeding
              An admin account is seeded in the database on the backend, ensuring there is always an admin for managing the system.

🛠️ Technologies Used


   Frontend:

            Angular

            TypeScript

            HTML

            CSS / SCSS
   Backend:

            C#

            ASP.NET Core Web API

            Dapper

            SQL Server
 
           JWT Authentication

🎯 Learning Goals

      Implement secure JWT authentication.

      Understand and work with Dapper for database access.

      Build a complete booking system.

      Connect the Angular frontend with the .NET backend.

      Handle protected API routes using AuthGuard.

      Implement admin seeding for initial system setup.
