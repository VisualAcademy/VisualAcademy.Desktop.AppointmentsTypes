-- AppointmentsTypes 테이블에서 특정 Id 값에 해당하는 레코드를 삭제하는 저장 프로시저
-- @Id: 삭제할 레코드의 Id 값
CREATE PROCEDURE AppointmentsTypes_Delete
@Id INT
AS
BEGIN
DELETE FROM AppointmentsTypes
WHERE Id = @Id;
END
