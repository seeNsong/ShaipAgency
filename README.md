# ShaipAgency
개인프로젝트


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
