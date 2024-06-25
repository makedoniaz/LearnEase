INSERT INTO Roles ([Name])
VALUES ('Admin'), ('Author'), ('User');

INSERT INTO Users ([Name], [Email],[Password])
VALUES ('admin', 'admin@mail.ru', 'admin')

INSERT INTO UserRoles ([UserId], [RoleId])
VALUES (1, 1)