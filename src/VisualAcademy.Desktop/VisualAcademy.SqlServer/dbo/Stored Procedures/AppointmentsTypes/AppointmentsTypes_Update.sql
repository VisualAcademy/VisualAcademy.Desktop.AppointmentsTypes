CREATE PROCEDURE AppointmentsTypes_Update
  @Id INT,
  @AppointmentTypeName NVARCHAR(50)
AS
BEGIN
  UPDATE AppointmentsTypes
  SET AppointmentTypeName = @AppointmentTypeName
  WHERE Id = @Id;
END