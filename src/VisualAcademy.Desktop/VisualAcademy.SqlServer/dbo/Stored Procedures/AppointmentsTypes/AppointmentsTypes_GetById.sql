CREATE PROCEDURE AppointmentsTypes_GetById
  @Id INT
AS
BEGIN
  SELECT [Id], [AppointmentTypeName], [IsActive], [DateCreated] 
  FROM AppointmentsTypes 
  WHERE Id = @Id;
END
