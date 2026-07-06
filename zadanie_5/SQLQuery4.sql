USE BoardGamesClub;
GO

-- 1. Выборка с фильтрацией и сортировкой
SELECT Title, Price, IsAvailable 
FROM Games 
WHERE Price > 3000 AND IsAvailable = 1
ORDER BY Price DESC;

-- 2. Изменение данных
UPDATE Games SET Price = Price * 1.1;

-- 3. Удаление данных
DELETE FROM Members WHERE Email = 'anna@yandex.ru';

-- 4. Выборка с группировкой
-- Посчитаем количество игр у каждого издателя
SELECT PublisherID, COUNT(*) AS GamesCount
FROM Games
GROUP BY PublisherID;

-- 5. Выборка из нескольких связанных таблиц (JOIN)
SELECT G.Title, P.Name AS PublisherName
FROM Games G
INNER JOIN Publishers P ON G.PublisherID = P.PublisherID;

-- LEFT JOIN (Левое соединение): Показать всех участников и их очки в сессиях
SELECT M.FullName, MS.Score, S.Location
FROM Members M
LEFT JOIN MemberSessions MS ON M.MemberID = MS.MemberID
LEFT JOIN PlaySessions S ON MS.SessionID = S.SessionID;