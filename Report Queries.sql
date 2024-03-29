-- Report 1
SELECT Title AS ProjectTitle,
(SELECT FirstName + ' ' + LastName FROM Person P JOIN Advisor A ON A.Id = P.Id JOIN ProjectAdvisor PA ON A.Id = PA.AdvisorId WHERE AdvisorRole = 11 AND ProjectId = PR.Id) AS [Main Advisor],
(SELECT FirstName + ' ' + LastName FROM Person P JOIN Advisor A ON A.Id = P.Id JOIN ProjectAdvisor PA ON A.Id = PA.AdvisorId WHERE AdvisorRole = 12 AND ProjectId = PR.Id) AS [Co-Advisor],
(SELECT FirstName + ' ' + LastName FROM Person P JOIN Advisor A ON A.Id = P.Id JOIN ProjectAdvisor PA ON A.Id = PA.AdvisorId WHERE AdvisorRole = 14 AND ProjectId = PR.Id) AS [Industry Advisor],
FirstName + ' ' + LastName AS [Student Name] 
FROM Project AS PR
LEFT JOIN GroupProject GP ON GP.ProjectId = PR.Id
LEFT JOIN [Group] G ON G.Id = GP.GroupId
LEFT JOIN GroupStudent GS ON GS.GroupId = G.Id
LEFT JOIN Student S ON S.Id = GS.StudentId
JOIN Person P ON P.Id = S.Id;

-- Report 2
SELECT Title AS [Project], E.Name AS [Evaluation], TotalMarks, ObtainedMarks, FirstName + ' ' + LastName AS [Name] 
FROM Project P JOIN
GroupProject GP ON GP.ProjectId = P.Id JOIN
[Group] G ON G.Id = GP.GroupId JOIN
GroupEvaluation GE ON GE.GroupId = G.Id JOIN
Evaluation E ON E.Id = GE.EvaluationId LEFT JOIN
GroupStudent GS ON GS.GroupId = G.Id JOIN
Student S ON S.Id = GS.StudentId JOIN 
Person PE ON PE.Id = S.Id;

-- Report 3 Students without Groups
SELECT S.Id, RegistrationNo, FirstName + ' ' + LastName AS [Name], (SELECT [Value] FROM Lookup WHERE Id = Gender AND Category = 'GENDER') AS [Gender], Contact, Email, DateOfBirth 
FROM Person P JOIN
Student S ON S.Id = P.Id LEFT JOIN
GroupStudent GS ON GS.StudentId = S.Id
WHERE GS.GroupId IS NULL;

-- Report 4 Projects Evaluation with Weighted Marks and 
SELECT P.Title AS Project, SUM(TotalWeightage) AS [Weightage],
CAST((SUM((GE.ObtainedMarks * E.TotalWeightage) / E.TotalMarks)) AS DECIMAL(5, 2)) AS [Weighted Marks]
FROM Project P JOIN 
GroupProject GP ON P.Id = GP.ProjectId JOIN 
GroupEvaluation GE ON GP.GroupId = GE.GroupId JOIN 
Evaluation E ON GE.EvaluationId = E.Id
GROUP BY P.Title;

-- Report 5 project with their evaluations not conducted fully
SELECT P.Title AS Project, SUM(E.TotalWeightage) AS [Weightage],
(SELECT TOP 1 FirstName + ' ' + LastName FROM Person P1 JOIN Advisor A1 ON A1.Id = P1.Id JOIN ProjectAdvisor PA1 ON A1.Id = PA1.AdvisorId WHERE AdvisorRole = 11 AND ProjectId = P.Id) AS [Main Advisor],
    (SELECT TOP 1 FirstName + ' ' + LastName 
     FROM Person P2 
     JOIN Advisor A2 ON A2.Id = P2.Id 
     JOIN ProjectAdvisor PA2 ON A2.Id = PA2.AdvisorId 
     WHERE AdvisorRole = 12 AND ProjectId = P.Id) AS [Co-Advisor],
    (SELECT TOP 1 FirstName + ' ' + LastName 
     FROM Person P3 
     JOIN Advisor A3 ON A3.Id = P3.Id 
     JOIN ProjectAdvisor PA3 ON A3.Id = PA3.AdvisorId 
     WHERE AdvisorRole = 14 AND ProjectId = P.Id) AS [Industry Advisor]
FROM 
    Project P 
LEFT JOIN 
    GroupProject GP ON P.Id = GP.ProjectId 
LEFT JOIN 
    GroupEvaluation GE ON GP.GroupId = GE.GroupId 
LEFT JOIN 
    Evaluation E ON GE.EvaluationId = E.Id
GROUP BY P.Title, P.Id
HAVING SUM(E.TotalWeightage) IS NULL OR SUM(E.TotalWeightage) <> 100;
