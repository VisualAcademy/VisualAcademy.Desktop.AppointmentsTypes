CREATE PROCEDURE AppointmentsTypes_Delete
  @Id INT
AS
BEGIN
  DELETE FROM AppointmentsTypes
  WHERE Id = @Id;
END