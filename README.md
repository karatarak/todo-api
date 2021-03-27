# todo-api

## Objective
Build a To Do List-type API that meets the challenge requirements

## Requirements
- Build a backend API
- Deploy a console that demonstrates the output of what you have created
- The user should be able to create multiple items that they want to remember to do and must be able to:
	- add item
	- edit item
	- remove item
- Each item must have:
	- Title
	- Description
	- Status (pending, completed)
	- Due Date
- Make it simple for the user to track the status of multiple items as well as remember when things are due
- Must work on a desktop web browser

## Approach
This repository contains a .NET 5.0 webapi project that implements a REST API for managing todo list items.  It's a very basic prototype and is missing many features, some of which are discussed below in the Next Steps section.

The API exposes an items endpoint which can we used by a web app or mobile app to create a kanban-style todo experience. Each item has a board identitifer indicating which board it is on and a status indicating which column it is own. There is no fixed status list currently. Users are free to set any string status on a item which would 'move' it into a new column. A boards endpoint provides a GET method to fetch items related to a board in a kanban data structure.

- The code is modified from a dotnet webapi template
- The app can be packaged and run locally using a provided Dockerfile
- Three controllers are implemented:
	- ItemsController provides RESTful CRUD operations for a Item resource  (GET, POST (create), PUT (update), DELETE)
	- BoardsController provides a GET method for retrieving item data in a kanban data structure
	- VersionController returns a version string and is meant as a health check endpoint.
- The ItemsController andBoardController are injected with ItemsRepository which holds an in-memory list of all items. It abstracts the data layer from the controller. Thsi would be replaced with a DB implementation in future
- A swagger webpage is auto-generated from the code and can be used to interact with the API
- The postman folder holds an exported Postman collection and environment file which provide scripts to interact with the boards and items endpoints

## Next Steps
A number of API features are missing and would be required for a production ready app

- App should be stateless so it neesd a backing database and a DB-specific repository implementation
- Authentication is required so users can only view, edit and remove their own items. The user_id query param (useful for allowing web browser GETs) should be unpacked from an authorisation token provided in the Authorization header.
- Model validation is mostly missing. The API should return 400 status codes if invalid parameters are sent
- Swagger file has no custom descriptions. Code annotations can be used to populate the swagger docs with useful commentary
- Extended unit tests on controllers. The in-memory repository can be used for testing controllers but should be populated with test data from the test class instead of in the Repository's constructor
- Support PATCH (or modify how PUT works if thats prefered default behaviour) so partial property updates on Items are viable. This would require a custom json mapping of request body params to detect whether a property is missing (ignore this field when updating), set to null or a valid value
- HTTPS support. TLS termination and HTTP redirection would be usually handled by a load balancer.

In addition their a a couple of issues with the current data model for supporting kanban boards

- Boards are infered from item properties but should be a first-class domain entity. This way they could be shared between different users
- Columns are inferred from status but as such, cannot have ordering of special behaviour associated with them (i.e hide item with 'completed'). Each board should have a set of valid statuses/columns a user could edit and have metadata like order, default etc

## Item Data Model

An item has the following data structure
'''
{
    "item_id": "816afa15-afa4-48c0-bc8e-c22b08a8829a",
    "user_id": "peter",
    "board_id": "default",
    "title": "Feed the tiger",
    "description": "Mr Snuggles is hungry!",
    "status": "pending",
    "created_date": "2021-03-27T19:48:53.280925Z",
    "due_date": "2021-04-01T10:10:10Z"
}
'''

## Running the app locally
In order to run the app locally you will need the dotnet development tooling installed. From the repository base folder you can use the following commands to build, test and run the Todo application. The application will run on http://localhost:5000

```
> dotnet build
> dotnet test
> dotnet run --project .\src\Todo\
```

## Running the app with docker
You can also build and run the app locally with docker. From the repository base folder you can use the following commands to build and run the Todo application. The application will run on http://localhost:5000 assuming you bind the port like in the command below
```
docker build . -t todo
docker run -d -p 5000:80 --name todo todo:latest
```

## Interacting with the app
The REST API will now be available at http://localhost:5000/. The base path for all endpoints is /api/v1. You can interact with the API using a web browser, from the generated swagger UI or using a collection of postman scripts.

### From a web browser
Some queries can be executed directly from the browser. Try the following:

GET the version
> http://localhost:5000/api/v1/version

GET all items for the board 'default'
> http://localhost:5000/api/v1/boards/default?user_id=peter

GET all items for the board 'shopping'
> http://localhost:5000/api/v1/boards/shopping?user_id=peter

GET all items for the user 'peter'
> http://localhost:5000/api/v1/items?user_id=peter

GET all items for the user 'peter' on the board 'default'
> http://localhost:5000/api/v1/items?user_id=peter?board_id=default

GET the item with the item_id '{{item_id}}'
> http://localhost:5000/api/v1/items/{{item_id}}


### From the swagger documentation
You can view the generated swagger documentation at http://localhost:5000/swagger/index.html. You can issue custom requrest against all endpints from this page.

> Please note: Set the version parameter to 1

### From the postman scripts
A postman collection has been exported to the /postman folder in the repository. Importing this collection into Postman will give you a suite of pre-created requests to execute against the API. You will need to import the todo-local environment and use this for the 'hostname' enivronment variable to be set correctly.

Post-request scripts for most of the GET requests will set the first item id to a local environment variable. This is used by the GET-item-by-id, PUT-item-by-id and DELETE-item-by-id endpoints.

