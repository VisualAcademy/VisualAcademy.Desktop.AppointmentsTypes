CREATE PROCEDURE AppointmentsTypes_GetAll
AS
BEGIN
  SELECT [Id], [AppointmentTypeName], [IsActive], [DateCreated] 
  FROM AppointmentsTypes;
END
