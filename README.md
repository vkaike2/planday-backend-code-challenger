# The Planday car factory

![A very fast looking car](planborgini.png)

With renowned brands like the PlanfaRomeo and the Voksday, the Planday car factory is the birthplace for many world-class cars.

Due to some manufacturing problems and an increasing number of orders, we need you to help us with a few tasks here in the factory.

## The scenario

Next month we have a big order of cars with different specs:
|Brand|Doors|Paint|Speakers|Quantity
|---|---|---|---|---|
|PlanfaRomeo|5 doors|Blue base with Orange stripes|1 subwoofer and 2 standard|75|
|Planborgini|3 doors|Pink base with red dots|10 subwoofers and 20 standard|15|
|Volksday|5 doors|Red base with black stripes|4 standard speakers|20|
|PlandayMotorWorks|3 doors|Black base with yellow dots|1 subwoofer 2 speakers|40|
|Plandrover|5 doors|Green base with gold stripes|4 standard speakers|20|

The factory is currently in a broken state and won't even compile. Your job is to make it compile and start producing cars again.

### Tasks
Please finish as many of these tasks as you can, but if you feel like making changes to other areas of the code, please feel free to do so.
1. In the CarFactory folder, you will find the solution file, CarFactory.sln. Open this
2. The start-up project should be CarFactory. Fix the compile errors, and run this project. This should open up a Swagger page on https://localhost:44329/index.html
3. Use the POST /Car endpoint to request the cars mentioned in the above section and make sure the code is working, fixing any errors you come across
4. Bill from quality insurance has been complaining we don’t have any tests to cover these new specs.
Write some unit tests to cover these different car specs where you find it appropriate.
5. Customer support get a lot of requests regarding paintjobs because peoples' phones changes the "type" for paintjob to have an uppercased letter, e.g. "Stripe", and our system doesn't allow that. Extend the system to support mixed casings, and write appropriate unit tests
6. Sales have been complaining we can’t make cars quick enough. They says the quicker we can make these cars the more money we can make! 
Spend the remainder of time optimising the code to make the endpoint faster. 
     
**When you are done coding, please zip the code and share with us**

### Rules
- You can’t edit the subcontractor 
- You can optimise code but don’t change any validation or business rules 
- In Painter.cs, please only edit FindPaintPassword 
- In CarAssembler.cs, please don't edit logic in the constructor or the CalibrateLocks function. You can add to the constructor, but the existing logic should stay the same.

# Solving the problem
 - [x] Project wasn't building.
   - [x] I just imported the System.Linq in the CarController.cs
 - [x] Use the POST /Car endpoint to request the cars mentioned in the above section and make sure the code is working, fixing any errors you come across
   - [x] The Paint.Type should be an enum and the string "strie" should be "stripe" I will only fix the string for now, I want to run the code first and do the refactoring part later
   - [x] Ok now I can send a POST request without error, but let see if I can find any problems with the output
      - [x] I'm sending 5 doors and the response is "Four Door"
         - It was a mistake into the ChassisCabin class, typeId: 0 = 3 doors and not 2, and TypeId: 1 = 5 doors accordingly to my input table
      - [x] Paint job is Empty
         - now It is returning  the paintJob but the return format is not good enought for me, I will go back here later
      - [x] I'm only allowed to put 2 itens into the speeker list, but I need to create a car with 2 subwoofer and 1 standard (3)
      - [x] The weels manufactory is "Plandrover" but I send "PlanfaRomeo"
      - [x] I need to check if the Painter code is already unlocked
 - [x] creating unity tests
      - [x] Painter
      - [x] Wheels
      - [x] Steels
      - [x] Interior
      - [x] Engine
      - [x] Chassis
      - [x] Assembly
      - [x] Factory
 - [x] refactoring the code
      - [x] First move the model from the controller to the domain
      - [x] Creating folders for the interaces
      - [x] Using the specific type for the ChassisPart in the 
      - [x] Refactoring the Welder
      - [x] Change the paint type to enum
      - [x] Add Exception Filter
      - [x] MemoryCache in the EngineProvider
      - [x] Turn it async for performance?!

# **MY Thougts**
1. Usually, I tend to separate the code in fewer class librearies, but for this excercise I notice that this isn't the main propouse, because I already had a code to work with, I just needed to understand and fix the logic.
   this is the structure that I am used to use:
   - CarFactory.api
      - (Presentation) this one has the Api itself.
   - CarFactory.services
      - (Aplication) this one has all the logics from the code 
   - CarFactory.domain
      - (Domain) have every Intefaces and models
   - CarFactory.data
      - (Infraestructure) Used to call other applications like databases using entity framework or other Apis, and can have the entities too.
   - CarFactory.test
      - containing the unit tests

2. About the logic in the CarFactory.cs.
   - My first thought was to remove the foreach, and create a new model containing the number of cars for the same brand, that will make the code perform better for sure. But with the time analizing the code I realize that
   this foreach was made on propouse, specialy to force to save informations in cache, and use the Slow Workers logic.

3. About Entity Framework
   - if I had used this framework will be easier to read the code and to fix future bugs related to this, but I don't think that this will increase the performance of the code and this was item 6 of my tasks.
   - Usualy I use the UnityOfWork archtecture, to manage all the repositories and have more controll about my transactions.



   
