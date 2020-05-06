using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ShaipAgency.Model.Request;
using ShaipAgency.Model.Standards;

namespace ShaipAgency.Model.User
{
    [Table("TB_USER_ACTIVITY")]
    public class UserActivityModel
    {
        [Key, ForeignKey("ApplicationUser")]
        public int UserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }


        [Column("RequestCount",TypeName ="int")]
        public int RequestCount { get; set; }


        [Column("CancellationCount", TypeName = "int")]
        public int CancellationCount { get; set; }


        [Column("BacktoBackCancellationCount", TypeName = "int")]
        public int BacktoBackCancellationCount { get; set; }


        [Column("Grade", TypeName = "int")]
        public int Grade { get; set; }


        [Column("Blocked", TypeName = "int")]
        public int Blocked { get; set; }

        /// <summary>
        /// 가입 시간
        /// </summary>
        [Column("SignUpDateTime",TypeName ="datetime")]
        public DateTime SignUpDateTime { get; set; }

        /// <summary>
        /// 마지막 로그인 시간
        /// </summary>
        [Column("LastSignInDateTime", TypeName = "datetime")]
        public DateTime LastSignInDateTime { get; set; }

        /// <summary>
        /// 탈퇴 시간
        /// </summary>
        [Column("WithdrawalDateTime", TypeName = "datetime")]
        public DateTime WithdrawalDateTime { get; set; }

        

    }

    [Table("TB_USER_PASSBOOK")]
    public class UserPassbookModel
    {

        [ForeignKey("ApplicationUser")]
        public int UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }


        /// <summary>
        /// 의뢰번호, Key : DbContext
        /// </summary>
        [ForeignKey("ReqMastersModel")]
        public string RequestNo { get; set; }
        public ReqMastersModel ReqMastersModel { get; set; }


        /// <summary>
        /// 이벤트코드, Key : DbContext
        /// </summary>
        [ForeignKey("StdEventCodeModel")]
        public string EventCode { get; set; }
        public StdEventCodeModel StdEventCodeModel { get; set; }

        
        /// <summary>
        /// 금액
        /// </summary>
        public int Amount { get; set; }


        /// <summary>
        /// 발생일
        /// </summary>
        [Column("CreationDateTime", TypeName = "datetime")]
        public DateTime CreationDateTime { get; set; }

    }
}
