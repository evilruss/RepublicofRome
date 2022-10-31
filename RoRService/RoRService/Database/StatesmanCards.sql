CREATE TABLE [StatesmanCards] (
  [Id] INTEGER  NOT NULL
, [CardNo] bigint  NOT NULL
, [Type] text NOT NULL
, [Name] text NOT NULL
, [Era] text NOT NULL
, [FamilyNo] bigint  NOT NULL
, [FamilyLetter] text NOT NULL
, [Military] bigint  NOT NULL
, [Oratory] bigint  NOT NULL
, [Loyalty] bigint  NOT NULL
, [Influence] bigint  NOT NULL
, [Popularity] bigint  NOT NULL
, [DisplayText] text NULL
, [DisplayImage] text NOT NULL
, CONSTRAINT [sqlite_master_PK_StatesmanCards] PRIMARY KEY ([Id])
);

INSERT INTO [StatesmanCards] ([Id],[CardNo],[Type],[Name],[Era],[FamilyNo],[FamilyLetter],[Military],[Oratory],[Loyalty],[Influence],[Popularity],[DisplayText],[DisplayImage]) VALUES (31,31,'Statesman','P. Cornelius Scipio Africanus','Early',1,'A',5,5,7,6,0,'Text','statesman.png');
INSERT INTO [StatesmanCards] ([Id],[CardNo],[Type],[Name],[Era],[FamilyNo],[FamilyLetter],[Military],[Oratory],[Loyalty],[Influence],[Popularity],[DisplayText],[DisplayImage]) VALUES (32,32,'Statesman','Q. Fabius Maximus Verrucosus Cunctator','Early',2,'A',5,2,7,3,0,'Text','statesman.png');
INSERT INTO [StatesmanCards] ([Id],[CardNo],[Type],[Name],[Era],[FamilyNo],[FamilyLetter],[Military],[Oratory],[Loyalty],[Influence],[Popularity],[DisplayText],[DisplayImage]) VALUES (33,33,'Statesman','T. Quinctius Flaminius','Early',18,'A',5,4,7,4,0,'Text','statesman.png');
INSERT INTO [StatesmanCards] ([Id],[CardNo],[Type],[Name],[Era],[FamilyNo],[FamilyLetter],[Military],[Oratory],[Loyalty],[Influence],[Popularity],[DisplayText],[DisplayImage]) VALUES (34,34,'Statesman','L. Aemilius Paullus Macedonicus','Early',19,'A',5,4,8,4,0,'Text','statesman.png');
INSERT INTO [StatesmanCards] ([Id],[CardNo],[Type],[Name],[Era],[FamilyNo],[FamilyLetter],[Military],[Oratory],[Loyalty],[Influence],[Popularity],[DisplayText],[DisplayImage]) VALUES (35,35,'Statesman','M. Porcius Cato The Elder','Early',22,'A',1,6,10,1,0,'Text','statesman.png');
INSERT INTO [StatesmanCards] ([Id],[CardNo],[Type],[Name],[Era],[FamilyNo],[FamilyLetter],[Military],[Oratory],[Loyalty],[Influence],[Popularity],[DisplayText],[DisplayImage]) VALUES (36,36,'Statesman','P. Cornelius Scipio Aemilianus Africanus','Middle',1,'B',4,3,7,5,0,'Text','statesman.png');
INSERT INTO [StatesmanCards] ([Id],[CardNo],[Type],[Name],[Era],[FamilyNo],[FamilyLetter],[Military],[Oratory],[Loyalty],[Influence],[Popularity],[DisplayText],[DisplayImage]) VALUES (37,37,'Statesman','L. Cornelius Sulla ','Middle',1,'C',4,4,5,5,0,'Text','statesman.png');
INSERT INTO [StatesmanCards] ([Id],[CardNo],[Type],[Name],[Era],[FamilyNo],[FamilyLetter],[Military],[Oratory],[Loyalty],[Influence],[Popularity],[DisplayText],[DisplayImage]) VALUES (38,38,'Statesman','M. Fulvius Flaccus','Middle',7,'A',2,5,6,5,0,'Text','statesman.png');
INSERT INTO [StatesmanCards] ([Id],[CardNo],[Type],[Name],[Era],[FamilyNo],[FamilyLetter],[Military],[Oratory],[Loyalty],[Influence],[Popularity],[DisplayText],[DisplayImage]) VALUES (39,39,'Statesman','C. Servilius Glaucia','Middle',21,'A',1,3,3,3,0,'Text','statesman.png');
INSERT INTO [StatesmanCards] ([Id],[CardNo],[Type],[Name],[Era],[FamilyNo],[FamilyLetter],[Military],[Oratory],[Loyalty],[Influence],[Popularity],[DisplayText],[DisplayImage]) VALUES (40,40,'Statesman','P. Popillius Laenas','Middle',23,'A',2,5,6,4,0,'Text','statesman.png');
INSERT INTO [StatesmanCards] ([Id],[CardNo],[Type],[Name],[Era],[FamilyNo],[FamilyLetter],[Military],[Oratory],[Loyalty],[Influence],[Popularity],[DisplayText],[DisplayImage]) VALUES (41,41,'Statesman','T. Sempronius Gracchus','Middle',25,'A',1,4,6,3,2,'Text','statesman.png');
INSERT INTO [StatesmanCards] ([Id],[CardNo],[Type],[Name],[Era],[FamilyNo],[FamilyLetter],[Military],[Oratory],[Loyalty],[Influence],[Popularity],[DisplayText],[DisplayImage]) VALUES (42,42,'Statesman','C. Sempronius Gracchus','Middle',25,'B',1,5,6,4,3,'Text','statesman.png');
INSERT INTO [StatesmanCards] ([Id],[CardNo],[Type],[Name],[Era],[FamilyNo],[FamilyLetter],[Military],[Oratory],[Loyalty],[Influence],[Popularity],[DisplayText],[DisplayImage]) VALUES (43,43,'Statesman','G. Marius','Middle',27,'A',5,3,6,5,0,'Text','statesman.png');
INSERT INTO [StatesmanCards] ([Id],[CardNo],[Type],[Name],[Era],[FamilyNo],[FamilyLetter],[Military],[Oratory],[Loyalty],[Influence],[Popularity],[DisplayText],[DisplayImage]) VALUES (44,44,'Statesman','G. Julisu Caesar','Late',4,'A',6,5,9,5,2,'Text','statesman.png');
INSERT INTO [StatesmanCards] ([Id],[CardNo],[Type],[Name],[Era],[FamilyNo],[FamilyLetter],[Military],[Oratory],[Loyalty],[Influence],[Popularity],[DisplayText],[DisplayImage]) VALUES (45,45,'Statesman','M. Porcius Cato The Younger','Late',22,'A',1,6,11,4,0,'Text','statesman.png');
INSERT INTO [StatesmanCards] ([Id],[CardNo],[Type],[Name],[Era],[FamilyNo],[FamilyLetter],[Military],[Oratory],[Loyalty],[Influence],[Popularity],[DisplayText],[DisplayImage]) VALUES (46,46,'Statesman','M. Tullius Cicero','Late',28,'A',1,6,10,3,1,'Text','statesman.png');
INSERT INTO [StatesmanCards] ([Id],[CardNo],[Type],[Name],[Era],[FamilyNo],[FamilyLetter],[Military],[Oratory],[Loyalty],[Influence],[Popularity],[DisplayText],[DisplayImage]) VALUES (47,47,'Statesman','M. Licinius Crassus','Late',29,'A',2,2,4,5,0,'Text','statesman.png');
INSERT INTO [StatesmanCards] ([Id],[CardNo],[Type],[Name],[Era],[FamilyNo],[FamilyLetter],[Military],[Oratory],[Loyalty],[Influence],[Popularity],[DisplayText],[DisplayImage]) VALUES (48,48,'Statesman','L. Licinius Crassus','Late',29,'B',5,3,10,3,0,'Text','statesman.png');
INSERT INTO [StatesmanCards] ([Id],[CardNo],[Type],[Name],[Era],[FamilyNo],[FamilyLetter],[Military],[Oratory],[Loyalty],[Influence],[Popularity],[DisplayText],[DisplayImage]) VALUES (49,49,'Statesman','G. Pompeius Magnus','Late',30,'A',1,3,9,5,5,'Text','statesman.png');
