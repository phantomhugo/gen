SELECT * FROM Animal WHERE BirthDate<DATEADD(year,-2,GETDATE()) and Sex=1 ORDER BY Name