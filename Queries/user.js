//Collection Setup
db.users.ensureIndex({ userId: 1 }, { unique: true, dropDups: true });
db.users.ensureIndex( { "logins.loginProvider" : 1, "logins.providerKey" : 1} , { unique: true, dropDups: true });


//TODO Create Provider Index

db.setProfilingLevel(2);
db.setProfilingLevel(0);
db.getProfilingStatus();


db.system.profile.find({"ns" : "navigatorCredentials.users"}).limit(1).sort( { ts : -1 } ).pretty()

//userId = Stuart Shay 
db.users.find({ "userId": "116590040434310834456" }).explain();
db.users.find({logins : { "loginProvider" : "Google", "providerKey" : "116590040434310834456" }}).explain();
db.users.find();


//$addToSet -- Adds Value to Array if not exists   
db.users.update({ "userId"   : "116590040434310834456",},
                           { $addToSet: { logins:  {loginProvider: "Glass", "providerKey" : "116590040434310834456" }  } }
);   
    
                           
                           







