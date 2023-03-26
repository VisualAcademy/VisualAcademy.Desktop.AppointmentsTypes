-- 테이블 생성: AppointmentTypes
-- 이 테이블은 예약 유형을 저장하며, 각 유형에 대한 정보를 포함합니다.
CREATE TABLE AppointmentsTypes (
  -- 기본 키: Id
  -- 자동 증가하는 정수 값 (1부터 시작)
  Id INT PRIMARY KEY IDENTITY(1,1),

  -- 예약 유형 이름: AppointmentTypeName
  -- 최대 50자의 NVARCHAR 형식
  AppointmentTypeName NVARCHAR(50) NOT NULL,

  -- 활성화 여부: IsActive
  -- 비트 타입 (0 또는 1, 1: 활성, 0: 비활성)
  -- 기본값: 1 (활성)
  IsActive BIT NOT NULL DEFAULT 1,

  -- 생성 날짜: DateCreated
  -- DATETIME 형식의 날짜와 시간
  -- 기본값: 현재 시스템 날짜와 시간 (GETDATE() 함수 사용)
  DateCreated DATETIME NOT NULL DEFAULT GETDATE()
)
