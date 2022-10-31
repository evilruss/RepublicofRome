CREATE TABLE [SenatorCards] (
  [Id] INTEGER  NOT NULL
, [CardNo] bigint  NOT NULL
, [Type] text NOT NULL
, [Name] text NOT NULL
, [Era] text NOT NULL
, [FamilyNo] bigint  NOT NULL
, [HasStatesman] text NOT NULL
, [Military] bigint  NOT NULL
, [Oratory] bigint  NOT NULL
, [Loyalty] bigint  NOT NULL
, [Influence] bigint  NOT NULL
, [Popularity] bigint  NOT NULL
, [DisplayText] text NULL
, [DisplayImage] text NOT NULL
, CONSTRAINT [sqlite_master_PK_SenatorCards] PRIMARY KEY ([Id])
);

INSERT INTO [SenatorCards] ([Id],[CardNo],[Type],[Name],[Era],[FamilyNo],[HasStatesman],[Military],[Oratory],[Loyalty],[Influence],[Popularity],[DisplayText],[DisplayImage]) VALUES (1,1,'Senator','Cornelius','Early',1,'N',4,3,9,5,0,'Text','senator.png');
INSERT INTO [SenatorCards] ([Id],[CardNo],[Type],[Name],[Era],[FamilyNo],[HasStatesman],[Military],[Oratory],[Loyalty],[Influence],[Popularity],[DisplayText],[DisplayImage]) VALUES (2,2,'Senator','Fabius','Early',2,'N',4,2,9,5,0,'Text','senator.png');
INSERT INTO [SenatorCards] ([Id],[CardNo],[Type],[Name],[Era],[FamilyNo],[HasStatesman],[Military],[Oratory],[Loyalty],[Influence],[Popularity],[DisplayText],[DisplayImage]) VALUES (3,3,'Senator','Valerius','Early',3,'N',4,3,9,4,0,'Text','senator.png');
INSERT INTO [SenatorCards] ([Id],[CardNo],[Type],[Name],[Era],[FamilyNo],[HasStatesman],[Military],[Oratory],[Loyalty],[Influence],[Popularity],[DisplayText],[DisplayImage]) VALUES (4,4,'Senator','Julius','Early',4,'N',4,3,9,4,0,'Text','senator.png');
INSERT INTO [SenatorCards] ([Id],[CardNo],[Type],[Name],[Era],[FamilyNo],[HasStatesman],[Military],[Oratory],[Loyalty],[Influence],[Popularity],[DisplayText],[DisplayImage]) VALUES (5,5,'Senator','Claudius','Early',5,'N',2,3,7,4,0,'Text','senator.png');
INSERT INTO [SenatorCards] ([Id],[CardNo],[Type],[Name],[Era],[FamilyNo],[HasStatesman],[Military],[Oratory],[Loyalty],[Influence],[Popularity],[DisplayText],[DisplayImage]) VALUES (6,6,'Senator','Manilius','Early',6,'N',3,2,7,4,0,'Text','senator.png');
INSERT INTO [SenatorCards] ([Id],[CardNo],[Type],[Name],[Era],[FamilyNo],[HasStatesman],[Military],[Oratory],[Loyalty],[Influence],[Popularity],[DisplayText],[DisplayImage]) VALUES (7,7,'Senator','Fulvius','Early',7,'N',2,2,8,4,0,'Text','senator.png');
INSERT INTO [SenatorCards] ([Id],[CardNo],[Type],[Name],[Era],[FamilyNo],[HasStatesman],[Military],[Oratory],[Loyalty],[Influence],[Popularity],[DisplayText],[DisplayImage]) VALUES (8,8,'Senator','Furius','Early',8,'N',3,3,8,3,0,'Text','senator.png');
INSERT INTO [SenatorCards] ([Id],[CardNo],[Type],[Name],[Era],[FamilyNo],[HasStatesman],[Military],[Oratory],[Loyalty],[Influence],[Popularity],[DisplayText],[DisplayImage]) VALUES (9,9,'Senator','Aurelius','Early',9,'N',2,3,7,3,0,'Text','senator.png');
INSERT INTO [SenatorCards] ([Id],[CardNo],[Type],[Name],[Era],[FamilyNo],[HasStatesman],[Military],[Oratory],[Loyalty],[Influence],[Popularity],[DisplayText],[DisplayImage]) VALUES (10,10,'Senator','Junius','Early',10,'N',1,2,8,3,0,'Text','senator.png');
INSERT INTO [SenatorCards] ([Id],[CardNo],[Type],[Name],[Era],[FamilyNo],[HasStatesman],[Military],[Oratory],[Loyalty],[Influence],[Popularity],[DisplayText],[DisplayImage]) VALUES (11,11,'Senator','Papirius','Early',11,'N',1,2,6,3,0,'Text','senator.png');
INSERT INTO [SenatorCards] ([Id],[CardNo],[Type],[Name],[Era],[FamilyNo],[HasStatesman],[Military],[Oratory],[Loyalty],[Influence],[Popularity],[DisplayText],[DisplayImage]) VALUES (12,12,'Senator','Acilius','Early',12,'N',2,2,7,3,0,'Text','senator.png');
INSERT INTO [SenatorCards] ([Id],[CardNo],[Type],[Name],[Era],[FamilyNo],[HasStatesman],[Military],[Oratory],[Loyalty],[Influence],[Popularity],[DisplayText],[DisplayImage]) VALUES (13,13,'Senator','Flaminius','Early',13,'N',4,2,6,3,0,'Text','senator.png');
INSERT INTO [SenatorCards] ([Id],[CardNo],[Type],[Name],[Era],[FamilyNo],[HasStatesman],[Military],[Oratory],[Loyalty],[Influence],[Popularity],[DisplayText],[DisplayImage]) VALUES (14,14,'Senator','Aelius','Early',14,'N',3,4,7,2,0,'Text','senator.png');
INSERT INTO [SenatorCards] ([Id],[CardNo],[Type],[Name],[Era],[FamilyNo],[HasStatesman],[Military],[Oratory],[Loyalty],[Influence],[Popularity],[DisplayText],[DisplayImage]) VALUES (15,15,'Senator','Sulpicius','Early',15,'N',3,2,8,2,0,'Text','senator.png');
INSERT INTO [SenatorCards] ([Id],[CardNo],[Type],[Name],[Era],[FamilyNo],[HasStatesman],[Military],[Oratory],[Loyalty],[Influence],[Popularity],[DisplayText],[DisplayImage]) VALUES (16,16,'Senator','Calpurnius','Early',16,'N',1,2,9,2,0,'Text','senator.png');
INSERT INTO [SenatorCards] ([Id],[CardNo],[Type],[Name],[Era],[FamilyNo],[HasStatesman],[Military],[Oratory],[Loyalty],[Influence],[Popularity],[DisplayText],[DisplayImage]) VALUES (17,17,'Senator','Plautius','Early',17,'N',2,1,6,2,0,'Text','senator.png');
INSERT INTO [SenatorCards] ([Id],[CardNo],[Type],[Name],[Era],[FamilyNo],[HasStatesman],[Military],[Oratory],[Loyalty],[Influence],[Popularity],[DisplayText],[DisplayImage]) VALUES (18,18,'Senator','Quinctius','Early',18,'N',3,2,6,1,0,'Text','senator.png');
INSERT INTO [SenatorCards] ([Id],[CardNo],[Type],[Name],[Era],[FamilyNo],[HasStatesman],[Military],[Oratory],[Loyalty],[Influence],[Popularity],[DisplayText],[DisplayImage]) VALUES (19,19,'Senator','Aemilius','Early',19,'N',4,2,8,1,0,'Text','senator.png');
INSERT INTO [SenatorCards] ([Id],[CardNo],[Type],[Name],[Era],[FamilyNo],[HasStatesman],[Military],[Oratory],[Loyalty],[Influence],[Popularity],[DisplayText],[DisplayImage]) VALUES (20,20,'Senator','Terentius','Early',20,'N',2,1,6,1,0,'Text','senator.png');
INSERT INTO [SenatorCards] ([Id],[CardNo],[Type],[Name],[Era],[FamilyNo],[HasStatesman],[Military],[Oratory],[Loyalty],[Influence],[Popularity],[DisplayText],[DisplayImage]) VALUES (21,21,'Senator','Servilius','Middle',21,'N',3,4,9,4,0,'Text','senator.png');
INSERT INTO [SenatorCards] ([Id],[CardNo],[Type],[Name],[Era],[FamilyNo],[HasStatesman],[Military],[Oratory],[Loyalty],[Influence],[Popularity],[DisplayText],[DisplayImage]) VALUES (22,22,'Senator','Porcius','Middle',22,'N',2,4,10,1,0,'Text','senator.png');
INSERT INTO [SenatorCards] ([Id],[CardNo],[Type],[Name],[Era],[FamilyNo],[HasStatesman],[Military],[Oratory],[Loyalty],[Influence],[Popularity],[DisplayText],[DisplayImage]) VALUES (23,23,'Senator','Popillius','Middle',23,'N',1,3,7,3,0,'Text','senator.png');
INSERT INTO [SenatorCards] ([Id],[CardNo],[Type],[Name],[Era],[FamilyNo],[HasStatesman],[Military],[Oratory],[Loyalty],[Influence],[Popularity],[DisplayText],[DisplayImage]) VALUES (24,24,'Senator','Cassius','Middle',24,'N',3,3,9,3,0,'Text','senator.png');
INSERT INTO [SenatorCards] ([Id],[CardNo],[Type],[Name],[Era],[FamilyNo],[HasStatesman],[Military],[Oratory],[Loyalty],[Influence],[Popularity],[DisplayText],[DisplayImage]) VALUES (25,25,'Senator','Sempronius','Middle',25,'N',1,3,6,3,0,'Text','senator.png');
INSERT INTO [SenatorCards] ([Id],[CardNo],[Type],[Name],[Era],[FamilyNo],[HasStatesman],[Military],[Oratory],[Loyalty],[Influence],[Popularity],[DisplayText],[DisplayImage]) VALUES (26,26,'Senator','Octavius','Late',26,'N',2,3,9,4,0,'Text','senator.png');
INSERT INTO [SenatorCards] ([Id],[CardNo],[Type],[Name],[Era],[FamilyNo],[HasStatesman],[Military],[Oratory],[Loyalty],[Influence],[Popularity],[DisplayText],[DisplayImage]) VALUES (27,27,'Senator','Marius','Late',27,'N',4,2,9,4,0,'Text','senator.png');
INSERT INTO [SenatorCards] ([Id],[CardNo],[Type],[Name],[Era],[FamilyNo],[HasStatesman],[Military],[Oratory],[Loyalty],[Influence],[Popularity],[DisplayText],[DisplayImage]) VALUES (28,28,'Senator','Tullius','Late',28,'N',2,3,7,3,0,'Text','senator.png');
INSERT INTO [SenatorCards] ([Id],[CardNo],[Type],[Name],[Era],[FamilyNo],[HasStatesman],[Military],[Oratory],[Loyalty],[Influence],[Popularity],[DisplayText],[DisplayImage]) VALUES (29,29,'Senator','Licinius','Late',29,'N',3,2,9,3,0,'Text','senator.png');
INSERT INTO [SenatorCards] ([Id],[CardNo],[Type],[Name],[Era],[FamilyNo],[HasStatesman],[Military],[Oratory],[Loyalty],[Influence],[Popularity],[DisplayText],[DisplayImage]) VALUES (30,30,'Senator','Pompeius','Late',30,'N',2,2,7,2,0,'Text','senator.png');