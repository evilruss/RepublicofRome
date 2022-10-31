CREATE TABLE [ConcessionCards] (
  [Id] INTEGER  NOT NULL
, [CardNo] bigint  NOT NULL
, [Type] text NOT NULL
, [Name] text NOT NULL
, [DisplayText] text NULL
, [DisplayImage] text NOT NULL
, [Income] bigint  NOT NULL
, CONSTRAINT [sqlite_master_PK_ConcessionCards] PRIMARY KEY ([Id])
);

INSERT INTO [ConcessionCards] ([Id],[CardNo],[Type],[Name],[DisplayText],[DisplayImage],[Income]) VALUES (1,50,'Concession','Armaments','Assign to a Senator to immediately collect 2 Talents per new Legion when purchased by the State.','armaments.png',2);
INSERT INTO [ConcessionCards] ([Id],[CardNo],[Type],[Name],[DisplayText],[DisplayImage],[Income]) VALUES (2,51,'Concession','Egyptian Grain','<p>Assign to a Senator for regular income. M</p> <p>Income x 2 if Drought or Pirates also -2 from Popularity. +1 income multiple and Popularity loss per additional Drought event.</p>Destroyed by Alexandrine War.','grain.png',5);
INSERT INTO [ConcessionCards] ([Id],[CardNo],[Type],[Name],[DisplayText],[DisplayImage],[Income]) VALUES (3,52,'Concession','Harbour Fees','Assign to Senator for regular income.','harbour-fees.png',3);
INSERT INTO [ConcessionCards] ([Id],[CardNo],[Type],[Name],[DisplayText],[DisplayImage],[Income]) VALUES (4,53,'Concession','Land Commissioner','<p>Assign to Senator for income when a Land Bill is in play. </p> Return to Forum if no Land Bill in effect at the end of the Senate Phase.','land-commissioner.png',3);
INSERT INTO [ConcessionCards] ([Id],[CardNo],[Type],[Name],[DisplayText],[DisplayImage],[Income]) VALUES (5,54,'Concession','Mining','Assign to a Senator for regular income.','mining.png',3);
INSERT INTO [ConcessionCards] ([Id],[CardNo],[Type],[Name],[DisplayText],[DisplayImage],[Income]) VALUES (6,55,'Concession','Ship Building','Assign to a Senator to immediately collect 3 Talents per new Fleet when purchased by the State.','ship-building.png',3);
INSERT INTO [ConcessionCards] ([Id],[CardNo],[Type],[Name],[DisplayText],[DisplayImage],[Income]) VALUES (7,56,'Concession','Scilian Grain','<p>Assign to a Senator for regular income. M</p> <p>Income x 2 if Drought or Pirates also -2 from Popularity. +1 income multiple and Popularity loss per additional Drought event.</p>Destroyed by Scilian Slave Revolt.','grain.png',4);
INSERT INTO [ConcessionCards] ([Id],[CardNo],[Type],[Name],[DisplayText],[DisplayImage],[Income]) VALUES (8,57,'Concession','Tax Farmer 1','Assign to a Senator for regular income.','tax-farmer.png',2);
INSERT INTO [ConcessionCards] ([Id],[CardNo],[Type],[Name],[DisplayText],[DisplayImage],[Income]) VALUES (9,58,'Concession','Tax Farmer 2','Assign to a Senator for regular income.','tax-farmer.png',2);
INSERT INTO [ConcessionCards] ([Id],[CardNo],[Type],[Name],[DisplayText],[DisplayImage],[Income]) VALUES (10,59,'Concession','Tax Farmer 3','Assign to a Senator for regular income.','tax-farmer.png',2);
INSERT INTO [ConcessionCards] ([Id],[CardNo],[Type],[Name],[DisplayText],[DisplayImage],[Income]) VALUES (11,60,'Concession','Tax Farmer 4','Assign to a Senator for regular income.','tax-farmer.png',2);
INSERT INTO [ConcessionCards] ([Id],[CardNo],[Type],[Name],[DisplayText],[DisplayImage],[Income]) VALUES (12,61,'Concession','Tax Farmer 5','Assign to a Senator for regular income.','tax-farmer.png',2);
INSERT INTO [ConcessionCards] ([Id],[CardNo],[Type],[Name],[DisplayText],[DisplayImage],[Income]) VALUES (13,62,'Concession','Tax Farmer 6','Assign to a Senator for regular income.','tax-farmer.png',2);
