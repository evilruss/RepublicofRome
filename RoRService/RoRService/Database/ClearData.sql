delete from Cards where GameId = 1;
delete from CardVariables where GameId = 1;
delete from Events where GameId = 1;
delete from FactionCards where FactionId in (select Id from Factions where GameId = 1);
delete from Factions where GameId = 1;
delete from OfficeHolders where GameId = 1;
delete from Republics where GameId = 1;
update Games set Status = "Open" where Id = 1;