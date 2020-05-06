# ShaipAgency
개인프로젝트
=============
- Register 할 때 ShaipName 짓기 <<

## 처리할일

### 예치금 이용내역 구현 (팝업)
### 예치금 환불 승인처리
### 예치금 충전 승인처리


## 08_요청관리_처리_페이지_구현



## 07_예치금_환불_신청_구현

### 마감처리 페이지 추가 
- 예치금 시스템 마감 처리 추가

### 예치금_시스템마감_db처리
- Table : TB_SYS_CLOSING_USER_DEPOSIT
- Stored Procedure : USP_EXE_SYS_CLOSING

### 실시간계산 함수_구현
- Scalar Function : UFN_GET_USER_DEPOSIT

### 예치금 환불 신청 구현
- 환불요청금액 > 예치금 조건 체크 추가


---------------------------------------------------------------------

## 06_예치금_충전_신청_구현
### [DB Project] 테이블추가
- TB_STD_REQUEST_CODE
  : 의뢰요청을 코드로 저장
- TB_STD_REQUEST_STATUS_CODE
  : 의뢰의 상태를 코드로 저장
- TB_STD_REQUEST_STATUS_ROUTE_INFO
  : 이벤트 발생에 따른 의뢰 상태 변화를 저저장
- TB_REQ_MASTERS
  : 의뢰 요청의 마스터 테이블
- TB_REQ_DETAILS_DEPOSIT
  : 의뢰 요청[예치금충전]의 세부 테이블
- TB_REQ_TIMESTAMPS
  : 모든 의뢰의 이벤트별 타임 스탬프
- TB_USER_ACTIVITY
  : 이용자의 활동내역 저장
- TB_USER_PASSBOOK
  : 이용자의 예치금 통장
  
### [DB Project] Stored Procedure 추가
- USP_SAHIP_TEMPLATE
  : 샤입 프로시져의 기본 뼈대
- USP_REQ_DEPOSIT_MOD_01
  : 예치금 충전/환불 신청에 대한 DB UPDATE가 이루어지는 액션
- USP_EXE_TB_USER_ACTIVITY_UPDATE
  : 이용자 활동내역 UPDATE 하는 프로시져
- USP_EXE_USER_PASSBOOK_UPDATE
  : 이용자 예치금 통장을 UPDATE 하는 프로시져

### [DB Project] Function 추가
- UFN_GET_REQUEST_NO
  : 의뢰번호를 생성시키는 함수
- UFN_GET_NEXT_STATUS
  : 현재 의뢰번호에 발생된 이벤트에 따라 다음 상태를 리턴하는 함수

### Model 객체 생성
- REQ_MODELS => TB_REQ에 매칭
- STD_MODELS => TB_STD에 매칭
- USER_MODELS => TB_USER에 매칭

### Data(Service) 생성
- 폴더 Standards => 기준정보 (TB_STD 테이블의 업데이트 담당)
- 기준정보는 Entityframework 를 통해 접근 및 갱신
- 의뢰의 요청,접수,처리 는 Stored Procedure를 통해 처리

### 예치금 충전 신청 기능 구현
- 로그인 후 예치금 충전/환불 팝업을 통해 신청
- 업데이트 Stored Procedure : USP_REQ_DEPOSIT_MOD_01

### [TestModel수정]
- IEnumerable 리턴

### DxGrid Customization 구현
- TestDxGridColumnTemplate.razor
  + 테이블 컬럼헤더 테마적용
  + RowPreviewTemplate/NewRow/EditRow

---------------------------------------------------------------------

### 06-1_MatBlazor설치
- Nuget Package 설치 : MatBlazor

### Balance Dropdown 추가
- Balance Deposit 모달창 추가

### Remote Db Access 세팅 
- Azure Keyvault 셋팅
- appsetting.json => DbMode : LocalDbConnection, RemoteDb

---------------------------------------------------------------------

## 05_Database_Access_샘플페이지_작성
- SQL Database Project 추가
- EF & None EF Version Database Access 샘플 페이지 추가

## 04_Identity_재적용
- Shaip 로고 삽입
- Index 화면 항목 수정 (주문,결제,구매,배송)
- 새로운 Theme에 Identity 적용
- HDharmony B 적용


## 03_Theme변경_DevExpressDemoTheme
- wwwroot 파일 백업 -> BlazorTemplatesOrigin
- DevExpress 테마 적용 


## 02_회원정보출력_예제추가
- 회원정보 출력 예제 페이지 추가

### DB Communication 
- IApplicationUserRepository 선언
- ApplicationDbContext DBset설정
- ApplicationUserRepository 구현

### Demo Page 추가
- Users Blazor Page추가
- 메뉴추가

- spinner추가 (loding)
- site.css
- import Shaip.css
- Nuget설치 : DevExpress.Blazor


## 01_Identity_추가
- Staffoled 파일 추가
- ApplicationUser 추가 
- IdentityUser참조부분 -> ApplicationUser 참조로 변경
- import 추가
- \Areas\Identity\Pages\_ViewImports.cshtml
- \Pages\_ViewImports.cshtml
- DbContext 상속타입 추가 : IdentityDbContext<ApplicationUser>
### DB Migration
- Add-Database CreateApplicationUser

(추가고려)
- ShaipNameIndex 추가(수동)
- UserId 타입수정(int), 자동증가 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED ([Id] ASC)
- UserId 연관 DB 타입 수정
