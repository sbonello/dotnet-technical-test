## Instructions
Swagger interface can be accessed by using /swagger sub folder
Persistance was implemented using MongoDB. Please change appsettings.json
Transaction log was implemented using EventStore


Additional Notes

Locking assumes there will be one instance of the api. If multiple instances are required a distributed lock mechansism has to be implemented. Eg using redis. 

The .net client I used for EventStore is deprecated but I couldn't find a more update client version. it works but i hate to base a service on components that are not supported anymore.

