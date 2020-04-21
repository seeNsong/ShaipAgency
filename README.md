# ShaipAgency
개인프로젝트

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
