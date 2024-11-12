# LexiAuthenAPI Project

## Overview
LexiAuthenAPI is an authentication and authorization API designed to manage users, roles, permissions, and related security features. The project is built using ASP.NET Core and Entity Framework Core, providing robust and scalable authentication services.

## Project Structure
The project is structured as follows:
- `LexiAuthenAPI.Api`: The main web API project, handling HTTP requests and routing.
- `LexiAuthenAPI.Domain`: Contains the domain models and entities.
- `LexiAuthenAPI.Infrastructure`: Implements data access and the `ApplicationDbContext` for interacting with the database.

## Key Features
- User management (Create, update, delete users)
- Role management (Assign, remove roles)
- Permission handling (UI, data, and menu permissions)
- Token-based authentication using JWT
- Implementation of SOLID principles for maintainability and scalability

## Prerequisites
Ensure you have the following installed:
- .NET SDK 8.0 or later
- SQL Server or a compatible database engine
- Entity Framework Core tools

## Getting Started
1. **Clone the repository**:
   ```bash
   git clone https://github.com/yourusername/LexiAuthenAPI.git
   cd LexiAuthenAPI
