USE BoardGamesClub;
GO

-- 1. Вставляем издателя
INSERT INTO Publishers (Name, ContactEmail) VALUES ('Hobby World', 'info@hobbyworld.ru');

-- 2. Вставляем игры (используем формат YYYYMMDD - он самый надежный)
INSERT INTO Games (Title, Price, ReleaseDate, IsAvailable, PublisherID) VALUES 
('Catan', 3500.00, '20230101', 1, (SELECT TOP 1 PublisherID FROM Publishers)),
('Ticket to Ride', 4200.00, '20220515', 1, (SELECT TOP 1 PublisherID FROM Publishers)),
('Nemesis', 12000.00, '20231010', 0, (SELECT TOP 1 PublisherID FROM Publishers));

-- 3. Вставляем участников
INSERT INTO Members (FullName, Email) VALUES 
('Иван Иванов', 'ivan@mail.ru'),
('Анна Сидорова', 'anna@yandex.ru');

-- 4. Вставляем сессию
INSERT INTO PlaySessions (SessionDate, Location) VALUES ('2024-05-20T18:00:00', 'Зал 1');

-- 5. Связываем участников с сессией
INSERT INTO MemberSessions (MemberID, SessionID, Score)
SELECT 
    (SELECT MemberID FROM Members WHERE Email = 'ivan@mail.ru'),
    (SELECT TOP 1 SessionID FROM PlaySessions), 
    100;
