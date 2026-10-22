# 1700

Course Work for 1700

## Course Objective

Learn to build modern, dynamic web applications using ASP.NET Core, Microsoft’s web framework. From your first HTTP request to a fully functioning, database-backed web application. The course progresses through these topics:

- Core building blocks of ASP.NET Core apps
- Requests and responses
- Routing
- Simple APIs
- Razor Pages
- SQLite
- Entity Framework Core
- CRUD operations
- Form validation
- API endpoints
- User-friendly patterns: search, feedback, error handling

## Glossary

https://btech.instructure.com/courses/621814/pages/glossary?module_item_id=13445053

# Vocabulary

### Handler

### Razor Pages

When you create a Razor Pages project from the built-in template, you get a complete, working web application before you've written a line of code.

### .NET SDK

The tools, compilers, and libraries for building and running .NET applications. This course uses .NET 10.

### HTTP Methods

GET, POST, PUT, and DELETE that tell the server what the client wants to do.

### Routing

How ASP.NET Core decides which endpoint should handle an incoming request.

### Endpoint

A specific path and HTTP method that your app responds to, such as GET /hello.

### DRY Code

Don't Repeat Yourself Code.

### Polymorphism

### API

Application Programming Interface.
An API is a set of rules for how one piece of software can request information from, or send instructions to, another. On the web, that usually happens over HTTP.
An API is made up of endpoints. Each endpoint is a specific path and HTTP method that a client can call to get data or trigger an action.

### Minimal APIs

A way to build lightweight HTTP Endpoints.

# MODULE 0: Getting Started

## Terminal Basics

How:

- Windows Key
- "powershell"

Why:

- Create, move, and organize files quickly
- Run code and development tools
- Install and manage packages or libraries
- Work with source control tools like Git
- Launch and test applications

### Commands

pwd Show the current folder (stands for “print working directory”)
ls List files and folders in the current directory
cd foldername Change directory (move into a folder)
cd .. Move up one level (to the parent folder)
cd /path/to/folder Go directly to a folder using an absolute path
clear Clear the screen
cd ~ Return to Home

## Installing the .NET SDK

Step 1: Install Visual Studio Code
Step 2: Install the C# Dev Tools Extension
Step 3: Follow the Prompts, Click Install .NET SDK when prompted.

## C# Crash Course

Strongly typed: Declare variable types explicitly (or use var for type inference).
Once a variable's type is set, it cannot change.

Strings use double quotes ("Hello"), not single quotes.
Single quotes are for char type, which holds exactly one character.
C# does not auto-convert between types. Can't compare an int and a string directly.

### Functions/Methods

In C#, all code must be organized within classes, so all functions are actually methods (functions belonging to a class). Methods are defined inside classes:

    class Program
    {
        static void Greet(string name)
        {
            Console.WriteLine("Hello, " + name);
        }

        static void Main()
        {
            Greet("Alice");
        }
    }

Methods cannot exist outside of classes
Must declare the return type (void, int, string, etc.)
Use static for methods that don't need an instance of the class
Use void return type for methods that will not return a value

### Arrays/Lists/Collections

#### Arrays

Arrays have fixed sizes.
int[] numbers = { 1, 2, 3 };
string[] names = new string[5]; // empty array with 5 slots
Console.WriteLine(numbers[0]); // 1

#### Lists

Lists have dynamic sizes.
using System.Collections.Generic;

    List<string> cities = new List<string>();
    cities.Add("New York");
    cities.Add("London");
    cities.Remove("London");

### Classes/Objects

C# is object-oriented from the ground up:
public: accessible from anywhere
private: only accessible within the same class (default for fields)
protected: accessible within the class and its subclasses
class Dog
{
public string Name;
public int Age;

        public void Bark()
        {
            Console.WriteLine(Name + " says woof!");
        }
    }

    // Using the class:
    Dog myDog = new Dog();
    myDog.Name = "Fido";
    myDog.Age = 3;
    myDog.Bark();

### Inheritance/Polymorphism

virtual: allows a method to be overridden in derived classes
override: indicates you're providing a new implementation of a virtual method
: is used for inheritance (class Cat inherits from Animal)
class Animal
{
public virtual void Speak()
{
Console.WriteLine("Animal noise");
}
}

    class Cat : Animal
    {
        public override void Speak()
        {
            Console.WriteLine("Meow");
        }
    }

### Exception Handling

Handle runtime errors with try-catch blocks:
try
{
int result = 10 / 0;
}
catch (DivideByZeroException ex)
{
Console.WriteLine("Cannot divide by zero!");
}
catch (Exception ex)
{
Console.WriteLine("Something went wrong: " + ex.Message);
}
finally
{
Console.WriteLine("This always runs");
}

### Namespace and "Using" Statements

Organize code with namespaces and import functionality with using:

using System;
using System.Collections.Generic;

namespace MyApplication
{
class Program
{
static void Main()
{
Console.WriteLine("Hello World!");
}
}
}

Common namespaces:
System: Basic functionality (Console, Convert, etc.)
System.Collections.Generic: Lists, dictionaries, etc.
System.IO: File and directory operations

### String Formatting

String interpolation (preferred):
string name = "Alice";
int age = 25;
string message = $"Hello {name}, you are {age} years old";
String concatenation:
string message = "Hello " + name + ", you are " + age + " years old";

### Console Input/Output

Output:
Console.WriteLine("What's your name?");
Console.Write("Enter age: "); // no newline
Input:
string name = Console.ReadLine();
Converting input to numeric types:
Console.WriteLine("Enter a number:");
string input = Console.ReadLine();
int number = int.Parse(input);
// Or safer:
if (int.TryParse(input, out int safeNumber))
{
Console.WriteLine($"You entered: {safeNumber}");
}

### Key Differences from Javascript

Concept JavaScript C#
Variable declaration let x = 5 int x = 5;
String quotes Single or double Double only (single for char)
Type conversion Automatic Explicit (int.Parse())
Function definition function name() static returnType Name()
Object creation {} or constructor new ClassName()
Arrays [1, 2, 3] new int[] {1, 2, 3}
Truthiness Many values are truthy/falsy Only true/false

## Testing APIs: Postman and cURL

Note: Some API are picky and require a / at the end of the URL.

### Postman (GUI)

- Download: https://www.postman.com/downloads/
- "New" > "HTTP Request"
- Method: "GET"
- "Send"

### CommandLine (cURL)

- Open Powershell
- curl https://binaryjazz.us/wp-json/genrenator/v1/genre/

# MODULE 1: Project Setup and Minimal API Intro

Minimal APIs are a way to build lightweight HTTP Endpoints.

- Small backend services and microservices.
- APIs that serve data to mobile apps or JavaScript front ends.
- Prototypes and internal tools.

## Razor Pages Web App & Project Structure

### Razor Pages

    a template for a working web application as part of ASP.NET Core.
    Organized around individual web pages.
    Routed from Pages/ as the root of the website.
    Each page is a pair of files that live side by side:
        .cshtml         The page's HTML markup, with Razor syntax for embedding C#
        .cshtml.cs      The PageModel, a C# class with the page's server-side logic

### Tour the Project

    Using the Solution Explorer, which loads automatically if a .sln file is present.
    Pages/
        Holds the .cshtml & .cshtml.cs file pairs.
    Pages/Shared/_Layout.cshtml
       Holds shared layout that wraps every page: the header, navigation bar, and footer.
    wwwroot/
        Public root for static files like CSS, Javascript, and Images.
    Program.cs
        The entry point of your application, where the app is configured and started.
        Two halves:
            Before "builder.Build()":
                You register the services your app needs. Here, that's support for Razor Pages.
            After "builder.Build()":
                You configure how the app handles requests and map them to pages and endpoints.
    appsettings.json
        Configuration settings in JSON format, such as logging levels and, later in the course, database connection strings.
    Properties/launchSettings.json
        Settings Visual Studio uses when it runs your app locally, including the URLs and port numbers. That's where the https://localhost:7044-style address comes from.
        It's where your port number comes from.
    ServerSideCodeAlong.csproj
        Never really edited by hand.
        The project file, named after your project. It defines:
            Which version of .NET the project targets (net10.0)
            Which NuGet packages the project depends on
            Build settings and project metadata

## Minimal APIs and Running An ASP.NET App Locally

    API & Endpoints
        APIs are how software communicate with one another. Composed of Endpoints
    Minimal APIs
        request → route match → handler → response.
            request: https://localhost:7044/hello
            route match: app.MapGet("/hello",
            handler:  () => "Hello from the Minimal API!");
            response: HTTP Response
        Let you define Endpoints in a single-line in Program.cs
        Can return more than just plain text. Returned Objects are automatically converted to JSON. Property names are automatically made into camelCase.
            Ex.
                app.MapGet("/hello", () => "Hello from the Minimal API!");
                app.MapGet("/math", () =>
                    {
                        int x = 1;
                        return x * 3;
                    });
        Endpoints can also take input straight from the URL using a route parameter:
            Ex.
                app.MapGet("/hello/{name}", (string name) => $"Hello, {name}!");
    Routing
        Requests are made up of two parts:
            - A path, like /hello
            - a method, like GET
    Running the App
        "Ctrl + F5" OR "dotnet run" in a terminal.

## HTTP & REST
