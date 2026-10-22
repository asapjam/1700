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

### Data annotations

Attributes you put on properties to declare the rules for valid input.

### Handler

### REST (Representational State Transfer)

a set of principles for designing APIs.

### Safe HTTP Method

Doesn't change server's data.

### Idempotent

Sending the same request once or ten times leaves the server in the same state.

### HTTP (HyperText Transfer Protocol)

The set of rules for how clients and servers talk to each other, and everything you build in this course runs on top of it.

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

# FUNDAMENTAL INSTRUCTIONS

    cd <path where you want to create a new folder>
    dotnet new webapp -o <New_Project_Folder_Name> --framework net10.0
    dotnet dev-certs https --trust
    dotnet run
    OR
    dotnet watch (for hot reloading)
    THEN
    in launchSettings.json
    put the HTTPS profile at the top, not the bottom.

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

    HTTP Request & Response are literally just text.
    The HTTP Request/Response Cycle:
        REQUEST (Client)
            - w/ a METHOD, like GET, POST, etc
            - w/ a path, like /hello
            - w/ a header, containing extra info about the request
            - w/ a body, data sent along with the request
            Ex.
                GET /hello HTTP/1.1
                Host: localhost:7044
        RESPONSE (Server)
            - w/ a status code, like 200 OK or 404 Not Found.
            - w/ a header, containing extra info about the response
            - w/ a body, the content being returned (HTML, JSON, plain text)
            Ex.
                HTTP/1.1 200 OK
                Content-Type: text/plain; charset=utf-8
                Hello from the Minimal API!

    HTTP Methods/Verbs:
        GET
            (Retrieve a resource)
            (Idempotent)
            (Safe)
        POST
            (Create a new resource)
            (Idempotent)
        PUT
            (Replace an existing resource entirely)
            (NOT-Idempotent).
        PATCH
            (Update part of an existing resource)
            (Idempotent)
        DELETE
            (Remove a resource)
            (Idempotent)

    HTTP Status Codes:
        First digit informs you on the type of result:
            1xx: Informational.     The request is still being processed.
            2xx: Success.           The request worked.
            3xx: Redirection.       What you want is somewhere else.
            4xx: Client error.      Something is wrong with the request itself.
            5xx: Server error.      The request was fine, but the server failed handling it.

    REST (Representational State Transfer):
        A set of principles for designing APIs.
        Core Conventions:
            Resource-based:             Paths name things. You'll see /bands/42, not /getBand?id=42.
            Methods do the HTTP Verb:   GET /bands/1, will get you the first band.
            Stateless:                  Each request carries everything the server needs to handle it.
            Consistent:                 Once you know the pattern, you can guess how an API works.
        REST in Minimal APIs:
            Map method name tells you the HTTP method.
            Route is the resource path.
            Results helpers let you return the right status code.
            EX.
                app.MapPost("/bands", (Band band) =>
                {
                    bands.Add(band);
                    return Results.Created($"/bands/{band.Id}", band);
                });

## cURL & Postman

    Must trust the apps certificate by running the following in terminal:
        dotnet dev-certs https --trust

    cURL
        Good for one-off GET requests.
        Ex.
            curl.exe -i https://localhost:<your-port>/hello
    Postman
        Good for all other requests.
        Has a UI, so a bit easier to use.
        Must turn off "SSL certificate verification" in Settings while testing locally.

# MODULE 2: Exploring Razor Pages

    Razor Pages is a good fit for:
        - Business forms and data-entry screens
        - Admin dashboards and internal tools
        - Sites where most of the work happens on the server, with little or no JavaScript in the browser

## Anatomy of a Razor Page

    PageModel (.cshtml.cs)
        a C# class that inherits from PageModel
        Does two things:
            - Holds the page's data in public properties.
            - Handles requests, like onGet()
    Markup (.cshtml)
        An ordinary HTML file that can include C# code, by using the "@" symbol.
        Output values are HTML encoded.
        Example syntax:
            - @page :                       Required as first line to give the page a URL
            - @model FeaturedBandModel :    Which PageModel class this page uses.
            - @Model.BandName :             Outputs a value from the PageModel into the HTML.
            - @{ ... } :                    A code block for C# statements.
            - @if (...) { ... } :           HTML only appears in condition is true.
            - @* ... *@ :                   Notes that don't reach browser.

    From Request to HTML
        - https://localhost:7003/FeaturedBand
            points to FeaturedBand.cshtml, because the file is at Pages/FeaturedBand.cshtml and starts with @page.
        - GET request is sent and properties are set.
        - Razor evaulates C# code prefixed with @
        - Result is wrapped in shared layout and sent to browser.

    The execution is done server-side.

## Forms and Request Handling in Razor Pages

    HTML Forms define a method (GET/POST) and an action. Inputs require a name.
        Ex. HTML
            <form action="www.google.com" method="post">
                <label for="bandName">Suggest a band name:</label>
                <input id="bandName" name="BandName" />
                <button type="submit">Submit</button>
            </form>
        Ex. HTTP
            POST /SuggestBand HTTP/1.1
            Host: localhost:7044
            Content-Type: application/x-www-form-urlencoded

            BandName=Shorts+Are+Better+Than+Pants&__RequestVerificationToken=CfDJ8N...

    How Forms Send Data
        Forms that GET put the form data in the URL as a query string, like "/Search?term=metal".
        Forms that POST put the form data in the request body of the HTTP Request.

    Handling Requests in PageModel
        GET requests run on onGet()
        POST requests run on onPost()
        Ex. PageModel
            using Microsoft.AspNetCore.Mvc.RazorPages;
            namespace ServerSideCodeAlong.Pages;
            public class SuggestBandModel : PageModel
            {
                public string Confirmation { get; set; } = string.Empty;

                public void OnGet(){
                    // Runs when the page first loads.
                    // Prepare anything the page needs to display.
                }
                public void OnPost(){
                    // Runs when the form is submitted.
                    Confirmation = "Thanks! Your suggestion is in.";
                }
            }
        Ex. CsHTML
            @if (!string.IsNullOrEmpty(Model.Confirmation)) {
                <p>@Model.Confirmation</p>
            }
        Handlers can return void, which renders the current page again. They could also redirect to another page by returning an IActionResult:
            return RedirectToPage();

    Form Resubmission
        Post isn't safe to repeat, it can lead to duplicates in the database.
        Solved with Post/Redirect/Get (PRG) pattern:
            public IActionResult OnPost() {
                // Save the suggestion somewhere...
                return RedirectToPage();
            }

    Async Handlers OnGetAsync() & OnPostAsync()
        Used to handle slow operations like Database queries.
        Use OnGet() OR OnGetAsync(), NEVER both.
        Ex. C#
            public async Task OnGetAsync() {
                // Code that awaits slow operations goes here
            }
            public async Task<IActionResult> OnPostAsync() {
                // ...
                return Page();
            }

## Model Binding and the PageModel

    HTTP Requests just come back as plain text, not Objects. This sucks. So we have to bind them to Models.
    Model Binding
        ASP.NET Core's built-in system for unpacking. Mark the properties you want filled in with [BindProperty].
        Ex.
            public class SuggestBandModel : PageModel{
                    [BindProperty]
                    public string BandName { get; set; } = string.Empty;

                    public void OnPost() {
                        // By the time this runs, BandName, Genre, and Rating are already filled in.
                        Confirmation = $"Thanks! {BandName} is in!";
                    }
            }
        Bound properties are available anywhere the PageModel is.
        Ex. csHTML
            <p>@Model.BandName</p>
        Bound properties may be used with GET requests, via [BindProperty(SupportsGet = true)]
        Model Binding can also read query strings, like ?searchTerm=metal, and route values, like /bands/{id}.
        You may also bind properties through the handler.
        Ex. C#
            public void OnPost(string bandName){
                // bandName is bound from the request
            }
        If an invalid value is entered, like a string in a number field, it will return the default value and record the issue in the ModelState.

## Shared Layouts and Navigation

    Pages/Shared/_Layout.cshtml
        Defines the shared header, navbar, and footer across all webpages.
        Basically each invidual page is just the @RenderBody().

    Pages/_ViewStart.cshtml
        Runs before each webpage loads.
        Links the layout to the page.

    Conventionally pages that aren't accessible on their own are prefixed with an _.
    Links wont use href, they'll use asp-page.
    Pages can override the default Layout by linking to a different Layout.

## Tag Helpers and Validation

    Tag Helpers
        a Razor attribute, usually starting with asp-, that runs on the server and generates HTML.
        asp-for
            connects an element to a PageModel property.
            <label asp-for="BandName"></label>
            <input asp-for="BandName" />

    Data Annotations
        Like [BindProperty], but they define attributes for valid input.
        [Required(ErrorMessage = "Why even submit the form without a band name?")]
        [Display(Name = "Band name")]
        [StringLength(50, ErrorMessage = "Nobody can remember a band name that long. 50 chars max.")]

    Validation
        Declaring a type string?/int? allows the value to be optional in ASP.NET Core.
        ModelState.IsValid bool allows us to check if all the inputs were valid.
        <div asp-validation-summary="All"></div> at the top of a csHMTL file will list all errors together.