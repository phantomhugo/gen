SELECT CASE Sex WHEN 0 THEN 'Male' WHEN 1 THEN 'Female' END,COUNT(*) FROM Animal GROUP BY Sex

