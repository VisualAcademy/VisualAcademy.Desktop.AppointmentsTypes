CREATE PROCEDURE AppointmentsTypes_Insert
  @AppointmentTypeName NVARCHAR(50)
AS
BEGIN
  INSERT INTO AppointmentsTypes (AppointmentTypeName)
  VALUES (@AppointmentTypeName);
END