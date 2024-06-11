
# My Friends Application

**My Friends** application is a small project with one purpose: to learn new technologies and upgrade my existing knowledge.

***

## Project Goal

The goal is to create a multiplatform application that allows users to store information about their friends, including their info, hobbies, relationships, common events, and more. The application will be local, storing all the information on the user's device without any internet functions. The target platforms are **Windows** (primary) and **Android** (secondary).

## Technologies to Use

The following technologies will be used in this project:
* **MongoDB** - Database for storing all information. I know it is not the best (maybe it is the worst) choice for this application, but I need to learn MongoDB and understand how to use it in .NET. MongoDB is a non-relational database, which might be challenging for relational data, but it will provide a good learning experience.
* **Docker** - The entire development process, including tests, will be done in a Docker environment.
* **.NET** - The application will be written in the C# programming language.
* **MAUI** - The multiplatform interface will be created using the MAUI framework.
* **MVVM** - The application will follow the Model-View-ViewModel pattern, making it easy to modify and extend.
* **Traditional Tools** - GIT for version control, Visual Studio as the IDE, ChatGPT as a teacher, and coffee as fuel.

***

## Specification

This section describes the minimal functionality.

### Entities

The application will store data about friends and their relationships. Here is the list of basic entities with their minimum data:
* **Friend**
  * Name
  * Surname
  * Date of Birth
* **Relation**
  * RelationType
  * From Friend
  * To Friend
* **Likes**
  * Like Type
  * Liker

It is clear that these entities have relationships, and the database for this project is non-relational. Is this a bad choice or a challenge?

### Use Cases

* CRUD (Create, Read, Update, Delete) operations on friends
* CRD (Create, Read, Delete) operations on relation types
* CRD (Create, Read, Delete) operations on like types
* CRUD (Create, Read, Update, Delete) operations on relations
* CRUD (Create, Read, Update, Delete) operations on likes

All changes must be persistent.

### Tests

All application functionality (except UI) must be tested with Unit Tests.

***

## Project Plan

The project should be completed in two weeks, from 6/11/2024 to 6/25/2024. The work will be organized as follows:
1. **Planning**
   Plan all entities and their relationships. Draw wireframes.
   From 6/11/2024 to 6/12/2024.
2. **Implementation Preparation**
   Install everything essential for the project, create the solution in Visual Studio, and test how it works (mini tasks beyond this project).
   From 6/12/2024 to 6/13/2024.
3. **Data Access Layer Implementation**
   Implement the data access layer of the application: Entities, Repositories, Unit of Work.
   From 6/13/2024 to 6/14/2024.
4. **Business Logic Implementation**
   Implement the business logic of the application: Facades, Models.
   From 6/15/2024 to 6/16/2024.
5. **App Implementation**
   Implement UI: View Models, Views.
   From 6/17/2024 to 6/18/2024.

It is not necessary to follow this plan day by day, but it is important to try.

***

## Reward

Self-respect, chips, and ice cream.
