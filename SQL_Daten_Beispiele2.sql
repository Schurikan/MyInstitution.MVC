select * from Clients
--delete from Clients

insert into Clients
(
	Forename
	,Surname
	,DateOfBirth
	,Image
)
Values
 ('Samweis'		,'Gamdschie', SYSDATETIME(), 'https://i.pinimg.com/originals/2e/28/0e/2e280e0092cf128c8f369b9d8ac068bc.jpg')
,('Frodo'		,'Beutlin'	, SYSDATETIME(), 'https://assets.cdn.moviepilot.de/files/93294e94a0bb74aa3d43ab3840ae9745c7efd38f9ce911c8067721bc4034/fill/1200/576/frodo-beutlin.jpg')
,('Bilbo'		,'Beutlin'	, SYSDATETIME(), 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSP4RfgDVyQdswpoL9Vnm6KqBVIXzZp2jfAaQ&usqp=CAU')
,('Peregrin'	,'Tuk'		, SYSDATETIME(), 'https://external-preview.redd.it/lKpnV4_aKJf9k9v3GEs6gfF_vSY0eXUQkhk5c3Z8vxQ.jpg?auto=webp&s=6679d88aac0776c6ca791e164d481e03a1f9362f')
,('Meriadoc'	,'Brandybock', SYSDATETIME(), 'https://www.omdb.org/image/default/1744.jpeg')

select * from Employees
--delete from Employees

insert into Employees
(
	Forename
	,Surname
	,Image
)
Values
 ('Aragon', 'Aragon', 'https://i.pinimg.com/originals/67/8a/46/678a4612d3a3d78efff6fffd337df2a0.jpg')
,('Legolas', 'Legolas', 'https://static.wikia.nocookie.net/lotr/images/9/95/Legolask.jpg/revision/latest/top-crop/width/360/height/450?cb=20140821065656&path-prefix=de')
,('Gandalf', 'Gandalf', 'https://cms.qz.com/wp-content/uploads/2018/08/gandalf-lord-of-the-rings-e1534255368438.jpg?quality=75&strip=all&w=1200&h=630&crop=1')
,('Gimli', 'Gimli', 'https://i.stack.imgur.com/dDzEo.jpg')
,('Galadriel', 'Galadriel', 'https://assets.cdn.moviepilot.de/files/babe08479c4a060c1f20a2d6762f7968438519fb66e5875835c88b5b0a6c/fill/992/477/herr%20der%20ringe%20galadriel.jpg')
,('Saruman', 'Saruman', 'https://static.wikia.nocookie.net/lotr/images/0/0c/Christopher_Lee_as_Saruman.jpg/revision/latest/top-crop/width/360/height/450?cb=20170127113833')

select * from Groups
--delete from Groups

insert into Groups
(
	Name
	,GroupLeaderEmployeeId
	,Image
)
values
('Auenland', (select Id from Clients where Forename = 'Bilbo'), 'https://i.pinimg.com/originals/93/84/04/9384045d76e1c98663291a75b696dfc7.jpg')
,('Bruchtal', (select Id from Employees where Forename = 'Legolas'), 'https://images-na.ssl-images-amazon.com/images/I/61l9b%2BJk19L._AC_SY355_.jpg')
,('Mordor', (select Id from Employees where Forename = 'Saruman'), 'https://i.pinimg.com/originals/4c/4e/0a/4c4e0adbab31969afe40bc33d60f1279.jpg')